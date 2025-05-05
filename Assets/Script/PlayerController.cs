using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Hareket Ayarlarý")]
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode jumpKey;

    [Header("Ground Check")]
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    [Header("VFX & SFX")]
    public ParticleSystem jumpEffect;
    public AudioClip jumpSound;
    public AudioClip landSound;
    [Range(0f, 1f)] public float landVolume = 0.5f;
    private AudioSource audioSource;

    [Header("Clamp Ayarlarý")]
    public bool isLeftPlayer = true;

    private Rigidbody2D rb;
    private Vector3 originalScale;
    private bool wasGrounded;
    private bool hasLanded = false;  // Ýniþ sesini tek sefer çalmak için flag

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        wasGrounded = true; // Baþlangýçta yerde kabul et
    }

    void Update()
    {
        HandleMove();
        HandleJump();
    }

    void HandleMove()
    {
        Vector2 vel = rb.velocity;
        if (Input.GetKey(leftKey))
        {
            vel.x = -moveSpeed;
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else if (Input.GetKey(rightKey))
        {
            vel.x = moveSpeed;
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
        else
        {
            vel.x = 0;
        }
        rb.velocity = vel;
    }

    void HandleJump()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (isGrounded && Input.GetKeyDown(jumpKey))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Zýplama VFX
            if (jumpEffect != null)
                Instantiate(jumpEffect, groundCheck.position, Quaternion.identity);

            // Zýplama SFX
            if (jumpSound != null)
                audioSource.PlayOneShot(jumpSound);

            // Her zýplamada iniþ flag'ini sýfýrla ki bir sonraki iniþte ses çalabilsin
            hasLanded = false;
        }
        wasGrounded = isGrounded;
    }

    void LateUpdate()
    {
        Vector3 pos = transform.position;
        if (isLeftPlayer)
            pos.x = Mathf.Clamp(pos.x, -7.7f, 0f);
        else
            pos.x = Mathf.Clamp(pos.x, 0f, 7.7f);
        transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.collider.CompareTag("Platform")) return;

        // Yalnýzca üstten inerken ve daha önce inmemiþsek
        if (!hasLanded && rb.velocity.y <= 0f)
        {
            foreach (var contact in col.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    // Ýniþ SFX, landVolume ile çal
                    if (landSound != null)
                        audioSource.PlayOneShot(landSound, landVolume);

                    hasLanded = true;
                    break;
                }
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if (col.collider.CompareTag("Platform"))
        {
            // Platformdan ayrýldýkça iniþ flag'ini sýfýrla
            hasLanded = false;
        }
    }
}
