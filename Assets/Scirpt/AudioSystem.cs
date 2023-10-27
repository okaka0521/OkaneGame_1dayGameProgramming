using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AudioSystem : MonoBehaviour
{
	private AudioSource BGMSource;
	private AudioSource SESource;
	private GameObject BGMObject;
	private GameObject SEObject;
	[SerializeField] private AudioClip[] SEClips;
	[SerializeField] private AudioClip[] BGMClips;

	// Start is called before the first frame update
	void Start()
	{
		//get child
		BGMObject = transform.GetChild(0).gameObject;
		SEObject = transform.GetChild(1).gameObject;

		//dont destroy this object
		DontDestroyOnLoad(this);

		//set audio source
		BGMSource = BGMObject.GetComponent<AudioSource>();
		SESource = SEObject.GetComponent<AudioSource>();
	}

	// other scripts can call this function to play audio
	public void PlaySE(int index)
	{
		SESource.PlayOneShot(SEClips[index]);
	}

	public void PlayBGM(int index)
	{
		BGMSource.clip = BGMClips[index];
		BGMSource.Play();
	}

	//Fade out BGM
	public void FadeOutBGM()
	{
		BGMSource.DOFade(0, 1);
	}

	//Fade in BGM
	public void FadeInBGM()
	{
		BGMSource.DOFade(1, 1);
	}
}
