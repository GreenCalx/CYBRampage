using UnityEngine;
using System.Collections.Generic;

public class Monster : MonoBehaviour
{

    public int hp;
    public int ATK;

    public float damageAnimationLength = 0.25f;
    private float backToRegularColor;
    

    public bool isEnemy = true;

    public DamageText _damageTextRef;
    private List<DamageText> _damageTexts;
    protected SpriteRenderer _sr;

    protected Vector3 _transformPosition;

    // Use this for initialization
    public virtual void Start()
    {
        backToRegularColor = 0f;
        _damageTexts = new List<DamageText>();

    }

    // Update is called once per frame
    public virtual void Update()
    {
        // --- DAMAGE TEXT UPDATE -- //
        //s
        _damageTexts.RemoveAll(item => item == null);
        int dtCount = _damageTexts.Count;
        if (dtCount > 0)
        {
            //Vector3 offset = new Vector3();
            //if (_sr != null)
            //    offset.y = _sr.sprite.rect.width;
            Vector2 textPosition = Camera.main.WorldToScreenPoint(_transformPosition);

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
            //SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
            _sr.color = Color.white;
        }
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

    public void damage(int damageCount)
    {
        // Remove HP
        hp -= damageCount;

        // Blink red
        //SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        _sr.color = Color.red;


        // GUI Text damage
        if (_damageTextRef != null)
        {
            DamageText damageText = Instantiate(_damageTextRef) as DamageText;
            _damageTexts.Add(damageText);

            Vector2 textPosition = Camera.main.WorldToScreenPoint(_transformPosition);
            if (damageText != null)
                damageText.Display(textPosition, damageCount);
        }

        // Final Resolution
        if (hp <= 0)
            Kill();
        else
            backToRegularColor = damageAnimationLength;

    }

    public void Kill()
    {
        _damageTexts.Clear();
        Destroy(gameObject);
    }

}
