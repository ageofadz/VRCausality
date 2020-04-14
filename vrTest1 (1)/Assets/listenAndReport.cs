using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using VRTK;

public class listenAndReport : MonoBehaviour
{
    public int score;
    float rand;
    bool inCollision = false;

    public float speed;
    public GameObject ufo;
    public GameObject globalVars;
    public GameObject ufoSpawn;
    public AudioClip swish1;
    public AudioClip swish2;
    public AudioClip swish3;
    bool grabbed = false;
    bool swishing = false;
    int swishCount = 0;
    public Vector3 defaultPos = new Vector3(0,0,0);
    Vector3 previous = new Vector3(0,0,0);
    public Quaternion defaultRotation = new Quaternion();
    public Boolean bypassGame;
    // Start is called before the first frame update
    void Start()
    {
        defaultPos = transform.position;
        defaultRotation = transform.rotation;
        score = 0;
        if (bypassGame==true)
        {
            score += 25;
        }

        GetComponent<VRTK_InteractableObject>().InteractableObjectGrabbed += new InteractableObjectEventHandler(ObjectGrabbed);

        GetComponent<VRTK_InteractableObject>().InteractableObjectUngrabbed += new InteractableObjectEventHandler(InteractableObjectUngrabbed);
    }
    private void ObjectGrabbed(object sender, InteractableObjectEventArgs e)

    {
        grabbed = true;
        Debug.Log("Racket Grabbed");
        SteamVR_Events.HideRenderModels.Send(true);
    }

    public void release()
    {
        GetComponent<VRTK_InteractableObject>().ForceStopInteracting();
        score -= 400;
    }

    void swish()
    {
        rand = UnityEngine.Random.value;

        if (swishing == false)
        {

            swishCount = 15;
            swishing = true;
            if (rand <= .8)
            {
                AudioSource.PlayClipAtPoint(swish1, new Vector3(0, 0, 0));
            }
            else if (rand >= .2)
            {
                AudioSource.PlayClipAtPoint(swish2, new Vector3(0, 0, 0));
            }
            else if (rand <= .2 || rand >= .8)
            {
                AudioSource.PlayClipAtPoint(swish3, new Vector3(0, 0, 0));
            }
        }
        else swishCount -= 1;

    }

    private void InteractableObjectUngrabbed(object sender, InteractableObjectEventArgs e)
    {
        grabbed = false;
        SteamVR_Events.HideRenderModels.Send(false);
        Debug.Log("Racket Ungrabbed");
        transform.position = defaultPos;
        transform.rotation = defaultRotation;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    void OnCollisionEnter(Collision collision)
    {
        //Check for a match with the specified name on any GameObject that collides with your GameObject
        
            if (collision.gameObject.CompareTag("enemy") && inCollision == false)
            {
                inCollision = true;
                DateTime localDate = DateTime.Now;
                 globalVars.GetComponent<globalVars>().point();
            }
            if (collision.gameObject.CompareTag("yellow") && inCollision == false)
            {
                inCollision = true;
                DateTime localDate = DateTime.Now;

            if (globalVars.GetComponent<globalVars>().rai==1 | globalVars.GetComponent<globalVars>().rai ==3)
            {
                score += 5;
                globalVars.GetComponent<globalVars>().pointyellow();
                ufo.GetComponent<hover>().appearOnYellow();
            }

            if (globalVars.GetComponent<globalVars>().rai == 2 | globalVars.GetComponent<globalVars>().rai == 4)
            {
                score -= 5;
                globalVars.GetComponent<globalVars>().pointyellow();
                ufoSpawn.GetComponent<spawnUFOs>().yellowSpawned = false;
                ufoSpawn.GetComponent<spawnUFOs>().spawnYellow();
            }

        }
    }
    void OnCollisionExit(Collision collision)
    {
        inCollision = false;
    }

    // Update is called once per frame
    void Update()
    {
        speed = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
        if (speed>3)
        {
            swish();
        }
        if (swishCount<=0)
        {
            swishing = false;
        }
        if (score < 0)
        {
            score = 0;
        }
        if (speed>0 && grabbed==false)
        {
            transform.SetPositionAndRotation(defaultPos, defaultRotation);
        }
    }

}
