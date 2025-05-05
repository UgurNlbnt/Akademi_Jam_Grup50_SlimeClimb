using UnityEngine;
using DG.Tweening;

public class SelfDestructOnTouch : MonoBehaviour
{
    [Tooltip("Çarpýþtýktan kaç saniye sonra kýrýlma animasyonu baþlasýn")]
    public float destroyDelay = 4f;
    [Tooltip("Sallama þiddeti")]
    public float shakeAmount = 0.05f;
    [Tooltip("Animasyonda kullanýlacak minimum SDA hýz")]
    public float minShakeSpeed = 10f;
    [Tooltip("Animasyonda kullanýlacak maksimum SDA hýz")]
    public float maxShakeSpeed = 60f;

    [Header("Kýrýlma Animasyonu")]
    [Tooltip("Küçülme animasyonunun süresi (saniye)")]
    public float breakDuration = 0.5f;
    [Tooltip("Küçülme animasyonunda kullanýlacak Ease tipi")]
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

            // 4 saniye sonra BeginBreak'i çaðýr
            DOVirtual.DelayedCall(destroyDelay, BeginBreak, ignoreTimeScale: false);
        }
    }

    void Update()
    {
        if (!countdownStarted) return;

        // Geri sayýmý güncelle
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
        // Kýrýlma animasyonu: ölçek 1?0
        transform
            .DOScale(Vector3.zero, breakDuration)
            .SetEase(breakEase)
            .OnComplete(() => Destroy(gameObject));
    }
}
