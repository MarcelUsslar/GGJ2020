using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEnglishSettingsMenu : MonoBehaviour
{

    public GameObject Error1;
    public GameObject Error2;
    public GameObject Error3;
    public GameObject ErrorText;

    bool Charging;



    void Start()
    {

        Invoke("StartVacuum", 4);


    }

    private void Update()
    {

        if (Charging)
        {

           Vacuum();

        }

    }


    void StartVacuum()
    {

        Charging = true;
        Invoke("Done", 1);

    }

    void Vacuum()
    {

        var Error1Tran = Error1.transform;

        Error1.transform.position = new Vector3(0, Mathf.Lerp(Error1Tran.position.y,-10f,0.05f), 1);
        Error1.transform.localScale = new Vector3 (Mathf.Lerp(Error1Tran.localScale.x, 0.05f, 0.1f), Mathf.Lerp(Error1Tran.localScale.y, 1f, 0.5f), 1);

        var Error2Tran = Error2.transform;

        Error2.transform.position = new Vector3(0, Mathf.Lerp(Error2Tran.position.y, -10f, 0.05f), 1);
        Error2.transform.localScale = new Vector3(Mathf.Lerp(Error2Tran.localScale.x, 0.05f, 0.1f), Mathf.Lerp(Error2Tran.localScale.y, 1f, 0.5f), 1);

        var Error3Tran = Error3.transform;

        Error3.transform.position = new Vector3(0, Mathf.Lerp(Error3Tran.position.y, -10f, 0.05f), 1);
        Error3.transform.localScale = new Vector3(Mathf.Lerp(Error3Tran.localScale.x, 0.05f, 0.1f), Mathf.Lerp(Error3Tran.localScale.y, 1f, 0.5f), 1);

        var ErrorTextTran = ErrorText.transform;

        ErrorText.transform.position = new Vector3(0, Mathf.Lerp(ErrorTextTran.position.y, -10f, 0.04f), 1);
        ErrorText.transform.localScale = new Vector3(Mathf.Lerp(ErrorTextTran.localScale.x, 0.04f, 0.1f), Mathf.Lerp(ErrorTextTran.localScale.y, 1f, 0.4f), 1);


    }

    void Done()
    {

        Debug.Log("done");

    }

}
