namespace VeluriyamLibHjson.Modifier
{
    /// <summary>
    /// <br/>用于修改物品Tooltip的GlobalItem类
    /// </summary>
    internal class ModifyKeybind
    {
        /// <summary>
        /// <br/>用于匹配Tooltip中需要被替换的按键绑定文本的正则表达式
        /// </summary>
        private static Regex MatchingKeyBindRegex =
            new Regex(@"\[(\w+\/\w+):([^\]]+)\]", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        /// <summary>
        /// <br/>匹配具体需要替换的键位和键名位置的正则表达式
        /// </summary>
        private static Regex MarchingKeyOrName = new Regex(@"(Name|Key)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        /// <summary>
        /// <br/>用于替换Tooltip中需要被替换的按键绑定文本的方法，目前只支持键盘按键
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tooltips"></param>
        public static void ModifyTooltips(List<TooltipLine> tooltips)
        {
            // int i = 0;
            foreach (var tooltip in tooltips)
            {
                // tooltip.Text = $"{i++} {tooltip.Text}";
                tooltip.Text = MatchingKeyBindRegex.Replace(tooltip.Text, match =>
                {
                    // 局部变量声明
                    KeyConfiguration inputMode = PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard];
                    string iname = match.Groups[1].Value;// 获取到的ExampleMod/ExampleKey
                    List<string> keybinds = inputMode.KeyStatus[iname];// 绑定的按键名字，即具体键位

                    // 不存在这个按键绑定时
                    if (!inputMode.KeyStatus.ContainsKey(iname))
                        return VeluriyamLanguage.SafeGetTextValue(VeluriyamLanguage.vkey + "ModifyTooltips.CanNotFoundKeybind");

                    // 未绑定按键时
                    if (keybinds.Count == 0)
                        return VeluriyamLanguage.SafeGetTextValue(VeluriyamLanguage.vkey + "ModifyTooltips.UnboundKey");

                    // 一切正常时
                    // 返回一个被替换掉所有Key和Name的字符串
                    return MarchingKeyOrName.Replace(match.Groups[2].Value, m =>
                    {
                        Match key = Regex.Match(iname, @"(.+)\/(.+)");
                        string name = VeluriyamLanguage.SafeGetTextValue($"Mods.{key.Groups[1].Value}.Keybinds.{key.Groups[2].Value}.DisplayName");
                        string keys = string.Join(",", keybinds);
                        return m.Groups[1].Value switch
                        {
                            "Key" => keys,
                            "Name" => name,
                            _ => ""
                        };
                    });
                });
            }
        }
    }
}