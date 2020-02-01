using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconPress : MonoBehaviour
{

    bool isClicked;
    float randomDirection;
    float randomRotation;
    float yController = 0.25f;

    public static Vector3 positionOfDownloadButton;

    private void Start()
    {
        randomDirection = Random.Range(-0.1f, 0.1f);
        randomRotation = Random.Range(10, 20);

        if (Random.value > 0.5f)
        {

            randomRotation = randomRotation * -1;

        }

    }

    private void OnMouseDown()
    {

        if (isClicked)
            {
                return;
            }

            isClicked = true;

        positionOfDownloadButton = transform.position;
        EventHomeScreen.Instance.Reduce(1);

    }

    void Update()
    {
        Fall();
        CheckHeight();
    }

    void Fall()
    {

        if (isClicked)
        {

            Move();
            Rotate();

        }

    }

    private void Move()
    {

        transform.Translate(new Vector3(randomDirection, yController, 0), Space.World);
        yController = yController - 0.02f;

    }

    void Rotate()
    {

        transform.Rotate(new Vector3(0, 0, randomRotation));

    }

    void CheckHeight()
    {

        if (transform.position.y < -5)
        {

            EventHomeScreen.Instance.CheckIfDone();
            Destroy(this.gameObject);

        }

    }

}