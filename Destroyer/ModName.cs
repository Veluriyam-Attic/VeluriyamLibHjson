namespace VeluriyamLibHjson.Destroyer
{
    /// <summary>
    /// 关于模组名称相关的修改
    /// </summary>
    public static class ModName
    {
        #region ModifyConfigName
        /// <summary>
        /// <br/>用来修改模组在模组配置页面的显示名称
        /// <br/>更加安全的写法,可以在对应Mod没有加载的情况下不会报错,而是直接跳过
        /// <br/>请在<see langword="ModSystem.OnLocalizationsLoaded"/>方法中调用该方法
        /// </summary>
        /// <remarks>
        /// <br/>具体原理为先判断是否加载了这个Mod，如果没有的话则会跳过修改
        /// <br/>如果没有报错，则会通过反射获取这个<see langword="Mod"/>类的<see langword="DisplayName"/>属性，并将其值修改为对应语言文本的值
        /// <br/>由于这个属性是<see langword="{get; internal set;}"/>，所以必须使用反射才能修改它的值
        /// </remarks>
        /// <param name="ModName">你要获得的<see langword="Mod"/>类的名字</param>
        /// <param name="key">你希望修改为的文本对应的本地化键</param>
        public static void ModifyConfigName(string ModName,string key)
        {
            if (!ModLoader.HasMod(ModName))
                return;

            Type mod = ModLoader.GetMod(ModName).GetType();
            PropertyInfo prop = mod.GetProperty("DisplayName", BindingFlags.Public | BindingFlags.Instance);
            prop.SetValue(mod, VeluriyamLanguage.SafeGetText(key).Value);
        }

        /// <summary>
        /// <br/>用来修改模组在模组配置页面的显示名称
        /// <br/>请确定你知道你在干什么，这意味着你需要确保你的Mod强引用你要修改的Mod
        /// <br/>请在<see langword="ModSystem.OnLocalizationsLoaded"/>方法中调用该方法
        /// </summary>
        /// <remarks>
        /// <br/>通过反射获取这个<see langword="Mod"/>类的<see langword="DisplayName"/>属性，并将其值修改为对应语言文本的值
        /// <br/>由于这个属性是<see langword="{get; internal set;}"/>，所以必须使用反射才能修改它的值
        /// </remarks>
        /// <typeparam name="T">你要修改的<see langword="Mod"/>类</typeparam>
        /// <param name="ModName">你要获得的<see langword="Mod"/>类的名字</param>
        /// <param name="key">你希望修改为的文本对应的本地化键</param>
        public static void ModifyConfigName<T>(string ModName,string key) where T : Mod
        {
            PropertyInfo prop = typeof(T).GetProperty("DisplayName", BindingFlags.Public | BindingFlags.Instance);
            prop.SetValue(typeof(T), VeluriyamLanguage.SafeGetText(key).Value);
        }
        #endregion
    }
}
