using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ChartSelector : MonoBehaviour
{
    public GameObject[] difficultyCircle;
    public TextMeshProUGUI[] difficultyNumber;
    public GameObject[] difficultySelected;
    public GameObject songItem;
    public GameObject canvas;
    float canvasTargetY;
    int songIndex;
    int difficultyIndex;
    string fileName;
    string[] difficulties = new string[5] { "easy", "normal", "hard", "another", "insane" };
    public bool isAuto;
    List<string[]> songList = new List<string[]>();

    //List<string[]> songDict = new List<string[]>();

    // Start is called before the first frame update
    void Start()
    {
        TextAsset list = Resources.Load("songList") as TextAsset;
        StringReader reader = new StringReader(list.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            string[] data = line.Split(',');
            songList.Add(data);
        }

        for (int i = 0; i < songList.Count; i++)
        {
            var item = Instantiate(songItem, new Vector3(306, 256 + (-50) * i, 0), Quaternion.identity, canvas.transform);
            //var item = Instantiate(songItem, canvas.transform, true);
            var itemText = item.GetComponent<Text>();
            itemText.text = songList[i][1];
        }

        canvasTargetY = canvas.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow) && songIndex < songList.Count - 1)
        {
            canvasTargetY += 50;
            songIndex++;
            if (songList[songIndex][difficultyIndex + 3] == "0")
            {
                difficultyIndex = 0;
                while (songList[songIndex][difficultyIndex + 3] == "0")
                {
                    difficultyIndex++;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && songIndex > 0)
        {
            canvasTargetY -= 50;
            songIndex--;
            if (songList[songIndex][difficultyIndex + 3] == "0")
            {
                difficultyIndex = 0;
                while (songList[songIndex][difficultyIndex + 3] == "0")
                {
                    difficultyIndex++;
                }
            }
        }

        if (canvas.transform.position.y < canvasTargetY)
        {
            canvas.transform.Translate(0, +1, 0);
        }
        else if (canvas.transform.position.y > canvasTargetY)
        {
            canvas.transform.Translate(0, -1, 0);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && difficultyIndex < 4)
        {
            int _difficultyIndex = difficultyIndex;
            difficultyIndex++;
            while (songList[songIndex][difficultyIndex + 3] == "0")
            {
                difficultyIndex++;
                if (difficultyIndex > 4)
                {
                    difficultyIndex = _difficultyIndex;
                    break;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && difficultyIndex > 0)
        {
            int _difficultyIndex = difficultyIndex;
            difficultyIndex--;
            while (songList[songIndex][difficultyIndex + 3] == "0")
            {
                difficultyIndex--;
                if (difficultyIndex < 0)
                {
                    difficultyIndex = _difficultyIndex;
                    break;
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.Return))
        {
            CurrentStats.fileName = songList[songIndex][0];
            CurrentStats.difficultyString = difficulties[difficultyIndex];
            CurrentStats.difficultyNumber = songList[songIndex][difficultyIndex + 3];
            CurrentStats.isAuto = isAuto;
            Go();
        }

        for (int i = 0; i < 5; i++)
        {
            if (songList[songIndex][i + 3] != "0")
            {
                difficultyCircle[i].SetActive(true);
                difficultyNumber[i].text = songList[songIndex][i + 3];
            }
            else
            {
                difficultyCircle[i].SetActive(false);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (difficultyIndex == i)
            {
                difficultySelected[i].SetActive(true);
            }
            else
            {
                difficultySelected[i].SetActive(false);
            }
        }
    }

    void Go()
    {
        SceneManager.LoadScene("Play");
    }
}
