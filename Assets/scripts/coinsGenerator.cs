using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class coinsGenerator : MonoBehaviour
{
    
    public GameObject coinPrefab;
    public float generationInterval = 1.0f;
    public Rect spawnArea;
    public int points = 50;
    [SerializeField] Text coins;
    private void Start()
    {
        coins.text = points.ToString();
        InvokeRepeating("GenerateCoin", 2f, 2f);
    }

    private void GenerateCoin()
    {
        float x = Random.Range(spawnArea.xMin, spawnArea.xMax);
        float y = Random.Range(spawnArea.yMin, spawnArea.yMax);
        Vector3 spawnPosition = new Vector3(x, y, 0f);

        GameObject coin = Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }
    public void increasePoints()
    {
        points += 5;
        Debug.Log(coins == null);
        coins.text = points.ToString();
    }
    public void decreasePoints(int decPoints)
    {
        points -= decPoints;
        coins.text = points.ToString();
    }

}

