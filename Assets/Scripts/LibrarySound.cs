using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibrarySound : MonoBehaviour
{
    // Start is called before the first frame update
    CsoundUnity _csound;
    double currentAmp = 0;
    double frequency = 440;
    bool rightHandIn;
    void Start()
    {
        _csound = GetComponent<CsoundUnity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHandIn)
        {
            _csound.SetChannel("Frequency", frequency);
            _csound.SetChannel("Amplitude", currentAmp);
        }
        
    }

    public void SetAmp(double amp)
    {
        currentAmp = amp;
    }

    public void SetFrequency(double freq)
    {
        frequency = freq;
    }

    public void isRightHandIn(bool rightHand)
    {
        rightHandIn = rightHand;
    }

}
