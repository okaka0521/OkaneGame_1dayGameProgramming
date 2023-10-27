using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class TitleScript : MonoBehaviour
{
	[SerializeField] private GameObject FadePanel;
	private GameObject AudioSystem;
	// Start is called before the first frame update
	async void Start()
	{
		AudioSystem = GameObject.FindWithTag("Audio");
		AudioSystem.GetComponent<AudioSystem>().PlayBGM(0);
		AudioSystem.GetComponent<AudioSystem>().FadeInBGM();
		await WokeGame(this.GetCancellationTokenOnDestroy());
	}

	public void StartButton()
	{
		AudioSystem.GetComponent<AudioSystem>().PlaySE(2);
		StartGame(this.GetCancellationTokenOnDestroy());
	}

	public void ExitButton()
	{
		AudioSystem.GetComponent<AudioSystem>().PlaySE(2);
		QuitGame(this.GetCancellationTokenOnDestroy());
	}

	async UniTask WokeGame(CancellationToken token)
	{
		FadePanel.SetActive(true);
		FadePanel.GetComponent<Image>().color = new Color(0, 0, 0, 1);
		await FadePanel.GetComponent<Image>().DOFade(0, 1).WithCancellation(token);
		FadePanel.SetActive(false);
	}

	async UniTask StartGame(CancellationToken token)
	{
		//load async
		AudioSystem.GetComponent<AudioSystem>().FadeOutBGM();
		var scene = SceneManager.LoadSceneAsync("Main");
		scene.allowSceneActivation = false;
		FadePanel.SetActive(true);
		await FadePanel.GetComponent<Image>().DOFade(1, 1).WithCancellation(token);
		//wait until scene is loaded
		await UniTask.WaitUntil(() => (scene.progress >= 0.9f));
		scene.allowSceneActivation = true;
	}

	async UniTask QuitGame(CancellationToken token)
	{
		AudioSystem.GetComponent<AudioSystem>().FadeOutBGM();
		FadePanel.SetActive(true);
		await FadePanel.GetComponent<Image>().DOFade(1, 1).WithCancellation(token);
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
		Application.Quit();
#endif
	}
}
