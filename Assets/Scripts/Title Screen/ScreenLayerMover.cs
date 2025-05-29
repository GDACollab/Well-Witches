using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ScreenLayerMover : MonoBehaviour
{
    [SerializeField] Image layer1;
    [SerializeField] float layer1MoveStrength;
    Vector3 layer1BasePosition;
    [SerializeField] Image layer2;
    [SerializeField] float layer2MoveStrength;
    Vector3 layer2BasePosition;

    // Start is called before the first frame update
    void Start()
    {
        layer1BasePosition = layer1.transform.position;
        layer2BasePosition = layer2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("mousePos:" + Input.mousePosition);
        Vector3 mouseInfo = MouseBasedOnCenter();
        Vector3 L1pos = layer1BasePosition - mouseInfo * layer1MoveStrength;
        //clamp the position so that it can't go too far and revea the edges
        Vector3 L1pos2 = new Vector3(Mathf.Clamp(L1pos.x, -layer1.rectTransform.rect.width * 0.05f, layer1.rectTransform.rect.width * 0.05f),
            Mathf.Clamp(L1pos.y, -layer1.rectTransform.rect.height * 0.05f, layer1.rectTransform.rect.height * 0.05f));
        layer1.rectTransform.localPosition = L1pos2;
        layer2.rectTransform.localPosition = layer2BasePosition - mouseInfo * layer2MoveStrength;
    }

    //Mouse isn't properly centered, this helper funcion corrects the offset
    //NOTE TO SELF; correct when I have the chance! Should Ideally autocorrect based on the camera and/or image size
    Vector3 MouseBasedOnCenter()
    {
        Vector3 myMousePos = new Vector3(Mathf.Clamp(Input.mousePosition.x, 0, Screen.width),
            Mathf.Clamp(Input.mousePosition.y, 0, Screen.height));
        Vector3 myInfo = myMousePos - new Vector3(Screen.width / 2, Screen.height / 2);
        //myInfo = new Vector3(clamp)
        return myInfo;
    }
}
