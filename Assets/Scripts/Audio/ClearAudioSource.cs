using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A.Audio
{
	[RequireComponent(typeof(AudioSource))]
    public class ClearAudioSource : MonoBehaviour
    {
        private AudioSource _audioSource = null;

		private void Awake()
		{
            _audioSource = GetComponent<AudioSource>();
		}

		private void LateUpdate()
		{
			if (!_audioSource.isPlaying)
			{
				Destroy(gameObject);
			}
		}
	}
}
