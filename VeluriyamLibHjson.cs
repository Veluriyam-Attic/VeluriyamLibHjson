namespace VeluriyamLibHjson
{
    public class VeluriyamLibHjson : Mod
	{
        public override void Load()
        {
            // 添加关于修改管理模组页面中模组名字的IL钩子
            if (HjsonClientConfig.Instance.ModifyManageModName)
                ModifyModName.ModifyManageModNameHook();
        }
	}
}
