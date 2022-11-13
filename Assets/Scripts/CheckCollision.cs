using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    // Start is called before the first frame update
    public StartScreen startScreen;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="rightGuide")
        {
            startScreen.SetIsCollding(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "rightGuide")
        {
            startScreen.SetIsCollding(false);
        }
    }
}
