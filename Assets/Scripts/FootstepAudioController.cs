using UnityEngine;

public class FootstepAudioController : MonoBehaviour
{
    public AudioClip[] footstepAudio;
    private AudioSource audioSource;
    private int idx;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayFootstep()
    {
        audioSource.PlayOneShot(footstepAudio[idx]);
        idx = (idx + 1) % footstepAudio.Length;
    }
}
