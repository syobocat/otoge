using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    public float perfectRange = 50; // ƒ~ƒŠ•b’PˆÊ‚Å”»’è•

    public static float speed = 10;
    float fallingTime = 0; // ‚±‚ê‚ª5000‚É‚È‚é‚Æ”»’è‚Ò‚Á‚½‚è

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        fallingTime += Time.deltaTime * 1000;
        /*if (fallingTime > 5000 - perfectRange && fallingTime < 5000 + perfectRange)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255.0f, 0.0f, 0.0f, 1.0f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(255.0f, 255.0f, 255.0f, 1.0f);
        }*/
        if (fallingTime > 5000 + SettingsManager.goodRange)
        {
            Destroy(gameObject);
            CurrentStats.currentChart.RemoveAt(0);
            CurrentStats.miss += 1;
            Debug.Log("Miss");
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(0, -speed * Time.deltaTime, 0);
    }
}
