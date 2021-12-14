using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    public event Action<Player> OnDied;
    //private SoundManager soundManager;
    private bool isJumping = false;
    private bool isBouncing = false;

    //items
    [Header("Items collection")]
    public Text textComponent;
    public int itemsCollected = 0;

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

    // Attack
    [Space(10)]
    [Header("Attack")]
    [SerializeField]
    private bool isAttacking;
    private bool canAttack = true;
    //private CustomTrigger2D attackTrigger;
    [SerializeField]
    private SpriteRenderer attackSprite;
    [SerializeField]
    [Range(0f, 2f)]
    private float attackDuration = 1f;
    [SerializeField]
    [Range(0f, 3f)]
    private float attackCooldown = 1.5f;
    [SerializeField]
    private float attackSpeedMultiplier = 0.5f;

    // Defense
    [Space(10)]
    [Header("Defense")]
    [SerializeField]
    private bool isDefending;
    //private CustomTrigger2D defenseTrigger;
    [SerializeField]
    private SpriteRenderer defenseSprite;
    [SerializeField]
    private float defenseSpeedMultiplier = 0.0f;

    // Blinking
    [Space(10)]
    [Header("Blinking")]
    [SerializeField]
    private bool blinking = false;
    private float blinkingDuration = 1f;

    //Climbing
    [Space(10)]
    [Header("Climbing")]
    [SerializeField]
    private bool hasStartedClimb = false;

    private Transform ladder;
    private float vertical = 0f;
    private float climbSpeed = 0.2f;

    private SpriteRenderer sprite;
    private Animator anim;
    private Blink blink;

    private float gravity = 3f;

    void Start()
    {
        //soundManager = SoundManager.instance;

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        blink = GetComponent<Blink>();
        //attackTrigger = transform.Find ("AttackTrigger").
        //GetComponentInChildren<CustomTrigger2D> ();
        //defenseTrigger = transform.Find ("DefenseTrigger").
        // GetComponentInChildren<CustomTrigger2D> ();
        //defenseTrigger.OnCustomTriggerEnter2D += OnDefenseTrigger;
        textComponent.text = "x" + itemsCollected;
    }

    private void Update()
    {
        //vertical = Input.GetAxisRaw("Vertical") * climbSpeed;

        float h = Input.GetAxis("Horizontal");

        if (h >= 0.1f)
            transform.localScale = Vector3.one;
        else if (h <= -0.1f)
            transform.localScale = new Vector3(-1f, 1f, 1f);

        if (Input.GetAxis("Horizontal") != 0f)
            Move();
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && !isJumping)
            Jump(false);

        if (Input.GetMouseButtonDown(0))
        {
            Move();
        }
        if (Input.GetMouseButtonUp(1) && isDefending)
        {
            EnableDefense(false);
        }

        if (Input.GetMouseButtonDown(0) && canAttack &&
            !isAttacking && !isDefending)
        {
            canAttack = false;
            EnableAttack(true);
            Invoke("StopAttack", attackDuration);
            Invoke("ResetCanAttack", attackCooldown);
            //soundManager.PlaySFX (SoundManager.SFXType.PLAYER_ATTACK, 0.5f);
        }
        else if (Input.GetMouseButtonDown(1) && !isAttacking && !isDefending)
        {
            EnableDefense(true);
        }

        if (blinking)
        {
            blink.StartBlink();
        }
        else
        {
            blink.StopBlink();
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

    private void StopAttack()
    {
        EnableAttack(false);
    }

    private void ResetCanAttack()
    {
        canAttack = true;
    }

    private void EnableAttack(bool enable)
    {
        isAttacking = enable;
        //attackTrigger.trigger.enabled = enable;
        attackSprite.enabled = enable;
        anim.SetBool("Attacking", enable);
    }

    private void StopDefense()
    {
        EnableDefense(false);
    }

    private void EnableDefense(bool enable)
    {
        isDefending = enable;
        //defenseTrigger.trigger.enabled = enable;
        defenseSprite.enabled = enable;
        anim.SetBool("Defending", enable);
    }

    private void UpdateSpeedMultiplier()
    {
        speedMultiplier = 1f;
        if (isAttacking)
            speedMultiplier *= attackSpeedMultiplier;
        if (isDefending)
            speedMultiplier *= defenseSpeedMultiplier;
    }

    private void Jump(bool isOnTire)
    {
        isJumping = true;
        isGrounded = false;
        if (isOnTire)
        {
            rb.AddForce(Vector2.up * jumpForceTire, ForceMode2D.Impulse);
        }
        else
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        //soundManager.PlaySFX (SoundManager.SFXType.JUMP);
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
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" && !blinking)
        {
            TakeDamage();
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
        if (collision.tag == "EnemyProjectile" && !blinking)
        {
            TakeDamage();
        }
        if (collision.tag == "Cloud")
        {
            speedMultiplier = 0.5f;
        }
        if (collision.tag == "Item")
        {
            itemsCollected++;
            textComponent.text = "x" + itemsCollected;
            Destroy(collision.gameObject);
        }
        if (collision.tag == "FinalBarrier")
        {
            SceneManager.LoadScene("Cutscene1");
        }
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
            Debug.Log("outside!!");
            speedMultiplier = 1f;
        }
        if (collision.tag == "Ladder")
        {
            rb.gravityScale = gravity;
        }
    }

    private void OnDefenseTrigger(Collider2D obj)
    {
        if (obj.gameObject.tag == "EnemyProjectile")
        {
            //soundManager.PlaySFX (SoundManager.SFXType.DEFEND);
            Destroy(obj.gameObject);
        }
    }


    private void TakeDamage()
    {

        gameObject.layer = LayerMask.NameToLayer("PlayerBlink");
        blinking = true;
        StartCoroutine(EndBlinkingAfterTime(blinkingDuration));
    }

    IEnumerator EndBlinkingAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        blinking = false;
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    public static float Half(float value)
    {
        return Mathf.Floor(value) + 0.5f;
    }

    private void ResetClimbing()
    {
        if (hasStartedClimb)
        {
            hasStartedClimb = false;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}