 using UnityEngine;
using System.Collections;

public class Hobo : MonoBehaviour {

    private float _randValue;
    private Animator _animator;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
        _animator.SetBool("HeatTheHobo", false);
    }
	
	// Update is called once per frame
	void Update () {
        
        _randValue = Random.Range(0f, 1f);

        if (_randValue > 0.9f)
            _animator.SetBool("HeatTheHobo", true );
        else
            _animator.SetBool("HeatTheHobo", false);
    }
}
