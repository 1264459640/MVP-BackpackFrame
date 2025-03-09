using UnityEngine;

namespace Utilities
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
        
    }
}
