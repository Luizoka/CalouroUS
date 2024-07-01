using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class logicMath : MonoBehaviour
{
    public InputField inputField;
    public static string savedValue;

    void Start() {
        if (inputField == null) {
            Debug.LogError("InputField não atribuído no Inspector.");
            return;
        }

        if (!string.IsNullOrEmpty(savedValue))
            inputField.text = savedValue;

        inputField.onEndEdit.AddListener(SaveValue);
    }

    public void SaveValue(string newValue) {
        if(newValue == "12")
        {
            Debug.Log("Você acertou !: " + newValue);
        }
    }
}
