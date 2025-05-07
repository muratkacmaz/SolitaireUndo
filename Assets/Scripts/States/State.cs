using System;

/// <summary>
///
///  State class that encapsulates a value and provides a mechanism
///  to notify when the value changes.
///  It was not used in the project but is a good example of how to implement
/// 
/// </summary>
public class State<T>
{
    Func<T, T> setter;

    public State()
    {
    }
    public State(Func<T, T> setter)
    {
        this.setter = setter;
    }

    public Action<T> Changed;

    private T v;
    public T Value
    {
        get => v;
        set
        {
            this.v = setter != null ? setter.Invoke(value) : value;

            Changed?.Invoke(value);
        }
    }
}