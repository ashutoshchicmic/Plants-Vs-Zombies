using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class buttonAlpha : MonoBehaviour
{
    public Image image;
    public void Start()
    {
        StartCoroutine(IncreaseAlphaOverTime());
    }
    IEnumerator IncreaseAlphaOverTime()
    {
        float alpha = 0f;
        while (alpha < 1f)
        {
            alpha += Time.deltaTime / 2f;
            image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
            yield return null;
        }
    }
    public void changeSeen()
    {
        SceneManager.LoadScene(1);
    }
}


