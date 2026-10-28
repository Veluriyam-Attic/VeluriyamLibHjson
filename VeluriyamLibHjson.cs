namespace VeluriyamLibHjson
{
    public class VeluriyamLibHjson : Mod
	{
        public static Mod Instance = null;

        public override void Load()
        {
            Instance = this;

            ILModifies.ModifyIL();

            OnModifies.ModifyOn(true);
        }

        public override void Unload()
        {
            Instance = null;
            
            OnModifies.ModifyOn(false);
        }

        internal static void Log(string message) => Instance.Logger.Info(message);
    }
}
