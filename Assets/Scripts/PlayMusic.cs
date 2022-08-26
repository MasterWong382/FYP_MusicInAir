using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayMusic : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    bool toPlay = false;
    private double phase;
    private double increment;
    double frequency = 440;
    private double sampling_frequency = 48000;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.C))
        {
            toPlay = !toPlay;
            if (toPlay)
            {
                audioSource.Play();
            }
            else
            {
                audioSource.Stop();
            }
        } 

    }




        void OnAudioFilterRead(float[] data, int channels)
        {
        increment = frequency * 2 * Math.PI / sampling_frequency;
        for (var i = 0; i < data.Length; i += channels)
        {
            phase += increment;

            if (channels == 2)
            {
                data[i + 1] = data[i];
            }

            if (phase > 2 * Math.PI)
            {
                phase = 0;
            }
        }
    }



}
