using UnityEngine;
using System.Collections;

public class PlaceCupAnimation : MonoBehaviour {
    private bool animatedYet=false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (WorldGUI.sI47 && !animatedYet)
        {
            animation.Play("CupDispenserExtend");
            animatedYet = true;
        }
        if (animatedYet && !animation.IsPlaying("CupDispenserExtend"))
        {
            WorldGUI.sO48 = true; //Sets the sensor on te conveyor belt  as true
                    }
        if (!WorldGUI.sI47 && animatedYet)
        {
            CupGenerator.addCupCommand = true; //Display Cup
            animation.Play("CupDispenserRetract");
            animatedYet = false;
        }
	}
}
