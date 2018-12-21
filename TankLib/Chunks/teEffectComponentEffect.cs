﻿using System.IO;
using System.Runtime.InteropServices;

namespace TankLib.Chunks {
    public class teEffectComponentEffect : IChunk {
        public Structure Header;
        public string    ID => "ECEF";

        public void Parse(Stream stream) {
            using (var reader = new BinaryReader(stream)) {
                Header = reader.Read<Structure>();
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        public struct Structure {
            public teResourceGUID Effect;
        }
    }
}
