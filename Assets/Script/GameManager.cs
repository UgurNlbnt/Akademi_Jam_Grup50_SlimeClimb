using UnityEngine;
using TMPro;
using DG.Tweening;  // DOTween namespace

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("UI")]
    public TextMeshProUGUI timerText;   // Ortadaki saya� (TMP)
    public GameObject winPanel;         // Panel (ba�ta kapal�)
    public TextMeshProUGUI winText;     // "Sar� slime kazand�!" vs.
    public TextMeshProUGUI p1StatsText; // Detayl� istatistik metni
    public TextMeshProUGUI p2StatsText;

    [Header("Oyuncular")]
    public PlayerScore player1Score;    // PlayerScore component�i
    public PlayerScore player2Score;

    [Header("�arpanlar")]
    public float survivalMultiplier = 0.6f;
    public float scoreMultiplier = 0.2f;

    private float survivalTimer = 0f;
    private bool gameEnded = false;

    [HideInInspector] public bool p1Dead = false;  // Art�k public
    [HideInInspector] public bool p2Dead = false;  // Art�k public

    private float p1DeathTime, p2DeathTime;
    private int p1DeathScore, p2DeathScore;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        // Panel ba�ta kapal� ve �l�ek s�f�r olsun
        winPanel.SetActive(false);
        winPanel.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        if (gameEnded) return;

        // Saya�
        survivalTimer += Time.deltaTime;
        if (timerText != null)
            timerText.text = survivalTimer.ToString("F2");
    }

    /// <summary>
    /// �ld���nde KillOnContact�tan veya ba�ka yerden �a�r�lacak.
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

        // Her iki oyuncu da �ld�yse oyunu bitir
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
            // Sar� slime
            winText.text = "Sari slime kazandi!";
            winText.color = Color.yellow;
        }
        else if (p2Final > p1Final)
        {
            // Mor slime
            winText.text = "Mor slime kazandi!";
            // Dilersen Color.magenta de olur, veya kendi hex�in:
            winText.color = new Color32(128, 0, 128, 255);
        }
        else
        {
            winText.text = "Berabere!";
            winText.color = Color.white;
        }

        // Detayl� istatistikler
        if (p1StatsText != null)
            p1StatsText.text = $"Sari: sure={p1DeathTime:F2}, skor={p1DeathScore}, puan={p1Final:F2}";
        if (p2StatsText != null)
            p2StatsText.text = $"Mor: sure={p2DeathTime:F2}, skor={p2DeathScore}, puan={p2Final:F2}";

        // 1.1 saniye bekle, sonra DOTween ile paneli pop a�
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
