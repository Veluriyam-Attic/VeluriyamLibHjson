namespace VeluriyamLibHjson.Core
{
    internal class OnModifies
    {
        internal static void ModifyOn(bool apply)
        {
            if (apply)
            {
                On_ChatManager.ParseMessage += ModifyKeybind.ModifyTooltips;
                ImageTheTooltip.On_ItemLoader_ModifyTooltips();
            }
            else
            {
                On_ChatManager.ParseMessage -= ModifyKeybind.ModifyTooltips;
            }
        }
    }
}
