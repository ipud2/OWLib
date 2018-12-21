// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All

namespace TankLib.STU.Types {
    [STU(0xBE7A8749, "STUGenericSettings_PurchaseableUnlocks")]
    public class STUGenericSettings_PurchaseableUnlocks : STUGenericSettings_Base {
        [STUField(0xCFE57CEB, "m_availableContentUnlocks", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUAvailableContentUnlock[] m_availableContentUnlocks;

        [STUField(0x4807C17A, "m_entitlementUnlocks", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUEntitlementUnlock[] m_entitlementUnlocks;
    }
}
