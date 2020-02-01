using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownloadButton : MonoBehaviour
{

    public GameObject DownloadingIcon;

    private void OnMouseDown()
    {

        //wifi check
        if(true)
        {

            DownloadingIcon.transform.position = this.transform.position;
            DownloadingIcon.SetActive(true);
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            Invoke("CallDone", 5);

        }

    }

    private void Update()
    {
        RotateDownloadingIcon();
    }

    void RotateDownloadingIcon()
    {

        DownloadingIcon.transform.Rotate(new Vector3(0, 0, -5f));


    }

    void CallDone()
    {
        Debug.Log("called");
        DownloadingIcon.SetActive(false);
        EventHomescreenwDB.Instance.Downloaded();

    }


}
