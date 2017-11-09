/// <summary>
/// Custom messages with usage information.
/// 
/// From: who will (usually) send this message
/// To: who is the intended receiver
/// Param: what is the type of the parameter
/// </summary>
public static class Messages {

    /// <summary>
    /// From: ItemScript
    /// To: PlayerItemManager
    /// Param: ItemScript
    /// </summary>
    public const string EQUIP_ITEM = "EquipItem";

    /// <summary>
    /// From: ItemScript
    /// To: PlayerItemManager
    /// Param: ItemScript
    /// </summary>
    public const string UNEQUIP_ITEM = "Unequip";

    /// <summary>
    /// From: Key
    /// To: any MonoBehaviour
    /// Param: Key
    /// </summary>
    public const string UNLOCK = "Unlock";

    /// <summary>
    /// From: ItemScript
    /// To: PlayerItemManager
    /// Param: none
    /// </summary>
    public const string DROP_EQUIPPED = "DropEquipped";

    /// <summary>
    /// From: InteractionTrigger
    /// To: any
    /// Param: none
    /// </summary>
    public const string INTERACT = "Interact";

    /// <summary>
    /// From: Any
    /// To: AbstractDamageReceiver
    /// Param: DamageInfo
    /// </summary>
    public const string DAMAGE = "ReceiveDamage";
}
