using System;

public interface IInvoke
{
    Type GetInvokeType();
}

public abstract class AInvokeHandler<A>: IInvoke where A: struct
{
    public Type GetInvokeType()
    {
        return typeof (A);
    }

    public abstract void Handle(A a);
}

public abstract class AInvokeHandler<A, T>: IInvoke where A: struct
{
    public Type GetInvokeType()
    {
        return typeof (A);
    }

    public abstract T Handle(A a);
}