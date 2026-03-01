namespace VeluriyamLibHjson.Core
{
    public class HjsonClientConfig : ModConfig
    {
        public static HjsonClientConfig Instance => ModContent.GetInstance<HjsonClientConfig>();

        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(true)]
        public bool ModifyManageModName { get; set; }
    }
}
