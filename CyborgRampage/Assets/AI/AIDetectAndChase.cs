using UnityEngine;
using System.Collections;

public class AIDetectAndChase : MonoBehaviour {

    public enum DETECTION_STYLE
    {
        RADIAL
    };




    public CircleCollider2D _detectionArea;
    public float moveSpeed = 4f;
    public float detectionRange = 10f;
    public DETECTION_STYLE detectionStyle;

    private Transform _playerTransformRef;
    private bool _aggro;
    private MoveScript _moveScript;


    // Use this for initialization
    void Start ()
    {
        _aggro = false;
        _detectionArea = GetComponent<CircleCollider2D>();
        _moveScript = GetComponentInParent<MoveScript>();
        _moveScript.direction = new Vector2(0, 0);

        detectionStyle = DETECTION_STYLE.RADIAL;
    }

    //void OnTriggerEnter2D(Collider2D c)
    //{
    //    if (c.name == "PLAYER")
    //        _aggro = true;
    //    else
    //        return;

    //}

    void Detect()
    {
        Transform parentTransform = GetComponentInParent<Transform>();
        Transform playerTransform = GameObject.Find("PLAYER").transform;
        _aggro = (Vector3.Distance(parentTransform.position, playerTransform.position) <= detectionRange);
    }

    void Chase()
    {
        Transform parentTransform = GetComponentInParent<Transform>();
        Transform playerTransform = GameObject.Find("PLAYER").transform;

        if (!_moveScript || !playerTransform || !parentTransform)
            return; //error

        // Set movement speed
        _moveScript.speed = new Vector2(moveSpeed, moveSpeed);

        // Set movement direction
        parentTransform.LookAt(playerTransform);

        _moveScript.direction = parentTransform.forward;

        //if (parentTransform.forward.x < 0) // NOT WORKING
        //    parentTransform.Rotate(new Vector3(0, 0, 1), 180f);
    }

    // Update is called once per frame
    void Update () {

        if (_aggro)
            Chase();
        else
            Detect();
    }
}
