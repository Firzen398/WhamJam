using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class MainScript : MonoBehaviour
{
    string path = "Assets/Data/lyrics.json";

    private object data;

    // Start is called before the first frame update
    void Start()
    {

        StreamReader reader = new StreamReader(path); 

        data =JsonConvert.DeserializeObject(reader.ReadToEnd());


        //Debug.Log(data);

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Time.realtimeSinceStartup);
    }
}
