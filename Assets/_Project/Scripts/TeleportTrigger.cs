using TheLastLand._Project.Scripts.GameSystems.Interactor;
using UnityEngine;

namespace TheLastLand._Project.Scripts
{
    public class TeleportTrigger : TeleportBase
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                Interact();
            }
        }
    }
}