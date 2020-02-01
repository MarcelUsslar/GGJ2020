using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsButton : MonoBehaviour
{

    float scale;
    int AnimationState = 0;

    public GameObject EventSettingsMenuScript;

    void Awake()
    {
        transform.position = IconPress.positionOfDownloadButton;
        transform.localScale = new Vector3(0.01f, 0.01f, 1);
    }

    private void Update()
    {

        if(AnimationState == 0)
        {

            StartAnimate();

        }

        else
        {

            AnimateToOriginal();

        }
    }

    void StartAnimate()
    {

        transform.localScale = new Vector3(scale, scale, 1);

        if (scale < 0.4f)
        {

            scale = scale + 0.05f;

        }

        if (scale >= 0.4f)
        {

            Invoke("SetAnimationStateToTwo", 0.2f);

        }

    }

    void SetAnimationStateToTwo()
    {

        AnimationState = 1;

    }

    void AnimateToOriginal()
    {

        transform.localScale = new Vector3(scale, scale, 1);

        if (scale > 0.3f)
        {
            Debug.Log("lul");
    
            scale = scale - 0.02f;

        }

    }

    private void OnMouseDown()
    {

        EventSettingsMenuScript.GetComponent<EventSettingsMenu>().OpenSettings();
        Destroy(this.gameObject);

    }

}
