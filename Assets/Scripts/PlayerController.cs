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
        if(!GameController.instance.isRunning()){
            return;
        }
        if (!isInDig)
        {
            Debug.DrawRay(transform.position, Vector3.forward);
            if (Input.GetKeyDown("up"))
            {
                if (isClear(Vector3.forward))
                {
                    this.transform.Translate(Vector3.forward);
                    turnDone();
                }
            }

            if (Input.GetKeyDown("down"))
            {
                if (isClear(Vector3.back))
                {
                    this.transform.Translate(Vector3.back);
                    turnDone();
                }
            }
            if (Input.GetKeyDown("left"))
            {
                if (isClear(Vector3.left))
                {
                    this.transform.Translate(Vector3.left);
                    turnDone();
                }
            }
            if (Input.GetKeyDown("right"))
            {
                if (isClear(Vector3.right))
                {
                    this.transform.Translate(Vector3.right);
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

    bool isClear(Vector3 direction)
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, direction);
        Physics.Raycast(transform.position, direction, out hit, 0.9f);
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
        RaycastHit hit;
        Physics.Raycast(transform.position, Vector3.forward, out hit, 0.5f);
        if (hit.collider == null)
            return false;
        if (hit.collider.tag.StartsWith("Scene"))
            return true;
        return false;
    }
}
