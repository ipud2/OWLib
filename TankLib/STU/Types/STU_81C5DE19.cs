// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All

namespace TankLib.STU.Types {
    [STU(0x81C5DE19)]
    public class STU_81C5DE19 : STU_51C18ED3 {
        [STUField(0xB4FF0E2F, "m_animAlias")]
        public teStructuredDataAssetRef<STUAnimAlias> m_animAlias;

        [STUField(0xF94DAB61, ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STU_9D30DD99 m_F94DAB61;

        [STUField(0x560940DC, "m_onFinished", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUAnimBlendTree_OnFinished m_onFinished;
    }
}
