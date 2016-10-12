using UnityEngine;
using System.Collections;

public class BasicGun : MonoBehaviour {


    public Transform bulletTransformRef;
    public float fireRate ;

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

        Transform bulletTransform = Instantiate(bulletTransformRef) as Transform;

        // Set the bullet at gunpoint (only +width currently)
        bulletTransform.position = transform.position;
        PlayerArmController currentArm = GetComponentInParent<PlayerArmController>();
        Transform gunpoint = transform.Find("GunPoint");
        if (gunpoint != null)
            bulletTransform.position = gunpoint.position;

        // rotate the projectile to match arms's rotation
        bulletTransform.rotation = transform.rotation;

        MissileController bullet = bulletTransform.gameObject.GetComponent<MissileController>();
        if (bullet != null)
        {
            bullet.IsEnemyProjectile = isEnemy; 
        }

        //define direction
        MoveScript move = bulletTransform.gameObject.GetComponent<MoveScript>();
        if ( move!=null )
        {

            Vector3 sp = Camera.main.WorldToScreenPoint(bulletTransform.position);
            Vector3 dir = (Input.mousePosition - sp).normalized;
            move.direction = dir;
            _shootingDirection = dir;
        }

    }

    public Vector3 FindPointingDirection()
    {
        //Transform bulletTransform = Instantiate(bulletTransformRef) as Transform;
        //Vector3 sp = Camera.main.WorldToScreenPoint(bulletTransform.position);
        //Vector3 dir = (Input.mousePosition - sp).normalized;
        return _shootingDirection;
    }
}
