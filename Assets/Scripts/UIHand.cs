using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHand : MonoBehaviour
{
    // Start is called before the first frame update
    public Text noteDisplay;
    public GameObject guide1;
    public GameObject guide2;
    public GameObject guide1AndHalf;
    public GameObject guide3;

    public GameObject guide1_piano;
    public GameObject guide2_piano;
    public GameObject guide1AndHalf_piano;
    public GameObject guide3_piano;


    public UnitySoundLeap unitySound;
    float numOfOctave=3;
    string[] savedOctave = {"1","1.5","2","3"};
    int currentOctaveIndex = 3; //default is 2 octaves
    public Text octaveStr;


    public GameObject settingPage;
    public Text fixedOrUnfixed;

    //instrument Select
    public Dropdown musicSelectDropDown;

    public GameObject keyBoardGuidesOnly;
    public GameObject keyGuidesOnly;
    bool showPianoGuide = false;
 

    void Start()
    {
        unitySound.SetOctave(numOfOctave);
        numOfOctave = unitySound.GetOctave();
        octaveStr.text = numOfOctave.ToString();
        fixedOrUnfixed.text = "UnFixed";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateNoteDisplay(string note)
    {
        noteDisplay.text = note;
    }

    public void UpdateGuides()
    {
        OffAllGuide();


        if (numOfOctave == 2)
        {
            guide2.SetActive(true);
            guide2_piano.SetActive(true);
        }

        else if (numOfOctave == 1.5)
        {
            guide1AndHalf.SetActive(true);
            guide1AndHalf_piano.SetActive(true);
        }

        else if (numOfOctave == 1)
        {
            guide1.SetActive(true);
            guide1_piano.SetActive(true);

        }
        else if (numOfOctave == 3)
        {
            guide3.SetActive(true);
            guide3_piano.SetActive(true);
        }
        octaveStr.text = numOfOctave.ToString();
    }

    void OffAllGuide()
    {
        guide1.SetActive(false);
        guide1AndHalf.SetActive(false);
        guide2.SetActive(false);
        guide3.SetActive(false);
        guide1_piano.SetActive(false);
        guide1AndHalf_piano.SetActive(false);
        guide2_piano.SetActive(false);
        guide3_piano.SetActive(false);
    }

    public void IncreaseOctave()
    {
        if (currentOctaveIndex < savedOctave.Length - 1){

            currentOctaveIndex++;
            numOfOctave = float.Parse(savedOctave[currentOctaveIndex]);
            UpdateGuides();
            unitySound.SetOctave(numOfOctave);
        }

    }

    public void DecreaseOctave()
    {
        if (currentOctaveIndex > 0)
        {
            currentOctaveIndex--;
            numOfOctave = float.Parse(savedOctave[currentOctaveIndex]);
            UpdateGuides();
            unitySound.SetOctave(numOfOctave);
        }
    }


    public void DisplaySetting()
    {
        settingPage.SetActive(true);
    }

    public void HideSettings()
    {
        settingPage.SetActive(false);
    }

    public void PlaySound()
    {
        unitySound.SwitchPlay();
    }

    public void SwitchFixedFrequency()
    {
        unitySound.FixedUnFixed();
        
        if (fixedOrUnfixed.text == "Fixed")
        {
            fixedOrUnfixed.text = "UnFixed";
        }
        else
        {
            fixedOrUnfixed.text = "Fixed";
        }

    }

      public void UpdateInstrument()
    {
        unitySound.SetWaveType(musicSelectDropDown.value);
    }
   

    public void ToggleNoteGuide()
    {
        keyGuidesOnly.SetActive(!keyGuidesOnly.activeSelf);
    }


    public void ToggleKeyboardGuide()
    {
        keyBoardGuidesOnly.SetActive(!keyBoardGuidesOnly.activeSelf);
    }

 
}
