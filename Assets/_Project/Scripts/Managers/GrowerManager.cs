using UnityEngine;

namespace TheLastLand._Project.Scripts.Managers
{
    public class GrowerManager : MonoBehaviour
    {
        // unfinished script
        
        private static GrowerManager _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}