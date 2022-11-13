using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Leap;
using Leap.Unity;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    // Start is called before the first frame update
    float rightHandPositionX;
    float rightHandPositionY;  //100 to 300 for sitting

    float leftHandPositionX;
    float leftHandPositionY;  //100 to 300 for sitting

    float minLeapY = 100;
    float maxLeapY = 300;
    float minWorldY = -10f;
    float maxWorldY = 10f;

    float minLeapX = -200;
    float maxLeapX = 200;
    float minWorldX = -18f;
    float maxWorldX = 18f;
    bool gotLeftHand = false;
    public Transform rightHandGuide;
    public Transform leftHandGuide;
    public GameObject powerCharge;
    public AudioSource sparkAudio;
    public AudioSource sparkAudioLeft;
    public AudioSource chargingSound;
    public AudioSource startBeepSound;

    bool isColliding;
    float countDownToStart = 0f;

    public Animator titleAnim;
    public Animator subtitleAnim;
    public GameObject instruction;
    void Start()
    {
        
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
     
      
          rightHandPositionX = f2.Fingers[2].TipPosition.x;
          rightHandPositionY = f2.Fingers[2].TipPosition.y;

          leftHandPositionX = f1.Fingers[2].TipPosition.x;
          leftHandPositionY = f1.Fingers[2].TipPosition.y;


        if (rightHandPositionY > 100 && leftHandPositionY > 100)
        {   //Detect Hand above leap motion
            CheckForCollision();
            instruction.SetActive(true);
        }
        else
        {
            chargingSound.Stop();
            powerCharge.SetActive(false);
        }
     


      if (rightHandPositionY > 100)
        {
            if (!sparkAudio.isPlaying)
            {
                sparkAudio.Play();
            }
        }
        else
        {
            sparkAudio.Stop();
        }

        if (leftHandPositionY > 100)
        {
            if (!sparkAudioLeft.isPlaying)
            {
                sparkAudioLeft.Play();
            }
        }
        else
        {
            sparkAudioLeft.Stop();
        }




        //Left Hand Handle Volume


        if (f1.IsLeft)
            gotLeftHand = true;
        else
        {
            gotLeftHand = false;
        }
        UpdateLeftGuide();
        UpdateRightGuide();
    }

    void CheckForCollision()
    {
        if (isColliding)
        {
            if (!chargingSound.isPlaying)
            {
                chargingSound.Play();
            }
            countDownToStart += Time.deltaTime;
            powerCharge.SetActive(true);
            if (countDownToStart > 4f)
            {
                print("Start!");
                StartCoroutine(StartGame());
                titleAnim.SetBool("FadeUp", true);
                subtitleAnim.SetBool("FadeUp", true);
                //  LoadLevel();
            }
        }
        else
        {
            chargingSound.Stop();
            powerCharge.SetActive(false);
        }
    }
    void UpdateLeftGuide()
    {
        Vector3 currentPos = leftHandGuide.position;
        float displacementX = (leftHandPositionX - minLeapX) / (maxLeapX - minLeapX);
        float displacementY = (leftHandPositionY - minLeapY) / (maxLeapY - minLeapY);
        float mappedX = minWorldX + displacementX * (maxWorldX - minWorldX);
        float mappedY = minWorldY + displacementY * (maxWorldY - minWorldY);
        leftHandGuide.position = new Vector3(mappedX, mappedY, currentPos.z);
    }

    void UpdateRightGuide()
    {
        Vector3 currentPos = rightHandGuide.position;
        float displacementX = (rightHandPositionX - minLeapX) / (maxLeapX - minLeapX);
        float displacementY = (rightHandPositionY - minLeapY) / (maxLeapY - minLeapY);
        float mappedX = minWorldX + displacementX * (maxWorldX - minWorldX);
        float mappedY = minWorldY + displacementY * (maxWorldY - minWorldY);
        rightHandGuide.position = new Vector3(mappedX, mappedY, currentPos.z);
    }

    public void SetIsCollding(bool isCollide)
    {
        isColliding = isCollide;
    }

    IEnumerator StartGame()
    {
        chargingSound.Stop();
        if (!startBeepSound.isPlaying)
        {
            startBeepSound.Play();
        }
      
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene("MainLevel");
    }

}
