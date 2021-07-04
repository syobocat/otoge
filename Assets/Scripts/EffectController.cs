using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public float lifeTime;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 1000;
        if (time > lifeTime)
        {
            Destroy(gameObject);
        }
    }
}
