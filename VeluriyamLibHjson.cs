namespace VeluriyamLibHjson
{
    public class VeluriyamLibHjson : Mod
    {
        private static int LogedCount = 0;

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

        /// <summary>
        /// 一个内部的日志输出方法
        /// </summary>
        /// <param name="message"></param>
        internal static void Log(string message)
        {
            LogedCount++;
            Instance.Logger.Info($"HjsonLib-This is the n.{LogedCount} Log");
            Instance.Logger.Info(message);
        }
    }
}
