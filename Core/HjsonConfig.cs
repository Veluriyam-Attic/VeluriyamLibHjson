namespace VeluriyamLibHjson.Core
{
    public class HjsonConfig : ModConfig
    {
        public static HjsonConfig Instance => ModContent.GetInstance<HjsonConfig>();

        public override ConfigScope Mode => ConfigScope.ServerSide;

        // TODO:1.4.5未来内置支持这个功能，应移除
        /// <summary>
        /// 修改<see href="管理模组"/>页面的模组名称
        /// </summary>
        [DefaultValue(true)]
        [ReloadRequired]
        public bool ModifyManageModName;


        /// <summary>
        /// 为物品描述中的Buff绘制图片
        /// </summary>
        [DefaultValue(true)]
        public bool ImageTooltipBuff;

        /// <summary>
        /// 显示按下绑定快捷键就可以显示Buff详细描述的功能
        /// </summary>
        [DefaultValue(true)]
        public bool ShowBuffDetailHint;

        [DefaultValue(false)]
        [ReloadRequired]
        public bool EnableDebug;
    }
}
