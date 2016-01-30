using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private PlayerController mPlayer;
    private PlayerDigController mDigPlayer;
    private List<GuardController> mGuards;

public GameObject DigScene;
    private int turnCounter;

    // Use this for initialization
    void Start()
    {
        turnCounter = 0;
        if (mGuards == null)
            mGuards = new List<GuardController>();
        mGuards.Clear();
    }

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPlayer(PlayerController player)
    {
        mPlayer = player;
        Debug.Log("Player set to " + mPlayer.transform.name);
    }
    
    public void setDigPlayer(PlayerDigController player)
    {
        mDigPlayer = player;
        Debug.Log("DigPlayer set to " + mDigPlayer.transform.name);
    }

    public void setGuard(GuardController guard)
    {
        mGuards.Add(guard);
        Debug.Log("Guard: "+guard.transform.name+" Added.");
    }

    public void removeGuard(GuardController guard)
    {
        mGuards.Remove(guard);
        Debug.Log("Guard: "+guard.transform.name+" Removed.");
    }

    public void playerTurnDone()
    {
        turnCounter++;
        Debug.Log("Player turn "+turnCounter+" Done!");
    }

    public void StartDigScene(){
       // Camera.main.transform.Translate(new Vector3(40.0f,0,0.1f));
        //iTween.MoveTo(Camera.main.gameObject,iTween.Hash("z",-10.0f, "time", 2.0f,"oncomplete", "startDigging","oncompletetarget",this.gameObject));
        DigScene.SetActive(true);
        mDigPlayer.startDigging();
        playerTurnDone();
    }
    void StopDigScene(){
        DigScene.SetActive(false);
        
        playerTurnDone();
        mPlayer.BackToCell();
    }

    public void BackToCell(){
        StopDigScene();
    }
}
