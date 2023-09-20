using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public AudioClip soundClip; // 効果音ファイル
    private Button button;
    private AudioSource audioSource;

    private void Start()
    {
        // ボタンとAudioSourceコンポーネントを取得
        button = GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();

        // ボタンのクリックイベントに関数を追加
        button.onClick.AddListener(PlaySound);
    }

    private void PlaySound()
    {
        // 効果音を再生
        audioSource.PlayOneShot(soundClip);
    }
}
