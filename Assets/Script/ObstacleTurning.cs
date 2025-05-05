using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngelDönme : MonoBehaviour
{
    public float rotationSpeed = 180f;

    public ParticleSystem deathEffect;


    [Tooltip("Player öldüğünde çalacak ses")]
    public AudioClip deathSound;
    private AudioSource audioSource;


    void Start()
    {
        // Eğer AudioSource yoksa ekleyelim
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        // Z ekseni etrafında kendi merkezinde dön
        transform.Rotate(0f, 0f, rotationSpeed * Time.deltaTime);
    }



    void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        if (deathSound != null)
            audioSource.PlayOneShot(deathSound);

        // 1) Ölüm efekti
        if (deathEffect != null)
            Instantiate(deathEffect, col.transform.position, Quaternion.identity);

        

        // 2) Fizik ve görünürlüğü kapat, ama transform’u koru
        var sr = col.GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;
        var rb = col.GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;
        var col2d = col.GetComponent<Collider2D>();
        if (col2d != null) col2d.enabled = false;

        // 3) Sadece GameManager’a bildir
        GameManager.instance.PlayerDied(col.gameObject.name);
    }
}