using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Global;

namespace A.Combat
{
    public static class CombatHelpers
    {
        //: D&D inspired functions.
        public static int RollDamage(CombatDice combatDamageDice, int combatDamageModifier)
		{
            return Random.Range(0, (int)combatDamageDice) + combatDamageModifier;
		}
        //: Generates entirely new hitpoints.
        public static int RollHitPoints(CombatDice hitPointDice, int hitPointModifier, int level)
		{
            int hitPoints = (int)hitPointDice + hitPointModifier;
            //: Roll and add new hitpoints if level is higher than 1.
            for (int i = 1; i < level; i++)
			{
                hitPoints += Random.Range(0, (int)hitPointDice) + hitPointModifier;
            }
            return hitPoints;
        }
        //: Adds new hitpoints to existing hitpoints.
        public static int RollHitPoints(int currentHitPoints, CombatDice hitPointDice, int hitPointModifier)
		{
            return currentHitPoints += Random.Range(0, (int)hitPointDice) + hitPointModifier;
        }
        public static bool RollToHit(int attackModifier, int armorClassToBeat, GlobalDice dice = GlobalDice.d20)
		{
            return (Random.Range(0, (int)dice) + attackModifier) > armorClassToBeat;
        }
    }
}
