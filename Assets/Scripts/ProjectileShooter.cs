using UnityEngine;

public class ProjectileShooter : MonoBehaviour
{
    public Rigidbody projectile;
    public float projectileSpeed = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var transform1 = transform;
            var newProjectile = Instantiate(projectile, transform1.position, transform1.rotation);
            newProjectile.velocity = transform.forward * projectileSpeed;
        }
    }
}
