using UnityEngine;

public class CoreComponent : MonoBehaviour
{
    protected Core Core;

    protected virtual void Awake()
    {
        Core = transform.parent.GetComponent<Core>();

        if (Core == null)
        {
            Debug.LogError("No Core On the parent name: " + transform.parent.name);
        }
        
        Core.AddCoreComponent(this);
    }

    public virtual void LogicUpdate()
    {
        
    }
}
