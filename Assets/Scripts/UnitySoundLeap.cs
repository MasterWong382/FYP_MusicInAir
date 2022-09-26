using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Leap;
using Leap.Unity;

public class UnitySoundLeap : MonoBehaviour
{
    public UIHand uiScript;
    AudioSource audioSource;
    Vector3 mousePos;
    double frequency = 440;
    int startingFrequency = 220;
    double amplitude = 1;
    double currentAmp = 0;
    double maxAmp = 4;
    private double increment;
    private double phase;
    private double sampling_frequency = 48000;
    bool playSound = false;
    double powerRaised;
    float fixedPowerRaised;
    float sizeOfNote;
    int noteKey;
    public float numOfOctave;
    double noteIndex = 0;
    bool fixedFrequency = false;
    int maxPitchPosition = 293; //For Leap motion 3D space 293
    int minPitchPosition = 185; //For Leap motion 3D space 185
    int rightHandPositionRange = 0;
    float rightHandPosition;

    int maxVolumePosition = 300;
    int minVolumePosition = 170;
    float leftHandPositionRange;
    float leftHandPosition;

    Vector3 unityCoord;
    public Camera cam;
    public RectTransform pitchIndicator;
    Vector3 indicatorPosY;
    public int maxIndicatorPitchPos = 278;

    public RectTransform volumeIndicator;
    Vector3 volIndicatorPos;
    public int maxIndicatorVolPos = 128;
    int waveTypeIndex = 0;
    double lerpVolume = 0;
    bool canLerpVol = false;
    double lastFrequency = 0;
    float speedOfAmpChange = 2f;
    bool gotLeftHand = false;
    public Text volumeDisplay;
    public HandBorderGuide handBorderGuide;
    bool rightHandIn;

    public float sampleRate = 44100;
    public float waveLengthInSeconds = 2.0f;
    CsoundUnity _csound;

    //Csound



    // Start is called before the first frame update


    void Start()
    {
        _csound = GetComponent<CsoundUnity>();

        //  Debug.Log("Screen Height : " + Screen.height);
        // Debug.Log("Screen Width : " + Screen.width);
        CalculateTotalNote();
        UpdateGuide();
        rightHandPositionRange = maxPitchPosition - minPitchPosition;
        leftHandPositionRange = maxVolumePosition - minVolumePosition;
    }

    // Update is called once per frame
    void Update()
    {
       
        Controller leap = new Controller();
        Frame frame = leap.Frame();
        Hand f1 = new Hand();
        Hand f2 = new Hand();


        List<Hand> hands = frame.Hands;

        for (int visiblehands = 0; visiblehands < hands.Count; visiblehands++)
        {

            if (hands[visiblehands].IsRight)
                f2 = hands[visiblehands];
            else
                f1 = hands[visiblehands];
        }

        //Right Hand Handle Pitch
        rightHandPosition = (f2.Fingers[1].TipPosition.y+ f2.Fingers[2].TipPosition.y + 
                        f2.Fingers[3].TipPosition.y+ f2.Fingers[4].TipPosition.y)/ 4;

      
        //Left Hand Handle Volume
        leftHandPosition = f1.Fingers[2].TipPosition.y;
       

        if (f1.IsLeft)
            gotLeftHand = true;
        else
        {
            gotLeftHand = false;
        }
          
        
        if (fixedFrequency)
            FixedFrequency();
        else
            UnFixedFrequency();
        

        if (gotLeftHand)
        {
            UpdateVolume();
        }

        if (rightHandPosition >= minPitchPosition && rightHandPosition <= maxPitchPosition)
            rightHandIn = true;
        else
            rightHandIn = false;


        if (rightHandIn) { 
            if (currentAmp > 0.5f)
                speedOfAmpChange = 5f; //interpolate faster because no popping sound at higher volume
            else
                speedOfAmpChange = 2f; //interpolate slower as volume goes to 0 to reduce popping sound

            if (!gotLeftHand)
                amplitude = 2f; //If play without left hand, default volume is 2.0f
        }
        else
        {
            speedOfAmpChange = 0.5f;
           // amplitude = 0; //If no right hand, no sound
        }


        UpdatePitchIndicator();
        UpdateVolumeIndicator();
        UpdateCurrentAmp();
        volumeDisplay.text = amplitude.ToString();
        handBorderGuide.CheckHandBoundary(f1.Fingers[2].TipPosition.x, f2.Fingers[2].TipPosition.x);
        _csound.SetChannel("Frequency", frequency);
        _csound.SetChannel("Amplitude", currentAmp);


    }

