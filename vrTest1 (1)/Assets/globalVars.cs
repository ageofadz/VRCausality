using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Linq;
using UnityEngine.SceneManagement;

public class globalVars : MonoBehaviour
{
    //Set "comission" to true for commission, false for omission
    public bool comission = true;
    public GameObject lever;
    public GameObject racket;
    public GameObject screen;
    public GameObject table;
    public GameObject diagram;
    public GameObject redText;
    private AudioSource tutorialvoice;
    public bool option1;
    public bool tutorialBypass = false;
    public int rai = 0;
    int failures = 0;
    string raiStr = "";
    public bool ballGrabbed = false;

    //Print to CSV vars
    public string path = "";
    public string path2 = "";
    public string path3 = "";
    public string answer;
    private int qnum;
    string currUser = "User001";

    //Switch on lever
    public bool leverOn = false;

    // 0: tutorial
    // 1: ufo game
    // 2: lever prompt
    // 3: ufo game 2
    // 4: questionaire 
    public int playMode = 1;
    public AudioClip tutorialvoice0;
    public AudioClip tutorialvoice1;
    public AudioClip tutorialvoice2i;
    public AudioClip tutorialvoice3;
    public AudioClip tutorialvoice2a;
    public AudioClip tutorialvoice4;
    // Start is called before the first frame update
    void Start()
    {
        string dataPath = Application.persistentDataPath;
        path2 = dataPath + "/TestGrid.csv";
        tutorialvoice = GetComponent<AudioSource>();
        if (!File.Exists(path2))
        {
            System.IO.File.CreateText(path2).Dispose();
            StreamWriter writer2 = new StreamWriter(path2, true);
            writer2.WriteLine("4,");
            writer2.Close();
        }
        path3 = dataPath + "/Responses.csv";
        if (!File.Exists(path3))
        {
            System.IO.File.CreateText(path3).Dispose();
        }
        if (File.Exists(path2))
        {
            StreamReader reader = new StreamReader(path2, true);
            String last = File.ReadLines(path2).Last();
            reader.Close();
            StreamWriter writer2 = new StreamWriter(path2, true);
            last = last.Remove(1, 1);
            rai = int.Parse(last);
            if (rai == 4)
            {
                rai = 1;
            }
            else
            {
                rai++;
            }
            writer2.WriteLine("\n");
            writer2.WriteLine(rai + ",");
            print(rai);
            writer2.Close();
            reader.Close();
            switch (rai)
            {
                case 1:
                    raiStr = "Early,Action";
                    break;
                case 2:
                    raiStr = "Early,Inaction";
                    break;
                case 3:
                    raiStr = "Late,Action";
                    break;
                case 4:
                    raiStr = "Late,Inaction";
                    break;
            }

        }
        if (File.Exists(path3))
        {
            StreamReader reader3 = new StreamReader(path3, true);
            String first = File.ReadLines(path3).FirstOrDefault();
            reader3.Close();
            StreamWriter writer3 = new StreamWriter(path3, true);
            if (first != "ResponseID,Recency,Action,Comprehension,Failures,Question Rating,Age,Sex")
            {
                writer3.WriteLine("ResponseID,Recency,Action,Comprehension,Failures,Question Rating,Age,Sex");
            }
            writer3.Write("\n"+GetRandomString() + "," + raiStr);
            writer3.Close();
            reader3.Close();
        }
    }
    public static string GetRandomString()
    {
        string prand = Path.GetRandomFileName();
        prand = prand.Replace(".", ""); // Remove period.
        return prand;
    }


    private IEnumerator leverBegin()
    {
        yield return new WaitForSeconds(2);
        table.SetActive(true);
    }

    void Update()
        {
            if (racket.GetComponent<listenAndReport>().score >= 30)
            {
            racket.GetComponent<listenAndReport>().score = 0;
            StreamWriter writer3 = new StreamWriter(path3, true);
            writer3.Write(","+failures);
            writer3.Close();
            goToQuestion();
            }
            if (playMode == 2)
            {
                if (lever.GetComponent<leverScript>().selected1 == true)
                {
                    option1 = true;
                }

                if (lever.GetComponent<leverScript>().selected1 == false)
                {
                    option1 = false;
                }
            }
        }

        void goToLever()
        {
            leverOn = true;
            table.SetActive(true);
            racket.GetComponent<listenAndReport>().score = 0;
        }

        public void reInitTable()
        {
        //lever.SetActive(false);
        //lever.SetActive(true);
        }

        void goToGame()
        {
            table.SetActive(false);
            Debug.Log("goToGame active");
            answer = null;
            leverOn = false;
            playMode = 3;
            racket.transform.SetPositionAndRotation(racket.GetComponent<listenAndReport>().defaultPos, racket.GetComponent<listenAndReport>().defaultRotation);
            diagram.SetActive(false);
        }

        public void yellowPass()
        {
            switch (rai)
            {
                case 1:
                    racket.GetComponent<listenAndReport>().score -= 5;
                    failures++;
                    break;
                case 2:
                    racket.GetComponent<listenAndReport>().score += 5;
                    break;
                case 3:
                    racket.GetComponent<listenAndReport>().score -= 5;
                    failures++;
                    break;
                case 4:
                    racket.GetComponent<listenAndReport>().score += 5;
                    break;
            }
        }

