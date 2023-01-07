using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;

namespace A.StateMachines
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Combat/CanContinueCombatAction", fileName = "new CanContinueCombatAction")]
    public class CanContinueCombatAction : ScriptableCondition
    {
        [SerializeField] private bool _requiresPlayerAttackInput = false;

        public override bool Verify(StateComponent statesComponent)
        {
            CombatController combatController = statesComponent.GetComponent<CombatController>();
            if (_requiresPlayerAttackInput)
			{
                PlayerController playerController = statesComponent.GetComponent<PlayerController>();
                return combatController.EntityAnimatorController.GetAnimationSpeed() == 0 && playerController.InputManager.HasPlayerAttacked();
            } 
            else
			{
                return combatController.EntityAnimatorController.GetAnimationSpeed() == 0;
            }
        }
    }
}

