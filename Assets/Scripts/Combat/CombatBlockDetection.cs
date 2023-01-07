using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using A.Interfaces;
using A.StateMachines;

namespace A.Combat
{
    [RequireComponent(typeof(SphereCollider))]
    public class CombatBlockDetection : CombatComponent, ICombatCollider
    {
        enum BlockProtection { LowerBody, Body, UpperBody }

        [SerializeField] private BlockProtection _blockProtection = BlockProtection.Body;
        [Range(10f, 360f)]
        [SerializeField] private float _detectionAngle = 140f;
        [Range(1f, 2f)]
        [SerializeField] private float _detectionRadius = 1.5f;
        [Range(0f, 1f)]
        [SerializeField] private float _attackerFacingDefenderAngle = 0.8f;
        private SphereCollider _sphereCollider = null;

        private void Awake()
        {
            _sphereCollider = GetComponent<SphereCollider>();
            _sphereCollider.center = new Vector3(_sphereCollider.center.x, (float)_blockProtection, _sphereCollider.center.y);
            _sphereCollider.radius = _detectionRadius;
            SetCollider(false);
        }

        private void OnTriggerStay(Collider other)
        {
            if (combatController.CurrentState != "Parried") return;
            if (!other.gameObject.CompareTag("WeaponHitbox")) return;
            if (other.GetComponent<CombatComponent>() == null) return;
            if (other.GetComponent<ICombatCollider>() == null) return;

            ICombatCollider combatCollider = other.GetComponent<ICombatCollider>();
            CombatComponent attacker = other.GetComponent<CombatComponent>();
            if (attacker.combatController == combatController) return;
            if (attacker.combatController.Faction == combatController.Faction) return;

            Vector3 currentPosition = transform.position;
            Vector3 attackerPosition = other.transform.position - currentPosition;
            attackerPosition.y = 0f;
            if (attackerPosition.magnitude > _detectionRadius) return;
            //: Making sure the attacker is within the block radius.
            if (!IsAttackerInBlockRadius(attackerPosition)) return;
            //: Making sure the attacker is facing the defender.
            if (!IsAttackerFacingDefender(attacker.combatController.gameObject.transform, combatController.gameObject.transform)) return;

            CombatData combatData = new()
            {
                attacker = attacker.combatController,
                defender = combatController,
                combatResult = CombatResult.Blocked
            };
            attacker.ReceiveCombatData(combatData);
            ReceiveCombatData(combatData);
            combatCollider.SetCollider(false);
            SetCollider(false);
            Debug.Log("Blocked: " + attacker.combatController.gameObject.name);
        }

        public void SetCollider(bool value)
        {
            _sphereCollider.enabled = value;
        }

        public override void SetCurrentState(ScriptableState previousState, ScriptableState nextState)
        {
            if (nextState.name == "Hit" || nextState.name == "Idle")
			{
                SetCollider(false);
            }
        }

        private bool IsAttackerInBlockRadius(Vector3 attackerPosition)
		{
            return Vector3.Dot(attackerPosition.normalized, transform.forward) >= Mathf.Cos(_detectionAngle * 0.5f * Mathf.Deg2Rad);
        }

        private bool IsAttackerFacingDefender(Transform attackerTransform, Transform defenderTransform)
		{
            return Vector3.Dot(attackerTransform.forward, (defenderTransform.position - attackerTransform.position).normalized) > _attackerFacingDefenderAngle;
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Color colour = new(256, 0, 0);
            UnityEditor.Handles.color = colour;
            Vector3 rotatedForward = Quaternion.Euler(0, -_detectionAngle * 0.5f, 0) * transform.forward;
            UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, _detectionAngle, _detectionRadius);
        }
#endif
	}
}
