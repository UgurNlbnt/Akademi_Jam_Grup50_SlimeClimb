using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public GameObject otherPlayer;
    public KeyCode teleportKey;
    public Vector2 teleportTargetPosition;
    public string targetLayerName = "Player1"; // Görünecek layer
    public string originalLayerName = "Player2"; // Eski layer

    public float visibleDuration = 1.5f; // Kaç saniye sonra eski layer'a dönecek?

    void Update()
    {
        if (Input.GetKeyDown(teleportKey))
        {
            if (otherPlayer != null)
            {
                // Iþýnla
                otherPlayer.transform.position = teleportTargetPosition;

                // Layer'ý deðiþtir
                otherPlayer.layer = LayerMask.NameToLayer(targetLayerName);

                // Zamanlayýcýyla eski layer'a dön
                Invoke(nameof(RestoreLayer), visibleDuration);
            }
        }
    }

    void RestoreLayer()
    {
        if (otherPlayer != null)
        {
            otherPlayer.layer = LayerMask.NameToLayer(originalLayerName);
        }
    }
}
