using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnUFOs : MonoBehaviour
{
    public GameObject ufo;
    public GameObject globalVars;
    private GameObject ufoClone;
    public GameObject yellowUFO;

    public GameObject hit;
    public GameObject smoke;
    public GameObject racket;
    public int ufoCount=0;
    //gameobjects to pass to ufo


    float timeLeft = 5;
    int ufoID = 0;
    public bool yellowSpawned = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void spawnYellow()
    {
        yellowSpawned = true;
        ufoClone.GetComponent<hover>().hideOnYellow();
        Vector3 spawnpos;
        Random.InitState((int)System.DateTime.Now.Ticks);
        int spawnPointZ = Random.Range(-2, 1);
        spawnpos = new Vector3(transform.position.x, transform.position.y, transform.position.z + spawnPointZ);
        yellowUFO = Instantiate(yellowUFO, spawnpos, Quaternion.identity);
        yellowUFO.SetActive(true);
    }

    private void OnEnable()
    {
        ufoCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (racket.GetComponent<listenAndReport>().score >= 30)
        {
            Destroy(gameObject);
        }
        if (globalVars.GetComponent<globalVars>().playMode == 1 | globalVars.GetComponent<globalVars>().playMode == 3)
        {
            if (GameObject.Find("YellowUFO(Clone)") == null)
            { 
            //UFOs are released at a 5 second interval
            timeLeft -= Time.deltaTime;
            Vector3 spawnpos;
            Random.InitState((int)System.DateTime.Now.Ticks);
            int spawnPointZ = Random.Range(-2, 1);
            spawnpos = new Vector3(transform.position.x, transform.position.y, transform.position.z + spawnPointZ);
            if (timeLeft < 0)
            {
                ufoClone = Instantiate(ufo, spawnpos, Quaternion.identity);
                ufoClone.SetActive(true);
                ufoClone.GetComponent<hover>().explosionPrefab = hit;
                ufoClone.GetComponent<hover>().smokePrefab = smoke;
                ufoClone.GetComponent<hover>().racket = racket;
                ufoClone.GetComponent<hover>().globalVars = globalVars;

                //ufo.GetComponent<hover>().ufoinst = ufoClone;
                timeLeft = 5;
                ufoID += 1;
                ufoCount++;
            }
            }
            if (globalVars.GetComponent<globalVars>().rai == 3 | globalVars.GetComponent<globalVars>().rai == 4)
            {
                if (racket.GetComponent<listenAndReport>().score > 24 && yellowSpawned == false)
                {
                    spawnYellow();
                }
            }
            if (globalVars.GetComponent<globalVars>().rai == 1 | globalVars.GetComponent<globalVars>().rai == 2)
            {
                if (racket.GetComponent<listenAndReport>().score > 4 && yellowSpawned == false)
                {
                    spawnYellow();
                }
            }
        }

        if (globalVars.GetComponent<globalVars>().playMode != 1 && globalVars.GetComponent<globalVars>().playMode != 3 )
        {
            Destroy(ufoClone);
            if (globalVars.GetComponent<globalVars>().playMode == 4)
            {
                Destroy(gameObject);
            }
        }
    }
}
