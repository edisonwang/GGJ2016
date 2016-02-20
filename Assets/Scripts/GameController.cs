using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private PlayerController mPlayer;
    private PlayerDigController mDigPlayer;
    private List<GuardController> mGuards;
    private MenuController mMenuController;
    public Text StepCounter;
    public Text WinningCounter;
    bool mIsRunning = false;
    bool mIsStarted = false;
    bool isGuardTurn = false;

    public GameObject DigScene;
    public GameObject MenuScreen;
    private int turnCounter;
    public bool isDigging = false;
    
    public GameObject Winning;
    public GameObject Losing;
    
    private bool isWinning = false;
    private bool isLosing = false;

    // Use this for initialization
    void Start()
    {

        turnCounter = 0;
        if (mGuards == null)
            mGuards = new List<GuardController>();
        mIsRunning = false;
        MenuScreen.SetActive(true);
        DigScene.gameObject.SetActive(false);
        isDigging = false;
        Winning.SetActive(false);
        Losing.SetActive(false);
        //SceneManager.LoadScene(2,LoadSceneMode.Additive);
       
    }

    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {   
        if(isRunning()){
            if(Input.GetKeyDown("escape")){
                mMenuController.gameObject.SetActive(true);
                mIsRunning = false;
            }
            UpdateGuardStatus();
        }
        StepCounter.gameObject.SetActive(mIsRunning);
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
        if (mGuards == null)
        {
            mGuards = new List<GuardController>();
        }
        mGuards.Add(guard);
        Debug.Log("Guard: "+guard.transform.name+" Added.");
    }
    public void setMenuCOntroller(MenuController mc){
        mMenuController = mc;
    }

    public void removeGuard(GuardController guard)
    {
        mGuards.Remove(guard);
        Debug.Log("Guard: "+guard.transform.name+" Removed.");
    }

    public void playerTurnDone()
    {
        turnCounter++;
        StepCounter.text = turnCounter.ToString();
        Debug.Log("Player turn "+turnCounter+" Done!");
        GuardTurn();
    }

    public void StartDigScene(){
       // Camera.main.transform.Translate(new Vector3(40.0f,0,0.1f));
        //iTween.MoveTo(Camera.main.gameObject,iTween.Hash("z",-10.0f, "time", 2.0f,"oncomplete", "startDigging","oncompletetarget",this.gameObject));
        DigScene.SetActive(true);
        mDigPlayer.startDigging();
        isDigging = true;
        playerTurnDone();
    }
    void StopDigScene(){
        DigScene.SetActive(false);
        
        playerTurnDone();
        mPlayer.gameObject.SetActive(true);
        mPlayer.BackToCell();
        
        isDigging = false;
    }

    public void BackToCell(){
        StopDigScene();
    }
    
    public void setRunning(bool flag){
        mIsRunning = flag;
        if (flag == true){
            mIsStarted = true;
            
        }
        
    }
    
    public bool isRunning(){
        return mIsRunning;
    }
    
    public bool isStarted(){
        return mIsStarted;
    }
    
    public void Win(){
        Debug.Log("Win!!!!!!!");
        DigScene.SetActive(false);
        //Winning.SetActive(true);
        mIsRunning = false;
        isWinning = true;
        mMenuController.gameObject.SetActive(true);
        WinningCounter.text = turnCounter.ToString();
        WinningCounter.gameObject.SetActive(true);
    }
    
    public void Lose(){
        
        Debug.Log("Lose!!!!!!!");
        DigScene.SetActive(false);
        //Losing.SetActive(true);
        mIsRunning = false;
        isLosing = true;
        mMenuController.gameObject.SetActive(true);
        
    }
    public void GuardTurnDone(){
        isGuardTurn = false;
        //Debug.Log("Guard Turn Done.");
    }
    
    public void GuardTurn(){
        isGuardTurn = true;
        for (int i = 0; i < mGuards.Count; i++)
        {
            mGuards[i].ActivateTurn();
        }
    }

    public void UpdateGuardStatus()
    {
        for (int i = 0; i < mGuards.Count; i++)
        {
            if (!mGuards[i].Done)
            {
                return;
            }
        }
        GuardTurnDone();
    }
    
    public bool isGuardTurnDone(){
        return !isGuardTurn;
    }
    
    public void restartGame(){
        SceneManager.LoadScene(0);
    }
    
    public bool isWin(){
        return isWinning;
    }
    public bool isLose(){
        return isLosing;
    }
}
