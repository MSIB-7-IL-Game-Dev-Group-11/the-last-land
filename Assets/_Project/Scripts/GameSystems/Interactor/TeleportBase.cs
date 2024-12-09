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
            TeleportToPoint();
            
            if (!string.IsNullOrEmpty(targetSceneName))
            {
                OnSceneTeleport?.Invoke();
                SceneManager.LoadScene(targetSceneName);
            }
            else
            {
                OnWorldTeleport?.Invoke();
            }
        }

        private void TeleportToPoint()
        {
            var player = FindObjectOfType<Player>();
            if (player == null) return;
            player.transform.position = teleportPoint;
            if (Camera.main != null) Camera.main.transform.position = teleportPoint;
        }
    }
}