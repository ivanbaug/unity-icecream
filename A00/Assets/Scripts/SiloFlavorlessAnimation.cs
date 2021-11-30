using UnityEngine;
using System.Collections;

public class SiloFlavorlessAnimation : MonoBehaviour {

    public Component halo00;
    public Component halo01;

	// Use this for initialization
	void Start () {
        halo00 = transform.Find("Pump00").GetComponent("Halo");
        halo01 = transform.Find("Pump01").GetComponent("Halo");
	}
	
	// Update is called once per frame
	void Update () {
        
        if (WorldGUI.sI53)
            halo00.GetType().GetProperty("enabled").SetValue(halo00, true, null);
        else
            halo00.GetType().GetProperty("enabled").SetValue(halo00, false, null);

        if (WorldGUI.sI51)
            halo01.GetType().GetProperty("enabled").SetValue(halo01, true, null);
        else
            halo01.GetType().GetProperty("enabled").SetValue(halo01, false, null);
	}
}
