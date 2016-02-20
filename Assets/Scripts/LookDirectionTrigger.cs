using UnityEngine;
using System.Collections;

public class LookDirectionTrigger : MonoBehaviour
{
    public Vector2 directionToLook;

    public void OnTriggerEnter(Collider other)
    {
        ForceLookDirection(other.gameObject);
    }

    public void OnTriggerStay(Collider other)
    {
        ForceLookDirection(other.gameObject);
    }

    void ForceLookDirection(GameObject go)
    {
        if (go.tag == "Guard")
        {
            go.GetComponent<Animator>().SetInteger("horizontal", (int)directionToLook.x);
            go.GetComponent<Animator>().SetInteger("vertical", (int)directionToLook.y);
        }
    }
}
