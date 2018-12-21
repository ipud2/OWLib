﻿using System;
using System.IO;
using DataTool.FindLogic;
using DataTool.Flag;
using TankLib;
using TankLib.STU.Types;
using static DataTool.Helper.IO;
using static DataTool.Program;
using static DataTool.Helper.STUHelper;

namespace DataTool.ToolLogic.Extract {
    [Tool("extract-abilities", Description = "Extract abilities", CustomFlags = typeof(ExtractFlags))]
    public class ExtractAbilities : ITool {
        public void Parse(ICLIFlags toolFlags) { SaveAbilities(toolFlags); }

        public static void SaveAbilities(ICLIFlags toolFlags) {
            string basePath;
            if (toolFlags is ExtractFlags flags)
                basePath = flags.OutputPath;
            else
                throw new Exception("no output path");

            const string folderName = "Abilities";

            foreach (var key in TrackedFiles[0x9E]) {
                var loadout = GetInstance<STULoadout>(key);
                if (loadout == null) continue;

                var name = GetValidFilename(GetString(loadout.m_name)
                                            ?.TrimEnd()
                                            .Replace(".", "_")) ??
                           $"Unknown{teResourceGUID.Index(key):X}";

                var info = new Combo.ComboInfo();
                Combo.Find(info, loadout.m_texture);
                SaveLogic.Combo.SaveLooseTextures(flags, Path.Combine(basePath, folderName, name), info);

                using (var videoStream = OpenFile(loadout.m_infoMovie)) {
                    if (videoStream != null) {
                        videoStream.Position = 128; // wrapped in "MOVI" for some reason
                        WriteFile(videoStream, Path.Combine(basePath, folderName, name, $"{teResourceGUID.LongKey(loadout.m_infoMovie):X12}.bk2"));
                    }
                }
            }
        }
    }
}
