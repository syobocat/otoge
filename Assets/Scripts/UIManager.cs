using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Text perfect;
    public Text good;
    public Text miss;
    public TextMeshProUGUI combo;
    public Text songName;
    public TextMeshProUGUI difficultyString;
    public TextMeshProUGUI difficultyNumber;
    public TextMeshProUGUI score;
    // Start is called before the first frame update
    void Start()
    {
        songName.text = $"{CurrentStats.songName}";
        difficultyString.text = $"{CurrentStats.difficultyString.ToUpper()}";
        difficultyNumber.text = $"{CurrentStats.difficultyNumber}";
    }

    // Update is called once per frame
    void Update()
    {
        perfect.text = $"{CurrentStats.perfect}";
        good.text = $"{CurrentStats.good}";
        miss.text = $"{CurrentStats.miss}";
        combo.text = $"{CurrentStats.combo}";
        score.text = $"{((int)((CurrentStats.perfect + 0.5 * CurrentStats.good) / CurrentStats.notesCount * 1000000)).ToString("0,000,000")}";
    }
}
