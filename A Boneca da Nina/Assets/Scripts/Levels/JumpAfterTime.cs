using UnityEngine;

public class JumpAfterTime : MonoBehaviour {

    public Rigidbody2D rb;
    public Vector2 jumpForce;
    public float timeBetweenJumps;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        if (timeBetweenJumps >= 0f) {
            InvokeRepeating(nameof(Jump), timeBetweenJumps, timeBetweenJumps);
        }
        else {
            Jump();
        }
    }

    private void Jump() {
        rb.AddForce(jumpForce, ForceMode2D.Impulse);
        SoundManager.Instance.PlaySfxAtPosition(SoundManager.SfxType.JUMP_SFX, transform.position, 0.5f);
    }
}