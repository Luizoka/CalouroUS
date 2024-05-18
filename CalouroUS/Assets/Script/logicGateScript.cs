using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class logicGateScript : MonoBehaviour
{
    public GameObject LogicGateButton;
    [SerializeField] private bool isOn = false;
    int[] buttons = {0, 0, 0, 0, 0, 0, 0, 0};
    Action[] functions;
     // Inicialização do array com tamanho 8
    GameObject parentObject;
    void Start()
    {
        parentObject = GameObject.Find("logicGate");

        GameObject task_op_10 = parentObject.transform.Find("task op (10)").gameObject;
        if (task_op_10 != null){task_op_10.SetActive(true);}

        GameObject task_op_15 = parentObject.transform.Find("task op (15)").gameObject;
        if ( task_op_15 != null){task_op_15.SetActive(true);}

        GameObject task_op_16 = parentObject.transform.Find("task op (16)").gameObject;
        if ( task_op_16 != null){task_op_16.SetActive(true);}

        GameObject task_op_14 = parentObject.transform.Find("task op (14)").gameObject;
        if ( task_op_14 != null){task_op_14.SetActive(true);}
    }

    private void OnMouseUp()
    {
        isOn = !isOn;
        LogicGateButton.SetActive(!isOn);
        Debug.Log("Drag ended!");
    }

    void Update()
    {
        functions = new Action[8];

        // Atribuição das referências das funções aos elementos do array
        functions[0] = NOT;
        functions[1] = OR;
        functions[2] = OR;
        functions[3] = AND;
        functions[4] = NOR;
        functions[5] = NOR;
        functions[6] = NAND;
        functions[7] = NOT;

         if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                Debug.Log("CLICKED " + hit.collider.name);
                LogicGates(hit.collider.name, functions);
                Debug.Log("Conteúdo do array buttons: [" + string.Join(", ", buttons) + "]");
            }
        }
    }

    public void LogicGates(string name, Action[] functions)
    {
         Match match = Regex.Match(name, @"\((\d+)\)");

         if(match.Success){
            string numberStr = match.Groups[1].Value;
            Debug.Log("Olha o numberStr: " + numberStr);
            int number = int.Parse(numberStr);

            buttons[number] = buttons[number] > 0 ? 0 : 1;
            functions[number]();
         }
    }

    public void NOT()
    {
       if(buttons[0] == 0) 
       {
            AND();  
            GameObject task_op_10 = parentObject.transform.Find("task op (10)").gameObject;
            if (task_op_10 != null){task_op_10.SetActive(true);}

            GameObject task_op_8 = parentObject.transform.Find("task op (8)").gameObject;
            if ( task_op_8 != null){task_op_8.SetActive(false);}
       }
       if(buttons[0] == 1)
       {
         GameObject task_op_10 = parentObject.transform.Find("task op (10)").gameObject;
         if (task_op_10 != null){task_op_10.SetActive(false);}

         GameObject task_op_8 = parentObject.transform.Find("task op (8)").gameObject;
         if ( task_op_8 != null){task_op_8.SetActive(true);}

       }
       if(buttons[7] == 0)
       {
           NAND();
           GameObject task_op_16 = parentObject.transform.Find("task op (16)").gameObject;
           if ( task_op_16 != null){task_op_16.SetActive(true);}

           GameObject task_op_7 = parentObject.transform.Find("task op (7)").gameObject;
           if ( task_op_7 != null){task_op_7.SetActive(false);}
       }

       if(buttons[7] == 1)
       {
          GameObject task_op_16 = parentObject.transform.Find("task op (16)").gameObject;
          task_op_16.SetActive(false);

          GameObject task_op_7 = parentObject.transform.Find("task op (7)").gameObject;
          task_op_7.SetActive(true);
       }
    }

    public void AND()
    {
          if(buttons[0] == 0)
          {
             GameObject parentObject = GameObject.Find("logicGate");
             GameObject task_op_10 = parentObject.transform.Find("task op (10)").gameObject;
             if (task_op_10 != null){task_op_10.SetActive(true);}

            if(!(buttons[1] == 0 && buttons[2] == 0) && buttons[3] == 1 )
            {
                GameObject task_op_12 = parentObject.transform.Find("task op (12)").gameObject;
                if (task_op_12 != null){task_op_12.SetActive(true);} 
            }
            if(buttons[3] == 1)
            {
                GameObject task_op_3 = parentObject.transform.Find("task op (3)").gameObject;
                if (task_op_3 != null){task_op_3.SetActive(true);} 
                XOR();
            }
            if((buttons[3] == 0) || (buttons[1] == 0 && buttons[2] == 0))
            {
                GameObject task_op_3 = parentObject.transform.Find("task op (3)").gameObject;
                if (task_op_3 != null){task_op_3.SetActive(false);} 

                GameObject task_op_12 = parentObject.transform.Find("task op (12)").gameObject;
                if (task_op_12 != null){task_op_12.SetActive(false);}
                XOR(); 
            }
          }
    }

    public void NAND()
    {
       if(buttons[7] == 0 && buttons[6] == 1)
       {
         GameObject parentObject_1 = GameObject.Find("logicGate");
         GameObject task_op_7 = parentObject_1.transform.Find("task op (7)").gameObject;
         if ( task_op_7 != null){task_op_7.SetActive(false);}

         GameObject parentObject_2 = GameObject.Find("logicGate");
         GameObject task_op_15 = parentObject_2.transform.Find("task op (15)").gameObject;
         if (task_op_15 != null){task_op_15.SetActive(false);}

         GameObject parentObject_3 = GameObject.Find("logicGate");
         GameObject task_op_11 = parentObject_3.transform.Find("task op (11)").gameObject;
         if (task_op_11 != null){task_op_11.SetActive(true);}
       }
       else
       {
           GameObject parentObject_2 = GameObject.Find("logicGate");
           GameObject task_op_15 = parentObject_2.transform.Find("task op (15)").gameObject;
           task_op_15.SetActive(true);
           
            if(buttons[6] == 1)
            {
                GameObject parentObject = GameObject.Find("logicGate");
                GameObject task_op_11 = parentObject.transform.Find("task op (11)").gameObject;
                task_op_11.SetActive(true);
                if(buttons[7] == 1){
                    GameObject parentObject_3 = GameObject.Find("logicGate");
                    GameObject task_op_15_1 = parentObject_3.transform.Find("task op (15)").gameObject;
                    if ( task_op_15_1 != null){task_op_15_1.SetActive(true);}
                }
            }
            if(buttons[6] == 0)
            {
                GameObject parentObject = GameObject.Find("logicGate");
                GameObject task_op_11 = parentObject.transform.Find("task op (11)").gameObject;
                if ( task_op_11 != null){task_op_11.SetActive(false);}
            }
            XOR();
       }
    }

    public void OR()
    {
        if((buttons[1] == 1 && buttons[2] == 1) || (buttons[1] == 1 && buttons[2] == 0) || (buttons[1] == 0 && buttons[2] == 1))
        {
            if(buttons[0] == 0)
            {
             GameObject parentObject1 = GameObject.Find("logicGate");
             GameObject task_op_9 = parentObject1.transform.Find("task op (9)").gameObject;
             if (task_op_9 != null){task_op_9.SetActive(true);}
            }
            Debug.Log("Olá or");
            if(buttons[1] == 1)
            {
             GameObject parentObject1 = GameObject.Find("logicGate");
             GameObject task_op_1_ = parentObject1.transform.Find("task op (1)").gameObject;
             if (task_op_1_ != null){task_op_1_.SetActive(true);} 
            }
            if(buttons[1] == 0)
            {
             GameObject parentObject1 = GameObject.Find("logicGate");
             GameObject task_op_1_ = parentObject1.transform.Find("task op (1)").gameObject;
             if (task_op_1_ != null){task_op_1_.SetActive(false);} 
            }
            if(buttons[2] == 0)
            {
             GameObject parentObject1 = GameObject.Find("logicGate");
             GameObject task_op_2_ = parentObject1.transform.Find("task op (2)").gameObject;
             if (task_op_2_ != null){task_op_2_.SetActive(false);} 

            }
            if(buttons[2] == 1)
            {
             GameObject parentObject1 = GameObject.Find("logicGate");
             GameObject task_op_2_ = parentObject1.transform.Find("task op (2)").gameObject;
             if (task_op_2_ != null){task_op_2_.SetActive(true);} 
            }
             GameObject parentObject = GameObject.Find("logicGate");
             GameObject task_op_5 = parentObject.transform.Find("task op (5)").gameObject;
             if (task_op_5 != null){task_op_5.SetActive(true);} 
             AND();
        }
        else{
            Debug.Log("Tô aqui no or");
            GameObject parentObject = GameObject.Find("logicGate");
            GameObject task_op_5 = parentObject.transform.Find("task op (5)").gameObject;
            if (task_op_5 != null){task_op_5.SetActive(false);} 

            GameObject parentObject1 = GameObject.Find("logicGate");
            GameObject task_op_9 = parentObject1.transform.Find("task op (9)").gameObject;
            if (task_op_9 != null){task_op_9.SetActive(false);}
        }
    }

    public void NOR()
    {
        Debug.Log("Tô no nor");
        if(buttons[4] == 0 && buttons[5] == 0)
        {
           GameObject parentObject = GameObject.Find("logicGate");
           GameObject task_op_14 = parentObject.transform.Find("task op (14)").gameObject;
           if (task_op_14 != null){task_op_14.SetActive(true);} 

           GameObject parentObject_1 = GameObject.Find("logicGate");
           GameObject task_op_4_2 = parentObject_1.transform.Find("task op (4)").gameObject;
           if (task_op_4_2 != null){task_op_4_2.SetActive(false);} 

           GameObject parentObject_4 = GameObject.Find("logicGate");
           GameObject task_op_6 = parentObject_4.transform.Find("task op (6)").gameObject;
           if (task_op_6 != null){task_op_6.SetActive(false);}

           XOR();
        }
        else
        {
           GameObject parentObject = GameObject.Find("logicGate");
           GameObject task_op_14 = parentObject.transform.Find("task op (14)").gameObject;
           task_op_14.SetActive(false);
            
            if(buttons[4] == 1)
            {
               GameObject parentObject_1 = GameObject.Find("logicGate");
               GameObject task_op_4_2 = parentObject_1.transform.Find("task op (4)").gameObject;
               if (task_op_4_2 != null){task_op_4_2.SetActive(true);} 
            }
            if(buttons[5] == 1)
            {
               GameObject parentObject_2 = GameObject.Find("logicGate");
               GameObject task_op_6 = parentObject_2.transform.Find("task op (6)").gameObject;
               if (task_op_6 != null){task_op_6.SetActive(true);}
            }
            if(buttons[4] == 0)
            {
                Debug.Log("Cheguei aqui");
                GameObject parentObject_3 = GameObject.Find("logicGate");
                GameObject task_op_4_3 = parentObject_3.transform.Find("task op (4)").gameObject;
                if (task_op_4_3 != null){task_op_4_3.SetActive(false);} 
            }
            if(buttons[5] == 0)
            {
                GameObject parentObject_4 = GameObject.Find("logicGate");
                GameObject task_op_6 = parentObject_4.transform.Find("task op (6)").gameObject;
                if (task_op_6 != null){task_op_6.SetActive(false);}
            }
        }
    }

    public void XOR()
    {   

    GameObject xor1 = GameObject.Find("task op (12)");
    GameObject xor2 = GameObject.Find("task op (13)");
    GameObject xor3 = GameObject.Find("task op (14)");
    GameObject xor4 = GameObject.Find("task op (15)");

    GameObject parentObject = GameObject.Find("logicGate");

    if (parentObject != null)
    {
        GameObject lineLamp = parentObject.transform.Find("Lamp").gameObject;

        if (lineLamp != null)
        {
            if (xor1 != null || xor2 != null)
            {
                lineLamp.SetActive(true);
            }

            if (xor1 != null && xor2 != null)
            {
                lineLamp.SetActive(false);
            }
        }

        if (xor3 != null || xor4 != null)
        {
            GameObject task_op_10 = parentObject.transform.Find("task op (13)").gameObject;
            if (task_op_10 != null)
            {
                task_op_10.SetActive(true);
            }
        }

        if (xor3 != null && xor4 != null)
        {
            GameObject task_op_13 = parentObject.transform.Find("task op (13)").gameObject;
            if (task_op_13 != null)
            {
                task_op_13.SetActive(false);
            }
        }
    }
        
    }
    //NOT, AND, NAND, OR, NOR, XOr
}
