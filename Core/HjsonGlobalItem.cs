namespace VeluriyamLibHjson.Core
{
    internal class HjsonGlobalItem : GlobalItem
    {
        public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
        {
            // 修改键位和键名
             ModifyKeybind.ModifyTooltips(tooltips);
        }
    }
}
