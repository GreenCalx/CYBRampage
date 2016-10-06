using UnityEngine;
using System.Collections;

public class DamageText : MonoBehaviour {

    public Vector3 WorldOffset = Vector3.up * 2.0f;
    public Vector3 ScreenOffset = Vector3.zero;

    public Color textColor;
    public Font font;
    public int fontSize;

    private bool showText = false;
    private float currentTime = 0.0f, executedTime = 0.0f;
    public float persistenceTime = 1.0f;

    private Vector2 _RectPosition, _RectSize;
    private string _text;

    public int scrollingStep    = 5; 
    public Vector2 scrollingVector;
    private int i_scroll = 0;
    private float _opacity             = 250f;
    public float opacityDegressionRate = 2.5f;

    void Start()
    {

    }

    public void Display(  Vector2 rectPosition, int damageCount )
    {

        _RectPosition = rectPosition;
        _opacity = 250f;
        i_scroll = 0;

        if ((damageCount > 0) && (damageCount <= 999))
        {
            _RectSize = new Vector2(60, 40);
        }
        _text = damageCount.ToString();
        showText = true;
        executedTime = Time.time;
    }

    public void UpdatePosition( Vector2 rectPosition )
    {
        // Center it on the sprite
        _RectPosition.x = rectPosition.x;
        _RectPosition.y = Screen.height - rectPosition.y;

        // Offset it the top of the sprite
        SpriteRenderer sr = GetComponentInParent<SpriteRenderer>();
        if (sr != null)
            _RectPosition.y -= sr.sprite.textureRect.height * 1.5f;

        _RectPosition.y -= (scrollingVector.y * scrollingStep) * i_scroll++ ;
        _RectPosition.x += (scrollingVector.x * scrollingStep) * i_scroll++;

        if (_opacity >= opacityDegressionRate)
            _opacity -= opacityDegressionRate;
        else
            _opacity = 0f;
    }

    void Update()
    {
        currentTime = Time.time;

        if (executedTime != 0.0f)
        {
            if (currentTime - executedTime > persistenceTime)
            {
                executedTime = 0.0f;
                showText = false;
                
            }
        }
    }

    void OnGUI()
    {
        if (showText)
        {
            GUI.skin.label.font = font;
            GUI.skin.label.fontSize = fontSize;

            Color customColor = textColor;
            customColor.a = _opacity;
            GUI.contentColor = customColor;

            GUI.Label(new Rect(_RectPosition, _RectSize), _text);

            //DamageTextBehaviour dtb = GetComponentInChildren<DamageTextBehaviour>(true);
            //if (dtb != null)
            //{
            //    dtb.WorldOffset = _RectPosition;
            //    dtb.LateUpdate();
            //}
        }
            
    }
}
