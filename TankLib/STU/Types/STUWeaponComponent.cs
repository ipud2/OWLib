// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All

namespace TankLib.STU.Types {
    [STU(0x15CB6532, "STUWeaponComponent")]
    public class STUWeaponComponent : STUMirroredEntityComponent {
        [STUField(0xC63E11DD, "m_managerScript")]
        public teStructuredDataAssetRef<STU_6BE90C5C> m_managerScript;

        [STUField(0x3BD16B9E, "m_weapons", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUWeaponDefinition[] m_weapons;
    }
}
