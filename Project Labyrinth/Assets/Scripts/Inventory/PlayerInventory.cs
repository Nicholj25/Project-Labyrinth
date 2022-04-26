using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<InventoryItem> HeldItems;
    public InventoryItem CurrentItem {get; private set; }
    
    // Start is called before the first frame update
    void Start()
    {
        HeldItems = new List<InventoryItem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddItem(InventoryItem item)
    {
        HeldItems.Add(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        HeldItems.Remove(item);
    }

    public void EquipItem(InventoryItem item)
    {
        CurrentItem = item;
    }

    public void UnequipItem()
    {
        CurrentItem = null;
    }
}
