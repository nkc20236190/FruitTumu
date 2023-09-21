using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour
{
    public AudioSource audiosource; // AudioSourceコンポーネントへの参照

    private void Start()
    {
        // audiosourceを初期化（Inspectorパネルで設定されているはず）
        audiosource = GetComponent<AudioSource>();
    }

    public void PushStageSelectButton(int stageNo)
    {
        // audiosourceを使用して音を再生
        audiosource.Play();
        StartCoroutine("LoadGameScene", stageNo);
    }

    IEnumerator LoadGameScene(int stageNo)
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("GameStage" + stageNo);
    }
}
