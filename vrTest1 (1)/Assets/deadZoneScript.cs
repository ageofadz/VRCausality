using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deadZoneScript : MonoBehaviour
{
    public GameObject globalVars;
    public GameObject ufo;
    public GameObject yellowUfo;
    public GameObject racket;
    public GameObject ufoSpawn;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Trigger entered");
        if (collision.gameObject.name == "YellowUFO" || collision.gameObject.name == "YellowUFO")
        {
            Debug.Log("Yellow UFO passby");
            if (globalVars.GetComponent<globalVars>().rai==1 | globalVars.GetComponent<globalVars>().rai == 3)
            {
                racket.GetComponent<listenAndReport>().score -= 5;
                ufoSpawn.GetComponent<spawnUFOs>().yellowSpawned = false;
                ufoSpawn.GetComponent<spawnUFOs>().spawnYellow();
            }
            if (globalVars.GetComponent<globalVars>().rai == 2 | globalVars.GetComponent<globalVars>().rai == 4)
            {
                racket.GetComponent<listenAndReport>().score += 5;
            }
        }
        if (collision.gameObject.name == "Low_poly_UFO")
        {
            Physics.IgnoreCollision(ufo.GetComponent<Collider>(), GetComponent<Collider>());
            Debug.Log("passby");
        }
    }
}
