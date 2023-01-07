using UnityEngine;
using A.Controllers;

namespace A.StateMachines
{
	[CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Combat/HasAttackInput", fileName = "new HasAttackInput")]
	public class HasAttackInput : ScriptableCondition
	{
		public override bool Verify(StateComponent statesComponent)
		{
			PlayerController playerController = statesComponent.GetComponent<PlayerController>();
			return playerController.InputManager.HasPlayerAttacked();
		}
	}
}
