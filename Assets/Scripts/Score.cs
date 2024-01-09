using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI counterText;
    int score;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        ShowScore();
    }

    private void ShowScore()
    {
        counterText.text = score.ToString();
    }

    public void AddScore()
    {
        score++;
    }
}
