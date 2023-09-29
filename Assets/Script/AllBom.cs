using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBom : MonoBehaviour
{
    public AudioClip explosionSound; // 爆発音のためのオーディオクリップ

    private void OnMouseDown()
    {
        Level.Instance.AllBomDown(this); // 全部ボムの効果を実行

        // 効果音を再生
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && explosionSound != null)
        {
            audioSource.clip = explosionSound;
            audioSource.Play();
        }
    }
}
