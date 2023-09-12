using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HowtoPlayButton : MonoBehaviour
{
    public string SceneName;

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
