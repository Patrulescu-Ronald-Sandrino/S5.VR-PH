using System;
using System.Collections.Generic;
using UnityEngine;

public class DetectionRangeTrigger : MonoBehaviour
{
    [Range(0f, 1f)] public float hitChance = 0.5f;
    
    public GameObject cannonBallPrefab;
    public GameObject cannonBallDefensePrefab;
    public GameObject cannon;
    public Transform firePoint;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(cannonBallPrefab.tag)) return;
        Debug.Log("DetectionRange was hit");

        if (!(UnityEngine.Random.Range(0f, 1f) <= hitChance)) return;
        Debug.Log("DetectionRange chance passed");

        // TODO: update firePoint & cannon to be directed at the collider?
        var target = other.transform.position;
        // cannon.transform.rotation = Quaternion.LookRotation(target - firePoint.position);
        cannon.transform.LookAt(target);
        
        Debug.Log(target);

        // TODO: apply the slider force to the cannonBall? or use a variable here?
        var canonBallDefenseRigidbody = Instantiate(cannonBallDefensePrefab, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>();
        var fireForce = cannon.transform.forward * 4000f;
        canonBallDefenseRigidbody.AddForce(fireForce);
    }
}
