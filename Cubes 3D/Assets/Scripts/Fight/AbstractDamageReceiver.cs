using UnityEngine;

/// <summary>
/// SendMessage only accepts one parameter, so we wrap things up
/// </summary>
public struct DamageInfo {
    public float damageAmount;
    public GameObject source;

    public DamageInfo(float amt, GameObject src) {
        damageAmount = amt;
        source = src;
    }
}

public interface AbstractDamageReceiver {

    void ReceiveDamage(DamageInfo di);

}
