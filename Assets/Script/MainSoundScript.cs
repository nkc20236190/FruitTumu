using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundScript : MonoBehaviour
{
    public static MainSoundScript instance; // インスタンスをシングルトンとして保持

    public bool DontDestroyEnabled = true;

    // BGM再生用のAudioSource
    private AudioSource bgmAudioSource;

    void Awake()
    {
        // シングルトンの確立
        if (instance == null)
        {
            instance = this;
            if (DontDestroyEnabled)
            {
                // Sceneを遷移してもオブジェクトが消えないようにする
                DontDestroyOnLoad(gameObject);
            }
        }
        else
        {
            // 既存のインスタンスがある場合は、新しいインスタンスを破棄
            Destroy(gameObject);
        }

        // AudioSourceの取得
        bgmAudioSource = GetComponent<AudioSource>();
    }

    // BGM再生
    public void PlayBGM(AudioClip bgmClip)
    {
        bgmAudioSource.clip = bgmClip;
        bgmAudioSource.Play();
    }

    // BGM停止
    public void StopBGM()
    {
        bgmAudioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        // 何か追加の処理が必要な場合はここに追加
    }
}
