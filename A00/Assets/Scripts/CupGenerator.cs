using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CupGenerator : MonoBehaviour {
    public GameObject[] cupPrefabs;
    public GameObject[] spawnPoints;
    public  int numberCup = 0;
    public static bool addCupCommand = false;
    public static bool moveCupCommand = false;
    public static bool addChildCommand = false;
    public static bool cupMoving = false;
    private List<GameObject> mCup; //Set this one to public to debug on unity's gui
    private Transform clone;  //Set this one to public to debug on unity's gui
    public List<Transform> childClone;

    private int estado = 0;

    public bool childFound = false;
    
	// Use this for initialization
	void Start () {
        mCup = new List<GameObject>();
	}
    void Update()
    {
        if (addCupCommand)
        {
            addCupCommand = false;
            addCup();
        }
        if (moveCupCommand)
        {
            moveCupCommand = false;
            moveCup(numberCup-1);
        }
        if (addChildCommand)
        {
            addChildCommand = false;
            addChild(numberCup - 1);
        }
    }

    public void addCup()
    {
        if ((cupPrefabs.Length > 0) && (spawnPoints.Length > 0))
        {
            clone = Instantiate(cupPrefabs[0].transform, spawnPoints[0].transform.position, spawnPoints[0].transform.rotation) as Transform;
            mCup.Add(clone.gameObject);
            numberCup++;
        }
        //GameObject temp =Instantiate(mCup[0]) as GameObject;
        //Debug.Log("mCup: " + temp.ToString());
    }
    public void moveCup(int cup)
    {
        cupMoving = true;
        
        Vector3 newPos = new Vector3(-0.0f, 0.0f, 0.0f);

        switch (estado)
        {
            case 0:
                newPos = mCup[cup].transform.position + new Vector3(-1f, 0.0f, 0.0f);
                WorldGUI.sO48 = false;//Sets first sensor as false
                break;
            case 1:
                newPos = mCup[cup].transform.position + new Vector3(-1.2f, 0.0f, 0.0f);
                WorldGUI.sO44 = false;
                WorldGUI.sO46 = false;//Sets second sensor as false
                break;
            case 2:
                newPos = mCup[cup].transform.position + new Vector3(-1.17f, 0.0f, 0.0f);
                WorldGUI.sO42 = false;//Sets third sensor as false
                WorldGUI.sO40 = false;
                break;
            case 3:
                newPos = mCup[cup].transform.position + new Vector3(-5f, -1.42f, cup*-0.5f);
                WorldGUI.sO38 = false;//Sets fourth sensor as false
                childClone.Clear();
                estado = -1;
                break;
        }

        estado++;
        StartCoroutine(MoveObject(mCup[cup].transform, mCup[cup].transform.position, newPos, 3.0f));
    }

    public void addChild(int cup)
    {
        foreach (Transform child in mCup[cup].transform)
        {
            childClone.Add(child);
        }
        switch (estado)
        {
            case 0:
                break;
            case 1:
                childClone[2].gameObject.SetActive(true);
                break;
            case 2:
                childClone[1].gameObject.SetActive(true);
                break;
            case 3:
                childClone[0].gameObject.SetActive(true);
                break;
        }
    }


    IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        float i = 0.0f;
        float rate = 1.0f / time;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
        cupMoving = false;
        switch (estado)
        {
            case 0:
                
                break;
            case 1:
                WorldGUI.sO46 = true;
                break;
            case 2:
                WorldGUI.sO42 = true;
                break;
            case 3:
                WorldGUI.sO38 = true;
                //childClone[0].gameObject.SetActive(true);
                break;
        }

    }
}
