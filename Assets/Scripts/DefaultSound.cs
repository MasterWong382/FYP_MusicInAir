using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class DefaultSound : MonoBehaviour
{


    private double phase;
    private double sampling_frequency = 48000;

    int waveTypeIndex = 0;
    double frequency = 440;
    double currentAmp = 0;
    double maxAmp = 4;
    bool rightHandIn;

    // Start is called before the first frame update
    public void SetAmp(double amp)
    {
        currentAmp = amp;
    }

    public void SetFrequency(double freq)
    {
        frequency = freq;
    }
    public void SetWaveTypeIndex(int index)
    {
        waveTypeIndex = index;
    }

    public void isRightHandIn(bool rightHand)
    {
        rightHandIn = rightHand;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        // if (playSound)
        if (rightHandIn)
           {
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
}