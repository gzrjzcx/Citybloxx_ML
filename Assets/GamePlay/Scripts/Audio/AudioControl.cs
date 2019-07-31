using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
	 public Sound[] sounds;

	 public static AudioControl instance;

	 void Awake()
	 {
	 	if(!instance)
	 		instance = this;
	 	else
	 	{
	 		Destroy(this.gameObject);
	 		return;
	 	}


	 	foreach(Sound s in sounds)
	 	{
	 		s.source = gameObject.AddComponent<AudioSource>();
	 		s.source.clip = s.clip;

	 		s.source.volume = s.volume;
	 		s.source.pitch = s.pitch;
	 		s.source.loop = s.loop;
	 	}
	 }

	 void Start()
	 {
	 	Play("Theme");
	 }

	 public void Play(string name)
	 {
	 	Sound s = Array.Find(sounds, sound => sound.name == name);
	 	if(s == null)
	 		Debug.LogWarning("Sound '" + name + "' not found");

	 	s.source.Play();
	 }

	 public bool isPlaying(string name)
	 {
	 	Sound s = Array.Find(sounds, sound => sound.name == name);
	 	if(s == null)
	 		Debug.LogWarning("Sound" + name + " not found");
	 	return s.source.isPlaying;
	 }

	 public bool isMute(string name)
	 {
	 	Sound s = Array.Find(sounds, sound => sound.name == name);
	 	if(s == null)
	 		Debug.LogWarning("Sound" + name + " not found");
	 	return s.source.mute;
	 }

	 public void Stop(string name)
	 {
	 	Sound s = Array.Find(sounds, sound => sound.name == name);
	 	if(s == null)
	 		Debug.LogWarning("Sound '" + name + "' not found");

	 	s.source.Stop();	 	
	 }

	 public void MuteAllSoundEffects()
	 {
	 	foreach(Sound s in sounds)
	 	{
	 		if(!s.name.Equals("Theme"))
	 		{
	 			s.source.mute = true;
	 		}
	 	}
	 }

	 public void UnmuteAllSoundEffects()
	 {
	 	foreach(Sound s in sounds)
	 	{
	 		if(!s.name.Equals("Theme"))
	 		{
	 			s.source.mute = false;
	 		}
	 	}
	 }























}
