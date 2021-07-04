using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayController : MonoBehaviour
{
    public GameObject[] effect;
    float time = -5100f; // ノーツ出現までに5秒、ノーツ到達まで5秒かかるのでずらしておく

    string[] keys = new string[6] { "s", "d", "f", "j", "k", "l" }; // 入力キー設定

    // ホールドノー処理用
    GameObject[] holdNote = new GameObject[6];
    bool[] isHolding = new bool[6] { false, false, false, false, false, false };
    float[] holdLength = new float[6] { 0, 0, 0, 0, 0, 0 };
    float[] holdingTime = new float[6] { 0, 0, 0, 0, 0, 0 };

    void Start()
    {
        time -= CurrentStats.offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (!CurrentStats.isAuto)
        {
            time += Time.deltaTime * 1000; // Time.deltaTimeは単位が秒なのでミリ秒になおす

            // 判定
            for (int i = 0; i < 6; i++)
            {
                if (Input.GetKeyDown(keys[i]))
                {
                    Judge((i + 1).ToString());
                }
                if (Input.GetKey(keys[i]))
                {
                    effect[i].SetActive(true);
                }
                if (Input.GetKeyUp(keys[i]))
                {
                    effect[i].SetActive(false);
                }
            }

            // ホールドノーツ処理
            for (int i = 0; i < 6; i++)
            {
                if (isHolding[i])
                {
                    // ホールドし続けている場合
                    if (Input.GetKey(keys[i]))
                    {
                        // 終点判定のある音ゲーは罪
                        if (holdingTime[i] > holdLength[i])
                        {
                            CurrentStats.perfect += 1;
                            CurrentStats.combo += 1;
                            isHolding[i] = false;
                            Debug.Log("Perfect");
                            Destroy(holdNote[i]);
                        }
                        else
                        {
                            holdingTime[i] += Time.deltaTime; // ホールド時間追加
                        }
                    }
                    // ホールドブレーク
                    else
                    {
                        float diff = holdLength[i] - holdingTime[i]; // ホールドしていないといけない長さと実際にホールドした長さの差
                        isHolding[i] = false;

                        // それぞれ判定
                        if (diff < SettingsManager.perfectRange)
                        {
                            CurrentStats.perfect += 1;
                            CurrentStats.combo += 1;
                            Debug.Log("Perfect");
                            Destroy(holdNote[i]);
                        }
                        else if (diff < SettingsManager.goodRange)
                        {
                            CurrentStats.good += 1;
                            CurrentStats.combo += 1;
                            Debug.Log("Good");
                            Destroy(holdNote[i]);
                        }
                        else
                        {
                            CurrentStats.miss += 1;
                            CurrentStats.combo = 0;
                            Debug.Log("Miss");
                            Destroy(holdNote[i]);
                        }
                    }
                }
            }
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
                    var nc = g[maxItem].GetComponent<NotesController>();
                    float notesLength = nc.length;
                    if (notesLength > 0)
                    {
                        isHolding[int.Parse(lane) - 1] = true;
                        holdLength[int.Parse(lane) - 1] = notesLength / NotesController.speed;
                        holdNote[int.Parse(lane) - 1] = g[maxItem];
                    }
                    else
                    {
                        Destroy(g[maxItem]);
                    }
                    CurrentStats.currentChart.RemoveAt(i);
                }
                break;
            }
        }
    }
}
