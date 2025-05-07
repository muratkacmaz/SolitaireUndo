using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Manages the registration and retrieval of services in the application.
/// </summary>
[DefaultExecutionOrder(-100_000)]
public class ServiceProvider : MonoBehaviour
{
    private Dictionary<string, Service> _services = new ();
    public static ServiceProvider Instance;

    /// <summary>
    /// Initializes the singleton instance of the ServiceProvider.
    /// </summary>
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    /// <summary>
    /// Registers a service with the ServiceProvider.
    /// </summary>
    /// <param name="service">The service to register.</param>
    public void Register(Service service)
    {
        string name = service.GetType().Name;
        _services.Add(name, service);
    }

    /// <summary>
    /// Retrieves a service of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the service to retrieve.</typeparam>
    /// <returns>The requested service.</returns>
    public T Get<T>() where T : Service
    {
        TryGet(out T result);
        return result;
    }

    /// <summary>
    /// Attempts to retrieve a service of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of the service to retrieve.</typeparam>
    /// <param name="result">The retrieved service, or null if not found.</param>
    /// <returns>True if the service was found, otherwise false.</returns>
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