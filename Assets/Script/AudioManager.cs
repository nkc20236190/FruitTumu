using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public static AudioManager Instance
    {
        get { return instance; }
    }

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // シーン遷移時にオブジェクトを破棄しないようにする
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject); // 他のインスタンスが存在する場合、これを破棄する
        }
    }

    public void PlaySound(AudioClip soundClip)
    {
        audioSource.PlayOneShot(soundClip);
    }
}
