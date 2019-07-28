using UnityEngine.Audio;
using UnityEngine;

[System.SerializableAttribute]
public class Sound 
{
	public AudioClip clip;

	public string name;

	[RangeAttribute(0f, 1f)]
	public float volume;

	[RangeAttribute(0.1f, 3f)]
	public float pitch;

	public bool loop;

	[HideInInspector]
	public AudioSource source;
}
