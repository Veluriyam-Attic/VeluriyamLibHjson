namespace VeluriyamLibHjson.Content
{
    public class ExampleItem : ModItem
    {
        public override string Texture => $"Terraria/Images/Item_{ItemID.IronBroadsword}";

        public override bool IsLoadingEnabled(Mod mod)
        {
            #if DEBUG
            return true;
            #else
            return false;
            #endif
        }
    }
}