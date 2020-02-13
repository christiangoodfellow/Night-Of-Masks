using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{

    public GameObject ResetPanel;
    public GameObject LevelPanel;
    public GameObject MainPanel;
    public GameObject Dont_Reset;
    public GameObject LevelSelect;
    public GameObject Back_Button;

    public GameObject level2Button;
    public GameObject level3Button;


    private float levelsBeat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        LevelPanel.SetActive(false);
        ResetPanel.SetActive(false);
        MainPanel.SetActive(true);
        EventSystem es = EventSystem.current;
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(LevelSelect);

    }

    public void ResetButton()
    {
        EventSystem es = EventSystem.current;
        ResetPanel.SetActive(true);
        MainPanel.SetActive(false);
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(Dont_Reset);
    }

    public void DontReset()
    {
        EventSystem es = EventSystem.current;
        ResetPanel.SetActive(false);
        MainPanel.SetActive(true);
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(LevelSelect);
    }

    public void LevelSelectButton()
    {
        LevelPanel.SetActive(true);
        MainPanel.SetActive(false);
        EventSystem es = EventSystem.current;
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(Back_Button);
        levelsBeat = LevelSaveLoader.LoadLevelsBeat();
        if (levelsBeat >= 2)
        {
            level2Button.SetActive(true);
            level3Button.SetActive(true);
        }
        else if (levelsBeat >= 1)
        {
            level2Button.SetActive(true);
            level3Button.SetActive(false);
        }
        else
        {
            level2Button.SetActive(false);
            level3Button.SetActive(false);
        }
    }

    public void BackButton()
    {
        LevelPanel.SetActive(false);
        MainPanel.SetActive(true);
        EventSystem es = EventSystem.current;
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(LevelSelect);
    }

    public void Level1Button()//Load levels depending on what buttom is pressed
    {
        SceneManager.LoadScene("Level_1");
    }

    public void Level2Button()
    {
        SceneManager.LoadScene("Level_2");
    }

    public void Level3Button()
    {
        //SceneManager.LoadScene("Level_3");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void ResetLevelsBeat()
    {
        int resetnum = 0;
        LevelSaveLoader.SaveLevelBeat(resetnum);
        EventSystem es = EventSystem.current;
        ResetPanel.SetActive(false);
        MainPanel.SetActive(true);
        es.SetSelectedGameObject(null);
        es.SetSelectedGameObject(LevelSelect);
    }

}
