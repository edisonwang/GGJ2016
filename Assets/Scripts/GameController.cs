using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    private  static GameController _instance;
    private PlayerController mPlayer;
    private List<GuardController> mGuards;

	// Use this for initialization
	void Start () {
        if (_instance == null){
            initGameController();
            _instance = this;
            }
	}
    
    private void initGameController(){
        mPlayer = null;
        if(mGuards == null)
            mGuards = new List<GuardController>();
        mGuards.Clear();
    }
	
    public static GameController getInstance(){
        return _instance;
    }
    
	// Update is called once per frame
	void Update () {
	
	}
    
    public void setPlayer(PlayerController player){
        mPlayer = player;
    }
    
    public void setGuard(GuardController guard){
        mGuards.Add(guard);
    }
    
    public void removeGuard(GuardController guard){
        mGuards.Remove(guard);
    }
    
    
}
