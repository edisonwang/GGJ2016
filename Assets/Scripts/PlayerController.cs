using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    bool isInDig = false;
    GameController gc;
    public GameObject startPoint;
    // Use this for initialization
    void Start()
    {
        GameController.instance.setPlayer(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (!isInDig)
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
                //iTween.MoveTo(Camera.main.gameObject, new Vector3(0, 0, 1.0f), 10.0f);
                // iTween.MoveTo(Camera.main.gameObject,iTween.Hash("z",0.1f, "time", 2.0f,"oncomplete", "StartDigScene","oncompletetarget",GameController.instance.gameObject));
                if (isGoToDig())
                {
                    GameController.instance.StartDigScene();
                    isInDig = true;
                }
                else
                {
                    turnDone();
                }
            }
        }
    }

    bool isClear(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(direction.x,direction.y,0), direction, 0.1f);
        if (hit.collider == null)
            return true;
        if (hit.collider.tag.StartsWith("Building"))
            return false;
        return true;
    }

    void turnDone()
    {
        GameController.instance.playerTurnDone();
    }

    void StartDig()
    {
        Camera.main.transform.Translate(new Vector3(40.0f, 0, 0.1f));
        iTween.MoveTo(Camera.main.gameObject, iTween.Hash("z", -10.0f, "time", 2.0f, "oncomplete", "turnDone", "oncompletetarget", this.gameObject));
    }

    public void BackToCell()
    {
        Debug.Log("Back to cell");
        transform.position = startPoint.transform.position;
        isInDig = false;
    }

    bool isGoToDig()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.1f);
        if (hit.collider == null)
            return false;
        if (hit.collider.tag.StartsWith("Scene"))
            return true;
        return false;
    }
}
