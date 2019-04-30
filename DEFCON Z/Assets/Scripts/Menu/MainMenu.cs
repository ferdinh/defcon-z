using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Scene testLevel;
    public bool settings;
    public GameObject settingsPanel, menuPanel;
    [SerializeField]
    private Vector3 panelPosition, panelHiddenPosition;
    //private AssetBundle levels;

    // Start is called before the first frame update
    void Start()
    {
        panelPosition = settingsPanel.transform.position;
        panelHiddenPosition = panelPosition;
        panelHiddenPosition.x = Screen.width * 2;

        settingsPanel.transform.position = panelHiddenPosition;

        settings = false;
        //levels = AssetBundle.LoadFromFile("Assets/AssetBundles/Scenes");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void toggleSettings()
    {
        settings = (settings) ? false : true;

        if (settings)
        {
            settingsPanel.transform.position = panelPosition;
            menuPanel.transform.position = panelHiddenPosition;
        } else
        {
            settingsPanel.transform.position = panelHiddenPosition;
            menuPanel.transform.position = panelPosition;
        }
    }

    public void SettingsButton()
    {
        SceneManager.LoadScene("ObjectSelectionScene");
    }
    /// <summary>
    /// 
    /// </summary>
    public void PlayGame()
    {
        // Scene must be added to build settings to be loaded
        SceneManager.LoadScene("ObjectSelectionScene");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game!");
        Application.Quit();
    }
}
