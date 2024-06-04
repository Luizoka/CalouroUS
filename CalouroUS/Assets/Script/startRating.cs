using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startRating : MonoBehaviour
{
    private float elapsedTime;
    int[] secondsArray = {0, 60, 120, 180, 240, 300};
    public Text rankTimeText;
    public Text starnumber;

    void Start()
    {
        elapsedTime = TimeCounter.Instance.elapsedTime;
        rankTimeText.text = $"{elapsedTime:F2} seconds";
        Debug.Log("O que ta aparecendo aqui ?: " + CalculateStars(elapsedTime));
        DisplayStars(CalculateStars(elapsedTime));
    }

    public int CalculateStars(float time)
    {
       int intTime = Mathf.FloorToInt(time);
       for(int i = secondsArray.Length - 1; i >= 0; i--)
       {
            if(intTime > secondsArray[i]){
                Debug.Log("Tá passando aqui ?: " + intTime);
                return 5 - i;
            }
       }
       return 0;
    }

    public void DisplayStars(int starsNumbers)
    {
        starnumber.text = $"Numero de estrelas: {starsNumbers}";

        GameObject rankLayoutObject = GameObject.Find("rankLayoutObject");

        if (rankLayoutObject == null)
        {
            Debug.LogError("rankLayoutObject not found!");
            return;
        }

        GameObject canvasObject = rankLayoutObject.transform.Find("Canvas")?.gameObject;

        if (canvasObject == null)
        {
            Debug.LogError("Canvas object not found!");
            return;
        }

        GameObject layoutObject = canvasObject.transform.Find("layout")?.gameObject;

        if (layoutObject == null)
        {
            Debug.LogError("Layout object not found!");
            return;
        }

        for (int i = 1; i <= starsNumbers; i++)
        {
            GameObject star = layoutObject.transform.Find($"star ({i})")?.gameObject;
            Debug.Log("Olha star: " + star);
            if (star != null)
            {
                star.SetActive(true);
            }
            else
            {
                Debug.LogWarning($"Star 'star ({i})' not found!");
            }
        }
    }
}
