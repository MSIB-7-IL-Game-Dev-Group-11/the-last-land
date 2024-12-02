using TheLastLand._Project.Scripts.EventSystem.Common;
using UnityEngine;

namespace TheLastLand._Project.Scripts.EventSystem.Events
{
    [CreateAssetMenu(menuName = "Events/EmptyEventChannel")]
    public class EmptyEventChannel : EventChannel<Empty>
    {
    }
}