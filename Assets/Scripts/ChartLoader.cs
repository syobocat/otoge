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

        TextAsset chart = Resources.Load($"Charts/{CurrentStats.fileName}_{CurrentStats.difficultyString}") as TextAsset;
        StringReader reader = new StringReader(chart.text);
        string[] chartSetting = reader.ReadLine().Split(',');
        CurrentStats.songName = chartSetting[0];
        CurrentStats.artistName = chartSetting[1];
        CurrentStats.bpm = float.Parse(chartSetting[2]);
        CurrentStats.offset = float.Parse(chartSetting[3]);
        CurrentStats.notesCount = 0;

        reader.ReadLine();

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            string[] data = line.Split(',');
            if (data[3] != "0")
            {
                CurrentStats.notesCount++;
                if (data[2] != "0")
                {
                    CurrentStats.notesCount++;
                }
            }
            chartData.Add(data);
        }

        return chartData;
    }
}
