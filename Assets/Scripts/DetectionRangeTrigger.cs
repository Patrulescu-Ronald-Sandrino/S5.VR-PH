using System;
using UnityEngine;

public class DetectionRangeTrigger : MonoBehaviour
{
    [Range(0f, 1f)] public float hitChance = 0.5f;
    public float fireForce = 1000f;
    public bool isFireForceSameAsTarget = true;
    public float time = 1f;
    
    public GameObject cannonBallPrefab;
    public GameObject cannonBallDefensePrefab;
    public GameObject cannon;
    public Transform firePoint;
    
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(cannonBallPrefab.tag)) return;
        Debug.Log("DetectionRange was hit");

        if (!(UnityEngine.Random.Range(0f, 1f) <= hitChance))
        {
            Debug.Log("DetectionRange chance failed");
            return;
        }
        Debug.Log("DetectionRange chance passed");

        // TODO: update firePoint & cannon to be directed at the collider?
        var collisionPosition = other.transform.position;
        var velocity = other.transform.GetComponent<Rigidbody>().velocity;
        
        var target = collisionPosition;
        target = collisionPosition + velocity * time + 0.5f * Physics.gravity * time * time;
        
        // cannon.transform.rotation = Quaternion.LookRotation(target - firePoint.position);
        cannon.transform.LookAt(target);

        // TODO: apply the slider force to the cannonBall? or use a variable here?
        var canonBallDefenseRigidbody = Instantiate(cannonBallDefensePrefab, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>();
        if (isFireForceSameAsTarget)
        {
            fireForce = velocity.magnitude;
        }
        fireForce = (1 / time * (collisionPosition - firePoint.position) + velocity).magnitude;
        canonBallDefenseRigidbody.AddForce(cannon.transform.forward * fireForce);
    }
}
