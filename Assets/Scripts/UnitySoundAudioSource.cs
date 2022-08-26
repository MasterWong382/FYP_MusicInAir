using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Leap;
using Leap.Unity;

public class UnitySoundAudioSource : MonoBehaviour
{
    public UIHand uiScript;
    AudioSource audioSource;
    Vector3 mousePos;
    double frequency = 440;
    double amplitude = 1;
    double maxAmp = 4;
    private double increment;
    private double phase;
    private double sampling_frequency = 48000;
    bool playSound = false;
    float powerRaised;
    float fixedPowerRaised;
    float sizeOfNote;
    int noteKey;
    public float numOfOctave;
    float noteIndex = 0;
    bool fixedFrequency = true;
    int maxPitchPosition = 486;
    int minPitchPosition = 145;
    int rightHandPositionRange = 0;
    float rightHandPosition;

    int maxVolumePosition = 0;
    int minVolumePosition = -200;
    float leftHandPositionRange;
    float leftHandPosition;

    Vector3 unityCoord;
    public Camera cam;
    public RectTransform pitchIndicator;
    Vector3 indicatorPosY;
    public int maxIndicatorYPos = 278;

    public RectTransform volumeIndicator;
    Vector3 indicatorPosX;
    public int maxIndicatorXPos = 128;
    int waveTypeIndex = 0;
    double lerpVolume = 0;
    bool canLerpVol = false;
    double lastFrequency = 0;
    // Start is called before the first frame update
    void Start()
    {
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
        rightHandPosition = (f2.Fingers[0].TipPosition.y + f2.Fingers[1].TipPosition.y + f2.Fingers[2].TipPosition.y + f2.Fingers[3].TipPosition.y + f2.Fingers[4].TipPosition.y)/5;

        //Left Hand Handle Volume
        leftHandPosition = f1.Fingers[2].TipPosition.x;

       // unityCoord = f2.Fingers[0].TipPosition.ToVector3();
       //   cam.WorldToViewportPoint
       //  pointer.transform.position = unityCoord;

        // print(screenPos);

        if (fixedFrequency)
        {
            FixedFrequency();
        }
        else {
            UnFixedFrequency();
        }

        UpdateVolume();

        UpdatePitchIndicator();
        UpdateVolumeIndicator();
    }

    void UpdatePitchIndicator()
    {
        indicatorPosY = pitchIndicator.anchoredPosition;
        float newYPos = ((rightHandPosition-minPitchPosition) / rightHandPositionRange) * maxIndicatorYPos;
        pitchIndicator.anchoredPosition = new Vector3(indicatorPosY.x, newYPos, indicatorPosY.z);
    }

    void UpdateVolumeIndicator()
    {
        indicatorPosX = volumeIndicator.anchoredPosition;
        float newXPos = ((leftHandPosition - minVolumePosition) / leftHandPositionRange) * maxIndicatorXPos;
        volumeIndicator.anchoredPosition = new Vector3(newXPos, indicatorPosX.y, indicatorPosX.z);
    }

    void UpdateVolume()
    {
        // amplitude = ((leftHandPosition - minVolumePosition) / leftHandPositionRange) * (maxAmp-0.5) +0.5;
        amplitude = ((leftHandPosition - minVolumePosition) / leftHandPositionRange) * (maxAmp)+0.01f;
        if (amplitude < 0)
        {
            amplitude = 0.01f;
        }
        print(amplitude);
    }

    public void SetWaveType(int index)
    {
        waveTypeIndex = index;
    }
    void CalculateTotalNote()
    {
        int totalNotes = (int)(numOfOctave * 12);
        //sizeOfNote = Screen.height / totalNotes;
        sizeOfNote = (maxPitchPosition-minPitchPosition)/ totalNotes;
    }

    void UpdateGuide()
    {
        uiScript.UpdateGuides();
    }

    void UnFixedFrequency()
    {
     //   mousePos = Input.mousePosition;
      //  noteIndex = mousePos.y/ sizeOfNote; //0 to 36
        noteIndex = (rightHandPosition-minPitchPosition) / sizeOfNote;
        powerRaised = noteIndex / 12; //0 to 3
        frequency = 220 * Mathf.Pow(2, powerRaised);
        noteKey = (int)noteIndex % 12;
        DisplayNote(noteKey);
    } 

