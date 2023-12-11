using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class uicont : MonoBehaviour
{
    public TMP_Text score, intro;
    public Slider bar;
    public bool started = false;
    int total;
    int scr;
    float messageTime;
    string message;

    void Start()
    {
        total = 3;
    }

    void Update()
    {
        messageTime -= Time.deltaTime;
        bar.maxValue = total;
        bar.value = scr;

        if (!started){
            intro.text = "Tap on the starting location after scanning the play area";
        }
        else{
            intro.text = "";
            score.text = scr + " / " + total;
        }
        if (messageTime > 0) {
            intro.text = message;
        }
    }

    public void star(){
        started = true;
    }

    public bool begun(){
        return started; 
    }

    public void incriment(){
        scr++;
    }

    public void reset(){
        scr = 0;
        total++;
    }

    public void showMessage(string m) {
        message = m;
        messageTime = 5;
    }

    public bool collected() {
        if (scr == total) return true;
        else return false;
    }
}
