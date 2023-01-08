using UnityEngine;

public class BellCollisionEnterPlayAudioSource : MonoBehaviour
{
    public AudioSource audioSource;

    private void OnCollisionEnter(Collision collision)
    {
        audioSource.Play();
    }
}