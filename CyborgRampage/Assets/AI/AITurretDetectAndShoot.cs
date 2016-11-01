using UnityEngine;
using System.Collections;

public class AITurretDetectAndShoot : MonoBehaviour {

    public enum DETECTION_STYLE
    {
        RADIAL
    };

    public float detectionRange = 10f;
    public DETECTION_STYLE detectionStyle;

    private Transform _playerTransformRef;
    protected bool _aggro;

    private BasicGun _weapon;



    // Use this for initialization
    public virtual void Start () {
        _aggro = false;
        detectionStyle = DETECTION_STYLE.RADIAL;
        BasicGun[] weapons = GetComponents<BasicGun>();
        for (int i=0; i<weapons.Length;++i)
        {
            BasicGun weapon = weapons[i];
            if (weapon == null) continue;
            if (weapon.name == "main_weapon" )
                 _weapon = weapon;
        }
    }

    // Update is called once per frame
    public virtual void Update () {
        if (_aggro)
            Shoot();
        else
            Detect();
    }

    void Detect()
    {
        Transform parentTransform = GetComponent<Transform>();
        Transform playerTransform = GameObject.Find("PLAYER").transform;
        _aggro = (Vector3.Distance(parentTransform.position, playerTransform.position) <= detectionRange);
    }

    void Shoot()
    {
        {
            Transform parentTransform = GetComponent<Transform>();
            Transform playerTransform = GameObject.Find("PLAYER").transform;
            Transform gunpoint = transform.Find("GunPoint");
            // Set movement direction
            //parentTransform.LookAt(playerTransform);
            //parentTransform.forward = playerTransform.position;

            // Shoot in the direction
            if (_weapon && gunpoint)
            {
                //gunpoint.forward = parentTransform.forward;
                gunpoint.forward = playerTransform.position - parentTransform.position;
                _weapon.Fire(true);
            }
        }
    }
}
