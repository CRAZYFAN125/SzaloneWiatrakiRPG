using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshRefresh : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface navMesh;
    // Update is called once per frame
    void LateUpdate()
    {
        navMesh.BuildNavMesh();
    }
}
