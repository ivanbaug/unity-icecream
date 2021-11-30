using UnityEngine;
using System.Collections;

public class PlaceLidAnimation : MonoBehaviour {
    private bool animatedYet=false;
    private bool animatedYet2 = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (WorldGUI.sI39 && !animatedYet)
        {
            animation.Play("LidDispenserExtend");
            animatedYet = true;
        }
        if (animatedYet && !animation.IsPlaying("LidDispenserExtend"))
        {
            WorldGUI.sO38 = true; //Sets the sensor on te conveyor belt  as true
                    }
        if (!WorldGUI.sI39 && animatedYet)
        {
            CupGenerator.addChildCommand = true; //Display Cup
            animation.Play("LidDispenserRetract");
            animatedYet = false;
            animatedYet2 = true;
        }
        if (animatedYet2 && !animation.IsPlaying("LidDispenserRetract"))
        {
            animatedYet2 = false;
            CupGenerator.moveCupCommand = true;
            WorldGUI.sO44 = false;
            WorldGUI.sO40 = false;
        }
	}
}
