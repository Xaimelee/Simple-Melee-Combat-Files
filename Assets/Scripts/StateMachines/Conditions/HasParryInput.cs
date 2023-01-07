using UnityEngine;
using A.Controllers;

namespace A.StateMachines 
{
	[CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Combat/HasParryInput", fileName = "new HasParryInput")]
	public class HasParryInput : ScriptableCondition
	{
		public override bool Verify(StateComponent statesComponent)
		{
			PlayerController playerController = statesComponent.GetComponent<PlayerController>();
			return playerController.InputManager.HasPlayedParried();
		}
	}
}
