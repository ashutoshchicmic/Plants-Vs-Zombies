using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnZomies : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float spawnDelay = 2f;
    public Transform[] spawnZone;
    public Text timeText;
    public Text wonText;
    public GameObject[] plantsImages;
    float timer = 30;
    bool countSeconds=false;
    [SerializeField] GameObject endPanel;
    void Start()
    {
        Time.timeScale = 1f;
        Invoke("startSpawning", 10f);
    }
    private void Update()
    {
        if (countSeconds)
        {
            timer -= Time.deltaTime;
            if (timer < 0f)
            {
                countSeconds = false;
                changeText();
            }
            timeText.text = "Remain On Ground for " + (Mathf.RoundToInt(timer)).ToString() + " sec";
        }
    }
    void startSpawning()
    {
        StartCoroutine(SpawnZombies());
        InvokeRepeating("changeTime", 5f, 15f);
    }
    void changeTime()
    {
        if (spawnDelay > 1.9f)
            spawnDelay -= Time.deltaTime * 30f;
        else
        {
            spawnDelay = 0.2f;
            timeText.gameObject.SetActive(true);
            countSeconds = true;
        }
        print("spawnDelay " + spawnDelay);
    }
    IEnumerator SpawnZombies()
    {
        while (true)
        {
            int ran = Random.Range(0, spawnZone.Length);
            Vector3 spawnPosition = new Vector3(
            Random.Range(spawnZone[ran].position.x - spawnZone[ran].localScale.x / 2, spawnZone[ran].position.x + spawnZone[ran].localScale.x / 2),
            Random.Range(spawnZone[ran].position.y - spawnZone[ran].localScale.y / 2, spawnZone[ran].position.y + spawnZone[ran].localScale.y / 2),
            0
        );
            GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            zombie.gameObject.transform.Rotate(0, 180, 0);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    void changeText()
    {
            foreach(GameObject plant in plantsImages)
            {
                plant.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
            timeText.gameObject.SetActive(false);
            wonText.gameObject.SetActive(true);
        //Invoke("endPanelActive", 3f);
        endPanelActive();
        Time.timeScale = 0;
            FindAnyObjectByType<coinsGenerator>().points=0;
            CancelInvoke("changeText");
       
    }
    void endPanelActive()
    {
        print("EndPanel is Comig");
        endPanel.SetActive(true);
    }
}
