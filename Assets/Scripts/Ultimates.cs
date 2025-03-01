using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ultimates : MonoBehaviour
{
    public List<GameObject> Ultimate;
    public int ultNum;

    public GameObject currentUlt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentUlt.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (ultNum < Ultimate.Count - 1)
            {
                ultNum++;
            }
            else
            {
                ultNum = 0;
            }
            currentUlt = Ultimate[ultNum];
        }
    }
}
