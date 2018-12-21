﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TankLib;
using TankLib.ExportFormats;
using TankLib.STU;
using TankLib.STU.Types;
using TACTLib.Client;
using TACTLib.Client.HandlerArgs;
using TACTLib.Container;
using TACTLib.Core;
using TACTLib.Core.Product.Tank;

namespace CASCEncDump {
    internal class Program {
        public static uint BuildVersion;

        public static ClientHandler       Client;
        public static ProductHandler_Tank TankHandler;

        public static string RawIdxDir     => $"dump\\{BuildVersion}\\idx\\raw";
        public static string RawEncDir     => $"dump\\{BuildVersion}\\enc\\raw";
        public static string ConvertIdxDir => $"dump\\{BuildVersion}\\idx\\convert";
        public static string ConvertEncDir => $"dump\\{BuildVersion}\\enc\\convert";
        public static string NonBLTEDir    => $"dump\\{BuildVersion}\\nonblte";
        public static string KeyFilesDir   => $"dump\\{BuildVersion}\\keyfiles";
        public static string AllCMFDir     => $"dump\\{BuildVersion}\\allcmf";
        public static string GUIDDir       => $"dump\\{BuildVersion}\\guids";

        public static void Main(string[] args) {
            var          overwatchDir = args[0];
            var          mode         = args[1];
            const string language     = "enUS";

            // Usage:
            // {overwatch dir} dump  --  Dump hashes
            // {overwatch dir} compare-enc {other ver num}  --  Extract added files from encoding (requires dump from other version)
            // {overwatch dir} compare-idx {other ver num}  --  Extract added files from indices (requires dump from other version)
            // {overwatch dir} allcmf  --  Extract all files from the cmf

            // casc setup

            var createArgs = new ClientCreateArgs { SpeechLanguage = language, TextLanguage = language };
            if (mode != "allcmf" && mode != "dump-guids" && mode != "compare-guids" && mode != "dump-cmf")
                createArgs.HandlerArgs = new ClientCreateArgs_Tank { LoadManifest = false };
            Client      = new ClientHandler(overwatchDir, createArgs);
            TankHandler = (ProductHandler_Tank) Client.ProductHandler;

            BuildVersion = uint.Parse(Client.InstallationInfo.Values["Version"]
                                            .Split('.')
                                            .Last());

            // c:\\ow\\game\\Overwatch dump
            // "D:\Games\Overwatch Test" compare 44022

            if (mode == "dump")
                Dump(args);
            else if (mode == "compare-enc")
                CompareEnc(args);
            else if (mode == "compare-idx")
                CompareIdx(args);
            else if (mode == "allcmf")
                AllCMF(args);
            else if (mode == "dump-guids")
                DumpGUIDs(args);
            else if (mode == "compare-guids")
                CompareGUIDs(args);
            else if (mode == "dump-cmf")
                DumpCMF(args);
            else
                throw new Exception($"unknown mode: {mode}");
        }

        public static void DumpCMF(string[] args) {
            using (var writer = new StreamWriter($"{BuildVersion}.cmfhashes")) {
                foreach (var contentManifestFile in new[] { TankHandler.MainContentManifest, TankHandler.SpeechContentManifest })
                foreach (var hashData in contentManifestFile.HashList)
                    writer.WriteLine(hashData.ContentKey.ToHexString());
            }
        }

        public static void DumpGUIDs(string[] args) {
            using (var writer = new StreamWriter($"{BuildVersion}.guids")) {
                foreach (var file in TankHandler.Assets) writer.WriteLine(file.Key.ToString("X"));
            }
        }

        public static void CompareGUIDs(string[] args) {
            var otherVerNum = args[2];

            Directory.CreateDirectory(GUIDDir); // file name is the version it is compared to

            ulong[] last;
            using (var reader = new StreamReader($"{otherVerNum}.guids")) {
                last = reader.ReadToEnd()
                             .Split('\n')
                             .Where(x => !string.IsNullOrWhiteSpace(x))
                             .Select(x => ulong.Parse(x, NumberStyles.HexNumber))
                             .ToArray();
            }

            var added = TankHandler.Assets.Keys.Except(last)
                                   .ToList();
            var removed = last.Except(TankHandler.Assets.Keys)
                              .ToList();

            using (var writer = new StreamWriter(Path.Combine(GUIDDir, $"{otherVerNum}.added"))) {
                foreach (var addedFile in added) writer.WriteLine(teResourceGUID.AsString(addedFile));
            }

            using (var writer = new StreamWriter(Path.Combine(GUIDDir, $"{otherVerNum}.removed"))) {
                foreach (var removedFile in removed) writer.WriteLine(teResourceGUID.AsString(removedFile));
            }
        }

