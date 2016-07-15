﻿using System.IO;
using System.Runtime.InteropServices;

namespace OWLib.Types.STUD {
  public class Pose : ISTUDInstance {
    public ulong Key => 0x99A19357DAF2CF8C;
    public uint Id => 0x34C4FFE6;
    public string Name => "Pose";

    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public unsafe struct PoseHeader {
      public STUDInstanceInfo instance;
      public OWRecord animation1;
      public fixed float unk1[18];
      public OWRecord animation2;
      public fixed float unk2[18];
      public OWRecord animation3;
      public fixed float unk3[18];
      public fixed float unk4[4];
    }

    private PoseHeader header;
    public PoseHeader Header => header;

    public void Read(Stream input) {
      using(BinaryReader reader = new BinaryReader(input, System.Text.Encoding.Default, true)) {
        header = reader.Read<PoseHeader>();
      }
    }
  }
}
