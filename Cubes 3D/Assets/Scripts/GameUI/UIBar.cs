using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Display a value as a bar in the common way of e.g. a health or mana bar.
/// </summary>
public class UIBar : UIBehaviour {
    
    public Gradient color;

    public Image foreground;

    [RequireInterface(typeof(IExpressibleAsFraction))]
    public MonoBehaviour value;

    RectTransform rectTransform;

    override protected void Start() {
        base.Start();
        rectTransform = GetComponent<RectTransform>();
        LateUpdate();
    }

    void LateUpdate() {
        float frac = Mathf.Clamp01((value as IExpressibleAsFraction).GetFraction());
        foreground.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransform.rect.width * frac);
        foreground.color = color.Evaluate(frac);
    }
}
