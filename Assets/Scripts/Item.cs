using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected virtual void Collect(ICollector collector)
    {
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D col) //todo: remove virtual when upgrades will be tested
    {
        if (col.gameObject.TryGetComponent(out ICollector collector))
        {
            Collect(collector);
            Destroy(gameObject);
        }
    }
}
