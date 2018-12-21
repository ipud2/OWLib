// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All

namespace TankLib.STU.Types {
    [STU(0xFC47A2ED, "STUAnimBlendTreeSet")]
    public class STUAnimBlendTreeSet : STUInstance {
        [STUField(0x226CC159, ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STU_A323F0F5[] m_226CC159;

        [STUField(0x253EE7C8)]
        public ulong[] m_253EE7C8;

        [STUField(0x85453F7B)]
        public teStructuredDataAssetRef<STUAnimCategory>[] m_85453F7B;

        [STUField(0x85CC326B, ReaderType = typeof(InlineInstanceFieldReader))]
        public STU_DF9B7DE2 m_85CC326B;

        [STUField(0x8610C654)]
        public teStructuredDataAssetRef<ulong> m_8610C654;

        [STUField(0x999F01F8, ReaderType = typeof(InlineInstanceFieldReader))]
        public STU_C898217C[] m_999F01F8;

        [STUField(0x2B2C5C7F, "m_animParamUpdater", ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STU_72C48DD7 m_animParamUpdater;

        [STUField(0x6AFCD1A5, "m_blendTreeItems", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUAnimBlendTreeSet_BlendTreeItem[] m_blendTreeItems;

        [STUField(0x6DDF40DD, "m_bonePoseOverrideItems", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUAnimBlendTreeSet_BonePoseOverrideItem[] m_bonePoseOverrideItems;

        [STUField(0x2F9541A4, "m_crc")]
        public int m_crc;

        [STUField(0xD1467FCA, ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STUAnimGameData_Base m_D1467FCA;

        [STUField(0xE1FA44F9, "m_externalRefs")]
        public ulong[] m_externalRefs;

        [STUField(0xBA53D5ED, "m_guid")]
        public ulong m_guid;

        [STUField(0x93DA6E7C, "m_hardcodedAnimCategoryRefs", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUAnimBlendTreeSet_HardcodedAnimCategoryRefs m_hardcodedAnimCategoryRefs;

        [STUField(0xF9CA7995, "m_paramIds")]
        public uint[] m_paramIds;

        [STUField(0x384DE14F, "m_retargetParams", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUAnimBlendTreeSet_RetargetParams m_retargetParams;

        [STUField(0x84935843, "m_rootAnimAliasGUID")]
        public ulong m_rootAnimAliasGUID;
    }
}
