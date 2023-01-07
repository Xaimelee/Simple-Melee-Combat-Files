using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;

namespace A.StateMachines
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Combat/HasAnimationFinished", fileName = "new HasAnimationFinished")]
    public class HasAnimationFinished : ScriptableCondition
    {
		[SerializeField] private string _animationStateName = "myAnimation";
		[SerializeField] private int _layer = 0;

		public override bool Verify(StateComponent statesComponent)
		{
			CombatController combatController = statesComponent.GetComponent<CombatController>();
			return combatController.EntityAnimatorController.IsPlaying(_animationStateName, _layer);
		}
    }
}
