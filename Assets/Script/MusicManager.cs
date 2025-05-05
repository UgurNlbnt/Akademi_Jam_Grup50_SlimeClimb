using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Tooltip("Oynatýlacak müzik klibi")]
    public AudioClip musicClip;

    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.playOnAwake = false;

            audioSource.Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
