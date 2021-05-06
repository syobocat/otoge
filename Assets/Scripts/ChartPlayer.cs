using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChartPlayer : MonoBehaviour
{
    public float laneWidth;
    public GameObject notePrefab;

    string chartName;
    List<string[]> chartData = new List<string[]>();

    float time = 0;
    int notesCount = 1;

    // Start is called before the first frame update
    void Start()
    {
        chartName = "SampleChart";
        chartData = ChartLoader.LoadChart(chartName);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime * 1000;
        if (chartData.Count > notesCount)
        {
            if (float.Parse(chartData[notesCount][0]) + 5000 <= time)
            {
                Instantiate(notePrefab, new Vector3(float.Parse(chartData[notesCount][2]) * laneWidth -5, NotesController.speed * 5 - 3, 0), Quaternion.identity);
                notesCount++;
            }
        }
        

        //Debug.Log(time);
    }
}
