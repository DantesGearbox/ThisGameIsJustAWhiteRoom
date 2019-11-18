using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

	public Sound[] sounds;

	private void Awake() {
		foreach(Sound s in sounds) {
			s.audioSource = gameObject.AddComponent<AudioSource>();

			s.audioSource.clip = s.audioClip;
			s.audioSource.volume = s.volume;
			s.audioSource.pitch = s.pitch;
			s.audioSource.loop = s.loop;
		}
	}

	private void Start() {
		Play("Theme");
		Play("Step");
	}

	public void Play(string name) {
		Sound s = System.Array.Find(sounds, sound => sound.name == name);
		if(s == null) {
			Debug.LogWarning("Couldn't find sound of that name");
			return;
		}
		s.audioSource.Play();
	}

	public void ChangeVolume(string name, float volume) {
		Sound s = System.Array.Find(sounds, sound => sound.name == name);
		if (s == null) {
			Debug.LogWarning("Couldn't find sound of that name");
			return;
		}
		s.audioSource.volume = volume;
	}
}
