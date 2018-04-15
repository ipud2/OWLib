// Instance generated by TankLibHelper.InstanceBuilder
using TankLib.STU.Types.Enums;

// ReSharper disable All
namespace TankLib.STU.Types {
    [STUAttribute(0x07A0E32F, "STULoadout")]
    public class STULoadout : STUInstance {
        [STUFieldAttribute(0xB48F1D22, "m_name")]
        public teStructuredDataAssetRef<ulong> m_name;

        [STUFieldAttribute(0xCA7E6EDC, "m_description")]
        public teStructuredDataAssetRef<ulong> m_description;

        [STUFieldAttribute(0xFC33191B, "m_logicalButton")]
        public teStructuredDataAssetRef<ulong> m_logicalButton;

        [STUFieldAttribute(0x9290B942)]
        public teStructuredDataAssetRef<ulong> m_9290B942;

        [STUFieldAttribute(0x3CD6DC1E, "m_texture")]
        public teStructuredDataAssetRef<ulong> m_texture;

        [STUFieldAttribute(0xC8D38D7B, "m_infoMovie")]
        public teStructuredDataAssetRef<ulong> m_infoMovie;

        [STUFieldAttribute(0x7E3ED979)]
        public teStructuredDataAssetRef<ulong>[] m_7E3ED979;

        [STUFieldAttribute(0xB1124918)]
        public teStructuredDataAssetRef<ulong>[] m_B1124918;

        [STUFieldAttribute(0x2C54AEAF, "m_category")]
        public Enum_0243CBF9 m_category;

        [STUFieldAttribute(0x0E679979)]
        public int m_0E679979;
    }
}
