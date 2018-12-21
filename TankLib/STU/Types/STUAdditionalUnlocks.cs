// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All

namespace TankLib.STU.Types {
    [STU(0x5C499B8E, "STUAdditionalUnlocks")]
    public class STUAdditionalUnlocks : STUInstance {
        [STUField(0x2C01908B, "m_level")]
        public uint m_level;

        [STUField(0xDB803F2F, "m_unlocks")]
        public teStructuredDataAssetRef<STUUnlock>[] m_unlocks;
    }
}
