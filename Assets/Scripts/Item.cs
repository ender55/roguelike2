using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected virtual void Collect(ICollector collector)
    {
        
    }

    protected void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.TryGetComponent(out ICollector collector))
        {
            Collect(collector);
            Destroy(gameObject);
        }
    }
}
