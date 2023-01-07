using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Global;

namespace A.Combat
{
    [CreateAssetMenu(menuName = "Data/CombatEntityData", fileName = "new CombatEntityData")]
    public class CombatEntityData : ScriptableObject
    {
        public GlobalFactions Faction = GlobalFactions.Avelaine;
        public CombatDice hitDice = CombatDice.d8;
        public int hitPointModifier = 0;
        public int startingLevel = 1;
        public List<CombatStateSound> combatStateSounds = new();
        public List<CombatResultSound> combatResultSounds = new();
    }
}
