using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titlebgm : MonoBehaviour
{
    public AudioClip titleBGM; // TitleScene�p��BGM

    private void Start()
    {
        // TitleScene�Ɉړ������ۂ�BGM���Đ�
        if (MainSoundScript.instance != null)
        {
            MainSoundScript.instance.PlayBGM(titleBGM);
        }
    }
}
