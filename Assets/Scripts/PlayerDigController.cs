using UnityEngine;
using System.Collections;

public class PlayerDigController : MonoBehaviour
{
    private bool isInCell = true;
    public GameObject startPoint;
    // Use this for initialization
    void Start()
    {
        GameController.instance.setDigPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.instance.isRunning() || !GameController.instance.isGuardTurnDone()){
            return;
        }
        if (!isInCell)
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
                if (isBackToCell())
                {
                    isInCell = true;
                    GameController.instance.BackToCell();
                }


            }
        }
    }
    void turnDone()
    {
        GameController.instance.playerTurnDone();
    }

    public void startDigging()
    {
        Debug.Log("Start Digging");
        transform.position = startPoint.transform.position;
        isInCell = false;
    }
    bool isClear(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(direction.x,direction.y,0), direction, 0.1f);
        if (hit.collider == null)
            return true;
        if(hit.collider.tag.StartsWith("Win")){
            GameController.instance.Win();
        }
        if (hit.collider.tag.StartsWith("Building")){
            if(hit.collider.name.StartsWith("dig")){
                hit.collider.gameObject.GetComponent<DigBlockController>().Dig();
            }
            return false;
        }
        return true;
    }

    bool isBackToCell()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 0.1f);
        if (hit.collider == null)
            return false;
        if (hit.collider.transform.tag.StartsWith("Scene"))
            return true;
        return false;
    }
}
