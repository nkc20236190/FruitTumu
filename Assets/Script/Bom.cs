using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour
{
    public AudioSource audioSource; // ボムのオーディオ ソース
    public AudioClip bombSound;     // ボムの音声クリップ

    /// <summary>
    /// MouseDownイベント
    /// </summary>
    private void OnMouseDown()
    {
        Level.Instance.BomDown(this);
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.clip = bombSound;
            audioSource.Play();
        }
    }
}
