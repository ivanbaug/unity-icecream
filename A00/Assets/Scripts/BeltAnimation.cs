using UnityEngine;
using System.Collections;

public class BeltAnimation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (CupGenerator.cupMoving)
            animation.Play("BeltRim");
        else
            animation.Stop("BeltRim");
	}
}
