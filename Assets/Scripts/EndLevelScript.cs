using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelScript : MonoBehaviour
{
    public int levelNum;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))//only end level when player enters the end zone
        {
            int levelsBeat = LevelSaveLoader.LoadLevelsBeat();
            if(levelNum > levelsBeat)
            {
                LevelSaveLoader.SaveLevelBeat(levelNum);
            }
            SceneManager.LoadScene("Main_Menu");
        }
    }
}
