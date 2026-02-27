namespace VeluriyamLibHjson
{
    public class VeluriyamLibHjson : Mod
	{
        public override void Load()
        {
            // 添加关于修改管理模组页面中模组名字的IL钩子
            if (HjsonClientConfig.Instance.ModifyManageModName)
                ModifyModName.ModifyManageModNameHook();
            // 把自动补全本地化文件的方法炸了，要不然老是自动给别的Mod加本地化文件
            MethodInfo method = typeof(LocalizationLoader).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).First(m => m.Name == "UpdateLocalizationFilesForMod");
            MonoModHooks.Modify(method, Manipulator);
        }

        /// <summary>
        /// <br/>爆炸就是艺术
        /// </summary>
        /// <param name="il"></param>
        private static void Manipulator(ILContext il)
        {
            ILCursor cursor = new ILCursor(il);

            if (cursor.TryGotoNext(x => x.MatchCall(typeof(File), nameof(File.WriteAllText))))
            {
                cursor.Remove();

                cursor.EmitDelegate<Action<string, string>>((path, text) =>
                {
                    if (!path.Contains("VeluriyamLibHjson"))
                        File.WriteAllText(path, text);
                });
            }
        }

    }
}
