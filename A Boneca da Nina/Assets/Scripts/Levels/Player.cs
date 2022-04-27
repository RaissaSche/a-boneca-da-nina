using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private SoundManager _soundManager;
    private bool _isJumping = false;
    public GameObject mom;
    public CanvasFade fade;

    //Items
    [Header("Items collection")]
    public Text textComponent;
    public int itemsCollected = 0;
    public ParticleSystem stars;

    // Movement
    [Header("Movement Attributes")]
    public Rigidbody2D rb;
    [SerializeField]
    [Range(0f, 10f)]
    private float moveSpeed = 3f;
    private float _speedMultiplier = 1f;

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
    //private bool hasStartedClimb = false;
    public SpriteRenderer sprite;

    //private Transform ladder;
    //private float vertical = 0f;
    //private float climbSpeed = 0.2f;

    private Animator _animator;

    private float gravity = 3f;

    void Start()
    {
        _soundManager = SoundManager.Instance;

        rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        textComponent.text = "x" + itemsCollected;
        //filter = soundManager.GetComponent<AudioLowPassFilter>();
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
            && !_isJumping)
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
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * _speedMultiplier, rb.velocity.y);
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
        _isJumping = true;
        isGrounded = false;
        if (isOnTire)
        {
            rb.AddForce(Vector2.up * jumpForceTire, ForceMode2D.Impulse);
            _soundManager.PlaySfx(SoundManager.SfxType.SEAT_SFX, 0.7f);
        }
        else
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        _soundManager.PlaySfx(SoundManager.SfxType.JUMP_SFX, 0.7f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isJumping = false;
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

    [Obsolete("Obsolete")]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cloud"))
        {
            _soundManager.GetLowPassFilter().cutoffFrequency = 1800;
            _speedMultiplier = 0.5f;
        }
        if (collision.CompareTag("Item"))
        {
            itemsCollected++;
            textComponent.text = "x" + itemsCollected;
            stars.transform.position = collision.transform.position;
            Destroy(collision.gameObject);
            stars.enableEmission = true;
            stars.Play();            
            StartCoroutine(StopStars(collision.gameObject));
            _soundManager.PlaySfx(SoundManager.SfxType.GIGGLE_SFX, 0.7f);

        }
        if (collision.CompareTag("FinalBarrier"))
        {
            fade.FadeOut();
            LevelManager.instance.OpenScene("Cutscene1");
        }
        if (collision.CompareTag("FinalBarrier2"))
        {
            fade.FadeOut();
            LevelManager.instance.OpenScene("Cutscene2");
        }
        if (collision.CompareTag("CommonDoor")) {
            _soundManager.PlaySfx(SoundManager.SfxType.DOOR_SFX);
        }
        if (collision.CompareTag("SushSign")) {
            _soundManager.PlaySfx(SoundManager.SfxType.SILENCE_SIGN_SFX);
        }
        if (collision.CompareTag("MomDoor"))
        {
            mom.SetActive(false);
            _soundManager.PlayBackgroundMusic(SoundManager.BGMType.LEVEL_2, 1f);
        }
    }

    [Obsolete("Obsolete")]
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
        if (collision.CompareTag("Cloud"))
        {
            _soundManager.GetLowPassFilter().cutoffFrequency = 20000;
            _speedMultiplier = 1f;
        }
        if (collision.CompareTag("Ladder"))
        {
            rb.gravityScale = gravity;
        }
    }

    public static float Half(float value)
    {
        return Mathf.Floor(value) + 0.5f;
    }
}