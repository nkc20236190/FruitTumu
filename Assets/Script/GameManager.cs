using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public GameObject gameStartPanel;
    public float countdownDuration = 3.0f; // �J�E���g�_�E���̕b��

    private bool isCountdownFinished = false;

    private void Start()
    {
        Time.timeScale = 0f; // �Q�[�����~

        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        float countdownTimer = countdownDuration;

        while (countdownTimer > 0)
        {
            countdownText.text = Mathf.CeilToInt(countdownTimer).ToString();
            yield return new WaitForSecondsRealtime(1.0f);
            countdownTimer -= 1.0f;
        }

        // �J�E���g�_�E���I����̏���
        countdownText.text = "GO!";
        // 1�b�ҋ@
        yield return new WaitForSecondsRealtime(1.0f);
        gameStartPanel.SetActive(false); // �Q�[���J�n�p�l�����A�N�e�B�u�ɂ���
        isCountdownFinished = true;

        countdownText.gameObject.SetActive(false); // �J�E���g�_�E���e�L�X�g���A�N�e�B�u�ɂ���
        // �J�E���g�_�E���I����Ɏ����I�ɃQ�[�����J�n
        StartGame();
    }

    private void StartGame()
    {
        // �Q�[���̊J�n�����������ɒǉ�
        Time.timeScale = 1f; // �Q�[�����Đ�
    }
}
