using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private PlayerController mPlayer;
    private List<GuardController> mGuards;

    private int turnCounter;

    // Use this for initialization
    void Start()
    {
        turnCounter = 0;
        mPlayer = null;
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

    public void setGuard(GuardController guard)
    {
        mGuards.Add(guard);
    }

    public void removeGuard(GuardController guard)
    {
        mGuards.Remove(guard);
    }

    public void playerTurnDone()
    {
        turnCounter++;
        Debug.Log("Player turn "+turnCounter+" Done!");
    }

}
