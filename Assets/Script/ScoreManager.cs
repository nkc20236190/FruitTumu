using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    public static ScoreManager Instance { get; private set; }

    // プレイヤーごとのスコアを管理するディクショナリ
    private Dictionary<string, int> playerScores = new Dictionary<string, int>();



    void Start()
    {
        // PlayerPrefsから最高スコアを読み取り、表示する
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "" + highScore.ToString();
    }


    private void Awake()
    {
        // シングルトンインスタンスを設定
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // プレイヤー名を指定してスコアをリセット
    public void ResetScoreForPlayer(string playerName)
    {
        if (playerScores.ContainsKey(playerName))
        {
            playerScores[playerName] = 0;
        }
        else
        {
            playerScores.Add(playerName, 0);
        }
    }

    // プレイヤー名を指定してスコアを取得
    public int GetScoreForPlayer(string playerName)
    {
        if (playerScores.ContainsKey(playerName))
        {
            return playerScores[playerName];
        }
        return 0; // プレイヤー名が見つからない場合、初期スコアとして0を返す
    }
}
