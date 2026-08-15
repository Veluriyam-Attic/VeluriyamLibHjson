namespace VeluriyamLibHjson.Core
{
    public class HjsonConfig : ModConfig
    {
        public static HjsonConfig Instance => ModContent.GetInstance<HjsonConfig>();

        public override ConfigScope Mode => ConfigScope.ServerSide;

        [DefaultValue(true)]
        [ReloadRequired]
        public bool ModifyManageModName;


        [DefaultValue(true)]
        public bool ImageTooltipBuff;
    }
}
