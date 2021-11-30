using UnityEngine;
using System.Collections;

public class MenuBegin : MonoBehaviour {

    public bool isQuit = false;

    void OnMouseEnter()
    {
        renderer.material.color = Color.yellow;
        animation.Play("ResizeUp");
    }
    void OnMouseExit()
    {
        renderer.material.color = Color.white;
        animation.Play("ResizeDown");
    }
    void OnMouseDown()
    {
        if (isQuit)
        {
            Application.Quit();
        }
        else
        {
            Application.LoadLevel(1);
        }

    }
}
