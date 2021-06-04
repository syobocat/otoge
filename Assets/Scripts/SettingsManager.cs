using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingsManager
{
    private static float _laneWidth;
    private static float _hiSpeed;

    public static int perfectRange = 60;
    public static int goodRange = 120;
    public static int makikomiRange = 300;

    public static float hiSpeed
    {
        get
        {
            return _hiSpeed;
        }
        set
        {
            if (value < 10 && value > 0)
            {
                _hiSpeed = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"{nameof(value)} must be between 0 and 10.");
            }
        }
    }

    public static float laneWidth
    {
        get
        {
            return _laneWidth;
        }
        set
        {
            if (value > 0)
            {
                _laneWidth = value;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"{nameof(value)} must be bigger than 0.");
            }
        }
    }
}
