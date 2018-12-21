// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All

namespace TankLib.STU.Types {
    [STU(0xCCAD18DB)]
    public class STU_CCAD18DB : STUInstance {
        [STUField(0x413F29AE, "m_key")]
        public uint m_key;

        [STUField(0xC08C4427, "m_name")]
        public teString m_name;

        [STUField(0x47AA8444, "m_runtimeValue", ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STUConfigVar m_runtimeValue;
    }
}
