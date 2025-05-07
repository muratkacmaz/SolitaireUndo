using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100_000)]
public class ServiceProvider : MonoBehaviour
{
    private Dictionary<string, Service> _services = new Dictionary<string, Service>();
    public static ServiceProvider Instance;
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    
    public void Register(Service service)
    {
        string name = service.GetType().Name;
        _services.Add(name, service);
    }
    
    public T Get<T>() where T : Service
    {
        TryGet(out T result);
        return result;
    }
    
    public bool TryGet<T>(out T result) where T : Service
    {
        var type = typeof(T);
        
        string name = type.Name;
        if (!_services.ContainsKey(name))
        {
            result = null;
            return false;
        }
        
        result = (T)_services[name];
        return true;
    }
}
