using UnityEngine;
using System.Collections;
using RAIN.Core;

public class LookDirectionTrigger : MonoBehaviour
{
    public Vector2 directionToLook;

    public void OnTriggerEnter(Collider other)
    {
        ForceLookDirection(other.gameObject);
    }

    public void OnTriggerStay(Collider other)
    {
        //ForceLookDirection(other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        other.GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem("directionOverride", false);
    }

    void ForceLookDirection(GameObject go)
    {
        if (go.tag == "Guard")
        {
            go.GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem("directionOverride", true);
            go.GetComponentInChildren<AIRig>().AI.WorkingMemory.SetItem("direction", directionToLook);
            go.GetComponentInChildren<Animator>().SetInteger("horizontal", (int)directionToLook.x);
            go.GetComponentInChildren<Animator>().SetInteger("vertical", (int)directionToLook.y);
        }
    }
}
