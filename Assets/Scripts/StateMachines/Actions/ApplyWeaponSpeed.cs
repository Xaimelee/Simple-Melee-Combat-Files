using A.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A.StateMachines
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Actions/Combat/ApplyWeaponSpeed", fileName = "new ApplyWeaponSpeed")]
    public class ApplyWeaponSpeed : ScriptableAction
    {
        public override void Act(StateComponent statesComponent)
        {
            CombatController combatController = statesComponent.GetComponent<CombatController>();
            float animationSpeed = combatController.EntityAnimatorController.GetAnimationSpeed();
            animationSpeed += animationSpeed * combatController.combatWeaponData.speedMultiplier;
            combatController.EntityAnimatorController.SetAnimationSpeed(animationSpeed);
        }
    }
}
