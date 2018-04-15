// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All
namespace TankLib.STU.Types {
    [STUAttribute(0x5207484B, "STUModelLook")]
    public class STUModelLook : STUInstance {
        [STUFieldAttribute(0xBAFDAFBA, "m_materials", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUModelMaterial[] m_materials;

        [STUFieldAttribute(0x33DA887B)]
        public teStructuredDataAssetRef<ulong>[] m_33DA887B;

        [STUFieldAttribute(0x05692DC5, ReaderType = typeof(InlineInstanceFieldReader))]
        public STUAnimationPermutation[] m_05692DC5;

        [STUFieldAttribute(0x7B5D8241)]
        public teStructuredDataAssetRef<STUMaterialEffect> m_7B5D8241;

        [STUFieldAttribute(0xC03306D7)]
        public teStructuredDataAssetRef<ulong>[] m_C03306D7;

        [STUFieldAttribute(0x312C5F1A, "m_materialEffects", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUMaterialEffect[] m_materialEffects;
    }
}
