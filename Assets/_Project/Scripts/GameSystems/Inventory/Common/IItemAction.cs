using System.Collections.Generic;
using TheLastLand._Project.Scripts.GameSystems.Inventory.Model;
using UnityEngine;

namespace TheLastLand._Project.Scripts.GameSystems.Inventory.Common
{
    public interface IItemAction
    {
        string ActionName { get; }
        AudioClip ActionSfx { get; }
        bool PerformAction(GameObject character, List<ItemParameter> itemState);
    }
}