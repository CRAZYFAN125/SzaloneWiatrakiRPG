using System.IO;
using UnityEngine;

namespace Crazy.ModSystem
{
    public class MakeModFolderStruct : MonoBehaviour
    {
        public static MakeModFolderStruct Instance;
        /// <summary>
        /// Ścieżka do folderu z modami
        /// </summary>
        public string ModPath { get; private set; }
        public void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);

            ModPath = Path.Combine(Application.dataPath, "Mods");
            CheckFolderStruct();

        }

        public bool CheckFolderStruct()
        {

            if (string.IsNullOrEmpty(ModPath))
                return false;
            if (!Directory.Exists(ModPath))
                Directory.CreateDirectory(ModPath);
            return true;
        }
    }
}