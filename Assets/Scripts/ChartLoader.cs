using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ChartLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
}

    // Update is called once per frame
    void Update()
    {
        
    }

    public static List<string[]> LoadChart(string chartName)
    {
        List<string[]> chartData = new List<string[]>();

        TextAsset chart = Resources.Load($"Charts/{chartName}") as TextAsset;
        StringReader reader = new StringReader(chart.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            chartData.Add(line.Split(','));
        }

        return chartData;
    }
}
