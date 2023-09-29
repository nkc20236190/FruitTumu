using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadetwoScene : MonoBehaviour
{
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        // アニメーターを無効にする
        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    public void PlayAnimation()
    {
        if (animator != null)
        {
            // デバッグログを追加
            Debug.Log("PlayAnimation called");
            // ボタンが押されたときにアニメーターを有効にし、アニメーションを再生
            animator.enabled = true;
            animator.Play("YourAnimationName"); // アニメーション名を指定して再生
        }
    }

    public bool IsAnimationPlaying()
    {
        // アニメーションが再生中かどうかを返す
        return animator != null && animator.enabled && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;
    }
}
