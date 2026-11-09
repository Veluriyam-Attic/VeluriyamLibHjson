namespace VeluriyamLibHjson.Hotkeys;

/// <summary>
/// 显示Buff详情的对应绑定按键
/// </summary>
public class ShowBuffDetail : ModType
{
    public static ModKeybind Key;
    protected override void Register() => Key = KeybindLoader.RegisterKeybind(Mod, "ShowBuffDetail", Keys.LeftControl);
    public override void Unload() => Key = null;
}
