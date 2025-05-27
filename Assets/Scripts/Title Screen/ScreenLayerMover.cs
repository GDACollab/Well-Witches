using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        //Debug.Log("mousePos:" + MouseBasedOnCenter());
        Vector3 mouseInfo = MouseBasedOnCenter();
        layer1.transform.position = layer1BasePosition - mouseInfo * layer1MoveStrength;
        layer2.transform.position = layer2BasePosition - mouseInfo * layer2MoveStrength;
    }

    //Mouse isn't properly centered, this helper funcion corrects the offset
    //NOTE TO SELF; correct when I have the chance! Should Ideally autocorrect based on the camera and/or image size
    Vector3 MouseBasedOnCenter()
    {
        // based on 1920,1080 (divided by 4)
        return Input.mousePosition - new Vector3(layer1.rectTransform.rect.width / 4, layer1.rectTransform.rect.height / 4);
    }
}
