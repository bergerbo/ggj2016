using UnityEngine;
using System.Collections;

public class Soundscript : MonoBehaviour {

	private AudioSource sounds_audio_source_as;
	private AudioSource music_audio_source_as;

	public AudioClip Music;
	public AudioClip Clap;
	public AudioClip Dong;
	public AudioClip Tonner;
	public AudioClip CriRoi;
	public AudioClip Stab;
	public AudioClip Cri1;
	public AudioClip Cri2;
	public AudioClip Cri3;
	public AudioClip Cri4;
	public AudioClip Fart;
	public AudioClip Chant1;
	public AudioClip Chant2;

	//Joue un son
	public void Play_sound (string snd_name)  
	{															

		if (snd_name == "Clap")
		{
			sounds_audio_source_as.PlayOneShot(Clap);
		}								
		if (snd_name == "Dong")
		{
			sounds_audio_source_as.PlayOneShot(Dong);
		}	
		if (snd_name == "Tonner")
		{
			sounds_audio_source_as.PlayOneShot(Tonner);
		}	
		if (snd_name == "Cri1")
		{
			sounds_audio_source_as.PlayOneShot(Cri1);
		}	
		if (snd_name == "Cri2")
		{
			sounds_audio_source_as.PlayOneShot(Cri2);
		}	
		if (snd_name == "Cri3")
		{
			sounds_audio_source_as.PlayOneShot(Cri3);
		}	
		if (snd_name == "Cri4")
		{
			sounds_audio_source_as.PlayOneShot(Cri4);
		}	
		if (snd_name == "Fart")
		{
			sounds_audio_source_as.PlayOneShot(Fart);
		}	
		if (snd_name == "Chant1")
		{
			sounds_audio_source_as.PlayOneShot(Chant1);
		}	
		if (snd_name == "Chant2")
		{
			sounds_audio_source_as.PlayOneShot(Chant2);
		}	
		if (snd_name == "CriRoi")
		{
			sounds_audio_source_as.PlayOneShot(CriRoi);
		}	
		if (snd_name == "Stab")
		{
			sounds_audio_source_as.PlayOneShot(Stab);
		}	
	}

	//Stop all sounds
	public void Stop_sounds ()  
	{																
		sounds_audio_source_as.Stop ();							
	}
	

	
	//Decrease/Increase music volume (targetvolume,speed)
	private float Music_targetvolume=1f;
	private float Music_speed=1f;
	public void Music_FadeVolumeTo (float targetvolume, float speed)  
	{														
		Music_targetvolume=targetvolume;
		Music_speed=speed;
	}
	//Start the music
	public void Start_music ()  
	{	
		music_audio_source_as = transform.Find ("Audio_Music").gameObject.GetComponent<AudioSource> ();	
		music_audio_source_as.clip = Music;
		music_audio_source_as.Play ();
	}				


	// Use this for initialization
	void Start () {
		sounds_audio_source_as = transform.Find ("Audio_Sounds").gameObject.GetComponent<AudioSource> ();
		music_audio_source_as = transform.Find ("Audio_Music").gameObject.GetComponent<AudioSource> ();	

		//Jouer le son "clap"
		Play_sound ("Clap");

		//Play la musique
		Start_music ();
		//Mettre le volume à 0 à la vitesse 1
		//Music_FadeVolumeTo (0f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
		music_audio_source_as.volume = Mathf.Lerp(music_audio_source_as.volume, Music_targetvolume, Music_speed*Time.deltaTime);

	}
}
