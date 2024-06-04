using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameMenuScript : MonoBehaviour
{
    public GameObject Canvas1;
    public GameObject menuHowToPlayBG;
    public GameObject menuOptionBG;
    public GameObject menuCreditBG;
    public GameObject menuBack;

   public void OnButtonClick(GameObject clickedButton)
    {
        // Coloque aqui a lógica que você deseja executar ao clicar no botão
        string buttonName = clickedButton.name;

        if(buttonName == "menuButton"){
            SceneManager.LoadScene("Cena Principal");
        }
        if(buttonName == "menuButton (1)"){
            Canvas1.SetActive(false);
            menuHowToPlayBG.SetActive(true);
            menuBack.SetActive(true);
        }
        if(buttonName == "menuButton (2)"){
            Canvas1.SetActive(false);
            menuOptionBG.SetActive(true);
            menuBack.SetActive(true);
        }
        if(buttonName == "menuButton (3)"){
            Canvas1.SetActive(false);
            menuCreditBG.SetActive(true);
            menuBack.SetActive(true);
        }
        if(buttonName == "menuBackButton")
        {
            menuBack.SetActive(false);
            Canvas1.SetActive(true);
            menuHowToPlayBG.SetActive(false);
            menuOptionBG.SetActive(false);
            menuCreditBG.SetActive(false);
        }
        

        // Exemplo de mudança de cena
        //SceneManager.LoadScene("NomeDaCenaDesejada");
    }
}
