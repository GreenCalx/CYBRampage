using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D c)
    {
        MissileController bullet = c.gameObject.GetComponent<MissileController>();
        if (bullet != null)
        {
            Destroy(c.gameObject);
        }
    }
}
