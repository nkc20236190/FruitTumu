using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneBotton : MonoBehaviour
{
    public string SceneName;


    //// �{�^�����N���b�N���ꂽ�Ƃ��̏���
    //private void OnButtonClicked()
    //{
    //    // 0.5�b�̒x�������R���[�`�����J�n
    //    StartCoroutine(LoadSceneWithDelay());
    //}

    //// 0.5�b�x�������R���[�`��
    //private IEnumerator LoadSceneWithDelay()
    //{
    //    // 0.5�b�҂�
    //    yield return new WaitForSeconds(0.5f);
    //}

        // Start is called before the first frame update
        void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneName);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
