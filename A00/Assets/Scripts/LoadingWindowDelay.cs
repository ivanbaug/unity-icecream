using UnityEngine;
using System.Collections;

public class LoadingWindowDelay : MonoBehaviour {

    public int secs2wait = 3;
    public bool timeIsUp = false;
	// Use this for initialization
	void Start () {
        StartCoroutine(TimerEnumerator());
	}

    IEnumerator TimerEnumerator()
    {
        yield return new WaitForSeconds(secs2wait);
        timeIsUp = true;
    }
	
	// Update is called once per frame
	void Update () {
        if (timeIsUp)
        {
            Application.LoadLevel(2);
        }
	}
}