    void FixedFrequency()
    {
        // mousePos = Input.mousePosition;
        //   noteIndex = Mathf.RoundToInt(mousePos.y/sizeOfNote);  //0 to 36 but no decimal
        noteIndex = Mathf.RoundToInt((rightHandPosition - minPitchPosition) / sizeOfNote);
        fixedPowerRaised = noteIndex / 12;
        frequency = 220 * Mathf.Pow(2, fixedPowerRaised);  
        noteKey = (int)noteIndex % 12;
        DisplayNote(noteKey);
    }

    void OnAudioFilterRead(float[] data, int channels)

    {
        // if (playSound)
        if (rightHandPosition >= minPitchPosition && rightHandPosition <= maxPitchPosition)
        {
            increment = frequency * 2 * Math.PI / sampling_frequency;
            for (var i = 0; i < data.Length; i += channels)
            {
                phase += increment;

                switch (waveTypeIndex)
                {
                    case 0:
                        data[i] = (float)(amplitude * Math.Sin(phase));
                        break;
                    case 1:
                        data[i] = (float)(amplitude * (2 * (1 / Math.PI) * (Math.PI - Math.Abs(((phase + Math.PI / 2) % (2 * Math.PI)) - Math.PI)) - 1));
                        break;
                    case 2:
                        //   data[i] = (float)(amplitude * 2 * (((phase + Math.PI) / (2 * Math.PI)) - Mathf.Floor((float)((phase + Math.PI) / (2 * Math.PI)))) - 1);
                      double y = Math.Sin(phase) * Math.Exp(-0.0004 * phase);
                      double y1 = Math.Sin(2*phase) * Math.Exp(-0.0004 * phase)/2;
                      double y2 = Math.Sin(3*phase) * Math.Exp(-0.0004 * phase)/4;
                      double y3 = Math.Sin(4 * phase) * Math.Exp(-0.0004 * phase) / 8;
                      double y4 = Math.Sin(5* phase) * Math.Exp(-0.0004 * phase)/ 16;
                      double y5 = Math.Sin(6 * phase) * Math.Exp(-0.0004 * phase) / 32;
                        y = y + y1 + y2 + y3 + y4 + y5;
                        //y += y * y * y;

                        data[i] = (float)(amplitude * y);


                        break;

                }

                if (channels == 2)
                {
                    data[i + 1] = data[i];
                }

                if (phase > 2 * Math.PI)
                {
                    phase = 0;
                }
            }
            canLerpVol = true;
            lerpVolume = amplitude;
            lastFrequency = frequency;
        }
        else
        {
           // print("lala");
          //  print(lerpVolume);
            if (canLerpVol)
            {
                LerpVolume();
            }
            increment = lastFrequency * 2 * Math.PI / sampling_frequency;
            for (var i = 0; i < data.Length; i += channels)
            {
                phase += increment;

                switch (waveTypeIndex)
                {
                    case 0:
                        data[i] = (float)(lerpVolume * Math.Sin(phase));
                        break;
                    case 1:
                        data[i] = (float)(lerpVolume * (2 * (1 / Math.PI) * (Math.PI - Math.Abs(((phase + Math.PI / 2) % (2 * Math.PI)) - Math.PI)) - 1));
                        break;
                    case 2:
                        data[i] = (float)(lerpVolume * 2 * (((phase + Math.PI) / (2 * Math.PI)) - Mathf.Floor((float)((phase + Math.PI) / (2 * Math.PI)))) - 1);
                        break;

                }

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

    void LerpVolume()
    {
    
        if (lerpVolume > 0.1)
        {
        
            lerpVolume -= 0.05f;
        }
        else
        {
            if (lerpVolume > 0f)
            {
      
                lerpVolume -= 0.005f;
            }
            else
            {
                lerpVolume = 0f;
                canLerpVol = false;
            }
   
        }
      
    }
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