        public static void AllCMF(string[] args) {
            var types = args.Skip(2)
                            .Select(x => ushort.Parse(x, NumberStyles.HexNumber))
                            .ToArray();

            Directory.CreateDirectory(AllCMFDir);
            foreach (var asset in TankHandler.Assets) {
                var type = teResourceGUID.Type(asset.Key);
                if (!types.Contains(type)) continue;
                try {
                    using (var stream = TankHandler.OpenFile(asset.Key)) {
                        if (stream == null) continue;
                        var typeDir = Path.Combine(AllCMFDir, type.ToString("X3"));
                        Directory.CreateDirectory(typeDir);
                        using (Stream file = File.OpenWrite(Path.Combine(typeDir, teResourceGUID.AsString(asset.Key)))) {
                            stream.CopyTo(file);
                        }
                    }
                } catch (Exception e) {
                    Console.Out.WriteLine(e);
                }
            }
        }

        public static void Dump(string[] args) {
            using (var writer = new StreamWriter($"{BuildVersion}.enchashes")) {
                foreach (var entry in Client.EncodingHandler.Entries) {
                    var md5 = entry.Key.ToHexString();

                    writer.WriteLine(md5);
                }
            }

            using (var writer = new StreamWriter($"{BuildVersion}.idxhashes")) {
                foreach (var entry in Client.ContainerHandler.IndexEntries) {
                    var md5 = entry.Key.ToHexString();

                    writer.WriteLine(md5);
                }
            }
        }

        public static void CompareIdx(string[] args) {
            var otherVerNum = args[2];

            var missingKeys = new HashSet<ulong>();

            Directory.CreateDirectory(RawIdxDir);
            Directory.CreateDirectory(ConvertIdxDir);

            string[] otherHashes;
            using (var reader = new StreamReader($"{otherVerNum}.idxhashes")) {
                otherHashes = reader.ReadToEnd()
                                    .Split('\n')
                                    .Select(x => x.TrimEnd('\r'))
                                    .ToArray();
            }

            foreach (var indexEntry in Client.ContainerHandler.IndexEntries) {
                var md5 = indexEntry.Key.ToHexString();

                if (!otherHashes.Contains(md5))
                    try {
                        var stream = Client.OpenEKey(indexEntry.Key);
                        TryConvertFile(stream, ConvertIdxDir, md5);

                        stream.Dispose();
                    } catch (Exception e) {
                        if (e is BLTEKeyException exception)
                            if (missingKeys.Add(exception.MissingKey))
                                Console.Out.WriteLine($"Missing key: {exception.MissingKey:X16}");
                        //else {
                        //    Console.Out.WriteLine(e);
                        //}
                    }
            }

            Console.Write("done");
            Console.ReadLine();
        }

        public static void CompareEnc(string[] args) {
            var otherVerNum = args[2];

            var missingKeys = new HashSet<ulong>();

            Directory.CreateDirectory(RawEncDir);
            Directory.CreateDirectory(ConvertEncDir);

            string[] otherHashes;
            using (var reader = new StreamReader($"{otherVerNum}.enchashes")) {
                otherHashes = reader.ReadToEnd()
                                    .Split('\n')
                                    .Select(x => x.TrimEnd('\r'))
                                    .Where(x => !string.IsNullOrWhiteSpace(x))
                                    .ToArray();
            }

            var hashSet = new HashSet<CKey>(CASCKeyComparer.Instance);
            foreach (var hash in otherHashes.Select(CKey.FromString)) hashSet.Add(hash);

            foreach (var entry in Client.EncodingHandler.Entries) {
                var md5 = entry.Key.ToHexString();

                if (hashSet.Contains(entry.Key)) continue;
                try {
                    var stream = Client.OpenCKey(entry.Key);
                    TryConvertFile(stream, ConvertEncDir, md5);
                } catch (Exception e) {
                    if (e is BLTEKeyException exception) {
                        if (missingKeys.Add(exception.MissingKey)) Console.Out.WriteLine($"Missing key: {exception.MissingKey:X16}");
                    } else {
                        Console.Out.WriteLine(e);
                    }
                }
            }
        }

