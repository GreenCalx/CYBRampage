using UnityEngine;
using System.Collections.Generic;

public class Hobo : MonoBehaviour
{

    private float _randValue;
    private Animator _animator;
    public NpcDialogBoxText NpcText;
    private int ProximityFlag;
    private NpcDialogBoxText _Text;
    public bool _npctextinstansed = false;
    public bool ok;
    protected Vector3 _transformPosition;

    public Transform b;

    // Use this for initialization
    void Start()
    {
        //_Text = new NpcDialogBoxText();
        _animator = GetComponent<Animator>();
        _animator.SetBool("HeatTheHobo", false);

    }

    // Update is called once per frame
    void Update()
    {
        _transformPosition = this.gameObject.transform.position;
        if(_npctextinstansed)
        {
            Vector2 textPosition = Camera.main.WorldToScreenPoint(_transformPosition);
            _Text.LastSeenPosition(textPosition);
            _Text.Update();
            
            b.position.Set(_Text.GetPosition().x, _Text.GetPosition().y, 0);
            b.localPosition.Set(_Text.GetPosition().x, _Text.GetPosition().y, 0);
           


        }


       _randValue = Random.Range(0f, 1f);

        if (_randValue > 0.9f)
            _animator.SetBool("HeatTheHobo", true);
        else
            _animator.SetBool("HeatTheHobo", false);
    }



    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.name.Equals("PLAYER"))
        {
            // we near enought of the madafaka
            ProximityFlag = 1;
            ok = AllowPlayerDialog();
        }
    }
    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.name.Equals("PLAYER"))
        {
            // we near enought of the madafaka
            ProximityFlag = 1;
            ok = AllowPlayerDialog();
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

    public bool AllowPlayerDialog()
    {
        if (Input.GetKey(KeyCode.E))
        {
            if (ProximityFlag == 1)
            {
                // we can now speak to the madafaka
                if (_npctextinstansed == false)
                {
                    if (NpcText != null)
                    {
                        _Text = Instantiate(NpcText) as NpcDialogBoxText;
                        _npctextinstansed = true;

                    }
                }

                if (_npctextinstansed)
                {
                    Vector2 textPosition = Camera.main.WorldToScreenPoint(_transformPosition);
                    //Vector2 textPosition = new Vector2(200, 200);
                    _Text.Display(textPosition, "SLK");

                }

                return true;
            }
        }
        return false;
    }
}
