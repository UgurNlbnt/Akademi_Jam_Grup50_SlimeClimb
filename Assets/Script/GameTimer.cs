using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    [Tooltip("Saniye cinsinden süreyi gösterecek TMP Text")]
    public TextMeshProUGUI timerText;

    private float elapsed;

    // Baþka script’lerden kolay eriþim için public getter
    public float ElapsedTime => elapsed;

    void Update()
    {
        // Süreyi arttýr
        elapsed += Time.deltaTime;

        // Ýstediðin formatta yazdýr (burada 2 ondalýk basamak)
        timerText.text = elapsed.ToString("F2");
    }
}
