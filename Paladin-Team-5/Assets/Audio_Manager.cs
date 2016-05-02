using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
	public AudioClip combat_Clip;
	public AudioClip passive_Clip;

	public static Audio_Manager clips;
	public static AudioSource audio_Source;

	void Start()
	{
		Audio_Manager.clips = this;
		Audio_Manager.audio_Source = this.GetComponent<AudioSource>();
	}

	public void play_Passive_Clip()
	{
		if(Audio_Manager.audio_Source.clip != Audio_Manager.clips.passive_Clip)
		{
			Audio_Manager.audio_Source.clip = Audio_Manager.clips.passive_Clip;
			Audio_Manager.audio_Source.Play();
		}
	}
}
