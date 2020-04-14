using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playonplay : MonoBehaviour
{
    public GameObject globalVars;
    private AudioSource music;
    // Start is called before the first frame update
    void Start()
    {
        music = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (globalVars.GetComponent<globalVars>().playMode == 3 && music.isPlaying==false)
        {
            music.Play();
        }
        if (globalVars.GetComponent<globalVars>().playMode == 4 && music.isPlaying == true)
        {
            music.Stop();
        }
    }
}
