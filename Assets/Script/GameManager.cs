using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public GameObject gameStartPanel;
    public float countdownDuration = 3.0f; // カウントダウンの秒数
    public AudioSource audioSourceCountdown; // カウントダウンの音を再生するAudioSourceをアタッチ
    public AudioClip countdownSound; // カウントダウンの音
    public AudioClip goSound; // "GO!"の音
    public AudioSource bgmAudioSource;
    public AudioClip newBGM;

    private bool isCountdownFinished = false;

    private void Start()
    {
        Time.timeScale = 0f; // ゲームを停止
        // TitleSceneで再生されているBGMを停止
        if (MainSoundScript.instance != null)
        {
            MainSoundScript.instance.StopBGM();
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
                audioSourceCountdown.PlayOneShot(countdownSound); // カウントダウン音を再生
                countdownText.text = countdownValue.ToString();
            }
            else
            {
                audioSourceCountdown.PlayOneShot(goSound); // "GO!"音を再生
                countdownText.text = "GO!";

                // "GO!"の瞬間にBGMを再生
                if (bgmAudioSource != null)
                {
                    bgmAudioSource.Play();
                }
            }

            yield return new WaitForSecondsRealtime(1.0f);
            countdownTimer -= 1.0f;
        }
        // カウントダウン終了後の処理
        gameStartPanel.SetActive(false); // ゲーム開始パネルを非アクティブにする
        isCountdownFinished = true;

        countdownText.gameObject.SetActive(false); // カウントダウンテキストを非アクティブにする

        // カウントダウン終了後に自動的にゲームを開始
        StartGame();

    }

    private void StartGame()
    {
        // ゲームの開始処理をここに追加
        Time.timeScale = 1f; // ゲームを再生
    }
}
