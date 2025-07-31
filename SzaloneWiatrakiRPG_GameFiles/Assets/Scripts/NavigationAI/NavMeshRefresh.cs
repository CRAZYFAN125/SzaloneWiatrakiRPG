using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshRefresh : MonoBehaviour
{
    [SerializeField]
    NavMeshSurface navMesh;
    float time;
    // Update is called once per frame
    void LateUpdate()
    {
        if (time>5)
        {
        navMesh.BuildNavMesh();
            time = 0;
        }
        time += Time.fixedDeltaTime;
    }
}
