using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReappearItemAction : ItemInteraction
{
    // Start is called before the first frame update
    void Start()
    {
        // Moving book to correct location
        Inventory.CurrentItem.transform.parent = this.transform;
        Inventory.CurrentItem.gameObject.SetActive(true);

        // Play book animation
        Inventory.CurrentItem.transform.localPosition = new Vector3();
        Inventory.CurrentItem.transform.localRotation = new Quaternion();

        // Remove book from inventory
        Inventory.RemoveItem(Inventory.CurrentItem);
        Inventory.UnequipItem();

        InteractionComplete?.Invoke();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
