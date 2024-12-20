using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreC : MonoBehaviour
{
    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        int x = PlayerPrefs.GetInt("Score");
        text.text = x.ToString();
    }
}
