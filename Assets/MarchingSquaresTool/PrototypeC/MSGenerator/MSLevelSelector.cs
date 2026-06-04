using UnityEngine;

namespace MarchingSquaresTool.PrototypeC.MSGenerator
{
    [RequireComponent(typeof(MSGenerator))]
    public class MSLevelSelector : MonoBehaviour
    {
        [SerializeField] private string levelName;
    }
}