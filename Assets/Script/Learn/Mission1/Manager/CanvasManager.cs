using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager Instance { get; private set; }
    

    private void Awake()
    {
        Instance = this;
    }

    [SerializeField] TMP_Text scoreText;
    public GameObject gameOver;
    [SerializeField] TMP_Text gameOverScore;

    public void UpdateText(string score)
    {
        scoreText.text = "Score : " + score;
        gameOverScore.text = scoreText.text;
    }
}
