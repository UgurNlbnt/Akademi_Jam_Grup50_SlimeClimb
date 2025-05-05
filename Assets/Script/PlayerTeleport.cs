using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public GameObject otherPlayer;
    public KeyCode teleportKey;
    public Vector2 teleportTargetPosition;
    public string targetLayerName = "Player1"; // G�r�necek layer
    public string originalLayerName = "Player2"; // Eski layer

    public float visibleDuration = 1.5f; // Ka� saniye sonra eski layer'a d�necek?

    void Update()
    {
        if (Input.GetKeyDown(teleportKey))
        {
            if (otherPlayer != null)
            {
                // I��nla
                otherPlayer.transform.position = teleportTargetPosition;

                // Layer'� de�i�tir
                otherPlayer.layer = LayerMask.NameToLayer(targetLayerName);

                // Zamanlay�c�yla eski layer'a d�n
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
