using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    bool isInDig = false;
    bool isOnTrgger = false;
    GameController gc;
    public GameObject startPoint;
    public GameObject arrow;
    
    Vector3 bedPosition;
    public GameObject Bed;
    // Use this for initialization
    void Start()
    {
        GameController.instance.setPlayer(this);
        arrow.SetActive(false);
    }
    
    // Update is called once per frame
    void Update()
    {
        if(!GameController.instance.isRunning() || !GameController.instance.isGuardTurnDone()){
            return;
        }
        if (!isInDig)
        {
            Debug.DrawRay(transform.position, Vector3.forward);
            /*if (Input.mouseScrollDelta.magnitude > 0)
            {
                turnDone();
            }*/
            if (Input.GetKeyDown("up"))
            {
                if (isClear(Vector3.forward))
                {
                    this.transform.Translate(Vector3.forward);
                    isOnTrgger = isGoToDig();
                    turnDone();
                    return;
                }
            }

            if (Input.GetKeyDown("down"))
            {
                if (isClear(Vector3.back))
                {
                    this.transform.Translate(Vector3.back);
                    isOnTrgger = isGoToDig();
                    turnDone();
                    return;
                }
            }
            if (Input.GetKeyDown("left"))
            {
                if (isClear(Vector3.left))
                {
                    this.transform.Translate(Vector3.left);
                    isOnTrgger = isGoToDig();
                    turnDone();
                }
                return;
            }
            if (Input.GetKeyDown("right"))
            {
                if (isClear(Vector3.right))
                {
                    this.transform.Translate(Vector3.right);
                    isOnTrgger = isGoToDig();
                    turnDone();
                }
                return;
            }
            if (Input.GetKeyDown("space"))
            {
                //iTween.MoveTo(Camera.main.gameObject, new Vector3(0, 0, 1.0f), 10.0f);
                // iTween.MoveTo(Camera.main.gameObject,iTween.Hash("z",0.1f, "time", 2.0f,"oncomplete", "StartDigScene","oncompletetarget",GameController.instance.gameObject));
                if (isGoToDig())
                {
                    bedPosition = Bed.transform.position;
                    iTween.MoveTo(Bed,iTween.Hash("z",bedPosition.z + 1.0f, "time", 1.0f,"oncomplete", "goDig","oncompletetarget",this.gameObject));
                    isInDig = true;
                    GetComponent<AudioSource>().Play();
                }
                else
                {
                    turnDone();
                }
                return;
            }
        }
        arrow.SetActive(isOnTrgger);
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
        //iTween.MoveTo(Camera.main.gameObject, iTween.Hash("z", -10.0f, "time", 1.0f, "oncomplete", "turnDone", "oncompletetarget", this.gameObject));
    }

    public void BackToCell()
    {
      
        transform.position = startPoint.transform.position;
        iTween.MoveTo(Bed,iTween.Hash("z",bedPosition.z, "time", 1.0f,"oncomplete", "comeback","oncompletetarget",this.gameObject));
           Debug.Log("Back to cell");
        GetComponent<AudioSource>().Play();

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
    
    void goDig(){
        this.gameObject.SetActive(false);
        GameController.instance.StartDigScene();
        //Bed.gameObject.transform.position = bedPosition;
    }
    
    void comeback(){
        isInDig = false;
        isOnTrgger = false;
        
        Debug.Log("Back!!!!!");
    }
    
}
