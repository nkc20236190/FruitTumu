using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titlebgm : MonoBehaviour
{
    public AudioClip titleBGM; // TitleScene用のBGM

    private void Start()
    {
        // TitleSceneに移動した際にBGMを再生
        if (MainSoundScript.instance != null)
        {
            MainSoundScript.instance.PlayBGM(titleBGM);
        }
    }
}
