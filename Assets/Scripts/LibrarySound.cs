using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LibrarySound : MonoBehaviour
{
    /*
    // Start is called before the first frame update
    [Tooltip("The names of the sound font files to copy from Resources to the Persistent Data Path folder. " +
        "Don't specify the extension. In this example will be added the '.sf2' extension to the copied files.")]
    [SerializeField] private string[] _soundFontsNames;
    [Tooltip("Ensure this CsoundUnity GameObject is inactive when hitting play, " +
        "otherwise the CsoundUnity initialization will run. " +
        "Setting the Environment Variables on a running Csound instance can have unintended effects.")]
    */
    CsoundUnity csoundUnity;
    public GameObject[] allInstruments;
    double currentAmp = 0;
    double frequency = 440;
    public float test;
    bool rightHandIn;
    public int preset;
    int instrumentIndex = 0;
    void Start()
    {
        /*
        csoundUnity.enabled = false;
        test = 1;
        foreach (var sfName in _soundFontsNames)
        {
            var dir = Path.Combine(Application.persistentDataPath, "CsoundFiles");
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);

            var destinationPath = Path.Combine(dir, sfName + ".sf2");
            if (!File.Exists(destinationPath))
            {
                var sf = Resources.Load<TextAsset>(sfName);
                Debug.Log($"Writing sf file at path: {destinationPath}");
                Stream s = new MemoryStream(sf.bytes);
                BinaryReader br = new BinaryReader(s);
                using (BinaryWriter bw = new BinaryWriter(File.Open(destinationPath, FileMode.OpenOrCreate)))
                {
                    bw.Write(br.ReadBytes(sf.bytes.Length));
                }
            }
        }
        // activate CsoundUnity!
        csoundUnity.SetChannel("Preset", preset);
        csoundUnity.enabled = true;
        */
        csoundUnity = allInstruments[0].GetComponent<CsoundUnity>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rightHandIn)
        {
            csoundUnity.SetChannel("Frequency", frequency);
            csoundUnity.SetChannel("Amplitude", currentAmp);

        }
        else
        {
            csoundUnity.SetChannel("Amplitude", 0);
        }
    }

    void UpdateInstrument()
    {
        TurnOffAllInstruments();
        allInstruments[instrumentIndex].SetActive(true);
        csoundUnity = allInstruments[instrumentIndex].GetComponent<CsoundUnity>();
    }

    void TurnOffAllInstruments()
    {
        for (int i = 0; i < allInstruments.Length; i++)
        {
            allInstruments[i].SetActive(false);
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

    public void SetWaveTypeIndex(int index)
    {
        instrumentIndex = index;
        UpdateInstrument();
    }

}
