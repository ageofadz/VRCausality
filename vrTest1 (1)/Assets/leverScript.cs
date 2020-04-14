using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using VRTK;
using TMPro;

public class leverScript : MonoBehaviour
{
    float currAngle = 0;
    public bool selected2 = false;
    public bool selected1 = false;
    public bool selected0 = false;
    string path = "Assets/Resources/Output.txt";
    string currUser = "User001";
    public GameObject comissionCheck;
    public GameObject light1;
    public GameObject light2;
    public GameObject racket;
    public GameObject text1;
    public bool grabbable;
    void Start()
    {
        //GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);
        bool comission = comissionCheck.GetComponent<globalVars>().comission;
        grabbable = true;

    }

    private void OnEnable()
    {
        grabbable = false;
        light1.SetActive(false);
        light2.SetActive(false);
        selectednone();
        StartCoroutine(allowSelect());
    }

    IEnumerator allowSelect()
    {
        yield return new WaitForSeconds(2);
        grabbable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbable == true)
        {
            currAngle = transform.localRotation.z;
            if (transform.localRotation.z > .6)
            {
                oneSelected();
            }
            if (transform.localRotation.z < -.6)
            {
                twoSelected();
            }
            if (transform.localRotation.z > -.1 && transform.localRotation.z < .1)
            {
                selectednone();
            }
        }
    }

    void oneSelected()
    {
        grabbable = false;
        StartCoroutine(allowSelect());
        light1.SetActive(true);
        light2.SetActive(false);
        print("1 selected, angle is " + transform.localRotation.z);
        selected1 = true;
        selected2 = false;
        comissionCheck.GetComponent<globalVars>().opt1();
        comissionCheck.GetComponent<globalVars>().option1=true;
    }

    void twoSelected()
    {
        grabbable = false;
        StartCoroutine(allowSelect());
        light1.SetActive(false);
        light2.SetActive(true);
        print("2 selected, angle is " + transform.localRotation.z);
        selected1 = false;
        selected2 = true;
        comissionCheck.GetComponent<globalVars>().opt2();
        comissionCheck.GetComponent<globalVars>().option1 = false;
    }

    public void selectednone()
    {
        light1.SetActive(false);
        light2.SetActive(false);
        if (selected1 == true)
        {
            selected1 = false;
            text1.GetComponent<TextMeshPro>().text = ("Confirm Answer");
        }
    }
}
