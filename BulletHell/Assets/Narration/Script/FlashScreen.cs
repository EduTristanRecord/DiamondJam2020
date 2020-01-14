using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashScreen : MonoBehaviour 
{

    public CanvasGroup whiteScreen;
    private bool flash = false;
    

    void Start() {
        whiteImage.alpha = 0;
    }

    void Update() {
        if (flash)
            whiteScreen.alpha = whiteScreen.alpha - Time.deltaTime;
    }

    public void UseFlash() {
        whiteScreen.alpha = 1;
        flash = true;
    }
}