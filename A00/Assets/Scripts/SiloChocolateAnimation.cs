using UnityEngine;
using System.Collections;

public class SiloChocolateAnimation : MonoBehaviour {
    private Component haloEnf; //Halo enfriador
    private Component haloVal; //Halo valvula
    private Transform valveChild;
    public ParticleSystem chocolateParticles;
    private bool animando = false;

	// Use this for initialization
	void Start () {
        valveChild = transform.Find("Valve");
        haloEnf = GetComponent("Halo");
        haloVal = valveChild.GetComponent("Halo");
	}
	
	// Update is called once per frame
	void Update () {
        if (WorldGUI.sI49)
        {
            haloEnf.GetType().GetProperty("enabled").SetValue(haloEnf, true, null);
        }

        else
        {
            haloEnf.GetType().GetProperty("enabled").SetValue(haloEnf, false, null);
        }

        if (WorldGUI.sI41){
            haloVal.GetType().GetProperty("enabled").SetValue(haloVal, true, null);
            chocolateParticles.Play();
            StartCoroutine(TimerEnumerator());
            animando = true;
        }
            
        else{
            if (animando)
            {
                animando = false;
                CupGenerator.addChildCommand = true;                
            }
            haloVal.GetType().GetProperty("enabled").SetValue(haloVal, false, null);
            chocolateParticles.Stop();
            //WorldGUI.sO40 = false;
        }
            
	}
    IEnumerator TimerEnumerator()
    {
        yield return new WaitForSeconds(2);
        WorldGUI.sO40 = true;//Sabor2 lleno
    }
}
