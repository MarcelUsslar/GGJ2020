using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventCrackedScreen : MonoBehaviour
{
    int numberOfTriggersInScene;

    private void Start()
    {
        GameObject[] Triggers = GameObject.FindGameObjectsWithTag("Triggers");
        numberOfTriggersInScene = Triggers.Length;
        Debug.Log(numberOfTriggersInScene);
        Camera.main.backgroundColor = Color.white;
    }

    void Update()
    {
        ObjectFollowMouse();
    }

    void ObjectFollowMouse()
    {
        if (Input.GetMouseButton(0))
        {

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position =
                new Vector3(Mathf.Lerp(transform.position.x, mousePosition.x, 1f),
                Mathf.Lerp(transform.position.y, mousePosition.y, 1f),
                0);

        }


    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        CheckTriggers();
    }

    void CheckTriggers()
    {

        numberOfTriggersInScene = numberOfTriggersInScene - 1;

        if (numberOfTriggersInScene == 0)
        {

            Debug.Log("Done");

        }

    }
}
