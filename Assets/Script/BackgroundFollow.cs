using UnityEngine;

public class BacgroundItem : MonoBehaviour
{
    public Transform cameraToFollow; // BURAYA elle atanacak kamera
    public float effectValue = 0.5f;

    private float initialYDistance;

    void Start()
    {
        if (cameraToFollow == null)
        {
            Debug.LogWarning("BackgroundItem: Kamera atanmadý!");
            return;
        }

        initialYDistance = transform.position.y - cameraToFollow.position.y;
    }

    void LateUpdate()
    {
        if (cameraToFollow == null) return;

        float cameraY = cameraToFollow.position.y;
        float offsetY = cameraY * effectValue;

        transform.position = new Vector3(
            transform.position.x,
            cameraY + initialYDistance - offsetY,
            transform.position.z
        );
    }
}
