using Cinemachine;
using TheLastLand._Project.Scripts.Extensions;
using TheLastLand._Project.Scripts.SeviceLocator;
using UnityEngine;

namespace TheLastLand._Project.Scripts.Managers
{
    public class MainSystemManager : MonoBehaviour
    {
        private static MainSystemManager _instance;

        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        private CinemachineConfiner2D _confiner;

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

        private void OnEnable()
        {
            LevelConfiner.OnConfinerEnter += SetConfiner;
        }

        private void OnDisable()
        {
            LevelConfiner.OnConfinerEnter -= SetConfiner;
        }

        private void Initialize()
        {
            _instance = this;
            ServiceLocator.Global.RegisterServiceIfNotExists(_instance);
            DontDestroyOnLoad(gameObject);

            _confiner = virtualCamera.GetComponent<CinemachineConfiner2D>();
        }

        private void SetConfiner(Collider2D newConfiner)
        {
            if (_confiner != null)
            {
                _confiner.m_BoundingShape2D = newConfiner;
            }
        }
    }
}