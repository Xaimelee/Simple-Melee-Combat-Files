using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;

namespace A.Combat
{
	public class CombatData
	{
		public CombatController attacker = null;
		public CombatController defender = null;
		public CombatResult combatResult = CombatResult.Hit;
		public CombatRollResult combatRollResult = CombatRollResult.Succeded;
	}
}
