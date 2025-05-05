using UnityEngine;
using TMPro;
using DG.Tweening;  // DOTween namespace

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public TextMeshProUGUI timerText;   // Ortadaki sayaç (TMP)
    public GameObject winPanel;         // Panel (baþta kapalý)
    public TextMeshProUGUI winText;     // "Sarý slime kazandý!" vs.
    public TextMeshProUGUI p1StatsText; // Detaylý istatistik metni
    public TextMeshProUGUI p2StatsText;

    [Header("Oyuncular")]
    public PlayerScore player1Score;    // PlayerScore component’i
    public PlayerScore player2Score;

    [Header("Çarpanlar")]
    public float survivalMultiplier = 0.6f;
    public float scoreMultiplier = 0.2f;

    private float survivalTimer = 0f;
    private bool gameEnded = false;

    [HideInInspector] public bool p1Dead = false;  // Artýk public
    [HideInInspector] public bool p2Dead = false;  // Artýk public

    private float p1DeathTime, p2DeathTime;
    private int p1DeathScore, p2DeathScore;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Panel baþta kapalý ve ölçek sýfýr olsun
        winPanel.SetActive(false);
        winPanel.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (gameEnded) return;

        // Sayaç
        survivalTimer += Time.deltaTime;
        if (timerText != null)
            timerText.text = survivalTimer.ToString("F2");
    }

    /// <summary>
    /// Öldüðünde KillOnContact’tan veya baþka yerden çaðrýlacak.
    /// </summary>
    public void PlayerDied(string playerName)
    {
        if (gameEnded) return;

        if (playerName == "Player1" && !p1Dead)
        {
            p1Dead = true;
            p1DeathTime = survivalTimer;
            p1DeathScore = player1Score.Score;
        }
        else if (playerName == "Player2" && !p2Dead)
        {
            p2Dead = true;
            p2DeathTime = survivalTimer;
            p2DeathScore = player2Score.Score;
        }

        // Her iki oyuncu da öldüyse oyunu bitir
        if (p1Dead && p2Dead)
            EndGame();
    }

    private void EndGame()
    {
        gameEnded = true;

        // Final puan hesap
        float p1Final = p1DeathTime * survivalMultiplier + p1DeathScore * scoreMultiplier;
        float p2Final = p2DeathTime * survivalMultiplier + p2DeathScore * scoreMultiplier;

        // ?? Kazanan metni ve rengi
        if (p1Final > p2Final)
        {
            // Sarý slime
            winText.text = "Sari slime kazandi!";
            winText.color = Color.yellow;
        }
        else if (p2Final > p1Final)
        {
            // Mor slime
            winText.text = "Mor slime kazandi!";
            // Dilersen Color.magenta de olur, veya kendi hex’in:
            winText.color = new Color32(128, 0, 128, 255);
        }
        else
        {
            winText.text = "Berabere!";
            winText.color = Color.white;
        }

        // Detaylý istatistikler
        if (p1StatsText != null)
            p1StatsText.text = $"Sari: sure={p1DeathTime:F2}, skor={p1DeathScore}, puan={p1Final:F2}";
        if (p2StatsText != null)
            p2StatsText.text = $"Mor: sure={p2DeathTime:F2}, skor={p2DeathScore}, puan={p2Final:F2}";

        // 1.1 saniye bekle, sonra DOTween ile paneli pop aç
        DOVirtual.DelayedCall(1.1f, () =>
        {
            winPanel.SetActive(true);
            winPanel.transform
                .DOScale(1f, 0.5f)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);

            Time.timeScale = 0f;
        }, ignoreTimeScale: true);
    }
}