        public void point()
        {
        }
        public void pointyellow()
        {
            switch (rai)
            {
                case 2:
                    failures += 1;
                    break;

                case 4:
                    failures += 1;
                    break;
            }
        }
        public void opt1() //PLAYS TUTORIAL VOICE, CONFIRMS QUESTIONAIRRE ANSWERS
        {
        //remove buttons
        if (!tutorialvoice.isPlaying && tutorialBypass == false && playMode == 0)
        {
            StartCoroutine(playVoice());
        }
        else if (tutorialBypass == true && playMode==0)
        {
            reInitTable();
            opt0();
        }
        else if (playMode == 2 | playMode==4)
        {
            print("Answer confirmed as "+answer);
            StreamWriter writer3 = new StreamWriter(path3, true);
            switch (qnum)
            {
                case 0:
                    if (answer == "lose points")
                    {
                        if (rai == 1 | rai == 3)
                        {
                            //action
                            writer3.Write(",Wrong");
                        }
                        if (rai == 2 | rai == 4)
                        {
                            //inaction
                            writer3.Write(",Correct");
                        }
                    }

                    if (answer == "get points")
                    {
                        if (rai == 1 | rai == 3)
                        {
                            //action
                            writer3.Write(",Wrong");
                        }
                        if (rai == 2 | rai == 4)
                        {
                            //inaction
                            writer3.Write(",Correct");
                        }
                    }

                    if (answer == "replay tutorial")
                    {
                        if (!tutorialvoice.isPlaying)
                        {
                            writer3.Close();
                            StreamWriter writer2 = new StreamWriter(path2, true);
                            writer2.WriteLine("\n");
                            if (rai!=1)
                            writer2.WriteLine(rai-1 + ",");
                            else if (rai==1)
                            writer2.WriteLine("4,");
                            writer2.Close();
                            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                        }
                    }

                    if (answer == "nothing")
                    {
                        writer3.Write(",Wrong");
                    }

                    if (answer== "no answer")
                    {
                        writer3.Write(",Wrong");
                        opt0();
                    }

                    if (answer == "dont know")
                    {
                        writer3.Write(",Wrong");
                        opt0();
                    }

                    if (answer==null)
                    {
                        writer3.Write(",Wrong");
                        opt0();
                    }
                    qnum++;
                    redText.GetComponent<getScore>().qnum++;
                    answer = "answered";
                    redText.GetComponent<getScore>().setText("Now pull lever towards 'start game' \n to start game.");
                    redText.GetComponent<getScore>().switchOff();
                    break;

                case 1:
                    writer3.Write(","+answer);
                    qnum++;
                    redText.GetComponent<getScore>().qnum++;
                    opt0();
                    redText.GetComponent<getScore>().question();
                    break;

                case 2:
                    writer3.Write("," + answer);
                    qnum++;
                    redText.GetComponent<getScore>().qnum++;
                    opt0();
                    redText.GetComponent<getScore>().question();
                    break;

                case 3:
                    writer3.Write("," + answer);
                    qnum++;
                    redText.GetComponent<getScore>().qnum++;
                    opt0();
                    redText.GetComponent<getScore>().question();
                    break;

                case 4:
                    table.SetActive(false);
                    break;
            }
            writer3.Close();
        }
        //If the GameObject's name matches the one you suggest, output this message in the console
        }
        IEnumerator playVoice()
        {
                redText.GetComponent<getScore>().setText("");
                tutorialvoice.clip = tutorialvoice0;
                tutorialvoice.Play();
                yield return new WaitForSeconds(tutorialvoice.clip.length);

                while (!ballGrabbed)
                {
                    yield return null;
                }

                tutorialvoice.clip = tutorialvoice1;
                tutorialvoice.Play();
                yield return new WaitForSeconds(tutorialvoice.clip.length);
                diagram.GetComponentInChildren<blinking>().moveToPointer();
                tutorialvoice.clip = tutorialvoice3;
                tutorialvoice.Play();
                yield return new WaitForSeconds(tutorialvoice.clip.length);
                if (rai == 1 | rai == 3)
                {
                    tutorialvoice.clip = tutorialvoice2a;
                    redText.GetComponent<getScore>().setText("Hit yellow UFO for 5 points\nIf you miss, you lose 5 points");
                }
                else
                {
                    tutorialvoice.clip = tutorialvoice2i;
                    redText.GetComponent<getScore>().setText("Don't hit yellow UFO for 5 points\nIf you hit, you lose 5 points");
                }
                tutorialvoice.Play();
                yield return new WaitForSeconds(tutorialvoice.clip.length);
                tutorialvoice.clip = tutorialvoice4;
                tutorialvoice.Play();
                redText.GetComponent<getScore>().setText("");
                yield return new WaitForSeconds(tutorialvoice.clip.length);
                reInitTable();
                opt0();
                diagram.SetActive(false);
                yield break;
            }
        public void opt0()
        {
        if (playMode != 4)
        {
            playMode = 2;
        }
        lever.GetComponent<leverScript>().selectednone();
        qnum = redText.GetComponent<getScore>().qnum;
        redText.GetComponent<getScore>().question();
        print("Question number: "+qnum);
        }
        public void opt1d()
        {
            
        }

        public void opt2() //STARTS THE GAME
        {
            if (answer=="answered" && playMode!=4)
            {
            //If the GameObject's name matches the one you suggest, output this message in the console
                answer = "1";
                goToGame();
            }
        }

        public void opt2d()
        {
            DateTime localDate = DateTime.Now;
            //If the GameObject's name matches the one you suggest, output this message in the console
        }

        public void goToQuestion()
        {
            playMode = 4;
            table.SetActive(true);
            lever.SetActive(true);
            opt0();
            redText.GetComponent<getScore>().question();
            lever.GetComponent<leverScript>().selectednone();
            racket.GetComponent<listenAndReport>().release();
        }

    }