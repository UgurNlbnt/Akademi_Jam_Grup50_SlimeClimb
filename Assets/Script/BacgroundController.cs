using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BacgroundController : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public Transform playerLeft;
    public Transform playerRight;

    public float chunkHeight = 10f;
    public float xOffsetLeft = -4f;
    public float xOffsetRight = 4f;

    private float nextSpawnY_Left;
    private float nextSpawnY_Right;

    void Start()
    {
        nextSpawnY_Left = playerLeft.position.y;
        nextSpawnY_Right = playerRight.position.y;
    }

    void Update()
    {
        // Sol taraf için arka plan
        while (playerLeft.position.y + 15f > nextSpawnY_Left)
        {
            Vector3 spawnPos = new Vector3(xOffsetLeft, nextSpawnY_Left, 5);
            Instantiate(backgroundPrefab, spawnPos, Quaternion.identity);
            nextSpawnY_Left += chunkHeight;
        }

        // Sað taraf için arka plan
        while (playerRight.position.y + 15f > nextSpawnY_Right)
        {
            Vector3 spawnPos = new Vector3(xOffsetRight, nextSpawnY_Right, 5);
            Instantiate(backgroundPrefab, spawnPos, Quaternion.identity);
            nextSpawnY_Right += chunkHeight;
        }
    }
}
