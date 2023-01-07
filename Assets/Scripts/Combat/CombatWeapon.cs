using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;
using A.Interfaces;
using A.StateMachines;

namespace A.Combat
{
    public class CombatWeapon : CombatComponent, ICombatCollider
    {
		private Collider _collider = null;

		private void Awake()
		{
			_collider = GetComponent<Collider>();
		}

		private void OnTriggerStay(Collider other)
		{
			if (combatController.CurrentState != "Attacked") return;
			if (!other.gameObject.CompareTag("EntityHurtbox")) return;
			if (other.GetComponent<CombatComponent>() == null) return;

			CombatComponent defender = other.GetComponent<CombatComponent>();
			if (defender.combatController == combatController) return;
			if (defender.combatController.Faction == combatController.Faction) return;

			CombatData combatData = new()
			{
				attacker = combatController,
				defender = defender.combatController,
				combatResult = CombatResult.Hit,
				combatRollResult = (CombatHelpers.RollToHit(0, defender.combatController.combatArmourData.armourClass)
				? CombatRollResult.Succeded : CombatRollResult.Failed)
			};
			defender.ReceiveCombatData(combatData);
			ReceiveCombatData(combatData);
			SetCollider(false);
			Debug.Log("Hit: " + defender.combatController.gameObject.name);
		}

		public void SetCollider(bool value)
		{
			if (combatController.CurrentState == "Stunned" || combatController.CurrentState == "Hit") 
			{
				_collider.enabled = false;
				return; 
			}
			_collider.enabled = value;
		}

		public override void SetCurrentState(ScriptableState previousState, ScriptableState nextState)
		{
			if (nextState.name == "Hit" || nextState.name == "Idle")
			{
				SetCollider(false);
			}
		}
	}
}
