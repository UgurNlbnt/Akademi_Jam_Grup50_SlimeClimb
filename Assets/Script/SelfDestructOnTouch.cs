using UnityEngine;
using DG.Tweening;

public class SelfDestructOnTouch : MonoBehaviour
{
    [Tooltip("�arp��t�ktan ka� saniye sonra k�r�lma animasyonu ba�las�n")]
    public float destroyDelay = 4f;
    [Tooltip("Sallama �iddeti")]
    public float shakeAmount = 0.05f;
    [Tooltip("Animasyonda kullan�lacak minimum SDA h�z")]
    public float minShakeSpeed = 10f;
    [Tooltip("Animasyonda kullan�lacak maksimum SDA h�z")]
    public float maxShakeSpeed = 60f;

    [Header("K�r�lma Animasyonu")]
    [Tooltip("K���lme animasyonunun s�resi (saniye)")]
    public float breakDuration = 0.5f;
    [Tooltip("K���lme animasyonunda kullan�lacak Ease tipi")]
    public Ease breakEase = Ease.InBack;

    private bool countdownStarted = false;
    private Vector3 originalPosition;
    private float timer;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!countdownStarted && collision.gameObject.CompareTag("Player"))
        {
            countdownStarted = true;
            originalPosition = transform.position;
            timer = destroyDelay;

            // 4 saniye sonra BeginBreak'i �a��r
            DOVirtual.DelayedCall(destroyDelay, BeginBreak, ignoreTimeScale: false);
        }
    }

    void Update()
    {
        if (!countdownStarted) return;

        // Geri say�m� g�ncelle
        timer -= Time.deltaTime;
        float t = Mathf.Clamp01(1f - (timer / destroyDelay));
        float shakeSpeed = Mathf.Lerp(minShakeSpeed, maxShakeSpeed, t);

        // Sallama
        float shakeX = Mathf.Sin(Time.time * shakeSpeed) * shakeAmount;
        float shakeY = Mathf.Cos(Time.time * shakeSpeed) * shakeAmount;
        transform.position = originalPosition + new Vector3(shakeX, shakeY, 0);
    }

    private void BeginBreak()
    {
        // K�r�lma animasyonu: �l�ek 1?0
        transform
            .DOScale(Vector3.zero, breakDuration)
            .SetEase(breakEase)
            .OnComplete(() => Destroy(gameObject));
    }
}