        public static void TryConvertFile(Stream stream, string convertDir, string md5) {
            using (var reader = new BinaryReader(stream, Encoding.UTF8, true)) {
                var magic = reader.ReadUInt32();

                stream.Position = 0;
                if (magic == teChunkedData.Magic) {
                    var chunkedData = new teChunkedData(reader);
                    if (chunkedData.Header.StringIdentifier == "MODL") {
                        var model = new OverwatchModel(chunkedData, 0);
                        using (Stream file = File.OpenWrite(Path.Combine(convertDir, md5) + ".owmdl")) {
                            file.SetLength(0);
                            model.Write(file);
                        }
                    }
                } else if (magic == 0x4D4F5649) { // MOVI
                    stream.Position = 128;
                    using (Stream file = File.OpenWrite(Path.Combine(convertDir, md5) + ".bk2")) {
                        file.SetLength(0);
                        stream.CopyTo(file);
                    }
                } else {
                    // ok might be a heckin bundle
                    /*int i = 0;
                    while (reader.BaseStream.Position < reader.BaseStream.Length) {
                        try {
                            magic = reader.ReadUInt32();
                            if (magic != teChunkedData.Magic) {
                                reader.BaseStream.Position -= 3;
                                continue;
                            }
                            reader.BaseStream.Position -= 4;
                            teChunkedData chunkedData = new teChunkedData(reader);
                            if (chunkedData.Header.StringIdentifier == "MODL") {
                                OverwatchModel model = new OverwatchModel(chunkedData);
                                using (Stream file = File.OpenWrite(Path.Combine(convertDir, md5) + $"-{i}.owmdl")) {
                                    file.SetLength(0);
                                    model.Write(file);
                                }
                            }
    
                            i++;
                        } catch (Exception) {
                            // fine
                        }
                    }*/

                    try {
                        //teStructuredData structuredData =new teStructuredData(stream, true);

                        var texture = new teTexture(reader);
                        if (!texture.PayloadRequired && texture.Header.DataSize <= stream.Length && (texture.Header.Flags == teTexture.Flags.CUBEMAP || texture.Header.Flags == teTexture.Flags.DIFFUSE || texture.Header.Flags == teTexture.Flags.MULTISURFACE || texture.Header.Flags == teTexture.Flags.UNKNOWN1 || texture.Header.Flags == teTexture.Flags.UNKNOWN2 || texture.Header.Flags == teTexture.Flags.UNKNOWN4 || texture.Header.Flags == teTexture.Flags.UNKNOWN5 || texture.Header.Flags == teTexture.Flags.WORLD) && texture.Header.Height < 10000 && texture.Header.Width < 10000 && texture.Header.DataSize > 68)
                            using (Stream file = File.OpenWrite(Path.Combine(convertDir, md5) + ".dds")) {
                                file.SetLength(0);
                                texture.SaveToDDS(file);
                            }
                    } catch (Exception) {
                        // fine
                    }

                    try {
                        stream.Position = 0;
                        var structuredData = new teStructuredData(stream, true);

                        if (structuredData.GetInstance<STUResourceKey>() != null) {
                            var key = structuredData.GetInstance<STUResourceKey>();

                            Console.Out.WriteLine("found key");
                            var longKey = ulong.Parse(key.m_keyID, NumberStyles.HexNumber);
                            var longRevKey = BitConverter.ToUInt64(BitConverter.GetBytes(longKey)
                                                                               .Reverse()
                                                                               .ToArray(),
                                                                   0);
                            var keyValueString = BitConverter.ToString(key.m_key)
                                                             .Replace("-", string.Empty);
                            var keyNameProper = longRevKey.ToString("X16");
                            Console.Out.WriteLine("Added Encryption Key {0}, Value: {1}", keyNameProper, keyValueString);
                        }

                        // if (structuredData.GetInstance<STUHero>() != null) {
                        //     
                        // }
                    } catch (Exception) {
                        // fine
                    }
                }
            }
        }
    }
}
