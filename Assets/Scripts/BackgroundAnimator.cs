using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundAnimator : MonoBehaviour
{
    [SerializeField]
    private GameObject background;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        float x = (float) Math.Sin(Time.realtimeSinceStartup);
        float y = (float) Math.Sin(2*Time.realtimeSinceStartup+1);

         background.transform.position = new Vector3(x,y,0);
    }
}
