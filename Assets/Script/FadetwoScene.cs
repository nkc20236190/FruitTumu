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
        // �A�j���[�^�[�𖳌��ɂ���
        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    public void PlayAnimation()
    {
        if (animator != null)
        {
            // �f�o�b�O���O��ǉ�
            Debug.Log("PlayAnimation called");
            // �{�^���������ꂽ�Ƃ��ɃA�j���[�^�[��L���ɂ��A�A�j���[�V�������Đ�
            animator.enabled = true;
            animator.Play("YourAnimationName"); // �A�j���[�V���������w�肵�čĐ�
        }
    }

    public bool IsAnimationPlaying()
    {
        // �A�j���[�V�������Đ������ǂ�����Ԃ�
        return animator != null && animator.enabled && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;
    }
}
