using UnityEngine;
using TMPro;

public class PlayerScore : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI scoreText;
    [Header("Ayarlar")]
    public float scoreIncreaseHeight = 0.5f;

    private float highestY;
    private int score = 0;

    // ?? GameManager��n okuyabilmesi i�in public getter
    public int Score => score;

    void Start()
    {
        highestY = transform.position.y;
        UpdateUI();
    }

    void Update()
    {
        if (transform.position.y > highestY + scoreIncreaseHeight)
        {
            score++;
            highestY = transform.position.y;
            UpdateUI();
        }
    }

    // ?? UI g�ncellemesini tek bir yerde toplad�k
    void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "SKOR: " + score;
    }
}
