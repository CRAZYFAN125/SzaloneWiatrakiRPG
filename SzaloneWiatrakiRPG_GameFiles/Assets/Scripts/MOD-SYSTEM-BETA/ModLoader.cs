using Dummiesman;
using MoonSharp.Interpreter;
using System;
using System.IO;
using UnityEngine;

namespace Crazy.ModSystem
{
    public class ModLoader : MonoBehaviour
    {
        private readonly Script script = new();
        [Multiline(8)]
        public string scriptString;

        [SerializeField]
        Material material;

        private void Awake()
        {
            Script.DefaultOptions.DebugPrint = s => print(s);
            script.Globals["print"] = (Action<DynValue>)CustomPrint;

            UserData.RegisterType<GameObject>();
            script.Globals["GetObjectByName"] = (Func<string, GameObject>)GetObjectByName;

            UserData.RegisterType<Transform>();
            script.Globals["MoveObjectByVector"] = (Func<Transform, Vector3, bool>)MoveObjectByVector;

            UserData.RegisterType<Vector3>();
            script.Globals["Vector3"] = (Func<float, float, float, Vector3>)((x, y, z) => new Vector3(x, y, z));

            script.Globals["Random"] = (Func<float, float, float>)((min, max) => UnityEngine.Random.Range(min, max));

            UserData.RegisterType<Quaternion>();
            script.Globals["Quaternion"] = new Table(script);
            script.Globals.Get("Quaternion").Table["identity"] = Quaternion.identity;
            script.Globals.Get("Quaternion").Table["Euler"] = (Func<float, float, float, Quaternion>)((x, y, z) => Quaternion.Euler(x, y, z));

            UserData.RegisterType<Texture2D>();
            UserData.RegisterType<MeshFilter>();
            UserData.RegisterType<Mesh>();

            script.Globals["RotateObjectByVector"] = (Func<Transform, Vector3, bool>)RotateObjectByVector;

            script.Globals["GetStringFromFile"] = (Func<string, string>)GetStringFromFile;
            script.Globals["GetTexture2D"] = (Func<string, Texture2D>)GetTexture2D;
            script.Globals["LoadOBJModel"] = (Func<string, Mesh>)LoadOBJModel;
            script.Globals["GetComponent"] = (Func<GameObject, string, Component>)GetComponentLUA;
        }
        #region Vector Logic
        private bool MoveObjectByVector(Transform transform, Vector3 position)
        {
            if (transform == null) return false;

            transform.position += position;
            return true;
        }
        private bool RotateObjectByVector(Transform transform, Vector3 rotation)
        {
            if (transform == null) return false;

            transform.rotation = Quaternion.Euler(rotation + transform.rotation.eulerAngles);
            return true;
        }
        #endregion

        #region File System Logic

        private string GetStringFromFile(string filePath)
        {
            var p = Path.Combine(MakeModFolderStruct.Instance.ModPath, filePath);

            if (File.Exists(p))
            {
                var data = File.ReadAllText(p);
                return data;
            }
            else
                return string.Empty;
        }

        private Texture2D GetTexture2D(string filePath)
        {
            var p = Path.Combine(MakeModFolderStruct.Instance.ModPath, filePath);
            Texture2D texture = new Texture2D(256, 256);
            if (File.Exists(p))
            {
                if (texture.LoadImage(File.ReadAllBytes(p)))
                    texture.Apply();
            }
            else
            {
                texture = new(2, 2);
                texture.SetPixel(1, 1, Color.black);
                texture.SetPixel(2, 1, Color.magenta);
                texture.SetPixel(2, 2, Color.black);
                texture.SetPixel(1, 2, Color.magenta);
                texture.Apply();
            }

            material.mainTexture = texture;
            return texture;
        }

        private Mesh LoadOBJModel( string path)
        {
            var p = Path.Combine(MakeModFolderStruct.Instance.ModPath, path);

            if (File.Exists(p))
            {
                OBJLoader loader = new OBJLoader();
                loader.Load(p);
                Mesh mesh = new()
                {
                    vertices = loader.Vertices.ToArray(),
                    uv = loader.UVs.ToArray()
                };
                return mesh;
            }
            print("Emergency Mesh");
            return new Mesh();

        }

        #endregion

        #region ETC

        private GameObject GetObjectByName(string name)
        {
            return GameObject.Find(name);
        }

        private void CustomPrint(DynValue value)
        {
            print(value);
        }

        private Component GetComponentLUA(GameObject _object, string name)
        {
            //! Szansa ¿e siê wyjebie
            if (_object == null) return null;

            return _object.GetComponent(name);
        }

        private bool ChangeMesh(GameObject _object, Mesh mesh)
        {
            //! Szansa ¿e siê wyjebie
            if (_object == null) return false;
            if (mesh == null || mesh == new Mesh()) {
                Debug.LogWarning("Empty mesh data");
                return false;
            }
            _object.GetComponent<MeshFilter>().mesh = mesh;
            return true;
        }
        #endregion

        void Start()
        {
            script.DoString(scriptString);
            //foreach (var item in script.Globals.Keys)
            //    print(item);
        }
    }
}