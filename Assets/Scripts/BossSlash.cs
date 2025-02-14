using UnityEngine;
using System.Collections;

public class BossSlash : MonoBehaviour
{
    public Transform slashSpawn;
    public Transform waveSpawn;
    public GameObject slashPrefab;
    public GameObject wavePrefab;
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Instantiate(slashPrefab, slashSpawn.position, Quaternion.identity);
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(wavePrefab, waveSpawn.position, Quaternion.identity);
    }
}
