using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class hover : MonoBehaviour
{

    bool smashed = false;
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.3f;
    public float frequency = 1f;
    public GameObject explosionPrefab;
    public GameObject smokePrefab;
    public GameObject racket;
    public GameObject globalVars;
    public AudioClip hit;
    public AudioClip ground;
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        racket = GameObject.Find("rocket");
        globalVars = GameObject.Find("GlobalVarHolder");
    }

    // Update is called once per frame
    void Update()
    {
        if (racket.GetComponent<listenAndReport>().score >= 30)
        {
            gameObject.SetActive(false);
        }

        var me = gameObject;
        Rigidbody rb = GetComponent<Rigidbody>();
        if (smashed == true)
        {
            //Instantiate(smokePrefab, me.transform.position, Quaternion.identity);
            //rb.
            Physics.IgnoreCollision(racket.GetComponent<Collider>(), GetComponent<Collider>());
        }
        transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

        // Float up/down with a Sin()
        if (smashed == false)
        {
            posOffset = transform.position;
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
            tempPos.x -= .05f;
            if (tempPos.y < .1f)
            {
                tempPos.y += .3f;
            }

            transform.position = tempPos;
        }

        if (globalVars.GetComponent<globalVars>().playMode==4)
        {
            Destroy(gameObject);
        }
    }

    public void hideOnYellow()
    {
        if (gameObject.name != "YellowUFO" | gameObject.name != "YellowUFO(Clone)")
        gameObject.SetActive(false);
    }

    public void appearOnYellow()
    {
        gameObject.SetActive(true);
    }

    void hitSound()
    {
        AudioSource.PlayClipAtPoint(hit, new Vector3(0,0,0));
    }
    void groundSound()
    {
        AudioSource.PlayClipAtPoint(ground, new Vector3(0, 0, 0));
    }
    
    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.name == "Low_poly_UFO")
        {
            Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            smashed = true;
            hitSound();
        }


        if (collision.gameObject.name == "rocket" && smashed==false)
        {
            smashed = true;
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.useGravity = true;
            Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            hitSound();
        }

        if (collision.gameObject.name == "BaseFloor" || collision.gameObject.name == "CourtFloor")
        {
            Instantiate(explosionPrefab, gameObject.transform.position, Quaternion.identity);
            smashed = true;
            groundSound();
            Destroy(gameObject);
            racket.GetComponent<listenAndReport>().score+=1;
        }

        if (collision.gameObject.name == "killZone" || collision.gameObject.name == "killZone")
        {
            Destroy(gameObject);
        }
    }
}