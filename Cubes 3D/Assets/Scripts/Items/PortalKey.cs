using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
class PortalKey : ItemScript {

    public void OnTriggerEnter(Collider coll) {
        // is it the gate trigger?
        GateTriggerBehaviour gateTriggerBehaviour = coll.GetComponent<GateTriggerBehaviour>();
        if(gateTriggerBehaviour != null) {
            gateTriggerBehaviour.gateOpen = true;

            // PortalKeys are always one-use
            GameObject.FindWithTag(Tags.Player).SendMessage(Messages.UNEQUIP_ITEM, this);
            Destroy(gameObject);
        }
    }
}