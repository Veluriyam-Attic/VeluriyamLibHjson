namespace VeluriyamLibHjson.Core
{
    /// <summary>
    /// <br/>对于<see langword="Language"/>类的一些方法的拓展
    /// <br/>由于<see langword="Language"/>类是静态类，所以没有办法写拓展方法
    /// </summary>
    public static class VeluriyamLanguage
    {
        internal const string vkey = "Mods.VeluriyamLibHjson.";

        #region GetText(Value)
        /// <summary>
        /// <br/>更加安全的，获取文本值的方法
        /// <br/>如果获取失败则会返回一个提示文本，而不是直接返回key导致显示错误
        /// </summary>
        /// <remarks>
        /// 请注意，如果你想要获取的文本就是键的名字，请不要使用该方法
        /// </remarks>
        /// <param name="key">你要获取的文本的本地化键</param>
        /// <param name="language">你要调用文本对应的语言，空则为游戏当前语言</param>
        /// <returns></returns>
        public static string SafeGetTextValue(string key , string language = "Default")
        {
            string outcome = Language.GetText(key).Value;
            if (outcome == key)
                return Language.GetText(vkey + "Logs.CanNotGetTextValue").Value;
            return outcome;
        }

        /// <summary>
        /// <br/>更加安全的，获取文本的方法
        /// <br/>如果获取失败则会返回一个提示文本，而不是直接返回key导致显示错误
        /// </summary>
        /// <remarks>
        /// 请注意，如果你想要获取的文本就是键的名字，请不要使用该方法
        /// </remarks>
        /// <param name="key">你要获取的文本的本地化键</param>
        /// <returns></returns>
        public static LocalizedText SafeGetText(string key)
        {
            LocalizedText outcome = Language.GetText(key);
            if (outcome.Value == key)
                return Language.GetText(vkey + "Logs.CanNotGetText");
            return outcome;
        }
        #endregion
    }
}
