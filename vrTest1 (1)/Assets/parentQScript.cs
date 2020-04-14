using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class parentQScript : MonoBehaviour
{
    public GameObject globalVars;
    public GameObject q1b;
    public GameObject q2b;
    public GameObject q3b;
    public GameObject q4b;
    public GameObject ageText;
    public GameObject confText;
    int index = 0;
    string[] ageandsex = new string[2];
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void onAnswer(string answer)
    {
        globalVars.GetComponent<globalVars>().answer = answer;
        print(answer);
    }
    public void answer2(Slider slider)
    {
        globalVars.GetComponent<globalVars>().answer =  slider.value.ToString();
        print(slider.value.ToString());
        ageText.GetComponent<Text>().text = slider.value.ToString();
    }

    public void q1()
    {
        q3b.SetActive(false);
        q2b.SetActive(false);
        q1b.SetActive(true);
        confText.SetActive(true);
    }
    public void q2()
    {
        q3b.SetActive(false);
        q2b.SetActive(true);
        q1b.SetActive(false);
        confText.SetActive(true);
    }
    public void q3()
    {
        q3b.SetActive(true);
        q2b.SetActive(false);
        q1b.SetActive(false);
        confText.SetActive(true);
    }
    public void q4()
    {
        q3b.SetActive(false);
        q4b.SetActive(true);
        confText.SetActive(true);
    }
    public void q5()
    {
        q2b.SetActive(false);
        q1b.SetActive(false);
        q3b.SetActive(false);
        q4b.SetActive(false);
    }
}
