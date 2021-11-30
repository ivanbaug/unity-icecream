using UnityEngine;
using System.Collections;

public class SiloVanillaAnimation : MonoBehaviour {
    private Component haloEnf; //Halo enfriador
    private Component haloVal; //Halo valvula
    private Transform valveChild;
    public ParticleSystem vanillaParticles;
    private bool animando = false;

	// Use this for initialization
	void Start () {
        valveChild=transform.Find("Valve");
        haloEnf = GetComponent("Halo");
        haloVal = valveChild.GetComponent("Halo");
	    
	}
	
	// Update is called once per frame
	void Update () {
        if (WorldGUI.sI49){
            haloEnf.GetType().GetProperty("enabled").SetValue(haloEnf, true, null);
        }

        else
        {
            haloEnf.GetType().GetProperty("enabled").SetValue(haloEnf, false, null);
        }

        if (WorldGUI.sI43) 
        {
            haloVal.GetType().GetProperty("enabled").SetValue(haloVal, true, null);
            vanillaParticles.Play();
            StartCoroutine(TimerEnumerator());
            animando = true;
        }
            
        else
        {
            if (animando)
            {
                animando = false;
                CupGenerator.addChildCommand = true;
                WorldGUI.sO44 = false;
            }
            haloVal.GetType().GetProperty("enabled").SetValue(haloVal, false, null);
            vanillaParticles.Stop();
            
            
        }
           

	}
    IEnumerator TimerEnumerator()
    {
        yield return new WaitForSeconds(2);
        WorldGUI.sO44 = true;//Sabor1 lleno
    }
}
