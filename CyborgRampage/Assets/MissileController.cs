using UnityEngine;
using System.Collections;

public class MissileController : MonoBehaviour {

    public int damage;
    public float damage_range_low = 1f;
    public float damage_range_max = 5f;
    public float lifeTime = 5f;
    private Rigidbody2D _rb2d;

    public bool IsEnemyProjectile = false;

	// Use this for initialization
	void Start ()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        Destroy(gameObject, lifeTime);
    }
	
	// Update is called once per frame
	void Update ()
    {
        damage = (int)Random.Range(damage_range_low, damage_range_max);
    }

    // Colliding with an other object
    void OnCollisionEnter2D(Collision2D c)
    {
        Destroy(gameObject); // shouldn't be called
    }
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.name == this.name) return; // ignore collision with self
        //else Destroy(gameObject);
    }
}
