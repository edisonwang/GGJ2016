using UnityEngine;
using System.Collections;

public class AudioTrigger : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (audioSource != null && other.tag == "Guard")
        {
            audioSource.Play();
        }
    }

}
