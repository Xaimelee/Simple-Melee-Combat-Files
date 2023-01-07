using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;
using A.StateMachines;

namespace A.Combat
{
    public abstract class CombatComponent : MonoBehaviour
    {
        public CombatController combatController = null;
		public StateComponent stateComponent = null;

		private void OnEnable()
		{
			stateComponent.onStateChanged += SetCurrentState;
		}

		private void OnDisable()
		{
			stateComponent.onStateChanged -= SetCurrentState;
		}

		public void ReceiveCombatData(CombatData combatData)
		{
			if (combatData == null) return;
			if (combatController == null) return;

			combatController.SetCombatData(combatData);
		}

		public abstract void SetCurrentState(ScriptableState previousState, ScriptableState nextState);
	}
}
