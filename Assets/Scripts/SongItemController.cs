using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongItemController : MonoBehaviour
{
    float targetY;
    // Start is called before the first frame update
    void Start()
    {
        targetY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"Current:{transform.position.y}, Target:{targetY}");
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            targetY -= 50;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            targetY += 50;
        }

        if (transform.position.y < targetY)
        {
            transform.Translate(0, +1, 0);
        }
        else if (transform.position.y > targetY)
        {
            transform.Translate(0, -1, 0);
        }
    }
}
