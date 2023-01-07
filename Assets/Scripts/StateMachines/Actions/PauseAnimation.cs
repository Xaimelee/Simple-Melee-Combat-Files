using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Controllers;

namespace A.StateMachines
{
    [CreateAssetMenu(menuName = "Scriptable State Machine/Actions/Combat/PauseAnimation", fileName = "new PauseAnimation")]
    public class PauseAnimation : ScriptableAction
    {
        [SerializeField] private string _animationStateName = "myAnimation";
        [SerializeField] private int _layer = 0;
        [SerializeField] private float _speed = 0f;
        [SerializeField] private float _requiredPosition = 0.1f;

        public override void Act(StateComponent statesComponent)
        {
            CombatController combatController = statesComponent.GetComponent<CombatController>();
            if (combatController.EntityAnimatorController.GetCurrentAnimationPosition(_layer) < _requiredPosition) return;
            if (combatController.EntityAnimatorController.GetAnimationSpeed() == _speed) return;

            combatController.EntityAnimatorController.SetCurrentAnimationSpeed(_animationStateName, _layer, _speed);
        }
    }
}

