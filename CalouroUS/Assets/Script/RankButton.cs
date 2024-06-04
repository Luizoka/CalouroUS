using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankButton : MonoBehaviour
{
    public GameObject TimeCounterObject;
    public GameObject rankButtonObject;
    public GameObject rankLayoutObject;

    public void OnButtonClick()
    {
        rankButtonObject.SetActive(false);
        TimeCounterObject.SetActive(false);
        rankLayoutObject.SetActive(true);
    }
}
