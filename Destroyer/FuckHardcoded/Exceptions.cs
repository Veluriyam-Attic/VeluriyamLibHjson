namespace VeluriyamLibHjson.Destroyer.FuckHardcoded;

public class CanNotFindTargetTextException : Exception
{
    public CanNotFindTargetTextException(string message) : base(message) { }
}

public class CanNotFindTargetMethodException : Exception
{
    public CanNotFindTargetMethodException(string message) : base(message) { }
}
