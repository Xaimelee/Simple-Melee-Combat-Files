using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;

namespace A.StateMachines
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Conditions/Combat/HasStunTimerStopped", fileName = "new HasStunTimerStopped")]
    public class HasStunTimerStopped : ScriptableCondition
    {
        public override bool Verify(StateComponent statesComponent)
        {
            CombatController combatController = statesComponent.GetComponent<CombatController>();
            return combatController.StunTimer <= 0f;
        }
    }
}
