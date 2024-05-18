using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;

public class BinaryConversion : MonoBehaviour
{
    public GameObject RedButton;
    [SerializeField] private bool isOn = false;
    int[,] matrix = {
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0},
        {0, 0, 0, 0, 0, 0, 0, 0}
    };
    [SerializeField] private TextMeshProUGUI wishedNumber0;
    [SerializeField] private TextMeshProUGUI wishedNumber1;
    [SerializeField] private TextMeshProUGUI wishedNumber2;
    [SerializeField] private TextMeshProUGUI wishedNumber3;
    [SerializeField] private TextMeshProUGUI decimalNumber0;
    [SerializeField] private TextMeshProUGUI decimalNumber1;
    [SerializeField] private TextMeshProUGUI decimalNumber2;
    [SerializeField] private TextMeshProUGUI decimalNumber3;

    void Start()
    {
        System.Random random = new System.Random();

        wishedNumber0.text = random.Next(256).ToString();
        Debug.Log("Olha aqui: " + wishedNumber0.text);
        wishedNumber1.text = random.Next(256).ToString();
        wishedNumber2.text = random.Next(256).ToString();
        wishedNumber3.text = random.Next(256).ToString();
  
    }

    private void OnMouseUp()
    {
        isOn = !isOn;
        RedButton.SetActive(!isOn);
        Debug.Log("Drag ended!");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                Debug.Log("CLICKED " + hit.collider.name);
                UpdateTheMatrix(hit.collider.name);
            }
        }
    }

    private void UpdateTheMatrix(string name)
    {
        Match match = Regex.Match(name, @"\((\d+)\)");
        Debug.Log("Olha o name: " + name);
        Debug.Log("Olha o match: " + match);
        if (match.Success)
        {
            string numberStr = match.Groups[1].Value;
            Debug.Log("Olha o numberStr: " + numberStr);
            int number = int.Parse(numberStr);

            if (number <= 7)
            {
                matrix[0, number] = matrix[0, number] > 0 ? 0 : 1;
                string binaryNumber = "";
                for(int i = 0; i <= 7; i++){binaryNumber += matrix[0, i];}
                binaryConversionDisplay(binaryNumber, 0);
            }
            else if (number >= 8 && number <= 15)
            {
                matrix[1, number - 8] = matrix[1, number - 8] > 0 ? 0 : 1;
                string binaryNumber = "";
                for(int i = 8; i <= 15; i++){binaryNumber += matrix[1, i - 8];}
                Debug.Log("Tá chegando aqui ?" + number);
                Debug.Log("Olha a função sendo chamada");
                binaryConversionDisplay(binaryNumber, 1);
            }
            else if (number >= 16 && number <= 23)
            {
                matrix[2, number-16] = matrix[2, number -16] > 0 ? 0 : 1;
                string binaryNumber = "";
                for(int i = 16; i <= 23; i++){binaryNumber += matrix[2, i - 16];}
                binaryConversionDisplay(binaryNumber, 2);
            }
            else if (number >= 24 && number <= 31)
            {
                matrix[3, number-24] = matrix[3, number-24] > 0 ? 0 : 1;
                string binaryNumber = "";
                for(int i = 24; i <= 31; i++){binaryNumber += matrix[3, i - 24];}
                binaryConversionDisplay(binaryNumber, 3);
            }
        }
    }
    private void binaryConversionDisplay(string binaryNumber, int rowId)
    {
        int decimalNumberDisplay = Convert.ToInt32(binaryNumber, 2);
        if(rowId == 0){
            decimalNumber0.text = decimalNumberDisplay.ToString();
        }
        else if(rowId == 1)
        {
            decimalNumber1.text = decimalNumberDisplay.ToString();
            Debug.Log("Cheguei aqui !" + binaryNumber);
        }
        else if(rowId == 2)
        {
            decimalNumber2.text = decimalNumberDisplay.ToString();
        }
        else if(rowId == 3)
        {
            decimalNumber3.text = decimalNumberDisplay.ToString();
        }
    }
}
