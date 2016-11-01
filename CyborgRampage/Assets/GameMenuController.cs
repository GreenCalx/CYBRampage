using UnityEngine;
using System.Collections;

public class GameMenuController : MonoBehaviour
{

    private bool _menuOpened;
    private CanvasGroup _canvasGroup;

    // Use this for initialization
    void Start()
    {
        _menuOpened = false;
        _canvasGroup = GetComponent<CanvasGroup>();
        HideAndDisable();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("OpenMenu"))
        {
            _menuOpened = togglePause();

            if (_menuOpened)
                DisplayAndEnable();
            else
                HideAndDisable();
        }

    }

    void OnGUI()
    {

    }

    void HideAndDisable()
    {
        _canvasGroup.alpha = 0f;
        _canvasGroup.blocksRaycasts = false;
    }

    void DisplayAndEnable()
    {
        _canvasGroup.alpha = 1f;
        _canvasGroup.blocksRaycasts = true;
    }



    private bool togglePause()
    {
        if (Time.timeScale == 0f)
        { Time.timeScale = 1f; return false; }
        else
        { Time.timeScale = 0f; return true; }
    } // togglePause
}
