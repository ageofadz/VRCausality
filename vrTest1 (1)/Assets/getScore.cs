using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getScore : MonoBehaviour
{
    public GameObject rocket;
    public GameObject vars;
    public GameObject canvas;
    public int qnum=0;
    bool questions = false;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }

    // Update is called once per frame
    void Update()
    {
        if (vars.GetComponent<globalVars>().playMode == 1 || vars.GetComponent<globalVars>().playMode == 3)
        {
            gameObject.GetComponent<TextMesh>().text = "Score: " + rocket.GetComponent<listenAndReport>().score;
        }

        if (gameObject.GetComponent<TextMesh>().text.StartsWith("What happens if"))
        {
            canvas.SetActive(true);
            canvas.GetComponent<parentQScript>().q1();
        }

        if (gameObject.GetComponent<TextMesh>().text.StartsWith("To what degree do you agree with this statement"))
        {
            canvas.SetActive(true);
            canvas.GetComponent<parentQScript>().q2();
        }
        if (gameObject.GetComponent<TextMesh>().text==("What is your age?"))
        {
            canvas.SetActive(true);
            canvas.GetComponent<parentQScript>().q3();
        }
        if (gameObject.GetComponent<TextMesh>().text==("What is your sex?"))
        {
            canvas.SetActive(true);
            canvas.GetComponent<parentQScript>().q4();
        }
    }

    public void setText(string s)
    {
        gameObject.GetComponent<TextMesh>().text = (s);
        print(s);
    }

    public void switchOff()
    {
        canvas.SetActive(false);
    }

    public void hide()
    {
        gameObject.GetComponent<TextMesh>().text = ("");
    }

    public void question()
    {
        canvas.SetActive(true);
        questions = true;
        if (qnum==0)
        {
            gameObject.GetComponent<TextMesh>().text = ("What happens if you hit the yellow UFO?");
            canvas.GetComponent<parentQScript>().q1();
        }
        if (qnum==1)
        {
            canvas.GetComponent<parentQScript>().q2();
            switch (vars.GetComponent<globalVars>().rai)
            {
                case 1:
                    gameObject.GetComponent<TextMesh>().text = ("To what degree do you agree with this statement:\nI won because I hit the yellow UFO");
                    break;
                case 2:
                    gameObject.GetComponent<TextMesh>().text = ("To what degree do you agree with this statement:\nI won because I didn't hit the yellow UFO");
                    break;
                case 3:
                    gameObject.GetComponent<TextMesh>().text = ("To what degree do you agree with this statement:\nI won because I hit the yellow UFO");
                    break;
                case 4:
                    gameObject.GetComponent<TextMesh>().text = ("To what degree do you agree with this statement:\nI won because I didn't hit the yellow UFO");
                    break;
            }

        }
        if (qnum == 2)
        {
            canvas.GetComponent<parentQScript>().q3();
            gameObject.GetComponent<TextMesh>().text = ("What is your age?");
        }
        if (qnum == 3)
        {
            canvas.GetComponent<parentQScript>().q4();
            gameObject.GetComponent<TextMesh>().text = ("What is your sex?");
        }
        if (qnum == 4) 
        {
            canvas.GetComponent<parentQScript>().q5();
            gameObject.GetComponent<TextMesh>().text = ("Thank you for participating!");
        }
    }
}
