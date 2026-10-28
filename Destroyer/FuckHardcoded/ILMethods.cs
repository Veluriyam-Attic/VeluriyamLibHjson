namespace VeluriyamLibHjson.Destroyer.FuckHardcoded;

#pragma warning disable CS1573
public static class ILMethods
{
    /// <summary>
    /// 这个方法让你可以快捷的填入对应的<see langword="MethodBase"/>(或<see langword="MethodInfo"/>)和原文本以及新文本去修改对应的文本
    /// 请注意,EnableException仅为调试时应该开启的选项
    /// 如果您不处于调试状态，请务必返回false
    /// 否则在原文本发生改变后可能导致Mod无法加载
    /// </summary>
    /// <param name="EnableException"></param>
    /// <exception cref="CanNotFindTargetTextException"></exception>
    public static void IL_Matchldstr(MethodBase method, string origStr, string newStr, bool EnableException = false)
    {
        EnableException = EnableException && HjsonConfig.Instance.EnableDebug;
        if (method == null)
        {
            if (!EnableException)
            {
                VLH.Log("HjsonLib-NullReferenceException");
                return;
            }
            throw new NullReferenceException();
        }

        MonoModHooks.Modify(method, il =>
        {
            ILCursor cursor = new(il);

            if (cursor.TryGotoNext(ldstr => ldstr.MatchLdstr(origStr)))
            {
                cursor.Remove();
                cursor.EmitLdstr(newStr);
            }
            else
            {
                if (!EnableException)
                {
                    VLH.Log($"HjsonLib-CanNotFindTargetTextException");
                    VLH.Log($"Can not localize {origStr}!");
                    VLH.Log($"In Method {method.Name}!");
                    VLH.Log($"The localized text is {newStr}!");
                    return;
                }
                throw new CanNotFindTargetTextException("HjsonLib-CanNotFindTargetTextException\n" +
                    $"Can not localize {origStr}!\n" +
                    $"In Method {method.Name}!\n" +
                    $"The localized text is {newStr}!");
            }
        });
    }
}
