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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        perfect.text = $"{CurrentStats.perfect}";
        good.text = $"{CurrentStats.good}";
        miss.text = $"{CurrentStats.miss}";
        combo.text = $"{CurrentStats.combo}";
    }
}
