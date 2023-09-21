using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneBotton : MonoBehaviour
{
    public string SceneName;

    public FadeoneScene FadetwoScene;
    //// ボタンがクリックされたときの処理
    //private void OnButtonClicked()
    //{
    //    // 0.5秒の遅延を持つコルーチンを開始
    //    StartCoroutine(LoadSceneWithDelay());
    //}

    //// 0.5秒遅延を持つコルーチン
    //private IEnumerator LoadSceneWithDelay()
    //{
    //    // 0.5秒待つ
    //    yield return new WaitForSeconds(0.5f);
    //}

    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            //SceneManager.LoadScene(SceneName);
            FadetwoScene.LoadScene(SceneName);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
