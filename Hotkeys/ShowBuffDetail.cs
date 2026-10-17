namespace VeluriyamLibHjson.Hotkeys;

public class ShowBuffDetail : ModSystem
{
    public static ModKeybind Key;

    public override void Load() => Key = KeybindLoader.RegisterKeybind(this.Mod, "ShowBuffDetail",Keys.LeftControl);

    public override void Unload() => Key = null;
}
