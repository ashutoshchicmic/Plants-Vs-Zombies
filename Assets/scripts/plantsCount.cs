using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class plantsCount : MonoBehaviour
{
    public int plantCount;
    [SerializeField]Text gameOverText;
    bool invokeBool = false;
    coinsGenerator cg;
    int zombieCount = 0;
    public Text zombieAttcked;
    public int zombiesCount=3;
    [SerializeField] GameObject endPanel;

    private void Start()
    {
        cg = FindAnyObjectByType<coinsGenerator>();
        Invoke("gameOver", 35f);
    }
    public void incPlant()
    {
        ++plantCount;
        print("incPlant " + plantCount);
    }
    public void decPlant()
    {
        --plantCount;
        print("decPlant " + plantCount);
    }
    void gameOver()
    {
        if (plantCount < 1)
        {
            gameOverFunc();
        }
        else
        {
            invokeBool = true;
        }
    }
    private void Update()
    {
        if(invokeBool && plantCount < 1)
        {
            gameOverFunc();
        }
    }
    void gameOverFunc()
    {
        gameOverText.text = "Game Over";
        gameOverText.gameObject.SetActive(true);
        endPanelActive();
        cg.points = 0;
        Time.timeScale = 0;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            print("zombie triggered with target");
            ++zombieCount;
            if (zombieCount > zombiesCount)
            {
                gameOverFunc();
                gameOverText.gameObject.SetActive(false);
                zombieAttcked.gameObject.SetActive(true);
                endPanelActive();
                zombieAttcked.text = "More than "+zombiesCount+" Zombies Attacked ur house.";
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Zombie"))
        {
            --zombieCount;
        }
    }
    void endPanelActive()
    {
        print("EndPanel is Comig");
        endPanel.SetActive(true);
    }
}
