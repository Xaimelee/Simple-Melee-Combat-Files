using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace A.Combat
{
    public class CombatEvents : MonoBehaviour
    {
        public UnityEvent OnParryEnabled;
        public UnityEvent OnParryDisabled;
        public UnityEvent OnAttackEnabled;
        public UnityEvent OnAttackDisabled;

        public void OnAttackCollisionEnabled()
        {
            //Debug.Log(gameObject.name + ": " + "Attack Collision Enabled.");
            OnAttackEnabled.Invoke();
        }

        public void OnAttackCollisionDisabled()
        {
            //Debug.Log(gameObject.name + ": " + "Attack Collision Disabled.");
            OnAttackDisabled.Invoke();
        }

        public void OnParryCollisionEnabled()
        {
            //Debug.Log(gameObject.name + ": " + "Parry Collision Enabled.");
            OnParryEnabled.Invoke();
        }

        public void OnParryCollisionDisabled()
        {
            //Debug.Log(gameObject.name + ": " + "Parry Collision Disabled.");
            OnParryDisabled.Invoke();
        }
    }
}
