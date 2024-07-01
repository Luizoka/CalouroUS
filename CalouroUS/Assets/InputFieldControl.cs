using UnityEngine;
using UnityEngine.UI;

public class InputFieldControl : MonoBehaviour {

    public int fieldId;
    public InputField inputField;
    public static string savedValue;
    public static string[] rightAnswers = new string[7] { "A N (B U C)", "A V B V C'", "B V (A U C)'", "A V B' V C",  "A V B V C", "A' V B V C", "C V (A U B)'"};
    public static int limit = 0;

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
        savedValue = newValue;
        if(savedValue == rightAnswers[fieldId])
        {
           limit++;
           answerCheck(limit);
        }
        else{
            limit--;
        }
        Debug.Log("Olha o limit: " + limit);
        Debug.Log("Valor salvo: " + savedValue);
    }

    public void answerCheck(int limit){
        if(limit == 7)
        {
            Debug.Log("Você ganhou !");
        }
    }
}
