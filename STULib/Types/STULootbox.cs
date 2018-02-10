// File auto generated by STUHashTool

using OWLib;
using static STULib.Types.Generic.Common;

namespace STULib.Types {
    [STU(0x56B6D12E, "STULootbox")]
    public class STULootbox : STUInstance {
        [STUField(0xCBE2DADD)]
        public STUGUID Entity;  // STUEntityDefinition

        [STUField(0xB2F9D222)]
        public STUGUID Entity2;  // STUEntityDefinition

        [STUField(0x3970E137)]
        public STUGUID Effect1;  // STUEffect

        [STUField(0xFFE7768F)]
        public STUGUID Effect2;  // STUEffect

        [STUField(0xFEC3ED62)]
        public STUGUID Effect3;  // STUEffect

        [STUField(0x041CE51F, "m_modelLook")]
        public STUGUID ModelLook;  // STUModelLook

        [STUField(0x9B180535)]
        public STUGUID Look2;  // STUModelLook

        [STUField(0xB48F1D22, "m_name")]
        public STUGUID Name;  // STULib.Types.posthash.STU_B456B24F

        [STUField(0xE02BEE24, "m_celebration")]
        public STUGUID Celebration;  // STUCelebration

        [STUField(0xD75586C0, "m_shopCards")]
        public STULootBoxShopCard[] ShopCards;

        [STUField(0x3DFAC8CA)]
        public STUGUID[] Strings;  // STULib.Types.posthash.STU_B456B24F

        [STUField(0x7AB4E3F8)]
        public Enums.STUEnumEventID Event;

        [STUField(0x45C33D76)]
        public byte m_45C33D76;

        [STUField(0xFA2D81E7)]
        public byte m_FA2D81E7;

        public string EventNameNormal => ItemEvents.GetInstance().GetEventNormal((ulong)Event);
        public string EventName => ItemEvents.GetInstance().GetEvent((ulong)Event);
    }
    
    [STU(0x819B4F6D, "STULootBoxShopCard")]
    public class STULootBoxShopCard : STUInstance {
        [STUField(0x87EACF5F)]
        public STUGUID Texture;  // STUTexture

        [STUField(0x90EB924A)]
        public STUGUID Text;  // STU_B456B24F
    }
}

