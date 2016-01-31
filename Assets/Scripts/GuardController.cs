using UnityEngine;
using System.Collections;
using RAIN.Core;

public class GuardController : MonoBehaviour
{
    private AIRig ai;
    public bool Done
    {
        get { return done; }
    }
    private bool done = true;

    void Start()
    {
        GameController.instance.setGuard(this);
        ai = GetComponentInChildren<AIRig>();
    }

    public void ActivateTurn()
    {
        done = false;
        //Debug.Log(ai.AI.WorkingMemory.ItemExists("aiTurn"));
        ai.AI.WorkingMemory.SetItem("aiTurn", true);
    }

    void Update()
    {
        if (!done)
        {
            if (ai.AI.WorkingMemory.GetItem<bool>("aiTurn") == false)
            {
                done = true;
            }
        }
    }
}