// Instance generated by TankLibHelper.InstanceBuilder

// ReSharper disable All

namespace TankLib.STU.Types {
    [STU(0x264F6EBC)]
    public class STU_264F6EBC : STU_C7C085F6 {
        [STUField(0x172CAE5E)]
        public teStructuredDataAssetRef<STUAnimation> m_172CAE5E;

        [STUField(0x2FBD6CBA)]
        public byte m_2FBD6CBA;

        [STUField(0x37EE887D)]
        public byte m_37EE887D;

        [STUField(0x83CE3EB5)]
        public float m_83CE3EB5;

        [STUField(0x95BD9624)]
        public teStructuredDataAssetRef<STUDataFlow> m_95BD9624;

        [STUField(0xE8502460)]
        public float m_E8502460;

        [STUField(0xF0E4656D, "m_globalLayerMultipliers", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUAnimGlobalLayerMultipliers m_globalLayerMultipliers;

        [STUField(0xEC42051A, "m_userWeights", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUAnimUserWeights m_userWeights;
    }
}
