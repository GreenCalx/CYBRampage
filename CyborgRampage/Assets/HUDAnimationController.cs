using UnityEngine;
using System.Collections;

public class HUDAnimationController : MonoBehaviour {


    private float _randValue;
    private Animator _animator;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
        _animator.SetBool("Stutter", false);
    }
	
	// Update is called once per frame
	void Update () {

        // - Update animation vars -
        _randValue = Random.Range(0f, 100f);

        if (_randValue > 99f)
            _animator.SetBool("Stutter", true);
        else
            _animator.SetBool("Stutter", false);
        // ------------------

    }
}
