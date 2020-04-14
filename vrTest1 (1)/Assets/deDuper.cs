using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class deDuper : MonoBehaviour
{
    // Start is called before the first frame update
    public static void Start(string path)
    {
        using (TextReader reader = File.OpenText(path))
        using (TextWriter writer = File.CreateText(path))
        {
            string currentLine;
            string lastLine = null;

            while ((currentLine = reader.ReadLine()) != null)
            {
                if (currentLine != lastLine)
                {
                    writer.WriteLine(currentLine);
                    lastLine = currentLine;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
