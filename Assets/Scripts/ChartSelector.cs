using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChartSelector : MonoBehaviour
{
    public string chartName;
    // Start is called before the first frame update
    void Start()
    {
        CurrentStats.chartName = chartName;
        Go();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Go()
    {
        SceneManager.LoadScene("Play");
    }
}
