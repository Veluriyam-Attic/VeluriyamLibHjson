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
        private static readonly Regex MatchingKeyBindRegex =
            new Regex(@"\[vkb\/(\w+\/\w+):([^\]]+)\]", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        /// <summary>
        /// <br/>匹配具体需要替换的键位和键名位置的正则表达式
        /// </summary>
        private static readonly Regex MarchingKeyOrName = new Regex(@"(Name|Key)", RegexOptions.Compiled | RegexOptions.CultureInvariant);

        /// <summary>
        /// <br/>用于替换Tooltip中需要被替换的按键绑定文本的方法，目前只支持键盘按键
        /// </summary>
        /// <param name="item"></param>
        /// <param name="tooltips"></param>
        public static List<TextSnippet> ModifyTooltips(On_ChatManager.orig_ParseMessage orig, string tooltips, Color baseColor)
        {
            tooltips = MatchingKeyBindRegex.Replace(tooltips, match =>
            {
                // 局部变量声明
                KeyConfiguration inputMode = PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard];
                string iname = match.Groups[1].Value;// 获取到的ExampleMod/ExampleKey
                Match key = Regex.Match(iname, @"(.+)\/(.+)");
                string keykey = key.Groups[1].Value switch
                {
                    "Terraria" => key.Groups[2].Value,
                    _ => iname
                };

                // 不存在这个按键绑定时
                if (!inputMode.KeyStatus.ContainsKey(keykey))
                    return VeluriyamLanguage.SafeGetTextValue(VeluriyamLanguage.vkey + "ModifyTooltips.CanNotFoundKeybind");

                List<string> keybinds = inputMode.KeyStatus[keykey];// 绑定的按键名字，即具体键位

                // 未绑定按键时
                if (keybinds.Count == 0)
                    return VeluriyamLanguage.SafeGetTextValue("LegacyMenu.195");

                // 一切正常时
                // 返回一个被替换掉所有Key和Name的字符串
                return MarchingKeyOrName.Replace(match.Groups[2].Value, m =>
                {
                    string name = key.Groups[1].Value switch
                    {
                        "Terraria" => VeluriyamLanguage.SafeGetTextValue(VanillanKeys[key.Groups[2].Value]),
                        _ => VeluriyamLanguage.SafeGetTextValue($"Mods.{key.Groups[1].Value}.Keybinds.{key.Groups[2].Value}.DisplayName")
                    };
                    string keys = string.Join(",", keybinds);
                    return m.Groups[1].Value switch
                    {
                        "Key" => keys,
                        "Name" => name,
                        _ => ""
                    };
                });
            });
            return orig(tooltips, baseColor);
        }

        private readonly static Dictionary<string, string> VanillanKeys = new() {
            ["MouseLeft"] = "LegacyMenu.162",
            ["MouseRight"] = "LegacyMenu.163",
            ["MouseMiddle"] = "tModLoader.MouseMiddle",
            ["MouseXButton1"] = "tModLoader.MouseXButton1",
            ["MouseXButton2"] = "tModLoader.MouseXButton2",
            ["Up"] = "LegacyMenu.148",
            ["Down"] = "LegacyMenu.149",
            ["Left"] = "LegacyMenu.150",
            ["Right"] = "LegacyMenu.151",
            ["Jump"] = "LegacyMenu.152",
            ["Throw"] = "LegacyMenu.153",
            ["Inventory"] = "LegacyMenu.154",
            ["Grapple"] = "LegacyMenu.155",
            ["SmartSelect"] = "LegacyMenu.160",
            ["SmartCursor"] = "LegacyMenu.161",
            ["QuickMount"] = "LegacyMenu.158",
            ["QuickHeal"] = "LegacyMenu.159",
            ["QuickMana"] = "LegacyMenu.156",
            ["QuickBuff"] = "LegacyMenu.157",
            ["MapZoomIn"] = "LegacyMenu.168",
            ["MapZoomOut"] = "LegacyMenu.169",
            ["MapAlphaUp"] = "LegacyMenu.170",
            ["MapAlphaDown"] = "LegacyMenu.171",
            ["MapFull"] = "LegacyMenu.173",
            ["MapStyle"] = "LegacyMenu.172",
            ["Hotbar1"] = "LegacyMenu.176",
            ["Hotbar2"] = "LegacyMenu.177",
            ["Hotbar3"] = "LegacyMenu.178",
            ["Hotbar4"] = "LegacyMenu.179",
            ["Hotbar5"] = "LegacyMenu.180",
            ["Hotbar6"] = "LegacyMenu.181",
            ["Hotbar7"] = "LegacyMenu.182",
            ["Hotbar8"] = "LegacyMenu.183",
            ["Hotbar9"] = "LegacyMenu.184",
            ["Hotbar10"] = "LegacyMenu.185",
            ["HotbarMinus"] = "LegacyMenu.174",
            ["HotbarPlus"] = "LegacyMenu.175",
            ["DpadRadial1"] = "LegacyMenu.186",
            ["DpadRadial2"] = "LegacyMenu.187",
            ["DpadRadial3"] = "LegacyMenu.188",
            ["DpadRadial4"] = "LegacyMenu.189",
            ["RadialHotbar"] = "LegacyMenu.190",
            ["RadialQuickbar"] = "LegacyMenu.244",
            ["DpadSnap1"] = "LegacyMenu.191",
            ["DpadSnap2"] = "LegacyMenu.192",
            ["DpadSnap3"] = "LegacyMenu.193",
            ["DpadSnap4"] = "LegacyMenu.194",
            ["LockOn"] = "LegacyMenu.231",
            ["ViewZoomIn"] = "LegacyMenu.168",
            ["ViewZoomOut"] = "LegacyMenu.169",
            ["Loadout1"] = "UI.Loadout1",
            ["Loadout2"] = "UI.Loadout2",
            ["Loadout3"] = "UI.Loadout3",
            ["ToggleCameraMode"] = "UI.ToggleCameraMode",
            ["ToggleCreativeMenu"] = "UI.ToggleCreativeMenu"
        };
    }
}