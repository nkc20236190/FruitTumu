using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public GameObject gameStartPanel;
    public float countdownDuration = 3.0f; // カウントダウンの秒数
    public AudioSource audioSourceBGM; // BGMのAudioSourceをアタッチ

    private bool isCountdownFinished = false;

    private void Start()
    {
        Time.timeScale = 0f; // ゲームを停止

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

        // カウントダウン終了後の処理
        countdownText.text = "GO!";
        // 1秒待機
        yield return new WaitForSecondsRealtime(1.0f);
        gameStartPanel.SetActive(false); // ゲーム開始パネルを非アクティブにする
        isCountdownFinished = true;

        countdownText.gameObject.SetActive(false); // カウントダウンテキストを非アクティブにする
        // カウントダウン終了後に自動的にゲームを開始
        StartGame();

        // 3秒後にBGMを再生
        yield return new WaitForSecondsRealtime(3.0f);
        audioSourceBGM.Play();
    }

    private void StartGame()
    {
        // ゲームの開始処理をここに追加
        Time.timeScale = 1f; // ゲームを再生
    }
}
