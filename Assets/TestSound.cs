using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    CsoundUnity csoundUnity;
    float frequency;
    // Start is called before the first frame update
    void Start()
    {
        csoundUnity = GetComponent<CsoundUnity>();
        frequency = 440f;
    }

    // Update is called once per frame
    void Update()
    {

        csoundUnity.SetChannel("freq", frequency);
        
        if (Input.GetKey(KeyCode.E)){
            frequency += 10f;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            frequency -= 10f;
        }
    }
}
