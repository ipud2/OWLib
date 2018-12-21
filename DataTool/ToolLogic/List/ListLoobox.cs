﻿using System.Collections.Generic;
using DataTool.DataModels;
using DataTool.Flag;
using DataTool.JSON;
using TankLib.STU.Types;
using static DataTool.Program;
using static DataTool.Helper.Logger;
using static DataTool.Helper.STUHelper;

namespace DataTool.ToolLogic.List {
    [Tool("list-lootbox", Description = "List lootboxes", CustomFlags = typeof(ListFlags))]
    public class ListLoobox : JSONTool, ITool {
        public void Parse(ICLIFlags toolFlags) {
            var lootboxes = GetLootboxes();

            if (toolFlags is ListFlags flags)
                if (flags.JSON) {
                    OutputJSON(lootboxes, flags);
                    return;
                }

            foreach (var lootbox in lootboxes) {
                Log($"{lootbox.Name}");
                if (lootbox.ShopCards != null)
                    foreach (var shopCard in lootbox.ShopCards)
                        Log($"\t{shopCard.Text}");
            }
        }

        public List<LootBox> GetLootboxes() {
            var @return = new List<LootBox>();

            foreach (var key in TrackedFiles[0xCF]) {
                var lootbox = GetInstance<STULootBox>(key);
                if (lootbox == null) continue;

                @return.Add(new LootBox(lootbox));
            }

            return @return;
        }
    }
}
