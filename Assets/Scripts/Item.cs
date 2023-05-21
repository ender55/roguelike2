using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private Sprite icon;

    public Sprite Icon => icon;
    
    protected abstract void Collect(Player player);

    protected void OnTriggerEnter2D(Collider2D col) //todo: remove virtual when upgrades will be tested
    {
        if (col.gameObject.TryGetComponent(out Player player))
        {
            Collect(player);
        }
    }

    //public Item Clone()
    //{
    //    
    //}
}
