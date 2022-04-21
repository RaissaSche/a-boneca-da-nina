using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private SoundManager soundManager;
    public AudioLowPassFilter filter;
    private bool isJumping = false;

    public GameObject mom;

    //Items
    [Header("Items collection")]
    public Text textComponent;
    public int itemsCollected = 0;
    public ParticleSystem stars;

    // Movement
    [Header("Moviment Attributes")]
    public Rigidbody2D rb;
    [SerializeField]
    [Range(0f, 10f)]
    private float moveSpeed = 3f;
    private float speedMultiplier = 1f;

    // Jump
    [Header("Jump")]
    [SerializeField]
    private bool isGrounded, isOnTire;
    [SerializeField]
    [Range(1f, 100f)]
    private float jumpForce = 5f;
    [Range(1f, 100f)]
    public float jumpForceTire = 5f;
    public Transform groundCheck;
    public LayerMask groundLayer;

    //Climbing
    [Space(10)]
    [Header("Climbing")]
    [SerializeField]
    private bool hasStartedClimb = false;
    public SpriteRenderer sprite;

    private Transform ladder;
    private float vertical = 0f;
    private float climbSpeed = 0.2f;

    private Animator anim;

    private float gravity = 3f;

    void Start()
    {
        soundManager = SoundManager.instance;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        textComponent.text = "x" + itemsCollected;
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");

        if (h >= 0.1f)
        {
            transform.localScale = Vector3.one;
            sprite.flipX = false;
        }

        else if (h <= -0.1f)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            sprite.flipX = true;
        }

        if (Input.GetAxis("Horizontal") != 0f)
        {
            Move();
        }

        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            && !isJumping)
        {
            Jump(false);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Move();
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * speedMultiplier, rb.velocity.y);
    }

    public void SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    private void Jump(bool isOnTire)
    {
        isJumping = true;
        isGrounded = false;
        if (isOnTire)
        {
            rb.AddForce(Vector2.up * jumpForceTire, ForceMode2D.Impulse);
            soundManager.PlaySFX(SoundManager.SFXType.SEAT_SFX, 0.7f);
        }
        else
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        soundManager.PlaySFX(SoundManager.SFXType.JUMP, 0.7f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isJumping = false;
            isGrounded = true;
        }
        if (collision.collider.CompareTag("Tire"))
        {
            if (!isOnTire)
            {
                isOnTire = true;
                Jump(isOnTire);
            }
        }
    }


    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.collider.CompareTag("Tire"))
        {
            isOnTire = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Cloud")
        {
            filter.cutoffFrequency = 1800;
            speedMultiplier = 0.5f;
        }
        if (collision.tag == "Item")
        {
            itemsCollected++;
            textComponent.text = "x" + itemsCollected;
            stars.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
            stars.enableEmission = true;
            stars.Play();            
            StartCoroutine(StopStars(collision.gameObject));
            soundManager.PlaySFX(SoundManager.SFXType.NINA_SFX, 0.7f);

        }
        if (collision.tag == "FinalBarrier")
        {
            LevelManager.Instance.OpenScene("Cutscene1");
        }
        if (collision.tag == "FinalBarrier2")
        {
            LevelManager.Instance.OpenScene("Cutscene2");
        }
        if (collision.tag == "MomDoor")
        {
            mom.SetActive(false);
        }
    }

    IEnumerator StopStars(GameObject item)
    {
        // Stops particle effect
        yield return new WaitForSeconds(.6f);
        stars.enableEmission = false;        
    }

    public IEnumerator LoadCutscene(String name, float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(name);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ladder"))
        {
            rb.gravityScale = 0.0f;
            rb.velocity = new Vector2(rb.velocity.x, 0f);

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                rb.velocity = new Vector2(rb.velocity.x, 5f);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Cloud")
        {
            filter.cutoffFrequency = 20000;
            speedMultiplier = 1f;
        }
        if (collision.tag == "Ladder")
        {
            rb.gravityScale = gravity;
        }
    }

    public static float Half(float value)
    {
        return Mathf.Floor(value) + 0.5f;
    }
}