using UnityEngine;

public class PlatformLandingEffect : MonoBehaviour
{
    [Tooltip("Player geldiðinde oynatýlacak efekt")]
    public ParticleSystem landingEffect;
    [Tooltip("Ne kadar aþaðý hýzla geliyorsa tetiklesin")]
    public float minFallSpeed = 0.1f;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Player")) return;

        // Karakter gerçekten yukarýdan düþerek inmiþ mi?
        if (col.rigidbody.velocity.y <= -minFallSpeed)
        {
            // Tüm temas noktalarýný kontrol et
            foreach (var contact in col.contacts)
            {
                // Platforma üstten iniþ mi? Normalin y bileþeni pozitif olmalý
                if (contact.normal.y > 0.5f)
                {
                    Instantiate(landingEffect, contact.point, Quaternion.identity);
                    break;
                }
            }
        }
    }
}
