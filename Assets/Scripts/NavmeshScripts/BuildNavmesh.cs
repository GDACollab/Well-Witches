using NavMeshPlus.Components;
using UnityEngine;

public class BuildNavmesh : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMesh;
    private void Start()
    {
        navMesh.BuildNavMesh();
    }
}
