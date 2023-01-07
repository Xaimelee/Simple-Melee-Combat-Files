using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A.Controllers
{
    public class EntityAudioController : MonoBehaviour
    {
		[SerializeField] private GameObject _audioSourcePrefab = null;
		[Range(-3f, 3f)]
		[SerializeField] private float _minimumRandomPitch = 0.9f;
		[Range(-3f, 3f)]
		[SerializeField] private float _maximumRandomPitch = 1.1f;
		[Range(0f, 1f)]
		[SerializeField] private float _minimumRandomVolume = 0.8f;
		[Range(0f, 1f)]
		[SerializeField] private float _maximumRandomVolume = 1f;

		public void Play(AudioClip[] audioClips, bool randomPitch = true, bool randomVolume = true)
		{
			if (audioClips == null) return;

			GameObject newAudio = Instantiate(_audioSourcePrefab, this.transform);
			AudioSource audioSource = newAudio.GetComponent<AudioSource>();
			float _defaultPitch = audioSource.pitch;
			float _defaultVolume = audioSource.volume;
			audioSource.pitch = randomPitch ? Random.Range(_minimumRandomPitch, _maximumRandomPitch) : _defaultPitch;
			audioSource.volume = randomVolume ? Random.Range(_minimumRandomVolume, _maximumRandomVolume) : _defaultVolume;
			audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
			audioSource.Play();
		}
	}
}
