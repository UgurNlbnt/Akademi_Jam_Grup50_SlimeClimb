using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaşlangıcPlatformYokEtme : MonoBehaviour
{
    [Tooltip("Başlangıç platformlarına atadığınız Tag")]
    public string initialPlatformTag = "InitialPlatform";
    [Tooltip("Kaç saniye sonra kırılma animasyonu başlasın")]
    public float breakDelay = 7f;
    [Tooltip("Küçülme animasyonu süresi")]
    public float breakDuration = 0.5f;
    [Tooltip("Animasyonda kullanılacak Ease tipi")]
    public Ease breakEase = Ease.InBack;

    void Start()
    {
        // breakDelay saniye sonra BreakAll() metodunu çağır
        DOVirtual.DelayedCall(breakDelay, BreakAll, ignoreTimeScale: false);
    }

    void BreakAll()
    {
        // Sahnedeki tüm başlangıç platformlarını bul
        var platforms = GameObject.FindGameObjectsWithTag(initialPlatformTag);
        foreach (var plat in platforms)
        {
            // Ölçek animasyonu: şimdiki boyuttan (1,1,1) → (0,0,0)
            plat.transform
                .DOScale(Vector3.zero, breakDuration)
                .SetEase(breakEase)
                .OnComplete(() => Destroy(plat));
        }
    }
}
