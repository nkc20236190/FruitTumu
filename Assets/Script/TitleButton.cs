using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class TitleButton : MonoBehaviour
{
    public string SceneName;
    public FadeoneScene FadeoneScene;
    //private AudioSource audioSource; // �{�^���̃I�[�f�B�I�\�[�X


    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            //// ���ʉ����Đ�
            //audioSource.Play();
            // ��ʑJ��
            //SceneManager.LoadScene(SceneName);
            FadeoneScene.LoadScene(SceneName);
        });
    }

    // Update is called once per frame
    void Update()
    {
        //audioSource = GetComponent<AudioSource>();
    }
}
