using UnityEngine;
using System.Collections;

public class DigBlockController : MonoBehaviour
{
    public Sprite[] sprites;
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
        if (health <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void Dig()
    {

        health--;
        Debug.Log(transform.name + " Health: " + health);
        if (health > 0)
            transform.GetComponent<SpriteRenderer>().sprite = sprites[(int)health - 1];
    }
}
