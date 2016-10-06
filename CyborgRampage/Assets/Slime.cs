using UnityEngine;
using System.Collections;

public class Slime : MonoBehaviour {

    public int hp = 3;
    public int ATK = 5;

    public bool damageOnTouch = true;

    public bool isEnemy = true;

    public float damageAnimationLength = 0.25f;
    private float backToRegularColor;

    

    private DamageText _damageText;

    public void damage(int damageCount)
    {
        // Remove HP
        hp -= damageCount;

        // Blink red
        SpriteRenderer sr =  gameObject.GetComponent<SpriteRenderer>();
        sr.color = Color.red;


        // GUI Text damage
        // Vector2 damageBoxSize = new Vector2(20, 40);
        //Rect textPosition = new Rect(gameObject.transform.position, damageBoxSize);
        _damageText = gameObject.GetComponent<DamageText>();
        // Vector2 textPosition = gameObject.transform.position;
        //Vector2 textPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);
        Vector2 textPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        if (_damageText != null)
            _damageText.Display(textPosition, damageCount);


        // Final Resolution
        if (hp<=0)
            Destroy(gameObject);
        else
            backToRegularColor = damageAnimationLength;

    }

    void OnTriggerEnter2D(Collider2D c)
    {
        MissileController bullet = c.gameObject.GetComponent<MissileController>();
        if ( bullet != null )
        {
            if ( bullet.IsEnemyProjectile != isEnemy ) // FriendlyFire OFF
            {
                damage(bullet.damage);
                Destroy(bullet.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if  ( c.gameObject.name.Equals("PLAYER") )
        {
            PlayerController player = c.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                Vector2 playerPos = player.transform.position;
                Vector2 thisPos = transform.position;
                Vector2 repulseVector = thisPos - playerPos;
                repulseVector *= 100;

                if (damageOnTouch)
                    player.damage(ATK, repulseVector);
            }

        }
    }

	// Use this for initialization
	void Start () {
        backToRegularColor = 0f;
        _damageText = null;
    }
	
	// Update is called once per frame
	void Update () {

        if (_damageText != null)
        {
            //Vector2 textPosition = Camera.main.WorldToViewportPoint(gameObject.transform.position);
            Vector2 textPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            _damageText.UpdatePosition(textPosition);
        }


        if (backToRegularColor > 0f)
            backToRegularColor -= Time.deltaTime;
        else
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.color = Color.white;
      
        }
   
	}
}
