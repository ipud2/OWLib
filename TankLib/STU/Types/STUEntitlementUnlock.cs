// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All

namespace TankLib.STU.Types {
    [STU(0xE39FB8A0, "STUEntitlementUnlock")]
    public class STUEntitlementUnlock : STUInstance {
        [STUField(0x45216F79)]
        public teString[] m_45216F79;

        [STUField(0xDB803F2F, "m_unlocks")]
        public teStructuredDataAssetRef<STUUnlock>[] m_unlocks;
    }
}
