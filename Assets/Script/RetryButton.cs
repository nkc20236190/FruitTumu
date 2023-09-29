using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RetryButton : MonoBehaviour
{
    public FadetwoScene FadetwoScene;

    // Start is called before the first frame update
    void Start()
    {
        // RetryButton���N���b�N���ꂽ�Ƃ��Ɏ��s����郊�X�i�[��ǉ�
        GetComponent<Button>().onClick.AddListener(OnRetryButtonClick);
    }

    // RetryButton���N���b�N���ꂽ�Ƃ��̏���
    void OnRetryButtonClick()
    {
        // FadetwoScene�̃A�j���[�V�������Đ�
        FadetwoScene.PlayAnimation();

        // �A�j���[�V�����̍Đ������������猻�݂̃V�[�����ēǂݍ���
        StartCoroutine(LoadSceneAfterAnimation());
    }

    IEnumerator LoadSceneAfterAnimation()
    {  
        // FadetwoScene��Animator�������ɂȂ�܂őҋ@
        while (FadetwoScene.IsAnimationPlaying())
        {
            yield return null;
        }


        // ���݂̃V�[�����ēǂݍ���
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
