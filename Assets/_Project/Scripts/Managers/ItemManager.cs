using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.SeviceLocator;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Managers
{
    public class ItemManager : MonoBehaviour
    {
        private static ItemManager _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                Initialize();
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        private void Initialize()
        {
            _instance = this;
            ServiceLocator.Global.RegisterServiceIfNotExists(_instance);
            DontDestroyOnLoad(gameObject);
        }
    }
}