namespace VeluriyamLibHjson.Hotkeys;

public class ShowBuffDetail : ModType
{
    public static ModKeybind Key;
    protected override void Register() => Key = KeybindLoader.RegisterKeybind(Mod, "ShowBuffDetail", Keys.LeftControl);
    public override void Unload() => Key = null;
}
