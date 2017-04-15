using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InteractionTrigger))]
public class Loot : MonoBehaviour {

    public ItemScript loot;

    public void Interact() {
        GameObject item = Instantiate(loot.gameObject);
        GameObject player = GameObject.FindWithTag(Tags.Player);
        player.SendMessage(Messages.EQUIP_ITEM, item.GetComponent<ItemScript>());
        Destroy(this.gameObject);
    }
}
