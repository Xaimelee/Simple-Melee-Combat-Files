using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A.Combat
{
    [CreateAssetMenu(menuName = "Data/CombatArmourData", fileName = "new CombatArmourData")]
    public class CombatArmourData : ScriptableObject
    {
        public int armourClass = 10;
        public float armourDamageReduction = 0.05f;
        public float stunDurationMultiplier = 0f;
        public List<CombatStateSound> combatStateSounds = new();
        public List<CombatResultSound> combatResultSounds = new();
    }
}