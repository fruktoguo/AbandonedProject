namespace YuoTools
{
    public delegate bool BoolAction();

    public delegate float FloatAction();

    public delegate int IntAction();

    public delegate string StringAction();

    public delegate bool BoolAction<T>(T t);

    public delegate float FloatAction<T>(T t);

    public delegate int IntAction<T>(T t);

    public delegate string StringAction<T>(T t);
}