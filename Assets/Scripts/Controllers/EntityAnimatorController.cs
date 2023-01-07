using UnityEngine;

namespace A.Controllers
{
    [RequireComponent(typeof(Animator))]
    public class EntityAnimatorController : MonoBehaviour
    {
		[HideInInspector]
		public Vector3 Direction = new();

		private Animator _animator = null;
		private float velocityX = 0f;
		private float velocityZ = 0f;

		private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
			ApplyDirectionalVelocity();
			_animator.SetFloat("Input X", velocityX);
			_animator.SetFloat("Input Z", velocityZ);
		}

        public void ApplyDirectionalVelocity()
		{   // Setting Z velocity.
			switch (Direction.z)
			{
				case > 0:
					velocityZ += Time.deltaTime * 6.0f;
					break;
				case < 0:
					velocityZ -= Time.deltaTime * 6.0f;
					break;
			}

			// Setting X velocity.
			switch (Direction.x)
			{
				case > 0:
					velocityX += Time.deltaTime * 6.0f;
					break;
				case < 0:
					velocityX -= Time.deltaTime * 6.0f;
					break;
			}

			// Reseting Z velocity.
			if (Direction.z == 0 && velocityZ != 0f)
			{
				if (velocityZ > 0f)
				{
					velocityZ -= Time.deltaTime * 6.0f;
					if (velocityZ < 0f)
					{
						velocityZ = 0f;
					}
				}
				else
				{
					velocityZ += Time.deltaTime * 6.0f;
					if (velocityZ > 0f)
					{
						velocityZ = 0f;
					}
				}
			}

			// When switched to opposite side, decay.
			if (Direction.x != 1 && velocityX > 0f)
			{
				velocityX -= Time.deltaTime * 6.0f;
			}

			// When switched to opposite side, decay.
			if (Direction.x != -1 && velocityX < 0f)
			{
				velocityX += Time.deltaTime * 6.0f;
			}

			// Reseting X velocity.
			if (Direction.x == 0 && velocityX != 0f)
			{
				if (velocityX > 0f)
				{
					velocityX -= Time.deltaTime * 6.0f;
					if (velocityX < 0f)
					{
						velocityX = 0f;
					}
				}
				else
				{
					velocityX += Time.deltaTime * 6.0f;
					if (velocityX > 0f)
					{
						velocityX = 0f;
					}
				}
			}

			velocityX = Mathf.Clamp(velocityX, -1, 1);
			velocityZ = Mathf.Clamp(velocityZ, -1, 1);
		}

		public void Play(string stateName, int layer, float startPosition, float speed = 1f)
		{
			_animator.SetFloat("Animation Speed", speed);
			_animator.Play(stateName, layer, startPosition);
		}

		public void SetAnimationSpeed(float speed)
		{
			_animator.SetFloat("Animation Speed", speed);
		}

		public void SetCurrentAnimationSpeed(string stateName, int layer, float speed = 1f)
		{
			if (!IsCurrentState(stateName, layer)) return;

			SetAnimationSpeed(speed);
		}

		public float GetAnimationSpeed()
		{
			return _animator.GetFloat("Animation Speed");
		}

		public float GetCurrentAnimationPosition(int layer)
		{
			return _animator.GetCurrentAnimatorStateInfo(layer).normalizedTime;
		}

		public bool IsCurrentState(string stateName, int layer)
		{
			return _animator.GetCurrentAnimatorStateInfo(layer).IsName(stateName);
		}

		public bool IsPlaying(string stateName, int layer)
		{
			return (_animator.GetCurrentAnimatorStateInfo(layer).IsName(stateName) &&
					_animator.GetCurrentAnimatorStateInfo(layer).normalizedTime < 1.0f);
		}
	}
}
