using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartPlayer : MonoBehaviour
{
    public float laneWidth;
    public GameObject notePrefab;
    public List<string[]> rawChartData = new List<string[]>();
    public List<string[]> chartData = new List<string[]>();

    float bpm = 0;
    float speedConstant = 0;


    float time = 0;
    int notesCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        SettingsManager.laneWidth = laneWidth;

        rawChartData = ChartLoader.LoadChart();
        chartData = ChartLoader.LoadChart();
        //chartData = new List<string[]>(rawChartData);
        bpm = CurrentStats.bpm;
        Debug.Log($"BPM: {bpm}");
        speedConstant = 240000 / bpm;

        int _bar = 0;
        float _position = 0;
        for (int i = 0; i < rawChartData.Count; i++)
        {
            if (_bar < int.Parse(rawChartData[i][0]) - 1)
            {
                chartData[i][1] = ((float.Parse(rawChartData[i][0]) - 1) * speedConstant).ToString();
                //Debug.Log(chartData[i][1]);
                _bar = int.Parse(rawChartData[i][0]) - 1;
                _position = 1f / float.Parse(rawChartData[i][1]);
            }
            else
            {
                chartData[i][1] = ((_bar + _position) * speedConstant).ToString();
                //Debug.Log(int.Parse(rawChartData[i][1]));
                _position += 1f / float.Parse(rawChartData[i][1]);
            }
            //Debug.Log(chartData[i][1]);
            //Debug.Log(_position);
        }
        CurrentStats.currentChart = new List<string[]>(chartData);
        CurrentStats.perfect = 0;
        CurrentStats.good = 0;
        CurrentStats.miss = 0;
        CurrentStats.combo = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 1000;
        CurrentStats.time = time;
        if (chartData.Count > notesCount + 1)
        {
            if (float.Parse(chartData[notesCount][1]) + 5000 <= time)
            {
                var note = Instantiate(notePrefab, new Vector3(float.Parse(chartData[notesCount][3]) * laneWidth - 7, NotesController.speed * 5 - 3, 0), Quaternion.identity);
                note.tag = chartData[notesCount][3];
                notesCount++;
            }
        }
    }
}
