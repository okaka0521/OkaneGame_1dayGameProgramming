using System.Net.Mime;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSystem : MonoBehaviour
{
	private int identity = 0;
	private double score = 0;
	private bool isGameOver = false;
	[SerializeField] private GameObject Panel;
	[SerializeField] private GameObject TitleFadePanel;
	[SerializeField] private GameObject GameOverObject;
	[SerializeField] private TextMeshProUGUI GameOverScoreText;
	[SerializeField] private GameObject GameOverTextVer;
	[SerializeField] private GameObject PauseMenu;
	private GameObject AudioSystem;
	private int game = 0;
	private bool isPause = false;

	// Start is called before the first frame update
	void Start()
	{
		AudioSystem = GameObject.FindWithTag("Audio");
		AudioSystem.GetComponent<AudioSystem>().PlayBGM(1);
		AudioSystem.GetComponent<AudioSystem>().FadeInBGM();
		GameOverObject.SetActive(true);
		Panel.GetComponent<Image>().color = new Color(1, 1, 1, 0);
		TitleFadePanel.GetComponent<Image>().color = new Color(0, 0, 0, 1);
		StartGame(this.GetCancellationTokenOnDestroy());
	}

	// Update is called once per frame
	void Update()
	{
		if (isGameOver && game == 0)
		{
			game = 1;
			EndGame(this.GetCancellationTokenOnDestroy());
		}
		//ESC key to menu
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PauseMenu.SetActive(true);
			isPause = true;
		}
	}

	async void StartGame(CancellationToken token)
	{
		TitleFadePanel.SetActive(true);
		await TitleFadePanel.GetComponent<Image>().DOFade(0, 1).WithCancellation(token);
		TitleFadePanel.SetActive(false);
		GameOverObject.SetActive(false);
	}
	async void EndGame(CancellationToken token)
	{
		Panel.SetActive(true);
		await Panel.GetComponent<Image>().DOFade(1, 1).WithCancellation(token);
		GameOverObject.SetActive(true);
		GameOverTextVer.SetActive(true);
		GameOverScoreText.text = score + " Yen";
	}

	public void TitleButton()
	{
		if (game == 1 || isPause)
		{
			ToTitle(this.GetCancellationTokenOnDestroy());
			AudioSystem.GetComponent<AudioSystem>().PlaySE(2);
			game++;
		}
	}

	public void ResumeButton()
	{
		PauseMenu.SetActive(false);
		isPause = false;
		AudioSystem.GetComponent<AudioSystem>().PlaySE(2);
	}

	async void ToTitle(CancellationToken token)
	{
		AudioSystem.GetComponent<AudioSystem>().FadeOutBGM();
		var scene = SceneManager.LoadSceneAsync("Title");
		scene.allowSceneActivation = false;
		GameOverObject.SetActive(true);
		TitleFadePanel.SetActive(true);
		await TitleFadePanel.GetComponent<Image>().DOFade(1, 1).WithCancellation(token);
		await UniTask.WaitUntil(() => (scene.progress >= 0.9f));
		scene.allowSceneActivation = true;
	}

	//property
	public int Identity
	{
		get { return identity; }
		set { identity = value; }
	}

	public double Score
	{
		get { return score; }
		set { score = value; }
	}

	public bool IsGameOver
	{
		get { return isGameOver; }
		set { isGameOver = value; }
	}

	public bool IsPause
	{
		get { return isPause; }
		set { isPause = value; }
	}
}
