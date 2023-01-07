using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A.Combat
{
    [CreateAssetMenu(menuName = "Data/CombatWeaponData", fileName = "new CombatWeaponData")]
    public class CombatWeaponData : ScriptableObject
    {
        public float speedMultiplier = 0f;
        public CombatDice damageDice = CombatDice.d4;
        public int damageAdditiveModifier = 0;
        public float stunDurationMultiplier = 0f;
        public List<CombatStateSound> combatStateSounds = new();
        public List<CombatResultSound> combatResultSounds = new();
    }
}