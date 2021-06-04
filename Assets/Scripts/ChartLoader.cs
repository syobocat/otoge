using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ChartLoader
{
    public static List<string[]> LoadChart()
    {
        List<string[]> chartData = new List<string[]>();

        TextAsset chart = Resources.Load($"Charts/{CurrentStats.chartName}") as TextAsset;
        StringReader reader = new StringReader(chart.text);
        string[] chartSetting = reader.ReadLine().Split(',');
        CurrentStats.songName = chartSetting[0];
        CurrentStats.artistName = chartSetting[1];
        CurrentStats.bpm = int.Parse(chartSetting[2]);

        reader.ReadLine();

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            chartData.Add(line.Split(','));
        }

        return chartData;
    }
}
