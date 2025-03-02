using System.Collections;
using UnityEngine;

namespace Assets.Scripts.GridSystem
{
    public class BuildSystemVFXHandler : MonoBehaviour
    {
        [SerializeField]
        GameObject Vfx;

        public void PlayErrorVFX(Vector3 position)
        {
            GameObject x = Instantiate(Vfx,position,Quaternion.identity);
            x.SetActive(true);
            Destroy(x,6);
        }
    }
}