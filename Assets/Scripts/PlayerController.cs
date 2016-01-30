using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    Vector2 oldPosition;
    GameController gc;
    // Use this for initialization
    void Start()
    {
        gc = GameController.getInstance();
        // gc.setPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("up"))
        {
            if (isClear(Vector2.up))
            {
                this.transform.Translate(Vector2.up);
                turnDone();
            }
        }

        if (Input.GetKeyDown("down"))
        {
            if (isClear(Vector2.down))
            {
                this.transform.Translate(Vector2.down);
                turnDone();
            }
        }
        if (Input.GetKeyDown("left"))
        {
            if (isClear(Vector2.left))
            {
                this.transform.Translate(Vector2.left);
                turnDone();
            }
        }
        if (Input.GetKeyDown("right"))
        {
            if (isClear(Vector2.right))
            {
                this.transform.Translate(Vector2.right);
                turnDone();
            }
        }
        if (Input.GetKeyDown("space"))
        {
            turnDone();
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name.StartsWith("wall"))
        {
            Debug.Log("Hit the wall!!!!");
            transform.position = oldPosition;
        }
    }
    bool isClear(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1.1f);
        if (hit.collider != null)
            return false;
        return true;
    }

    void turnDone()
    {
        Debug.Log("Turn done!");
    }
}
