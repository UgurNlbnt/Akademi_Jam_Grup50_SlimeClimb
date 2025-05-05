using UnityEngine;

public class PlatformSpawnerDual : MonoBehaviour
{
    [Header("Prefabs & Player'lar")]
    public GameObject platformPrefabLeft;
    public GameObject platformPrefabRight;
    public Transform playerLeft;
    public Transform playerRight;

    [Header("Spawn Ayarlarý")]
    public float verticalSpacing = 1.5f;
    public Vector2 leftXRange = new Vector2(-8f, -1f);
    public Vector2 rightXRange = new Vector2(1f, 8f);

    private float highestY_Left;
    private float highestY_Right;

    void Start()
    {
        if (playerLeft != null) highestY_Left = playerLeft.position.y;
        if (playerRight != null) highestY_Right = playerRight.position.y;
    }

    void Update()
    {
        // SOL TARAFTA: playerLeft var mý ve ölü deðil mi?
        if (playerLeft != null && !GameManager.instance.p1Dead)
        {
            while (highestY_Left < playerLeft.position.y + 10f)
            {
                float x = Random.Range(leftXRange.x, leftXRange.y);
                Vector3 pos = new Vector3(x, highestY_Left + verticalSpacing, 0);
                Instantiate(platformPrefabLeft, pos, Quaternion.identity);
                highestY_Left += verticalSpacing;
            }
        }

        // SAÐ TARAFTA: playerRight var mý ve ölü deðil mi?
        if (playerRight != null && !GameManager.instance.p2Dead)
        {
            while (highestY_Right < playerRight.position.y + 10f)
            {
                float x = Random.Range(rightXRange.x, rightXRange.y);
                Vector3 pos = new Vector3(x, highestY_Right + verticalSpacing, 0);
                Instantiate(platformPrefabRight, pos, Quaternion.identity);
                highestY_Right += verticalSpacing;
            }
        }
    }
}
