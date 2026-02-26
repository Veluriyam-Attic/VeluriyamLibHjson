namespace VeluriyamLibHjson.Destroyer
{
    /// <summary>
    /// 关于模组名称相关的修改
    /// </summary>
    public static class ModifyModName
    {
        #region ModifyConfigName
        /// <summary>
        /// <br/>用来修改模组在模组配置页面的显示名称
        /// <br/>更加安全的写法,可以在对应Mod没有加载的情况下不会报错,而是直接跳过
        /// <br/>请在<see langword="ModSystem.OnLocalizationsLoaded"/>方法中调用该方法
        /// </summary>
        /// <remarks>
        /// <br/>具体原理为先判断是否加载了这个Mod，如果没有的话则会跳过修改
        /// <br/>加载了则通过反射获取这个<see langword="Mod"/>类的<see langword="DisplayName"/>属性，并将其值修改为对应语言文本的值
        /// <br/>由于这个属性是<see langword="{get; internal set;}"/>，所以必须使用反射才能修改它的值
        /// </remarks>
        /// <param name="ModName">你要获得的<see langword="Mod"/>类的名字</param>
        /// <param name="key">你希望修改为的文本对应的本地化键</param>
        public static void ModifyConfigName(string ModName,string NewName)
        {
            if (!ModLoader.HasMod(ModName))
                return;

            Mod mod = ModLoader.GetMod(ModName);
            PropertyInfo prop = mod.GetType().GetProperty("DisplayName");
            prop.SetValue(mod, NewName);
        }

        /// <summary>
        /// <br/>用来修改模组在模组配置页面的显示名称
        /// <br/>请确定你知道你在干什么，这意味着你需要确保你的Mod强引用你要修改的Mod
        /// <br/>请在<see langword="ModSystem.OnLocalizationsLoaded"/>方法中调用该方法
        /// <br/>不推荐使用该重载
        /// </summary>
        /// <remarks>
        /// <br/>通过反射获取这个<see langword="Mod"/>类的<see langword="DisplayName"/>属性，并将其值修改为对应语言文本的值
        /// <br/>由于这个属性是<see langword="{get; internal set;}"/>，所以必须使用反射才能修改它的值
        /// </remarks>
        /// <typeparam name="T">你要修改的<see langword="Mod"/>类</typeparam>
        /// <param name="ModName">你要获得的<see langword="Mod"/>类的名字</param>
        /// <param name="key">你希望修改为的文本对应的本地化键</param>
        public static void ModifyConfigName<T>(T mod,string NewName) where T : Mod
        {
            PropertyInfo prop = mod.GetType().GetProperty("DisplayName", BindingFlags.Public | BindingFlags.Instance);
            prop.SetValue(mod,NewName);
        }
        #endregion

        #region ModifyManageModName
        private static Dictionary<string, string> names = new();

        /// <summary>
        /// <br/>修改你要修改的Mod，在管理模组页面的名字
        /// <br/>在<see langword="Mod"/>或<see langword="ModSystem"/>的<see langword="PostSetupContent"/>中调用该方法
        /// <br/>当你启用其他修改模组页面名字的模组时，该功能不会生效
        /// </summary>
        /// <remarks>
        /// <br/>该方法原理主要为向字典中添加你的所要汉化的Mod的Mod名字
        /// <br/>在其他位置进行更详细的更改
        /// <br/>详见<see langword="ModifyModName.ModifyManageModNameHook"/>方法
        /// </remarks>
        /// <param name="ModDisplayName">你要修改的Mod在管理Mod页面的名字</param>
        /// <param name="NewDisplayName"></param>
        public static void ModifyManageModName(string ModDisplayName, string NewDisplayName) => names.Add(ModDisplayName, NewDisplayName);

        /// <summary>
        /// <br/>修改你要修改的Mod，在管理模组页面的名字
        /// <br/>请不要调用该方法！！！
        /// </summary>
        /// <remarks>
        /// <br/>该方法原理主要为使用MonoModHooks修改管理模组页面的UI元素的显示名称
        /// <br/>在管理模组页面的UI元素中，模组的显示名称是通过调用<see langword="LocalMod.DisplayName"/>属性获得的
        /// <br/>通过修改这个属性的返回值来达到修改模组在管理模组页面的名字的目的
        /// <br/>如果你想要修改模组在管理模组页面的名字，请调用<see langword="ModifyModName.ModifyManageModName"/>方法来添加你想要修改的模组的名字和对应的文本
        /// <br/>如果出现问题，请关闭本Mod的Mod配置对应选项
        /// </remarks>
        internal static void ModifyManageModNameHook()
        {
            if (Main.dedServ)
                return;
            
            MonoModHooks.Modify(typeof(Main).Assembly.GetTypes().First(_ => _.Name == "UIModItem").GetMethod("OnInitialize"), Manipulator);
        }


        /// <summary>
        /// <br/>一个修改管理模组页面的UI元素的显示名称的IL钩子
        /// <br/>请不要调用这个方法！！！
        /// </summary>
        /// <param name="il"></param>
        private static void Manipulator(ILContext il)
        {
            var cursor = new ILCursor(il);

            while (cursor.TryGotoNext(
                MoveType.After,
                i => i.MatchCallvirt("Terraria.ModLoader.Core.LocalMod", "get_DisplayName")
            ))
            {
                cursor.EmitDelegate<Func<string, string>>(name =>
                {
                    return names.Keys.Contains(name)
                        ? names[name]
                        : name;
                });
            }
        }
        #endregion
    }
}
