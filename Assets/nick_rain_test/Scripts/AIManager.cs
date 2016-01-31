using UnityEngine;
using System.Collections;
using RAIN.Core;

public class AIManager : MonoBehaviour {

    public RAIN.Entities.Entity guardEntity;
    public AIRig aiRig;
    public bool aiTurn;

    void Start()
    {
        //guardEntity.EntityInit();
        //aiRig.AIStart();
        // guardEntity.
        aiRig.AI.WorkingMemory.GetItem<bool>("aiTurn");
    }

    void Update()
    {
        //aiRig.AIUpdate();
        aiRig.AI.WorkingMemory.SetItem<bool>("aiTurn", aiTurn);
    }
}
