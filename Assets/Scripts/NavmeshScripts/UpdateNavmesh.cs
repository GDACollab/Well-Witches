using NavMeshPlus.Components;
using System.Collections;
using UnityEngine;

// updates Navmesh surface every 3 seconds
// TODO: should be changed to every time an obstacle spawns/despawns whenever that is implemented
public class UpdateNavmesh : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    public bool updateMesh = true;
    public float updateCooldown = 3f;
    

    private void Start()
    {
        StartCoroutine(UpdateMesh());
    }

    private IEnumerator UpdateMesh()
    {
        while (updateMesh)
        {
            navMeshSurface.BuildNavMesh();
            yield return new WaitForSeconds(updateCooldown);
        }
    }

}
