using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // BGM
    [SerializeField] AudioSource audioSourceBGM = default;
    [SerializeField] AudioClip[] audioClips = default;
    //SE
    [SerializeField] AudioSource audioSourceSE = default;
    [SerializeField] AudioClip[] seClips = default;

    public enum BGM
    {
        Title,
        Game
    }

    public enum SE
    {
        Touch,//フルーツに触れたときの音
        Destroy,//フルーツを破壊したときの音
    }

    public static SoundManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayBGM(BGM.Game);
    }

    public void PlayBGM(BGM bgm)
    {
        audioSourceBGM.clip = audioClips[(int)bgm];
        audioSourceBGM.Play();
    }

    public void PlaySE(SE se)
    {
        audioSourceSE.PlayOneShot(seClips[(int)se]);
    }
}
