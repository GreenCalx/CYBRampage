using UnityEngine;
using System.Collections.Generic;

public class Slime : Monster
{

    public bool damageOnTouch = true;
    public float repulsionFactor = 200f;


    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.name.Equals("PLAYER"))
        {
            PlayerController player = c.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                Vector2 playerPos = player.transform.position;
                Vector2 thisPos = transform.position;
                Vector2 repulseVector = playerPos - thisPos ;
                repulseVector *= repulsionFactor;

                if (damageOnTouch)
                    player.damage(ATK, repulseVector);
            }
        }
    }

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public override void Update()
    {
        _transformPosition = gameObject.transform.position;
        //_sr = GetComponent<SpriteRenderer>();
        base.Update();
    }
}
