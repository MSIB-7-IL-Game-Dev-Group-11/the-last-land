using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheLastLand
{
    public abstract class CharacterStatModifierSO : ScriptableObject
    {
        public abstract void AffectCharacter(GameObject character, float val);
    }
}