    void UpdatePitchIndicator()
    {
        indicatorPosY = pitchIndicator.anchoredPosition;
        float newYPos = ((rightHandPosition - minPitchPosition) / rightHandPositionRange) * maxIndicatorPitchPos;
        pitchIndicator.anchoredPosition = new Vector3(indicatorPosY.x, newYPos, indicatorPosY.z);
    }

    void UpdateVolumeIndicator()
    {
        volIndicatorPos = volumeIndicator.anchoredPosition;
        float newXPos = ((leftHandPosition - minVolumePosition) / leftHandPositionRange) * maxIndicatorVolPos;
        volumeIndicator.anchoredPosition = new Vector3(volIndicatorPos.x, newXPos,volIndicatorPos.z);
    }

    void UpdateVolume()
    {
        // amplitude = ((leftHandPosition - minVolumePosition) / leftHandPositionRange) * (maxAmp-0.5) +0.5;
        amplitude = ((leftHandPosition - minVolumePosition) / leftHandPositionRange) * (maxAmp) ;
        amplitude = Mathf.Clamp((float)amplitude, 0, (float)maxAmp);
        //  print(leftHandPosition);
       
    }

    public void SetWaveType(int index)
    {
        waveTypeIndex  = index;
    }


    void CalculateTotalNote()
    {
        int totalNotes = (int)(numOfOctave * 12);
        //sizeOfNote = Screen.height / totalNotes;
        sizeOfNote = (maxPitchPosition - minPitchPosition) / totalNotes;
    }

    void UpdateCurrentAmp()
    {
        if (amplitude <= 0)
        {
            if (currentAmp >= amplitude)
            {
                currentAmp -= Time.deltaTime * speedOfAmpChange;
            }
        }

        else
        {
            if (Math.Abs(currentAmp - amplitude) > 0.0f)
            {
                if (currentAmp < amplitude)
                    currentAmp += Time.deltaTime * speedOfAmpChange;
                else if (currentAmp > amplitude)
                    currentAmp -= Time.deltaTime * speedOfAmpChange;
            }
        }
       
    }

    void UpdateGuide()
    {
        uiScript.UpdateGuides();
    }

    void UnFixedFrequency()
    {
        //   mousePos = Input.mousePosition;
        //  noteIndex = mousePos.y/ sizeOfNote; //0 to 36
        noteIndex = (rightHandPosition - minPitchPosition) / sizeOfNote;
        powerRaised = noteIndex / 12; //0 to 3
        frequency = startingFrequency * System.Math.Pow(2, powerRaised);
        noteKey = (int)noteIndex % 12;
        DisplayNote(noteKey);
    }

