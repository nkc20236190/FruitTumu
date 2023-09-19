using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishDialog : MonoBehaviour
{
    public TextMeshProUGUI HighScoreText;

    private void OnEnable()
    {
        // nullチェックを追加
        if (HighScoreText != null)
        {
            // ハイスコアを取得して表示します
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            HighScoreText.text = "" + highScore.ToString();
        }
    }
}
