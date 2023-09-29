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
        // RetryButtonがクリックされたときに実行されるリスナーを追加
        GetComponent<Button>().onClick.AddListener(OnRetryButtonClick);
    }

    // RetryButtonがクリックされたときの処理
    void OnRetryButtonClick()
    {
        // FadetwoSceneのアニメーションを再生
        FadetwoScene.PlayAnimation();

        // アニメーションの再生が完了したら現在のシーンを再読み込み
        StartCoroutine(LoadSceneAfterAnimation());
    }

    IEnumerator LoadSceneAfterAnimation()
    {  
        // FadetwoSceneのAnimatorが無効になるまで待機
        while (FadetwoScene.IsAnimationPlaying())
        {
            yield return null;
        }


        // 現在のシーンを再読み込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
