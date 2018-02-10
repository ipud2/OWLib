// File auto generated by STUHashTool
using static STULib.Types.Generic.Common;

namespace STULib.Types {
    [STU(0x882807BA)]
    public class STU_882807BA : STUInstance {
        [STUField(0xF0A9405A)]
        public STUGUID m_F0A9405A;  // STU_8880FCB0

        [STUField(0xC16A0BCD)]
        public STUGUID m_C16A0BCD;  // STU_42270D59
    }

    [STU(0x42270D59)]
    public class STUSkinOverride : STUDataOverride {
        [STUField(0xA5C9CA02)]
        public string UnknownString;

        [STUField(0xECCC4A5D)]
        public STUGUID SkinImage;

        [STUField(0x88F5CF3E)]
        public STUGUID[] Weapons;  // STU_F2646A89

        [STUField(0x71818BC5)]
        public STUGUID[] m_71818BC5;  // STUEffect

        [STUField(0xB8DC6D46)]
        public STUGUID m_B8DC6D46;  // STU_8880FCB0

        [STUField(0x70D5F086)]
        public STU_882807BA[] m_70D5F086;

        [STUField(0x02F889AE)]
        public STUGUID TeamColor;  // STU_42270D59/STUTeamColor. is this used?

        [STUField(0xC3E03C7F)]
        public STUColorRGBA UnknownColor;

        [STUField(0xE9DA85BB)]
        public byte m_E9DA85BB;
    }
}

