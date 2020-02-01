using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventSettingsMenu : MonoBehaviour
{
    public GameObject SettingsBackground;
    public GameObject KoreanSettingsText;
    public GameObject Korean1;
    public GameObject Korean2;
    public GameObject Korean3;


    float a = 0;
    float b = 0;

    bool Airplane;

    public void OpenSettings()
    {

        SettingsBackground.SetActive(true);
        KoreanSettingsText.SetActive(true);
        Korean1.SetActive(true);
        Korean2.SetActive(true);
        Korean3.SetActive(true);


    }

    private void Update()
    {
        OpenAnimation();

        //AirplaneMode
        if (Airplane)
        {

            Done();

        }
    }

    void OpenAnimation()
    {

        Airplane = true;
        if(SettingsBackground.active)
        {

            SettingsBackground.GetComponent<Image>().color = new Color(1, 1, 1, a);
            KoreanSettingsText.GetComponent<Image>().color = new Color(1, 1, 1, b);
            Korean1.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (b - 0.5f));
            Korean2.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (b - 0.8f));
            Korean3.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, (b - 1.1f));



            a = a + 0.1f;
            b = b + 0.05f;

        }


    }

    void Done()
    {

        Debug.Log("done");

    }

}
