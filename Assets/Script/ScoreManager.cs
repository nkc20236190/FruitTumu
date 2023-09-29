using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public int maxHighScore = 999999; // 最高スコアの最大値

    public static ScoreManager Instance { get; private set; }

    // プレイヤーごとのスコアを管理するディクショナリ
    private Dictionary<string, int> playerScores = new Dictionary<string, int>();



    void Start()
    {
        // PlayerPrefsから最高スコアを読み取り、表示する
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        // 最高スコアが最大値を超えないように制限
        highScore = Mathf.Clamp(highScore, 0, maxHighScore);
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

    // 最高スコアを設定するメソッド
    public void SetHighScore(int score)
    {
        // スコアが最大値を超えないように制限
        score = Mathf.Clamp(score, 0, maxHighScore);

        // PlayerPrefsに保存
        PlayerPrefs.SetInt("HighScore", score);
        PlayerPrefs.Save();
        highScoreText.text = score.ToString();
    }
}
