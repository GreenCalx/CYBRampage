using UnityEngine;
using System.Collections.Generic;

public class RadialMenu : MonoBehaviour {

    private struct Zone
    {
        public Zone(float pmin, float pmax) { min = pmin; max = pmax; }
        public float min, max;
    } ;

    private Vector3         _position;
    private float           _radius;
    private float           _zoneSize; // degree
    private List<Zone>     _listOfZones;

    // Constructor
    void Awake()
    {
        _radius = 0f;
        

    }


    public RadialMenu CreateMenu(float radius,int nZones)
    {

        _radius = radius;

        _zoneSize = 360f / nZones;
        _listOfZones = new List<Zone>();
        for (int iZone = 0; iZone < nZones; ++iZone)
        {
            Zone zone = new Zone(iZone*_zoneSize, (iZone+1)*_zoneSize);
            _listOfZones.Add(zone);
        }





        return this;
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
