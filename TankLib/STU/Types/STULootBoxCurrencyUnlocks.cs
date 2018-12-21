// Instance generated by TankLibHelper.InstanceBuilder

using TankLib.STU.Types.Enums;

// ReSharper disable All
namespace TankLib.STU.Types {
    [STU(0x16F7C0EA, "STULootBoxCurrencyUnlocks")]
    public class STULootBoxCurrencyUnlocks : STUInstance {
        [STUField(0x7AB4E3F8, "m_lootboxType")]
        public Enum_BABC4175 m_lootboxType;

        [STUField(0xDB803F2F, "m_unlocks", ReaderType = typeof(InlineInstanceFieldReader))]
        public STULootBoxCurrencyRarityUnlock[] m_unlocks;
    }
}
