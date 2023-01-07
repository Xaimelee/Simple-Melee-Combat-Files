using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using A.Controllers;
using A.Combat;

namespace A.StateMachines
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Actions/Combat/SetCurrentAnimationSpeed", fileName = "new SetCurrentAnimationSpeed")]
    public class SetCurrentAnimationSpeed : ScriptableAction
    {
        [SerializeField] private string _animationStateName = "myAnimationState";
		[SerializeField] private int _layer = 0;
		[SerializeField] private float _speed = -1f;
		[BoxGroup("Minimum Length Before Speed Change")]
		[SerializeField] private bool _hasRequiredPositionRequirement = false;
		[BoxGroup("Minimum Length Before Speed Change")]
		[EnableIf("_hasRequiredPositionRequirement")]
		[SerializeField] private float _requiredPosition = 0.3f;
		[BoxGroup("Desired Combat Result For Speed Change")]
		[SerializeField] private bool _hasRequiredCombatResult = false;
		[BoxGroup("Desired Combat Result For Speed Change")]
		[EnableIf("_hasRequiredCombatResult")]
		[SerializeField] private bool _wasAttacker = true;
		[BoxGroup("Desired Combat Result For Speed Change")]
		[EnableIf("_hasRequiredCombatResult")]
		[SerializeField] private CombatResult _combatResult = CombatResult.Hit;

		public override void Act(StateComponent statesComponent)
		{
			CombatController combatController = statesComponent.GetComponent<CombatController>();
			if (combatController.EntityAnimatorController.GetAnimationSpeed() == _speed) return;
			if (_hasRequiredPositionRequirement)
			{
				if (combatController.EntityAnimatorController.GetCurrentAnimationPosition(_layer) < _requiredPosition) return;
			}
			if (_hasRequiredCombatResult)
			{
				if (combatController.LastCombatData == null) return;
				if (combatController.LastCombatData.combatResult != _combatResult) return;
				if (combatController.GetWasAttacker() != _wasAttacker) return;
			}
			combatController.EntityAnimatorController.SetCurrentAnimationSpeed(_animationStateName, _layer, _speed);
		}
	}
}
