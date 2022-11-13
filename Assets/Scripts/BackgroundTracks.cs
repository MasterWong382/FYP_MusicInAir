using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BackgroundTracks : MonoBehaviour
{
    public AudioClip[] allTracks;
    AudioSource audioSource;
    public int[] timerToStart;

    int currentTrack = 0;
    bool playTrack = false;
    bool startCountDown = false;
    float countDown = 0;
    public Image displayScore;
    public Sprite[] pianoScores;
    public Text timer;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        SetCurrentTrack(0); //Default 0
    }

    // Update is called once per frame
    void Update()
    {
        if (startCountDown)
        {
            if (countDown>0)
            {
                countDown -= Time.deltaTime;
                timer.gameObject.SetActive(true);
                timer.text = "Starting in  " + ((int)countDown).ToString() + " seconds";
                print(timer);
            }
            else
            {
                ResetTimer();
            }
        }
    }

    private void ResetTimer()
    {
        startCountDown = false;
        countDown = 0;
        timer.gameObject.SetActive(false);
    }
    public void PlayTrack()
    {
        playTrack = !playTrack;
        if (playTrack)
        {
            timer.gameObject.SetActive(true);
            countDown = timerToStart[currentTrack];
            startCountDown = true;
            audioSource.Play();
        }
        else
        {
            timer.gameObject.SetActive(false);
            startCountDown = false;
            audioSource.Stop();
        }
    }
 
    void DisplayPianoSheet()
    {
        displayScore.sprite = pianoScores[currentTrack];
    }

    void LoadAudioClip()
    {
        audioSource.clip = allTracks[currentTrack];
    }

    public void SetCurrentTrack(int index)
    {
        currentTrack = index;
        LoadAudioClip();
        DisplayPianoSheet();
    }
    

}
