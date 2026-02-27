namespace VeluriyamLibHjson.Modifier;
/// <summary>
/// <br/>用于修改物品Tooltip的GlobalItem类
/// </summary>
public class ModifyKeybind : GlobalItem
{
    // public override bool InstancePerEntity => false;

    /// <summary>
    /// <br/>用于匹配Tooltip中需要被替换的按键绑定文本的正则表达式
    /// </summary>
    public static readonly Regex MatchingKeyBindRegex = 
        new Regex(@"\[([^\]]+):BoundKey\]", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    /// <summary>
    /// <br/>用于替换Tooltip中需要被替换的按键绑定文本的方法，目前只支持键盘按键
    /// </summary>
    /// <param name="item"></param>
    /// <param name="tooltips"></param>
    public override void ModifyTooltips(Item item, List<TooltipLine> tooltips)
    {
        // int i = 0;
        foreach (var tooltip in tooltips)
        {
            // tooltip.Text = $"{i++} {tooltip.Text}";
            tooltip.Text = MatchingKeyBindRegex.Replace(tooltip.Text, match =>
            {
                var inputMode = PlayerInput.CurrentProfile.InputModes[InputMode.Keyboard];
                if (!inputMode.KeyStatus.ContainsKey(match.Groups[1].Value))
                    return "[未加载按键]";
                return string.Join(", ", inputMode.KeyStatus[match.Groups[1].Value]);
            });
        }
    }
}