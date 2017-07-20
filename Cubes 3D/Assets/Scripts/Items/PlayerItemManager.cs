using UnityEngine;

/// <summary>
/// Manages use of items for the player.
/// </summary>
[RequireComponent(typeof(LineRenderer))] // for the throwing preview
public class PlayerItemManager : MonoBehaviour {

    public ItemScript equipped;

    /// <summary>
    /// How long the player needs to hold the drop button to switch to throwing.
    /// </summary>
    public float throwTimeThreshold;
    /// <summary>
    /// How fast the throw distance will increase.
    /// </summary>
    public float throwSpeedup;
    public float minThrowDistance;
    public float maxThrowDistance;

    public int throwPreviewLOD = 5;

    /// <summary>
    /// The last time the drop button was pressed.
    /// </summary>
    float dropPressed = 0;

    LineRenderer throwingPreview;

    void Start() {
        throwingPreview = GetComponent<LineRenderer>();
        throwingPreview.enabled = false;
        throwingPreview.useWorldSpace = false;
        throwingPreview.positionCount = throwPreviewLOD;
    }

    // Update is called once per frame
    void Update() {
        if(equipped != null) {
            if(Input.GetButtonDown(Inputs.UseItem)) {
                equipped.Use(gameObject);
            }
            if(Input.GetButtonDown(Inputs.DropItem)) {
                dropPressed = Time.time;
            }
            if(Input.GetButtonUp(Inputs.DropItem)) {
                if(Time.time - dropPressed < throwTimeThreshold)
                    DropEquipped();
                else
                    ThrowEquipped();
                throwingPreview.enabled = false;
            } else if(Input.GetButton(Inputs.DropItem)) {
                UpdateThrowPreview();
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
        item.transform.localPosition = new Vector3(0, 1, 0.5f);
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

    /// <summary>
    /// In addition to the effects of DropEquipped, a velocity is applied to the dropped item.
    /// (If a Rigidbody is present)
    /// </summary>
    void ThrowEquipped() {
        if(equipped == null)
            return;

        ItemScript tmp = equipped; // will be removed by dropping
        DropEquipped();

        // throw
        Rigidbody rb = tmp.GetComponent<Rigidbody>();
        if(rb != null) {
            rb.AddForce(-Physics.gravity / 2, ForceMode.VelocityChange); // -> about .65 seconds airtime, as the item starts at a height
            rb.AddForce(transform.forward * GetThrowTargetDistance() / 1.2f, ForceMode.VelocityChange); // 1.2 because unity physics is weird :/
        }
    }

    float GetThrowTargetDistance() {
        return Mathf.Clamp(minThrowDistance + (Time.time - dropPressed - throwTimeThreshold) * throwSpeedup, minThrowDistance, maxThrowDistance);
    }

    /// <summary>
    /// Updates the LineRenderer used for the throwing preview.
    /// </summary>
    void UpdateThrowPreview() {
        if(Time.time - dropPressed < throwTimeThreshold)
            return;
        Vector3 _start = new Vector3(0, 1, 0.5f);

        throwingPreview.enabled = true;
        for(int i = 0; i < throwPreviewLOD; ++i) {
            float t = (i / (throwPreviewLOD - 1f)) * 0.65f; //approximate airtime (see above)
            throwingPreview.SetPosition(i, _start//
            + Vector3.forward * GetThrowTargetDistance() / 0.65f * t//
            - Physics.gravity * t / 2 + Physics.gravity * t * t//
            );
        }
    }
}
