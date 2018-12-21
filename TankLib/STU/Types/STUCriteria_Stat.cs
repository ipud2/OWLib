// Instance generated by TankLibHelper.InstanceBuilder

using TankLib.STU.Types.Enums;

// ReSharper disable All
namespace TankLib.STU.Types {
    [STU(0x857C15FC, "STUCriteria_Stat")]
    public class STUCriteria_Stat : STUCriteria {
        [STUField(0x5D32D524)]
        public Enum_2D0A59BA m_5D32D524;

        [STUField(0x5ED79353)]
        public Enum_10064D07 m_5ED79353;

        [STUField(0xAB103723)]
        public byte m_AB103723;

        [STUField(0xAF872E86, "m_amount")]
        public double m_amount;

        [STUField(0x411ABFEA, "m_context")]
        public ulong m_context;

        [STUField(0x7FE9F87A, "m_heroGUID")]
        public ulong m_heroGUID;

        [STUField(0xEA58FA50, "m_mapGUID")]
        public ulong m_mapGUID;

        [STUField(0xBC4326FF, "m_stat")]
        public teStructuredDataAssetRef<STUStat> m_stat;
    }
}
