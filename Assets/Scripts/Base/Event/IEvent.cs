using System;

public interface IEvent
{
    Type GetEventType();
}

[Event]
public abstract class AEvent<A>: IEvent where A: struct
{
    public Type GetEventType()
    {
        return typeof (A);
    }

    protected abstract void Run(A a);

    public void Handle(A a)
    {
        try
        {
            Run(a);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}