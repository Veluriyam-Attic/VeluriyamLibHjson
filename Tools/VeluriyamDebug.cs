namespace VeluriyamLibHjson.Tools
{
    internal static class VeluriyamDebug
    {
        /// <summary>
        /// 自己的内部方法，注释要自己做主
        /// </summary>
        /// <param name="key"></param>
        /// <param name="time"></param>
        internal static void ErrPage(string key,int time = 10)
        {
            // 10好像是显示时间来着
            Utils.ShowFancyErrorMessage(VeluriyamLanguage.SafeGetText(VeluriyamLanguage.key + "Logs." + key).Value,time);
        }

    }
}