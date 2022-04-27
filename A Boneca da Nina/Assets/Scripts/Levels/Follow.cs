using UnityEngine;

public class Follow : MonoBehaviour {
    
    public Transform objectToFollow;
    public Transform beginningOfLevel;
    public Transform endOfLevel;

    public int initialSpeed;
    public int finalSpeed;

    public float offset;
    public float mostFarMomCanBe;
    private float _xOfFarthestPositionReached;

    public SpriteRenderer sprite;

    void Update() {
        //n�o aumentar a velocidade se voltar na fase
        if (transform.position.x > _xOfFarthestPositionReached) {
            _xOfFarthestPositionReached = transform.position.x;
        }

        //porcentagem normalizada de progresso na fase
        var beginningOfLevelPosition = beginningOfLevel.position;
        
        float progression = 1 / (endOfLevel.position.x - beginningOfLevelPosition.x) *
                            (_xOfFarthestPositionReached - beginningOfLevelPosition.x);

        //velocidade da m�e baseada nas configura��es de velocidade objetivo pro come�o e pro fim da fase
        float momSpeed = initialSpeed - progression * (initialSpeed - finalSpeed);

        //nova posi��o da m�e baseada na velocidade obtida e no offset da Nina
        var objectToFollowPosition = objectToFollow.position;
        
        Vector3 newMomPosition = new Vector3(Mathf.MoveTowards(
                transform.position.x, objectToFollowPosition.x + offset, momSpeed * Time.deltaTime),
            objectToFollowPosition.y,
            transform.position.z);

        //impedir que a m�e se afaste al�m do limite estipulado na mostFarMomCanBe
        if (newMomPosition.x > objectToFollow.position.x - mostFarMomCanBe) {
            transform.position = newMomPosition;
        }
        else {
            transform.position = new Vector3(objectToFollow.position.x - mostFarMomCanBe,
                objectToFollowPosition.y,
                objectToFollowPosition.z);
        }

        //virar pra m�e sempre estar olhando pra Nina <3
        sprite.flipX = objectToFollow.position.x < transform.position.x;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("MomComesCloser")) {
            mostFarMomCanBe = 7;
            GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}
