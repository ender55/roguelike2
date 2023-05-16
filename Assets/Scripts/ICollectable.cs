using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected abstract void Collect();

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.TryGetComponent(out Player player))
        {
            Collect();
        }
    }
}
