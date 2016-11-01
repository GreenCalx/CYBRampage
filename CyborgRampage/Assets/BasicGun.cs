using UnityEngine;
using System.Collections;

public class BasicGun : MonoBehaviour {


    public Transform bulletTransformRef;
    public float fireRate ;
    public float fireStrength;
    public string name = "";

    public Vector2 parentSpeed;

    private float fireCooldown;    // internal cd
    private Vector3 _shootingDirection;

	// Use this for initialization
	void Start () {
        _shootingDirection = new Vector2(-1, 0);
        fireCooldown = 0f;
	}

    public Vector2 GetShootingDirection()
    {
        return _shootingDirection;
    }
	
	// Update is called once per frame
	void Update () {

        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
	}

    public void Fire(bool isEnemy)
    {
        if (fireCooldown <= 0f)
        {
            fireCooldown = fireRate;
        }
        else
            return;

        Transform bulletTransform = Instantiate(bulletTransformRef) as Transform;

        // Set the bullet at gunpoint (only +width currently)
        Transform gunpoint = transform.Find("GunPoint");
        if (gunpoint != null)
            bulletTransform.position = gunpoint.position;
        else
            bulletTransform.position = transform.position;

        // rotate the projectile to match arms's rotation
        bulletTransform.rotation = transform.rotation;

        MissileController bullet = bulletTransform.gameObject.GetComponent<MissileController>();
        Rigidbody2D bulletRB2D = bulletTransform.gameObject.GetComponent<Rigidbody2D>();
        if (bullet != null)
        {
            bullet.IsEnemyProjectile = isEnemy;
        }

        //define direction
        MoveScript move = bulletTransform.gameObject.GetComponent<MoveScript>();
        if ( move!=null )
        {
            // Set bullet's direction to the gunpoint forward vector
            Vector3 dir = gunpoint.forward;
            if (bulletRB2D)
            {
                move.direction = dir * fireStrength;
                _shootingDirection = dir; // ?
            }
            else
            {

                move.direction = dir * fireStrength;
                _shootingDirection = dir; // ?
            }

        }

        // Add parent velocity
        move.direction += parentSpeed;

    }

    public Vector3 FindPointingDirection()
    {
        //Transform bulletTransform = Instantiate(bulletTransformRef) as Transform;
        //Vector3 sp = Camera.main.WorldToScreenPoint(bulletTransform.position);
        //Vector3 dir = (Input.mousePosition - sp).normalized;
        return _shootingDirection;
    }
}
