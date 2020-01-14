using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashScreen : MonoBehaviour 
{

    public CanvasGroup whiteScreen;
    private bool flash = false;
    

    void Start() {
        whiteScreen.alpha = 0;
    }

    void Update() {
        if (flash) {
            if (whiteScreen.alpha > 0) {
                whiteScreen.alpha = whiteScreen.alpha - Time.deltaTime;
            }
            else if (whiteScreen.alpha <= 0) {
                whiteScreen.alpha = 0;
                flash = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            UseFlash();
        }
    }

    public void UseFlash() {
        whiteScreen.alpha = 1;
        flash = true;
    }
}