using UnityEngine;

public class BellCollisionEnterPlayAudioSource : MonoBehaviour
{
    public AudioSource audioSource;

    void OnCollisionEnter(Collision collision)
    {
        var gameObjectName = collision.gameObject.name;
        if (gameObjectName is "CannonBall(Clone)" or "Brick(Clone)" or "ClapperEnd")
        {
            audioSource.Play();
        }
    }
}