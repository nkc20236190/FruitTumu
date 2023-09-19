using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int score = 0;

    void Start()
    {
        // スコアをリセット
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();

        // スコアを初期化
        score = 0;
        UpdateScoreText();
    }

    // スコアを更新し、表示するメソッド
    void UpdateScoreText()
    {
        scoreText.text = "" + score;
    }

    // スコアを増加させるメソッド（ゲーム内でスコアを加算する際に呼び出す）
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }
}
