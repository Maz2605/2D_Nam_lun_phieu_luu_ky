using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Core : MonoBehaviour
{
    private readonly List<CoreComponent> _components = new();

    private void Awake()
    {
        
    }

    public void AddCoreComponent(CoreComponent component)
    {
        if (_components.Contains(component)) return;
        _components.Add(component);
    }

    public void LogicUpdate()
    {
        foreach (CoreComponent component in _components)
        {
            component.LogicUpdate();
        }
    }

    public T GetCoreComponent<T>() where T : CoreComponent
    {
        // Tìm trong danh sách _components
        var coreComponent = _components.OfType<T>().FirstOrDefault();
        if (coreComponent != null)
        {
            return coreComponent;
        }

        coreComponent = GetComponentInChildren<T>();
        
        if (coreComponent != null)
        {
            return coreComponent;
        } 
        
        Debug.LogWarning(typeof(T) + " not found on " + transform.parent.name);

        return null;
    }

    public T GetCoreComponent<T>(ref T value) where T : CoreComponent
    {
        return value = GetCoreComponent<T>();
    }
}
