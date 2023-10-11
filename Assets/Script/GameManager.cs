using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public GameObject gameStartPanel;
    public float countdownDuration = 3.0f; // �J�E���g�_�E���̕b��
    public AudioSource audioSourceCountdown; // �J�E���g�_�E���̉����Đ�����AudioSource���A�^�b�`
    public AudioClip countdownSound; // �J�E���g�_�E���̉�
    public AudioClip goSound; // "GO!"�̉�
    public AudioSource bgmAudioSource;
    public AudioClip newBGM;

    private bool isCountdownFinished = false;
    private bool isBGMPlaying = false;

    private void Start()
    {
        Time.timeScale = 0f; // �Q�[�����~

        // TitleScene����GameScene�ɑJ�ڂ����Ƃ�����BGM���~
        if (SceneManager.GetActiveScene().name == "GameScene")
        {
            if (MainSoundScript.instance != null)
            {
                MainSoundScript.instance.StopBGM();
            }
        }

        StartCoroutine(StartCountdown());
    }

    private IEnumerator StartCountdown()
    {
        float countdownTimer = countdownDuration;

        while (countdownTimer >= 0)
        {
            int countdownValue = Mathf.CeilToInt(countdownTimer);

            if (countdownValue > 0)
            {
                audioSourceCountdown.PlayOneShot(countdownSound); // �J�E���g�_�E�������Đ�
                countdownText.text = countdownValue.ToString();
            }
            else
            {
                audioSourceCountdown.PlayOneShot(goSound); // "GO!"�����Đ�
                countdownText.text = "GO!";

                // "GO!"�̏u�Ԃ�BGM���Đ�
                if (bgmAudioSource != null && !isBGMPlaying)
                {
                    bgmAudioSource.Play();
                    isBGMPlaying = true;
                }
            }

            yield return new WaitForSecondsRealtime(1.0f);
            countdownTimer -= 1.0f;
        }
        // �J�E���g�_�E���I����̏���
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

    private void OnDisable()
    {
        // GameScene����TitleScene�ɖ߂�ۂ�BGM���Đ�
        if (SceneManager.GetActiveScene().name == "TitleScene")
        {
            if (MainSoundScript.instance != null)
            {
                MainSoundScript.instance.ResumeBGM();
            }
        }
    }
}
