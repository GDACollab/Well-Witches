using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class Ultimate2 : MonoBehaviour
{
    [SerializeField] private VisualEffect tree;

    public CameraShake cameraShake;
    public GameObject volume;
    public GameObject light2d;

    public float firstTreeDelay;
    public float secondTreeDelay;
    public float thirdTreeDelay;



    private void Start()
    {
        volume.SetActive(false);
        light2d.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            tree.Play();
            StartCoroutine(cameraShake.Shake(.15f, .2f, firstTreeDelay, volume, light2d));
            StartCoroutine(cameraShake.Shake(.15f, .2f, secondTreeDelay, volume, light2d));
            StartCoroutine(cameraShake.Shake(.15f, .2f, thirdTreeDelay, volume, light2d));
        }




    }

}
