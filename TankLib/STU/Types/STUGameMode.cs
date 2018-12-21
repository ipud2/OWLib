// Instance generated by TankLibHelper.InstanceBuilder

using TankLib.STU.Types.Enums;

// ReSharper disable All
namespace TankLib.STU.Types {
    [STU(0x494C33C4, "STUGameMode")]
    public class STUGameMode : STUInstance {
        [STUField(0x040601B2)]
        public byte m_040601B2;

        [STUField(0x0FC17230)]
        public byte m_0FC17230;

        [STUField(0x372E20EB)]
        public byte m_372E20EB;

        [STUField(0x37D4F9CD)]
        public int m_37D4F9CD;

        [STUField(0x3CE93B76, ReaderType = typeof(InlineInstanceFieldReader))]
        public STUGameModeVarValuePair[] m_3CE93B76;

        [STUField(0x6EB38130)]
        public teStructuredDataAssetRef<ulong> m_6EB38130;

        [STUField(0x70064613)]
        public byte m_70064613;

        [STUField(0x7F5B54B2)]
        public teStructuredDataAssetRef<ulong> m_7F5B54B2;

        [STUField(0x96C5A6F6)]
        public byte m_96C5A6F6;

        [STUField(0xA43573F4)]
        public teStructuredDataAssetRef<STUIdentifier>[] m_A43573F4;

        [STUField(0xAD4BF17F, ReaderType = typeof(InlineInstanceFieldReader))]
        public STUGameModeVarValuePair[] m_AD4BF17F;

        [STUField(0xCBAE46D4)]
        public float m_CBAE46D4;

        [STUField(0xCF63B633)]
        public teStructuredDataAssetRef<STU_6BE90C5C> m_CF63B633;

        [STUField(0x5DB91CE2, "m_displayName")]
        public teStructuredDataAssetRef<ulong> m_displayName;

        [STUField(0xF3E24B6F)]
        public byte m_F3E24B6F;

        [STUField(0xF40CE5D4)]
        public byte m_F40CE5D4;

        [STUField(0xF88BA3B9)]
        public teStructuredDataAssetRef<STU_6BE90C5C> m_F88BA3B9;

        [STUField(0x8A5415B9, "m_gameModeType")]
        public Enum_1964FED7 m_gameModeType;

        [STUField(0xE04197AF, "m_gameRulesetSchemas")]
        public teStructuredDataAssetRef<STUGameRulesetSchema>[] m_gameRulesetSchemas;

        [STUField(0xDA642982, "m_loadoutOverrides", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUGameModeLoadoutOverride[] m_loadoutOverrides;

        [STUField(0xA8957901, "m_requiredCelebration")]
        public teStructuredDataAssetRef<STUCelebration> m_requiredCelebration;

        [STUField(0xD440A0F7, "m_teams", ReaderType = typeof(InlineInstanceFieldReader))]
        public STUGameModeTeam[] m_teams;
    }
}
