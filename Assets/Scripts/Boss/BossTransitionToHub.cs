using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTransitionToHub : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.B))  //BOSS ROOM TRANSITION CLAUSE - Change to input if kept in final build
            {
                SceneHandler.Instance.ToHubScene();
            }
    }
}
