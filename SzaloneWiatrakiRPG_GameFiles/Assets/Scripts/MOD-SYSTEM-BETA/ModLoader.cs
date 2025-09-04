using MoonSharp.Interpreter;
using System;
using UnityEngine;

namespace Crazy.ModSystem
{
    public class ModLoader : MonoBehaviour
    {
        private readonly Script script = new();

        private void Awake()
        {
            Script.DefaultOptions.DebugPrint = s => print(s);
            script.Globals["print"] = (Action<DynValue>)CustomPrint;
        }

        private void CustomPrint(DynValue value)
        {
            print(value);
        }

        // Start is called before the first frame update
        void Start()
        {
            script.DoString("print('Hello')");
            foreach (var item in script.Globals.Keys)
            {
                print(item);
            }
        }
    }
}