using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Titlebgm : MonoBehaviour
{
    public AudioClip titleBGM; // TitleScene—p‚ÌBGM

    private void Start()
    {
        // TitleScene‚ÉˆÚ“®‚µ‚½Û‚ÉBGM‚ğÄ¶
        if (MainSoundScript.instance != null)
        {
            MainSoundScript.instance.PlayBGM(titleBGM);
        }
    }
}
