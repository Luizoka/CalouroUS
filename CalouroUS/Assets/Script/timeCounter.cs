using UnityEngine;
using UnityEngine.UI; // Para exibir o tempo em uma UI, se necessário

public class TimeCounter : MonoBehaviour
{
    public static TimeCounter Instance { get; private set; }

    public float elapsedTime = 0f;
    public Text timeText; 

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (timeText != null)
        {
            timeText.text = $"{elapsedTime:F2} seconds";
        }
    }
}
