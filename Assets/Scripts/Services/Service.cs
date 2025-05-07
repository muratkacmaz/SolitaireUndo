using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-10_000)]
public class Service : MonoBehaviour
{
    protected ServiceProvider _serviceProvider;

    protected virtual void Awake()
    {
        _serviceProvider = ServiceProvider.Instance;
        _serviceProvider.Register(this);
    }
    
}
