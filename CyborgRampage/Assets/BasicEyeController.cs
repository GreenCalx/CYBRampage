using UnityEngine;
using System.Collections;

public class BasicEyeController : Monster {

    private float _randValue;
    private Animator _animator;

    // Use this for initialization
    public override void Start()
    {
        base.Start();
        _animator = GetComponent<Animator>();
        _animator.SetBool("Blink", false);
        _sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    public override void Update()
    {
        _transformPosition = gameObject.transform.position;


        // - Update animation vars -
        _randValue = Random.Range(0f, 1f);

        if (_randValue > 0.99f)
            _animator.SetBool("Blink", true);
        else
            _animator.SetBool("Blink", false);
        // ------------------


        base.Update();
    }
}
