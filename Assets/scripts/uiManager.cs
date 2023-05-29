using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class uiManager : MonoBehaviour
{
    public Text timeText;
    public Text gameStart;
   
    int time = 10;
    void Start()
    {
        gameStart.gameObject.SetActive(true);
        InvokeRepeating("changeText", 1f, 1f);
    }

    void changeText()
    {
        --time;
        print(time);
        if (time < 1)
        {
            //gameStart.gameObject.SetActive(false);
            gameStart.text = "Zombies are coming";
            CancelInvoke("changeText");
            Destroy(gameStart.gameObject, 1.5f);
        }
        else
        gameStart.text = "Plant ur Plants: "+time.ToString()+ " sec";
    }
    public void RestartButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
    public void QuitButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
