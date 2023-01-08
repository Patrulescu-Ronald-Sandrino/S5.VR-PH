using UnityEngine;
using UnityEngine.UI;

// unity simulate cannon shoot https://youtu.be/xHYmUGyCwQU
public class CannonManager : MonoBehaviour
{
    public GameObject cannonBallPrefab;
    public Transform firePoint;
    public LineRenderer lineRenderer;

    public Slider _slider;

    private const int N_TRAJECTORY_POINTS = 10;

    private Camera _cam;
    private bool _pressingMouse = false;

    private Vector3 _initialVelocity;
    private Vector3 _lookAt = new(0, 7, 0);

    private float _power;

    void Start()
    {
        _cam = Camera.main;
        transform.LookAt(_lookAt);
        // lineRenderer.positionCount = N_TRAJECTORY_POINTS;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _initialVelocity = _slider.value * Vector3.one;
            _pressingMouse = true;
            // lineRenderer.enabled = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _pressingMouse = false;
            // lineRenderer.enabled = false;
            _Fire();
            _slider.value = 0;
        }

        if (_pressingMouse)
        {
            //     
            //     // coordinate transform screen > world
            //     /* _cam.ScreenToWorldPoint(Input.mousePosition) is constant */
            //     var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 15));
            //     mousePos.z = 0;
            //
            //     // look at
            //     // transform.LookAt(mousePos);
            //
            //     _initialVelocity = mousePos - firePoint.position;
            //
            //     // _UpdateLineRenderer();

            
            _slider.value += Time.deltaTime;
        }

        var lookAtDelta = _GetLookAtDelta();
        if (lookAtDelta != Vector3.zero)
        {
            Debug.Log("lookAtDelta: " + lookAtDelta);
            _lookAt += lookAtDelta;
            Debug.Log("_lookAt: " + _lookAt);
            transform.LookAt(_lookAt);
        }
    }

    private static Vector3 _GetLookAtDelta()
    {
        if (Input.GetKey(KeyCode.UpArrow))
            return Vector3.up;
        if (Input.GetKey(KeyCode.DownArrow))
            return Vector3.down;
        if (Input.GetKey(KeyCode.LeftArrow))
            return Vector3.forward;
        if (Input.GetKey(KeyCode.RightArrow))
            return Vector3.back;
        return Vector3.zero;
    }

    private void _Fire()
    {
        // instantiate a cannonball
        var cannonBall = Instantiate(cannonBallPrefab, firePoint.position, Quaternion.identity);
        // apply some force
        var rb = cannonBall.GetComponent<Rigidbody>();
        rb.AddForce(_slider.value * 100f * (firePoint.position - transform.position), ForceMode.Impulse);
    }

    private void _UpdateLineRenderer()
    {
        var g = Physics.gravity.magnitude;
        var v = _initialVelocity.magnitude;
        var angle = Mathf.Atan2(_initialVelocity.y, _initialVelocity.x);

        var start = firePoint.position;

        const float timeStep = 0.1f;
        var fTime = 0.0f;
        for (var i = 0; i < N_TRAJECTORY_POINTS; i++)
        {
            var dx = v * Mathf.Cos(angle) * fTime;
            var dy = v * Mathf.Sin(angle) * fTime - 0.5f * g * fTime * fTime;
            var pos = new Vector3(dx, dy, 0) + start;
            lineRenderer.SetPosition(i, pos);
            fTime += timeStep;
        }
    }
}