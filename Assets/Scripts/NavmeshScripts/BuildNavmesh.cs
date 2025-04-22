using NavMeshPlus.Components;
using UnityEngine;

// TODO: should be changed to every time an obstacle spawns/despawns whenever that is implemented
public class BuildNavmesh : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMesh;
    private void Start()
    {
        navMesh.BuildNavMesh();
    }
}
