using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    float time = -10000f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 1000;
        if (Input.GetKeyDown("s"))
        {
            Judge("1");
        }
        if (Input.GetKeyDown("d"))
        {
            Judge("2");
        }
        if (Input.GetKeyDown("f"))
        {
            Judge("3");
        }
        if (Input.GetKeyDown("j"))
        {
            Judge("4");
        }
        if (Input.GetKeyDown("k"))
        {
            Judge("5");
        }
        if (Input.GetKeyDown("l"))
        {
            Judge("6");
        }
    }

    void Judge(string lane)
    {
        for (int i = 0; i < CurrentStats.currentChart.Count; i++)
        {
            if ((CurrentStats.currentChart[i][3]) == lane)
            {
                // Debug.Log($"Tap Timing: {time}, Chart Timing: {float.Parse(CurrentStats.currentChart[i][1])}");
                if (float.Parse(CurrentStats.currentChart[i][1]) - SettingsManager.makikomiRange < time && float.Parse(CurrentStats.currentChart[i][1]) + SettingsManager.goodRange > time)
                {
                    if (float.Parse(CurrentStats.currentChart[i][1]) - SettingsManager.perfectRange < time && float.Parse(CurrentStats.currentChart[i][1]) + SettingsManager.perfectRange > time)
                    {
                        CurrentStats.perfect += 1;
                        CurrentStats.combo += 1;
                        Debug.Log("Perfect");
                    }
                    else if (float.Parse(CurrentStats.currentChart[i][1]) - SettingsManager.goodRange > time)
                    {
                        CurrentStats.miss += 1;
                        CurrentStats.combo = 0;
                        Debug.Log("Miss");
                    }
                    else
                    {
                        CurrentStats.good += 1;
                        CurrentStats.combo += 1;
                        Debug.Log("Good");
                    }
                    GameObject[] g = GameObject.FindGameObjectsWithTag(lane);
                    if (g.Length == 0)
                    {
                        break;
                    }
                    float max = 0;
                    int maxItem = 0;

                    int loop = 0;
                    foreach (var item in g)
                    {
                        if (item.transform.position.y < max)
                        {
                            max = item.transform.position.y;
                            maxItem = loop;
                        }
                        loop++;
                    }
                    Destroy(g[maxItem]);
                    CurrentStats.currentChart.RemoveAt(i);
                }
                break;
            }
        }
    }
}
