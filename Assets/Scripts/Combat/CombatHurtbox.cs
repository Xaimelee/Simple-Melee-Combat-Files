using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Interfaces;
using A.StateMachines;

namespace A.Combat
{
	public class CombatHurtbox : CombatComponent
	{
		public override void SetCurrentState(ScriptableState previousState, ScriptableState nextState)
		{
			return;
		}
	}
}
