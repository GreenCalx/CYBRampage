using UnityEngine;
using System.Collections.Generic;

public class DiagHobo : MonoBehaviour {

    public int DialogFlag = 0 ;
    private int ProximityFlag;
    

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.name.Equals("PLAYER"))
        {
            // we near enought of the madafaka
            ProximityFlag = 1;            
        }
    }
    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.name.Equals("PLAYER"))
        {
            // we near enought of the madafaka
            ProximityFlag = 1;
        }
    }
    void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.name.Equals("PLAYER"))
        {
            // we going to far of the madafaka
            ProximityFlag = 0;
        }
    }


    // Use this for initialization
    void Start () {
        DialogFlag = 0;
	}
	
	// Update is called once per frame
	void Update () {
        DialogFlag = 0;
        if (Input.GetKey(KeyCode.E))
        {
            if (ProximityFlag == 1)
            {
                // we can now speak to the madafaka
                DialogFlag = 1;
              
            }
        }
    }
}
