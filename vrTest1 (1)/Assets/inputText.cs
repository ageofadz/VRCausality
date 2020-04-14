using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VRTK;

public class inputText : MonoBehaviour
{
    public GameObject globalVars;
    public static string[] values= new string[10];
    private int qnum;
    // Start is called before the first frame update
    void Start()
    {
    GetComponent<VRTK_InteractableObject>().InteractableObjectUsed += new InteractableObjectEventHandler(ObjectUsed);
    }

    private void ObjectUsed(object sender, InteractableObjectEventArgs e)
    {
        if (globalVars.GetComponent<globalVars>().playMode == 4 | globalVars.GetComponent<globalVars>().playMode == 2)
        {
            print("Use detected on q: " + gameObject.name);
            int index = int.Parse(gameObject.name);
            print("index on q: " + index);
            //globalVars.GetComponent<globalVars>().answer = textFrame.text;        (replace with text based on number selected)
            print(values[index]);
            globalVars.GetComponent<globalVars>().answer = values[index];
        }
    }
    // Possibly implement each answer as a string in itself, and set the color of each string through the case switch
    // Easier implementation: have another textmesh below which shows current answer based on case/switch by passing string into method

    // Update is called once per frame
    void Update()
    {
    }


}
