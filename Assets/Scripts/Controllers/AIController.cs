using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A.Controllers
{
    public class AIController : MonoBehaviour
    {
		[HideInInspector] public bool canAttack = false;
        [HideInInspector] public bool canParry = false;

        private float cooldown = 1.0f;
        private float cooldownTimer = 0f;

        void LateUpdate()
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer > cooldown)
			{
                int chance = Random.Range(0, 100);

                if (chance < 30 && chance > 15)
				{
                    canAttack = true;
                }
                else if (chance < 15)
				{
                    canParry = true;
				}
                else
				{
                    canParry = false;
                    canAttack = false;
                }

                cooldownTimer = 0f;
            }
        }
    }
}
