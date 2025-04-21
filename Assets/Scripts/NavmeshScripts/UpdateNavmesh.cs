using NavMeshPlus.Components;
using System.Collections;
using UnityEngine;

// TODO: should be changed to every time an obstacle spawns/despawns whenever that is implemented
public class UpdateNavmesh : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    
    private void Start()
    {
        navMeshSurface.BuildNavMesh();
        //StartCoroutine(UpdateMesh());
    }
    public float updateCooldown = 3f;

    /* Caused FPS issues due to meshes being updated constantly+
    private IEnumerator UpdateMesh()
    {
        while (true)
        {
            navMeshSurface.BuildNavMesh();
            yield return new WaitForSeconds(updateCooldown);
        }
    }
    */

}
