using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public AudioSource audiosource; // AudioSource�R���|�[�l���g�ւ̎Q��

    private void Start()
    {
        // audiosource���������iInspector�p�l���Őݒ肳��Ă���͂��j
        audiosource = GetComponent<AudioSource>();
    }

    public void PushStageSelectButton(int stageNo)
    {
        // audiosource���g�p���ĉ����Đ�
        audiosource.Play();
        StartCoroutine("LoadGameScene", stageNo);
    }

    IEnumerator LoadGameScene(int stageNo)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("GameStage" + stageNo);
    }
}
