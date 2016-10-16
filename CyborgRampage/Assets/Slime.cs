using UnityEngine;
using System.Collections.Generic;

public class Slime : MonoBehaviour
{

    public int hp = 3;
    public int ATK = 5;

    public bool damageOnTouch = true;

    public bool isEnemy = true;

    public float damageAnimationLength = 0.25f;
    private float backToRegularColor;



    public DamageText damageTextRef;
    private List<DamageText> _damageTexts;
    private SpriteRenderer _sr;




    public void Kill()
    {
        _damageTexts.Clear();
        Destroy(gameObject);
    }

    public void damage(int damageCount)
    {
        // Remove HP
        hp -= damageCount;

        // Blink red
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        sr.color = Color.red;


        // GUI Text damage
        // Vector2 damageBoxSize = new Vector2(20, 40);
        //Rect textPosition = new Rect(gameObject.transform.position, damageBoxSize);
        if (damageTextRef != null)
        {
            DamageText damageText = Instantiate(damageTextRef) as DamageText;
            _damageTexts.Add(damageText);

            Vector2 textPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
            if (damageText != null)
                damageText.Display(textPosition, damageCount);
        }

        // Final Resolution
        if (hp <= 0)
            Kill();
        else
            backToRegularColor = damageAnimationLength;

    }

    void OnTriggerEnter2D(Collider2D c)
    {
        MissileController bullet = c.gameObject.GetComponent<MissileController>();
        if (bullet != null)
        {
            if (bullet.IsEnemyProjectile != isEnemy) // FriendlyFire OFF
            {
                damage(bullet.damage);
                Destroy(bullet.gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name.Equals("PLAYER"))
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
    void Start()
    {
        backToRegularColor = 0f;
        _damageTexts = new List<DamageText>();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        // --- DAMAGE TEXT UPDATE -- //
        //
        _damageTexts.RemoveAll(item => item == null);
        int dtCount = _damageTexts.Count;
        if (dtCount > 0)
        {
            //Vector3 offset = new Vector3();
            //if (_sr != null)
            //    offset.y = _sr.sprite.rect.width;
            Vector2 textPosition = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        
            for (int i = dtCount - 1; i >= 0; --i)
            {
                DamageText dt = _damageTexts[i];
                dt.LastSeenPosition(textPosition);
            }

        } // !if
        // ----------------------- //

        if (backToRegularColor > 0f)
            backToRegularColor -= Time.deltaTime;
        else
        {
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            sr.color = Color.white;

        }

    }
}
