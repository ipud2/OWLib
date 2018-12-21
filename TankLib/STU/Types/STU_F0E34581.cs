// Instance generated by TankLibHelper.InstanceBuilder

using TankLib.Math;

// ReSharper disable All
namespace TankLib.STU.Types {
    [STU(0xF0E34581)]
    public class STU_F0E34581 : STUInstance {
        [STUField(0x888BCD8B)]
        public teStructuredDataAssetRef<STUBoneLabel> m_888BCD8B;

        [STUField(0xEDF0511C)]
        public ulong m_EDF0511C;

        [STUField(0xFF592924)]
        public ulong m_FF592924;

        [STUField(0xB48F1D22, "m_name")]
        public teString m_name;

        [STUField(0x7DC1550F, "m_position")]
        public teVec3 m_position;

        [STUField(0xAF9D3A0C, "m_rotation")]
        public teQuat m_rotation;
    }
}
