using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    public float perfectRange; // 秒単位で判定幅

    public static float speed = 7;
    float fallingTime = 0; // これが5になると判定ぴったり

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fallingTime += Time.deltaTime;
        if (fallingTime > 5-perfectRange && fallingTime < 5+perfectRange)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255.0f, 0.0f, 0.0f, 1.0f);
        } else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 1.0f);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(0, -speed*Time.deltaTime, 0);
    }
}
