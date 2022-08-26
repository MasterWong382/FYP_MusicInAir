using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandBorderGuide : MonoBehaviour
{
    public GameObject volBorderLeft;
    public GameObject volBorderRight;
    public GameObject pitchBorderLeft;
    public GameObject pitchBorderRight;
    // Start is called before the first frame update
    public void CheckHandBoundary(float x, float y)
    {
        if (x < -250)
            volBorderLeft.SetActive(true);
        else
          volBorderLeft.SetActive(false);
        if (x > 25)
            volBorderRight.SetActive(true);
        else
            volBorderRight.SetActive(false);

        if (y < -20)
            pitchBorderLeft.SetActive(true);
        else
            pitchBorderLeft.SetActive(false);
        if (y > 240)
            pitchBorderRight.SetActive(true);
        else
            pitchBorderRight.SetActive(false);


    }
}
