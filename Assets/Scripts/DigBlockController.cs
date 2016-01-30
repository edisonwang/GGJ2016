using UnityEngine;
using System.Collections;

public class DigBlockController : MonoBehaviour
{
    public Sprite[] sprites;
    public Sprite bg;
    public float health = 4.0f;
    float initHealth;
    Color color;
    // Use this for initialization
    void Start()
    {
        initHealth = health;
        color = transform.GetComponent<SpriteRenderer>().color;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Dig()
    {

        health--;
        Debug.Log(transform.name + " Health: " + health);
        if (health > 0){
            transform.GetComponent<SpriteRenderer>().sprite = sprites[(int)health - 1];
        }else{
            transform.GetComponent<SpriteRenderer>().sprite = bg;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        
    }
}
