using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CurrentStats
{
    private static float _bpm;

    public static List<string[]> currentChart;

    public static string chartName { get; set; }
    public static string songName { get; set; }
    public static string artistName { get; set; }
    public static ushort perfect { get; set; }
    public static ushort good { get; set; }
    public static ushort miss { get; set; }
    public static ushort combo { get; set; }

    public static float bpm
    {
        get
        {
            return _bpm;
        }
        set
        {
            if (value > 0)
            {
                _bpm = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"{nameof(value)} must be bigger than 0.");
            }
        }
    }

    public static float time;
}
