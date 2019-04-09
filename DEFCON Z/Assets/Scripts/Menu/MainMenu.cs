using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Scene testLevel;
    //private AssetBundle levels;

    // Start is called before the first frame update
    void Start()
    {
        //levels = AssetBundle.LoadFromFile("Assets/AssetBundles/Scenes");
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("CameraTestScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
