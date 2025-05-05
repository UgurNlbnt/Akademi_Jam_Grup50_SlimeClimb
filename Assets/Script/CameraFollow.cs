using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("Takip edilecek karakter")]
    public Transform target;
    [Tooltip("Karakterin üstünde kaç birim pozisyonda kalacak")]
    public float yOffset = 1f;

    private float fixedX;
    private float fixedZ;

    void Start()
    {
        // Kamera X ve Z pozisyonunu sabit tut
        fixedX = transform.position.x;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (target == null) return;   // Karakter yoksa çýk

        // Hemen karakterin üstüne sýçra
        float newY = target.position.y + yOffset;
        transform.position = new Vector3(fixedX, newY, fixedZ);
    }

}
