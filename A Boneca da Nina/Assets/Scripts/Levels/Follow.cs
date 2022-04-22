using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform objectToFollow;
    public Transform beginningOfLevel;
    public Transform endOfLevel;

    public int initialSpeed;
    public int finalSpeed;

    public float offset;
    public float mostFarMomCanBe;
    private float xOfFartestPositionReached;

    public SpriteRenderer sprite;  

    void Update()
    {
        //não aumentar a velocidade se voltar na fase
        if (transform.position.x > xOfFartestPositionReached)
        {
            xOfFartestPositionReached = transform.position.x;
        }

        //porcentagem normalizada de progresso na fase
        float progression = 1 / (endOfLevel.position.x - beginningOfLevel.position.x) * 
            (xOfFartestPositionReached - beginningOfLevel.position.x);

        //velocidade da mãe baseada nas configurações de velocidade objetivo pro começo e pro fim da fase
        float momSpeed = initialSpeed - progression * (initialSpeed - finalSpeed);

        //nova posição da mãe baseada na velocidade obtida e no offset da Nina
       Vector3 newMomPosition = new Vector3(Mathf.MoveTowards(
            transform.position.x, objectToFollow.position.x + offset, momSpeed * Time.deltaTime),
            objectToFollow.position.y,
            transform.position.z);

        //impedir que a mãe se afaste além do limite estipulado na mostFarMomCanBe
        if (newMomPosition.x > objectToFollow.position.x - mostFarMomCanBe)
        {
            transform.position = newMomPosition;
        }
        else
        {
            transform.position = new Vector3(objectToFollow.position.x - mostFarMomCanBe,
                objectToFollow.position.y,
                objectToFollow.position.z);
        }

        //virar pra mãe sempre estar olhando pra Nina <3
        sprite.flipX = objectToFollow.position.x < transform.position.x;
    }
}
