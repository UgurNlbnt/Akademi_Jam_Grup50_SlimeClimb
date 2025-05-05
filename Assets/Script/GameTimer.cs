using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Saniye cinsinden s�reyi g�sterecek TMP Text")]
    public TextMeshProUGUI timerText;

    private float elapsed;

    // Ba�ka script�lerden kolay eri�im i�in public getter
    public float ElapsedTime => elapsed;

    void Update()
    {
        // S�reyi artt�r
        elapsed += Time.deltaTime;

        // �stedi�in formatta yazd�r (burada 2 ondal�k basamak)
        timerText.text = elapsed.ToString("F2");
    }
}
