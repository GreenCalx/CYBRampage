using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    // 0 : no knockback |  1 : full knockback
    public const float REPULSION_ATTENUATION = 1.0f;

    public float maxSpeed = 10f;

    private bool        _fFlip = false;
    private Rigidbody2D _rb2d;
    private Animator    _animator;
    private Animator    _HUDAnimator;
    private PlayerArmController _arm;

    private bool _desyncArms = false;

    // -- MISSILE -- 
    public KeyCode FIRE;
    public KeyCode SECONDARY_FIRE;
    public GameObject missile;

    // -- Various datas --
    private bool playerFacingRight = true;
    private bool playerFacingUp = false;
    private bool playerFacingDown = false;

    // -- RPG datas --
    public int health = 180;
    public int maxHealth = 200;
    private bool _isRepulsed = false;

    // -- GUI datas --
    

    // -- Combat datas --
    private int     _invulnerabilityFrames  = 15;
    private int     _invulnerabilityCounter = 0;
    private bool    _isInvincible           = false;


    // Use this to kill the player
    void Kill()
    {
        Destroy(gameObject);
        // NEED TO RECOVER CAMERA !!
    }

    // Use this for initialization
    void Start () {

        _rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _arm = GetComponentInChildren<PlayerArmController>();

        // Get HUD
        GameObject hud = GameObject.Find("HUD");
        _HUDAnimator = hud.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        _desyncArms = (Input.GetKey(FIRE) && Input.GetKey(SECONDARY_FIRE));
        if (_desyncArms)
        {
            PlayerArmController[] l = GetComponentsInChildren<PlayerArmController>();
            for (int i_arm = 0; i_arm < l.Length; ++i_arm)
            {
                PlayerArmController currentArm = l[i_arm];
                if (currentArm == null)
                    continue;
                if (currentArm.isOppositeArm)
                    currentArm.zAngleOffset = 180f;
            }

        }
            

        if (Input.GetKey(FIRE))
        {
            BasicGun[] guns = GetComponentsInChildren<BasicGun>();
            if (guns[0] != null)
            {
                guns[0].Fire(false);
            }
            if (guns[1] != null)
            { 
                guns[1].Fire(false);
            }
        }

        // Conditions upon eventual transformations
        if (_rb2d.velocity.x < 0f && playerFacingRight && Input.GetKey(KeyCode.LeftArrow))
        {
            _fFlip = true;
            playerFacingRight = false;
        }
        else if (_rb2d.velocity.x > 0f && !playerFacingRight && Input.GetKey(KeyCode.RightArrow))
        {
            _fFlip = true;
            playerFacingRight = true;
        }


    }


    void FixedUpdate()
    {
        float xmove = Input.GetAxis("Horizontal");
        float ymove = Input.GetAxis("Vertical");

        if (!_isRepulsed)
            _rb2d.velocity = new Vector2(xmove * maxSpeed, ymove * maxSpeed);
        else
            _isRepulsed = false;

        // Operate transforms if needed
        //if (_fFlip)
            //Flip();

        // Feed the animator
        float speed = Mathf.Sqrt(Mathf.Pow(xmove, 2) + Mathf.Pow(ymove, 2));
        _animator.SetFloat("Speed", speed);

        // Resolve invulnerability
        if (++_invulnerabilityCounter >= _invulnerabilityFrames)
        {
            _isInvincible = false;
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.color = Color.white;
        }

        //Update arm rotation
        if (_arm != null)
        {
            //BasicGun gun = GetComponent<BasicGun>();
            Transform armTransform = _arm.transform;
            Vector3 sp = Camera.main.WorldToScreenPoint(armTransform.position);
            Vector3 dir = (Input.mousePosition - sp).normalized;
            _arm.UpdateShootingDirection(dir);

        }


    }


    //Flip the image horizontally
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        _fFlip = false;
    }


    public void damage(int damageCount, Vector2 repulseVector)
    {

        if (_isInvincible)
            return;

        // Remove HP
        health -= damageCount;

        // Blink red
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.color = Color.red;
        _isInvincible = true;
        _invulnerabilityCounter = 0;

        // Animate HUD
        if (_HUDAnimator)
            _HUDAnimator.SetBool("Hurt", true);

        // Final Resolution
        if (health <= 0)
            Kill();

        // Apply repulsion
        // _rb2d.velocity = new Vector2(repulseVector.x * maxSpeed, repulseVector.y * maxSpeed);
        _rb2d.velocity = repulseVector * REPULSION_ATTENUATION;
        _isRepulsed = true;


    }

    void OnTriggerEnter2D(Collider2D c)
    {
        MissileController bullet = c.gameObject.GetComponent<MissileController>();
        if (bullet != null)
        {
            if (bullet.IsEnemyProjectile) // FriendlyFire OFF
            {
                Vector2 repulsionVector = new Vector2(0f, 0f);
                _rb2d.velocity -= bullet._rb2d.velocity ;
                damage(bullet.damage, repulsionVector);
                Destroy(bullet.gameObject);
            }
        }
    }


}
