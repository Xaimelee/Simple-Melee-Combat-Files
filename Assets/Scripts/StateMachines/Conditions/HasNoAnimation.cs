using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;

namespace A.StateMachines
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Combat/HasNoAnimation", fileName = "new HasNoAnimation")]
    public class HasNoAnimation : ScriptableCondition
    {
		[SerializeField] private string _animationStateName = "myAnimationState";
		[SerializeField] private int _layer = 0;

		public override bool Verify(StateComponent statesComponent)
		{
			CombatController combatController = statesComponent.GetComponent<CombatController>();
			return combatController.EntityAnimatorController.IsCurrentState(_animationStateName, _layer);
		}
    }
}
