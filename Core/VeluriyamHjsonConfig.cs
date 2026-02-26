using System.ComponentModel;

namespace VeluriyamLibHjson.Core
{
    public class VeluriyamHjsonConfig : ModConfig
    {
        public static VeluriyamHjsonConfig instance => ModContent.GetInstance<VeluriyamHjsonConfig>();

        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(true)]
        public bool ModifyManageModName { get; set; }
    }
}
