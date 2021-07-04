using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartPlayer : MonoBehaviour
{
    public float laneWidth;
    public GameObject notePrefab;
    public List<string[]> rawChartData = new List<string[]>();
    public List<string[]> chartData = new List<string[]>();
    AudioSource song;

    float bpm = 0;
    float speedConstant = 0;


    float time;
    int notesCount = 0;

    // Start is called before the first frame update
    void Awake()
    {
        song = this.GetComponent<AudioSource>();
        song.clip = Resources.Load($"Songs/{CurrentStats.fileName}") as AudioClip;
        SettingsManager.laneWidth = laneWidth;

        rawChartData = ChartLoader.LoadChart();
        chartData = ChartLoader.LoadChart();
        //chartData = new List<string[]>(rawChartData);
        bpm = CurrentStats.bpm;
        time = -CurrentStats.offset;
        Debug.Log($"BPM: {bpm}");
        Debug.Log($"Offset: {CurrentStats.offset}");
        speedConstant = 240000 / bpm;

        int _bar = 0;
        float _position = 0;
        int index = 0;
        int indexWithRest = 0;
        while (index < rawChartData.Count)
        {
            if (_bar < int.Parse(rawChartData[index][0]) - 1)
            {
                chartData[indexWithRest][1] = ((float.Parse(rawChartData[index][0]) - 1) * speedConstant).ToString();
                //Debug.Log(chartData[i][1]);
                _bar = int.Parse(rawChartData[index][0]) - 1;
                if (rawChartData[index][1] == "0")
                {
                    _position = 0f;
                }
                else
                {
                    _position = 1f / float.Parse(rawChartData[index][1]);
                }
            }
            else
            {
                chartData[indexWithRest][1] = ((_bar + _position) * speedConstant).ToString();
                //Debug.Log(int.Parse(rawChartData[i][1]));
                if (rawChartData[index][1] != "0")
                {
                    _position += 1f / float.Parse(rawChartData[index][1]);
                }
            }
            if (chartData[indexWithRest][3] == "0")
            {
                chartData.RemoveAt(indexWithRest);
            }
            else
            {
                indexWithRest++;
            }
            index++;
        }
        CurrentStats.currentChart = new List<string[]>(chartData);
        CurrentStats.perfect = 0;
        CurrentStats.good = 0;
        CurrentStats.miss = 0;
        CurrentStats.combo = 0;
        Invoke("PlaySong", 5);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 1000;
        CurrentStats.time = time;
        if (chartData.Count > notesCount)
        {
            if (float.Parse(chartData[notesCount][1]) + 100 <= time)
            {
                var note = Instantiate(notePrefab, new Vector3(float.Parse(chartData[notesCount][3]) * laneWidth - 6.95f, NotesController.speed * 5 - 3, 0), Quaternion.identity);
                note.tag = chartData[notesCount][3];

                if (chartData[notesCount][2] != "0")
                {
                    float length = NotesController.speed * (float.Parse(chartData[notesCount][2]) * speedConstant / 1000);
                    note.transform.localScale = new Vector3(1.5f, length + 0.2f, 1);
                    note.transform.Translate(0, length / 2, 0);
                    var nc = note.GetComponent<NotesController>();
                    nc.length = length;
                    /*
                    var endNote = Instantiate(notePrefab, new Vector3(float.Parse(chartData[notesCount][3]) * laneWidth - 7, NotesController.speed * (5 + float.Parse(chartData[notesCount][2]) * speedConstant / 1000) - 3, 0), Quaternion.identity);
                    endNote.tag = chartData[notesCount][3];
                    */
                }
                notesCount++;
            }
        }
    }

    void PlaySong()
    {
        song.Play();
    }
}
