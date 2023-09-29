using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bom : MonoBehaviour
{
    public AudioSource audioSource; // ボムのオーディオ ソース
    public AudioClip bombSound;     // ボムの音声クリップ
    public SpriteRenderer spriteRenderer; // BomオブジェクトのSpriteRenderer


    private bool hasBeenClicked = false;

    /// <summary>
    /// MouseDownイベント
    /// </summary>
    private void OnMouseDown()
    {
        if (!hasBeenClicked)
        {
            hasBeenClicked = true;
            Level.Instance.BomDown(this);

            if (audioSource != null && !audioSource.isPlaying)
            {
                audioSource.clip = bombSound;
                audioSource.Play();
                // オーディオを再生したら表示をONにする
                audioSource.enabled = true;

                // ボムのSpriteRendererを非表示にする
                if (spriteRenderer != null)
                {
                    spriteRenderer.enabled = false;
                }

                // 効果音を再生したら一定時間後にこのオブジェクトを破棄するコルーチンを開始
                StartCoroutine(StopAudioAfterDelay());
            }
        }
    }

    // 2秒後にオーディオを停止し、SpriteRendererを再表示するコルーチン
    private IEnumerator StopAudioAfterDelay()
    {
        yield return new WaitForSeconds(2.0f); // 2秒待つ

        // オーディオを停止
        audioSource.Stop();

        // 表示をOFFにする
        audioSource.enabled = false;

        // ボムのSpriteRendererを再表示する
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
    }
}
