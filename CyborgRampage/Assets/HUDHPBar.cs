using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUDHPBar : MonoBehaviour
{

    private float _currentHP;
    private float _maxHP;
    private float _currentWidth;
    private float _maxWidth;
    private Image _bar;
    private PlayerController _pc;
    private RectTransform _barTransform;

    public Sprite hpOk;
    public Sprite hpCare;
    public Sprite hpDanger;

    // Use this for initialization
    void Start()
    {

        GameObject player = GameObject.Find("PLAYER");
        if (player == null) return;
        _pc = player.GetComponent<PlayerController>();
        _bar = GetComponent<Image>();
        _barTransform = GameObject.Find("HUD/HP_Bar").transform as RectTransform;

        if (_bar)
        {
            _maxWidth = _barTransform.sizeDelta.x;
            _currentWidth = _maxWidth;
        }

        if (_pc)
        {
            _maxHP = _pc.maxHealth;
            _currentHP = _pc.health;
        }


    }

    // Update is called once per frame
    void Update()
    {
        //_barTransform = GameObject.Find("HUD/HP_Bar").transform as RectTransform;
        //Image barMat = GetComponent<Image>();
        if (_maxHP < 0)
            return; // ERROR

        _currentHP = _pc.health;
        float hp_ratio = _currentHP / _maxHP;

        if (hp_ratio < 0 || hp_ratio > 1)
            return; // ERROR

        if ( hp_ratio <= 0.3 )
            _bar.sprite = hpDanger;
        else if ( hp_ratio <= 0.5 )
            _bar.sprite = hpCare;
        else if ( _bar.sprite != hpOk )
            _bar.sprite = hpOk;

        _barTransform.sizeDelta = new Vector2(_maxWidth * hp_ratio, _barTransform.sizeDelta.y);
    } 
}
