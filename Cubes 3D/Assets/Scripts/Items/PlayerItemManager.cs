using UnityEngine;

/// <summary>
/// Manages use of items for the player.
/// </summary>
public class PlayerItemManager : MonoBehaviour {

    public ItemScript equipped;

    // Update is called once per frame
    void Update() {
        if(equipped != null) {
            if(Input.GetButtonDown("Fire1")) {
                equipped.Use(gameObject);
            }
            if(Input.GetKeyDown(KeyCode.Q)) {
                DropEquipped();
            }
        }
    }

    /// <summary>
    /// Calls DropEquipped() and then equips the new item.
    /// Invokes OnPickedUp() on the item after adding it to the Transform hierarchy.
    /// </summary>
    /// <param name="item">Item to equip.</param>
    public void EquipItem(ItemScript item) {
        DropEquipped();

        item.transform.SetParent(transform, false);
        item.transform.localPosition = new Vector3(0.5f, 0, 0.5f);
        equipped = item;
        item.OnEquipped(gameObject);
    }

    /// <summary>
    /// Removes this item from the equipment slot, if it is currently equipped.
    /// Makes no further modifications on the item.
    /// </summary>
    public void Unequip(ItemScript item) {
        if(equipped == item)
            equipped = null;
    }

    /// <summary>
    /// Drops the currently equipped item (if one is present).
    /// Calls OnDropped() on the item after removing it from the players Transfrom hierarchy.
    /// </summary>
    private void DropEquipped() {
        if(equipped == null)
            return;
        
        equipped.transform.SetParent(null);
        equipped.OnDropped(gameObject);
        equipped = null;
    }
}
