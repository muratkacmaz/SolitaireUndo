using System;
using System.Collections.Generic;
using UnityEngine;

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
            if (setter != null)
            {
                this.v = setter.Invoke(value);
            }
            else
            {
                this.v = value;
            }

            Changed?.Invoke(value);
        }
    }
}