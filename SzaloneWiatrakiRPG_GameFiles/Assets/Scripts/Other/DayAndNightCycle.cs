using System;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class DayAndNightCycle : MonoBehaviour
{
    [SerializeField]
    Transform Light;

    void LateUpdate()
    {
        var x = (DateTime.Now.Hour * 360) / 24f;
        //Light.Rotate(x, 0, 0);

        Light.rotation = Quaternion.Euler(x-90, -30, 0);
    }
}
