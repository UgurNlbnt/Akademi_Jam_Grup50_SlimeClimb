using UnityEngine;

public class PlatformLandingEffect : MonoBehaviour
{
    [Tooltip("Player geldi�inde oynat�lacak efekt")]
    public ParticleSystem landingEffect;
    [Tooltip("Ne kadar a�a�� h�zla geliyorsa tetiklesin")]
    public float minFallSpeed = 0.1f;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Player")) return;

        // Karakter ger�ekten yukar�dan d��erek inmi� mi?
        if (col.rigidbody.velocity.y <= -minFallSpeed)
        {
            // T�m temas noktalar�n� kontrol et
            foreach (var contact in col.contacts)
            {
                // Platforma �stten ini� mi? Normalin y bile�eni pozitif olmal�
                if (contact.normal.y > 0.5f)
                {
                    Instantiate(landingEffect, contact.point, Quaternion.identity);
                    break;
                }
            }
        }
    }
}
