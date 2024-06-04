using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using System;

public class logicGateScript : MonoBehaviour
{
    public GameObject LogicGateButton;
    [SerializeField] private bool isOn = false;
    int[] buttons = { 0, 0, 0, 0, 0, 0, 0, 0 };
    Action[] functions;
    GameObject parentObject;

    void Start()
    {
        parentObject = GameObject.Find("logicGate");

        GameObject task_op_10 = parentObject.transform.Find("task op (10)").gameObject;
        if (task_op_10 != null) { task_op_10.SetActive(true); }

        GameObject task_op_14 = parentObject.transform.Find("task op (14)").gameObject;
        if (task_op_14 != null) { task_op_14.SetActive(true); }

        GameObject task_op_15 = parentObject.transform.Find("task op (15)").gameObject;
        if (task_op_15 != null) { task_op_15.SetActive(true); }

        GameObject task_op_17 = parentObject.transform.Find("task op (17)").gameObject;
        if (task_op_17 != null) { task_op_17.SetActive(true); }

        XOR();
        NOT();
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
                //Debug.Log("CLICKED " + hit.collider.name);
                LogicGates(hit.collider.name, functions);
                //Debug.Log("Conteúdo do array buttons: [" + string.Join(", ", buttons) + "]");
            }
        }
    }

    public void LogicGates(string name, Action[] functions)
    {
        Match match = Regex.Match(name, @"\((\d+)\)");

        if (match.Success)
        {
            string numberStr = match.Groups[1].Value;
            Debug.Log("Olha o numberStr: " + numberStr);
            int number = int.Parse(numberStr);

            buttons[number] = buttons[number] > 0 ? 0 : 1;
            GameObject task_op_button = parentObject.transform.Find($"task op ({number})").gameObject;
            //Debug.Log("Olha o task op principal: " + task_op_button);
            if (task_op_button != null) { task_op_button.SetActive(!task_op_button.activeSelf); }
            functions[number]();
        }
    }

    public void buttonsActivate(int buttonNumber, bool activeState)
    {
        GameObject parentObject = GameObject.Find("logicGate");
        //Debug.Log("Funciona porra");
        GameObject task_op_ = parentObject.transform.Find($"task op ({buttonNumber})").gameObject;
        //Debug.Log("Tá pegando esse caralho dessa porra ?: " + task_op_);
        task_op_.SetActive(activeState);
    }

    public void NOT()
    {
        bool not1 = buttons[0] > 0 ? true : false;
        bool not3 = buttons[7] > 0 ? true : false;
        GameObject parentObject = GameObject.Find("logicGate");

        Debug.Log("not3: " + not3);

        if (parentObject != null)
        {
            //Debug.Log("Passou dessa parte ?");
            //Debug.Log("not1: " + not1);

            if (not1 == true)
            {
                buttonsActivate(0, true);
                buttonsActivate(10, false);
            }

            if (not3 == true) { buttonsActivate(17, false); buttonsActivate(7, true); NAND();}

            if (not1 == false)
            {
                buttonsActivate(10, true);
                buttonsActivate(0, false);
            }

            if (not3 == false) { buttonsActivate(17, true); buttonsActivate(7, false); NAND();}
        }
        NAND();
        AND();
        XOR();
    }

    public void AND()
    {
        //Debug.Log("Tá chegando no AND fdp ?");
        bool button3 = buttons[3] == 0 ? false : true;
        buttonsActivate(3, button3);
        GameObject parentObject = GameObject.Find("logicGate");
        GameObject and1 = GameObject.Find("task op (10)");
        GameObject and2 = GameObject.Find("task op (16)");
        GameObject and3 = GameObject.Find("task op (9)");
        GameObject and4 = GameObject.Find("task op (3)");

        if (and1 != null && and2 != null) {buttonsActivate(9, true);} 
        else 
        {
            buttonsActivate(9, false);
        }
        if (and3 != null && button3 == true) 
        {
            buttonsActivate(12, true);
        } 
        else 
        {
            buttonsActivate(12, false);
        }
        XOR();
    }

    public void NAND()
    {
        bool button6 = buttons[6] > 0 ? true : false;
        buttonsActivate(6, button6);

        GameObject parentObject = GameObject.Find("logicGate");
        GameObject nand1 = GameObject.Find("task op (6)");
        GameObject nand2 = GameObject.Find("task op (17)");


        if(nand1 != null && nand2 != null)
        {
            buttonsActivate(15, false);
        }
        else{
            buttonsActivate(15, true);
        }
        XOR();
    }

    public void OR()
    {
        bool not2 = !(buttons[1] == 0 && buttons[2] == 0);
        bool button1 = buttons[1] > 0 ? true : false;
        bool button2 = buttons[2] > 0 ? true : false;
        if (not2 == true)
        {
            buttonsActivate(16, true);
            buttonsActivate(1, button1);
            buttonsActivate(2, button2);
        }
        if(not2 == false)
        {
            buttonsActivate(16, false);
            buttonsActivate(1, button1);
            buttonsActivate(2, button2);
        }
        AND();
        XOR();
    }

    public void NOR()
    {
        bool button4 = buttons[4] > 0 ? true : false;
        bool button5 = buttons[5] > 0 ? true : false;
        
        buttonsActivate(4, button4);
        buttonsActivate(5, button5);

        if(button4 == false && button5 == false){buttonsActivate(14, true);}
        else{buttonsActivate(14, false);}
        XOR();
    }

    public void XOR()
    {
        GameObject parentObject = GameObject.Find("logicGate");
        GameObject xor1 = GameObject.Find("task op (12)");
        GameObject xor2 = GameObject.Find("task op (13)");
        GameObject xor3 = GameObject.Find("task op (14)");
        GameObject xor4 = GameObject.Find("task op (15)");

        if (parentObject != null)
        {
            GameObject lineLamp = parentObject.transform.Find("Lamp").gameObject;
            if((xor1 != null && xor2 != null) || (xor1 == null && xor2 == null))
            {
              lineLamp.SetActive(false);
            }else{lineLamp.SetActive(true);}

            if(xor3 != null && xor4 != null)
            {
                buttonsActivate(13, false);
            }
            else{buttonsActivate(13, true);}

        }
    }
}
