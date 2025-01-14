using NavMeshPlus.Components;
using System.Collections;
using UnityEngine;

public class UpdateNavmesh : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    public float updateCooldown = 3f;

    private void Start()
    {
        StartCoroutine(UpdateMesh());
    }

    private IEnumerator UpdateMesh()
    {
        while (true)
        {
            navMeshSurface.BuildNavMesh();
            yield return new WaitForSeconds(updateCooldown);
        }
    }

}
