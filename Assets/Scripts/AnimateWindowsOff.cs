using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateWindowsOff : MonoBehaviour
{

    private void FixedUpdate()
    {

        Move();
        Rotate();
        
    }

    void Move()
    {

        transform.Translate(new Vector3(0.5f, 0, 0), Space.World);

    }

    void Rotate()
    {

        transform.Rotate(new Vector3(0, 0, -10f), Space.Self);

    }

}
