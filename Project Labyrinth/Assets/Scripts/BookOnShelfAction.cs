using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookOnShelfAction : ItemInteraction
{
    /// <summary>
    /// Animator to use when placing book
    /// </summary>
    private Animator Animations;

    protected override void ItemUsageAction() 
    {
        // Moving book to correct location
        Inventory.CurrentItem.transform.parent = this.transform;
        Inventory.CurrentItem.gameObject.SetActive(true);

        // Play book animation
        Inventory.CurrentItem.transform.localPosition = new Vector3();
        Inventory.CurrentItem.transform.localRotation = new Quaternion();
        Animations.Play("Put Book On Shelf");

        // Remove book from inventory
        Inventory.RemoveItem(Inventory.CurrentItem);
        Inventory.UnequipItem();

        InteractionComplete?.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {
        Animations = this.GetComponent<Animator>();   
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
