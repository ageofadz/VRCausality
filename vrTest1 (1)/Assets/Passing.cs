using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passing : MonoBehaviour
{
    public GameObject globalVars;
    public GameObject ufo;
    public Light lt;
    bool passed = false;
    // Start is called before the first frame update
    void Start()
    {
        globalVars = GameObject.Find("GlobalVarHolder");
        lt = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x<-20 && passed==false)
        {
            passed = true;
            if (globalVars.GetComponent<globalVars>().rai == 2 | globalVars.GetComponent<globalVars>().rai == 4)
            {
                ufo.GetComponent<hover>().appearOnYellow();
            }
            globalVars.GetComponent<globalVars>().yellowPass();
                if (globalVars.GetComponent<globalVars>().rai == 1 | globalVars.GetComponent<globalVars>().rai == 3)
                {
                transform.Translate(new Vector3(58, 0, 0), transform);
                passed = false;
                }
        }
    }
}
