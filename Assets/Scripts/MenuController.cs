using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{

    public GameObject[] selections;
    int pointer;
    public GameObject selectionMask;
    public Sprite start;
    public Sprite resume;
    private bool isShowing = true;

    // Use this for initialization
    void Start()
    {
        GameController.instance.setMenuCOntroller(this);
        pointer = 0;
        selectionMask.transform.position = selections[pointer].transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if (isShowing)
        {


            if (Input.GetKeyDown("left"))
            {
                if (pointer > 0)
                {
                    pointer--;
                    selectionMask.transform.position = selections[pointer].transform.position;

                }
                else
                {
                    pointer = selections.Length - 1;
                    selectionMask.transform.position = selections[pointer].transform.position;

                }
                Debug.Log("Pointer: " + pointer);
            }
            if (Input.GetKeyDown("right"))
            {
                if (pointer < selections.Length - 1)
                {
                    pointer++;
                    selectionMask.transform.position = selections[pointer].transform.position;

                }
                else
                {
                    pointer = 0;
                    selectionMask.transform.position = selections[pointer].transform.position;

                }
            }
            if (Input.GetKeyDown("space"))
            {
                switch (pointer)
                {
                    case 0:
                        PlayGame();
                        break;
                    case 1:
                        Application.Quit();
                        break;
                    default:
                        return;

                }
            }
            if (Input.GetKeyDown("escape"))
            {
                if (GameController.instance.isStarted())
                {
                    PlayGame();
                    //GameController.instance.restartGame();
                }
            }
        }
    }

    void PlayGame()
    {
        GameController.instance.setRunning(true);
        gameObject.SetActive(false);
        changeToResume();
    }
    
    public void changeToResume(){
        selections[0].GetComponent<SpriteRenderer>().sprite = resume;
    }
}
