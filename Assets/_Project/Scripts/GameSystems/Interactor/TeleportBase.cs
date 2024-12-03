using TheLastLand._Project.Scripts.GameSystems.Interactor.Common;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TheLastLand._Project.Scripts.GameSystems.Interactor
{
    public abstract class TeleportBase : MonoBehaviour, IInteractable
    {
        public static event UnityAction OnWorldTeleport = delegate { };
        public static event UnityAction OnSceneTeleport = delegate { };

        [SerializeField] protected Vector2 teleportPoint;
        [SerializeField] protected string targetSceneName;

        public virtual void Interact()
        {
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                OnSceneTeleport?.Invoke();
                SceneManager.LoadScene(targetSceneName);
                SceneManager.sceneLoaded += OnSceneLoaded;
            }
            else
            {
                OnWorldTeleport?.Invoke();
                TeleportToPoint();
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            TeleportToPoint();
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void TeleportToPoint()
        {
            var player = FindObjectOfType<Player>();
            if (player != null)
            {
                player.transform.position = teleportPoint;
            }
        }
    }
}