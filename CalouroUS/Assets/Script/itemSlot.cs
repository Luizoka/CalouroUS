using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

public class itemSlot : MonoBehaviour, IDropHandler
{
   public int slotId;

   public static int[] encaixes = new int[6] { 0, 0, 0, 0, 0, 0 };
   
   public void OnDrop(PointerEventData eventData)
   {
    Debug.Log("Nome do objeto: " + eventData.pointerDrag.name);
    Debug.Log("OnDrop");

    if (eventData.pointerDrag != null)
    {
        string fullName = eventData.pointerDrag.name;

        Match match = Regex.Match(fullName, @"^([^\d]+)\((\d+)\)$");
        Debug.Log(fullName);
        if (match.Success)
        {
            string objectName = match.Groups[1].Value; // Parte do nome sem números
            string objectNumber = match.Groups[2].Value; // Parte do número

            int objectNumberInt = int.Parse(objectNumber);
        
            encaixes[slotId] = (slotId == objectNumberInt) ? 1 : 0;
            
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            Debug.Log("Encaixes: " + string.Join(", ", encaixes));

            checkGameCompletion();
        }
        else
        {
            Debug.Log("Formato do nome do objeto inesperado: " + fullName);
        }
    }
   }

   private void checkGameCompletion()
   {
        bool allEncaixesCorrect = true;
        foreach (int value in encaixes)
        {
            if (value != 1)
            {
                allEncaixesCorrect = false;
                break;
            }
        }

        // Se todos os valores são 1, o jogo termina
        if (allEncaixesCorrect)
        {
            Debug.Log("Parabéns! Você completou o jogo!");
            // Adicione aqui a lógica adicional para quando o jogo for completado
        }
   }
}
