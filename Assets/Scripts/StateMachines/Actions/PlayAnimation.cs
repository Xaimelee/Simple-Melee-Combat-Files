using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;

namespace A.StateMachines
{
	[CreateAssetMenu(menuName = "Scriptable State Machine/Actions/Combat/PlayAnimation", fileName = "new PlayAnimation")]
	public class PlayAnimation : ScriptableAction
    {
		[SerializeField] private string _animationStateName = "myAnimationState";
		[SerializeField] private int _layer = 0;
		[SerializeField] private float _animationStartPosition = 0f;
		[SerializeField] private float _speed = 1f;

		public override void Act(StateComponent statesComponent)
		{
			CombatController combatController = statesComponent.GetComponent<CombatController>();
			combatController.EntityAnimatorController.Play(_animationStateName, _layer, _animationStartPosition, _speed);
		}
    }
}
