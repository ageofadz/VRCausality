using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passingUfo : MonoBehaviour
{
    bool passed = false;
    public GameObject racket;
    public GameObject yellow;
    // Start is called before the first frame update
    void Start()
    {
        racket = GameObject.Find("rocket");

    }

    // Update is called once per frame
    //COMPLETE THIS SECTION
    void Update()
    {
        if (transform.position.x < -20 && passed == false)
        {
            passed = true;
            racket.GetComponent<listenAndReport>().score-=1;
        }
        if (Vector3.Distance(transform.position, yellow.transform.position) <5f && Vector3.Distance(yellow.transform.position, transform.position) > 0f)
        {
            transform.Translate(new Vector3(transform.position.x + 1f, transform.position.y, transform.position.z));
        }
    }
}
