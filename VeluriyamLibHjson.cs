namespace VeluriyamLibHjson
{
    public class VeluriyamLibHjson : Mod
	{
        public override void Load()
        {
            ILModifies.ModifyIL();

            OnModifies.ModifyOn(true);
        }

        public override void Unload()
        {
            OnModifies.ModifyOn(false);
        }
    }
}
