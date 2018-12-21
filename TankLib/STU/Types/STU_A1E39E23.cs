// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All

namespace TankLib.STU.Types {
    [STU(0xA1E39E23)]
    public class STU_A1E39E23 : STUInstance {
        [STUField(0x14C89C27)]
        public teStructuredDataAssetRef<STUIdentifier> m_14C89C27;

        [STUField(0x28B48CBF, ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STU_6AD9A8D2 m_28B48CBF;

        [STUField(0x29973CFA)]
        public byte m_29973CFA;

        [STUField(0x39A4D5AB, ReaderType = typeof(InlineInstanceFieldReader))]
        public STUStatescriptGraphWithOverrides m_39A4D5AB;

        [STUField(0x848EA68E)]
        public teStructuredDataAssetRef<STUIdentifier> m_848EA68E;

        [STUField(0xA392480A)]
        public float m_A392480A;

        [STUField(0xEB0A4811, ReaderType = typeof(EmbeddedInstanceFieldReader))]
        public STU_704FE611[] m_EB0A4811;

        [STUField(0xFEBDAC92)]
        public byte m_FEBDAC92;

        [STUField(0xB48F1D22, "m_name")]
        public teString m_name;
    }
}
