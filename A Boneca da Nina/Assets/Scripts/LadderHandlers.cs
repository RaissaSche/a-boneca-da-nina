using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderHandlers : MonoBehaviour
{
    void Awake()
    {
        /*The logic is self-explanatory and straightforward. I use half of the sprite’s height to create
        an offset and position handlers in their respective locations. To ensure proper sizing of the box
        collider boundaries I take the dimensions values from the sprite renderer component (lines 11-12).
        The ladder is almost ready to be placed in the game! The only thing we now have to take care of
        is the actual solid tile that will allow our characters to step on the upper platforms when they
         reach the top.*/

        float width = GetComponent<SpriteRenderer>().size.x;
        float height = GetComponent<SpriteRenderer>().size.y;
        Transform topHandler = transform.GetChild(0).transform;
        Transform bottomHandler = transform.GetChild(1).transform;
        topHandler.position = new Vector3(transform.position.x, transform.position.y + (height / 2), 0);
        bottomHandler.position = new Vector3(transform.position.x, transform.position.y - (height / 2), 0);
        GetComponent<BoxCollider2D>().offset = Vector2.zero;
        GetComponent<BoxCollider2D>().size = new Vector2(width, height);
    }
}
