using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotesController : MonoBehaviour
{
    public float length = 0f;

    public float perfectRange = 50; // ƒ~ƒŠ•b’PˆÊ‚Å”»’è•

    public static float speed = 15;
    // public static float speed = SettingsManager.hiSpeed;
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
        if (CurrentStats.isAuto)
        {
            if (length == 0 && fallingTime > 5000) {
                Destroy(gameObject);
            CurrentStats.currentChart.RemoveAt(0);
            CurrentStats.perfect += 1;
            CurrentStats.combo += 1;
            Debug.Log("Auto");
            } else if (fallingTime > 5000 +(length * 1000 / NotesController.speed)) {
                Destroy(gameObject);
            CurrentStats.currentChart.RemoveAt(0);
            CurrentStats.perfect += 2;
            CurrentStats.combo += 2;
            Debug.Log("Auto");
            }
        }
        if (length != 0)
        {
            if (fallingTime > 5000 + (length * 1000 / NotesController.speed) + SettingsManager.goodRange)
            {
                Destroy(gameObject);
                CurrentStats.currentChart.RemoveAt(0);
                CurrentStats.miss += 2;
                Debug.Log("Miss");
            }
        }
        else if (fallingTime > 5000 + SettingsManager.goodRange)
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
