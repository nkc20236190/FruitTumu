using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishDialog : MonoBehaviour
{
    public TextMeshProUGUI HighScoreText;

    private void OnEnable()
    {
        // null�`�F�b�N��ǉ�
        if (HighScoreText != null)
        {
            // �n�C�X�R�A���擾���ĕ\�����܂�
            int highScore = PlayerPrefs.GetInt("HighScore", 0);
            HighScoreText.text = "" + highScore.ToString();
        }
    }
}
