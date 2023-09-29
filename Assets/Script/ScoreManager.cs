using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;
    public int maxHighScore = 999999; // �ō��X�R�A�̍ő�l

    public static ScoreManager Instance { get; private set; }

    // �v���C���[���Ƃ̃X�R�A���Ǘ�����f�B�N�V���i��
    private Dictionary<string, int> playerScores = new Dictionary<string, int>();



    void Start()
    {
        // PlayerPrefs����ō��X�R�A��ǂݎ��A�\������
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        // �ō��X�R�A���ő�l�𒴂��Ȃ��悤�ɐ���
        highScore = Mathf.Clamp(highScore, 0, maxHighScore);
        highScoreText.text = "" + highScore.ToString();
    }


    private void Awake()
    {
        // �V���O���g���C���X�^���X��ݒ�
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

    // �v���C���[�����w�肵�ăX�R�A�����Z�b�g
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

    // �v���C���[�����w�肵�ăX�R�A���擾
    public int GetScoreForPlayer(string playerName)
    {
        if (playerScores.ContainsKey(playerName))
        {
            return playerScores[playerName];
        }
        return 0; // �v���C���[����������Ȃ��ꍇ�A�����X�R�A�Ƃ���0��Ԃ�
    }

    // �ō��X�R�A��ݒ肷�郁�\�b�h
    public void SetHighScore(int score)
    {
        // �X�R�A���ő�l�𒴂��Ȃ��悤�ɐ���
        score = Mathf.Clamp(score, 0, maxHighScore);

        // PlayerPrefs�ɕۑ�
        PlayerPrefs.SetInt("HighScore", score);
        PlayerPrefs.Save();
        highScoreText.text = score.ToString();
    }
}
