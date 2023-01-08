// using UnityEngine;
//
// // unity simulate cannon shoot https://youtu.be/xHYmUGyCwQU
// public class CannonManager2D : MonoBehaviour
// {
//     public GameObject cannonBallPrefab;
//     public Transform firePoint;
//     public LineRenderer lineRenderer;
//
//     private const int N_TRAJECTORY_POINTS = 10;
//
//     private Camera _cam;
//     private bool _pressingMouse = false;
//
//     private Vector3 _initialVelocity;
//     private Vector3 _lookAt;
//
//     void Start()
//     {
//         _cam = Camera.main;
//
//         lineRenderer.positionCount = N_TRAJECTORY_POINTS;
//     }
//
//     void Update()
//     {
//         if (Input.GetMouseButtonDown(0))
//         {
//             _pressingMouse = true;
//             lineRenderer.enabled = true;
//         }
//
//         if (Input.GetMouseButtonUp(0))
//         {
//             _pressingMouse = false;
//             lineRenderer.enabled = false;
//             _Fire();
//         }
//
//         if (_pressingMouse)
//         {
//             
//             // coordinate transform screen > world
//             /* _cam.ScreenToWorldPoint(Input.mousePosition) is constant */
//             var mousePos = _cam.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 15));
//             mousePos.z = 0;
//
//             // look at
//             transform.LookAt(mousePos);
//
//             _initialVelocity = mousePos - firePoint.position;
//
//             _UpdateLineRenderer();
//         }
//     }
//
//     private void _Fire()
//     {
//         // instantiate a cannonball
//         var cannonBall = Instantiate(cannonBallPrefab, firePoint.position, Quaternion.identity);
//         // apply some force
//         var rb = cannonBall.GetComponent<Rigidbody>();
//         Debug.Log("initial velocity: " + _initialVelocity);
//         rb.AddForce(_initialVelocity, ForceMode.Impulse);
//     }
//
//     private void _UpdateLineRenderer()
//     {
//         var g = Physics.gravity.magnitude;
//         var v = _initialVelocity.magnitude;
//         var angle = Mathf.Atan2(_initialVelocity.y, _initialVelocity.x);
//         
//         var start = firePoint.position;
//         
//         const float timeStep = 0.1f;
//         var fTime = 0.0f;
//         for (var i = 0; i < N_TRAJECTORY_POINTS; i++)
//         {
//             var dx = v * Mathf.Cos(angle) * fTime;
//             var dy = v * Mathf.Sin(angle) * fTime - 0.5f * g * fTime * fTime;
//             var pos = new Vector3(dx, dy, 0) + start;
//             lineRenderer.SetPosition(i, pos);
//             fTime += timeStep;
//         }
//
//     }
// }