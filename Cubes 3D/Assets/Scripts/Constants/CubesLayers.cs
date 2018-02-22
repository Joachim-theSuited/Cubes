using UnityEngine;

public static class CubesLayers {

    public static readonly string Floors = "Floors";

    public static readonly int FloorsMask = LayerMask.GetMask(CubesLayers.Floors);

    public static readonly string Sinking = "Default without Water";

    public static readonly int SinkingID = LayerMask.NameToLayer(Sinking);

}