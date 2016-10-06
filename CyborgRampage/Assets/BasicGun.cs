using UnityEngine;
using System.Collections;

public class BasicGun : MonoBehaviour {


    public Transform bulletTransformRef;

    public float fireRate = 0.25f;
    private float fireCooldown;    // internal cd

	// Use this for initialization
	void Start () {
        fireCooldown = 0f;
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

        bulletTransform.position = transform.position;

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
            move.direction = transform.forward;
            move.direction = dir;
        }

    }
}
