using NavMeshPlus.Components;
using UnityEngine;

// TODO: should be changed to every time an obstacle spawns/despawns whenever that is implemented
public class UpdateNavmesh : MonoBehaviour
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    

    private void Start()
    {
        navMeshSurface.BuildNavMesh();
    }

}