    void FixedFrequency()
    {
        // mousePos = Input.mousePosition;
        //   noteIndex = Mathf.RoundToInt(mousePos.y/sizeOfNote);  //0 to 36 but no decimal
        noteIndex = Mathf.RoundToInt((rightHandPosition - minPitchPosition) / sizeOfNote);
        fixedPowerRaised = (float)(noteIndex / 12);
        frequency = startingFrequency * Mathf.Pow(2, fixedPowerRaised);
        noteKey = (int)noteIndex % 12;
        DisplayNote(noteKey);
    }
    /*
    void OnAudioFilterRead(float[] data, int channels)
    {
        // if (playSound)
        if (rightHandIn)
        {

           // increment = frequency * 2 * Math.PI / sampling_frequency;
            for (var i = 0; i < data.Length; i += channels)
            {
                phase += frequency * 2 * Math.PI / sampling_frequency;

                switch (waveTypeIndex)
                {
                    case 0: //Sine
                        data[i] = (float)(currentAmp * Math.Sin(phase));
                        
                        break;

                    case 1: //Triangle
                        data[i] = (float)(currentAmp * (2 * (1 / Math.PI) * (Math.PI - Math.Abs(((phase + Math.PI / 2) % (2 * Math.PI)) - Math.PI)) - 1));
                        break;

                    case 2: //Square
                        data[i] = (float)(currentAmp * Math.Sign(Math.Sin(phase)));
                        break;

                    case 3: //Sawtooth
                        data[i] = (float)(currentAmp * 2 * (((phase + Math.PI) / (2 * Math.PI)) - Mathf.Floor((float)((phase + Math.PI) / (2 * Math.PI)))) - 1);
                        break;

                    case 4: //Flute
                        data[i] = (float)(currentAmp * ((2.5 * Math.Sin(phase) + 2.0 * Math.Cos(phase) +
                                  0.4 * Math.Sin(phase * 2.0) + 0.4 * Math.Cos(phase * 2.0) -
                                  0.4 * Math.Sin(phase * 3.0) + 0.2 * Math.Cos(phase * 3.0) -
                                  0.2 * Math.Sin(phase * 4.0) + 0.1 * Math.Cos(phase * 4.0) -
                                  0.1 * Math.Sin(phase * 5.0) + 0.0 * Math.Cos(phase * 5.0)) / 3.0));
                        break;

                    case 5: //Violin
                        data[i] = (float)(currentAmp * ((2.0 * Math.Sin(phase) - 2.9 * Math.Cos(phase) +
                                                                            0.9 * Math.Sin(phase * 2.0) - 0.9 * Math.Cos(phase * 2.0) -
                                                                            0.3 * Math.Sin(phase * 3.0) + 0.6 * Math.Cos(phase * 3.0) -
                                                                            0.9 * Math.Sin(phase * 4.0) - 1.8 * Math.Cos(phase * 4.0) -
                                                                            0.5 * Math.Sin(phase * 5.0) - 0.4 * Math.Cos(phase * 5.0)) / 5.0));
                        break;

                }

                if (channels == 2)
                {
                    data[i + 1] = data[i];
                }

               if (phase >= 2 * Mathf.PI)
            {
                phase -= 2 * Mathf.PI;
            }
            }

        }


    }
    */

    public void SetOctave(float newNumber)
    {
        numOfOctave = newNumber;
        CalculateTotalNote();
    }

    public int GetOctave()
    {

        return (int)numOfOctave;

    }

    public void SwitchPlay()
    {
        playSound = !playSound;
    }

    public void FixedUnFixed()
    {
        fixedFrequency = !fixedFrequency;
    }
    void DisplayNote(int noteKey)
    {
        string notePlayed = "";
        switch (noteKey)
        {
            case 0:
                notePlayed = "A";
                break;
            case 1:
                notePlayed = "A#";
                break;
            case 2:
                notePlayed = "B";
                break;
            case 3:
                notePlayed = "C";
                break;
            case 4:
                notePlayed = "C#";
                break;
            case 5:
                notePlayed = "D";
                break;
            case 6:
                notePlayed = "D#";
                break;
            case 7:
                notePlayed = "E";
                break;
            case 8:
                notePlayed = "F";
                break;
            case 9:
                notePlayed = "F#";
                break;
            case 10:
                notePlayed = "G";
                break;
            case 11:
                notePlayed = "G#";
                break;

        }

        uiScript.UpdateNoteDisplay(notePlayed);
    }
}
