﻿// File auto generated by STUHashTool

using STULib.Types.Generic;

namespace STULib.Types.Dump {
    [STU(0x176E7ACB)]
    public class STU_176E7ACB : Common.STUInstance {
        [STUField(0x0F6F8CD4)]
        public Common.STUGUID Image1;

        [STUField(0xA8D13B7A)]
        public Common.STUGUID Image2;

        [STUField(0x4D238303)]
        public Common.STUColorRGB Color1; // Alternate version of base color, sometimes same?

        [STUField(0x6E61520F)]
        public Common.STUColorRGB Color2; // Always same as color 1 ??

        [STUField(0x236B064C)]
        public Common.STUColorRGB Color3; // Always white or null ??

        [STUField(0x08D07397)]
        public Common.STUColorRGB Color4; // Alternate version of base color (much more faded)
    }
}
