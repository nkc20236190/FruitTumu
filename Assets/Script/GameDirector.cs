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
        // �X�R�A�����Z�b�g
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.Save();

        // �X�R�A��������
        score = 0;
        UpdateScoreText();
    }

    // �X�R�A���X�V���A�\�����郁�\�b�h
    void UpdateScoreText()
    {
        scoreText.text = "" + score;
    }

    // �X�R�A�𑝉������郁�\�b�h�i�Q�[�����ŃX�R�A�����Z����ۂɌĂяo���j
    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }
}
