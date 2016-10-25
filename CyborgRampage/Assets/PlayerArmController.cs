using UnityEngine;
using System.Collections;

public class PlayerArmController : MonoBehaviour {

    public Vector2 baseVector = new Vector2(1,0);

    private Vector2 _stickPoint;
    private Vector2 _directionVector;

    private float _xAngle = 0f, _yAngle = 0f, _zAngle = 0f ;

    private bool _flip = false;
    private bool _flipped = false;
    private bool switchedSide = false;

    private SpriteRenderer _sr;

    // -- BEHAVIOUR MODIFICATIONS --
    public bool _invert;  // invert rotation angle 

    public object st { get; private set; }

    // Use this for initialization
    void Start () {
        _sr = GetComponent<SpriteRenderer>();

        _directionVector = baseVector;

        //Stick point is the middle of the right as default
        _stickPoint = _sr.sprite.pivot;

        if (_invert)
            _flip = true;
    }
	
    public void UpdateShootingDirection(Vector2 directionVector)
    {

        _directionVector = directionVector;
    }

	// Update is called once per frame
	void Update ()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        _zAngle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        if (((_zAngle > 90) || (_zAngle < -90)))
            if (!switchedSide)
            {
                _flip = true; _flipped = false;
                switchedSide = true;
            }
        if (((_zAngle < 90) && (_zAngle > -90)))
            if (switchedSide)
            {
                _flip = true; _flipped = false;
                switchedSide = false;
            }

        // Apply behaviours

        // toggle it to make an auto -180°
        if (_invert) _zAngle -= 180;

        // Rotate 180 around x-axis if the _angle is above or below 90
        //  _yAngle = ((_zAngle > 90) || (_zAngle < -90)) ? 180f : 0f;


        // Sync the rotation
        _zAngle *= Time.timeScale;

        transform.rotation = Quaternion.Euler(new Vector3(_xAngle, _yAngle, _zAngle)) ;
        
        

        // update gunpoint's forwrd vector
        Transform gunpoint = transform.Find("GunPoint");
        if (gunpoint!=null)
        {
            Vector2 gunpointPosition = gunpoint.position;
            Vector2 armPosition = transform.position;
            gunpoint.forward = gunpointPosition - armPosition ;
        }

    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }


    void FixedUpdate()
    {
        if (_flip && !_flipped)
            Flip();
    }
    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.y *= -1;
        transform.localScale = scale;
        _flip = false;
        _flipped = true;
    }

}
 