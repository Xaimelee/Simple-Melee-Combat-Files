using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;

namespace A.StateMachines
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Actions/Combat/ReduceStunTimer", fileName = "new ReduceStunTimer")]
    public class ReduceStunTimer : ScriptableAction
    {
        public override void Act(StateComponent statesComponent)
        {
            CombatController combatController = statesComponent.GetComponent<CombatController>();
            combatController.StunTimer -= Time.deltaTime;
        }
    }
}