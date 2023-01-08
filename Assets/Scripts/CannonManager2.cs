using UnityEngine;
using UnityEngine.UI;

public class CannonManager2 : MonoBehaviour
{
    public float rotationSpeed = 1;
    public float fireForceConstant = 50.0f;

    public GameObject cannonBallPrefab;
    public Transform firePoint;
    public Slider slider;

    private bool _pressingMouse;

    void Start()
    {
        
    }

    void Update()
    {
        _UpdateCannonOrientation();
        _UpdateMouseShooting();
    }

    private void _UpdateCannonOrientation()
    {
        var horizontalRotation = Input.GetAxis("Horizontal");
        var verticalRotation = Input.GetAxis("Vertical");

        var rotationDelta = new Vector3(-verticalRotation * rotationSpeed, horizontalRotation * rotationSpeed, 0);
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + rotationDelta);
    }

    private void _UpdateMouseShooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pressingMouse = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _pressingMouse = false;
            _Fire();
        }

        if (_pressingMouse)
        {
            slider.value += Time.deltaTime;
        }
    }

    private void _Fire()
    {
        var cannonBallRigidbody = Instantiate(cannonBallPrefab, firePoint.position, firePoint.rotation).GetComponent<Rigidbody>();
        var fireForce = firePoint.forward * (slider.value * fireForceConstant);
        
        // cannonBallRigidbody.velocity = fireForce;
        /* or */
        cannonBallRigidbody.AddForce(fireForce, ForceMode.Impulse);
        
        slider.value = 0;
    }
}
