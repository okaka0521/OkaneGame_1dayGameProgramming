using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class PlayerControl : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI scoreText;
	[SerializeField] private Image NextImage;
	[SerializeField] private GameObject PlayerObject;
	private GameSystem gameSystem;
	private GameObject NextObject;
	private GameObject CurrentObject;
	private const float interval = 0.25f;
	private float time = 0f;
	private GameObject AudioSystem;

	// Start is called before the first frame update
	void Start()
	{
		AudioSystem = GameObject.FindWithTag("Audio");
		gameSystem = GameObject.FindWithTag("GameSystem").gameObject.GetComponent<GameSystem>();
		NextObject = Resources.Load<GameObject>("Prefab/1yen");
		SetNextOkane();
	}

	// Update is called once per frame
	void Update()
	{
		if (!gameSystem.IsGameOver && !gameSystem.IsPause)
		{
			TextUpdate();
			PlayerMove();
			time += Time.deltaTime;
		}
	}

	void TextUpdate()
	{
		scoreText.text = "SCORE " + gameSystem.Score;
	}

	void PlayerMove()
	{
		//Akey and Dkey, right arrow and left arrow to move
		if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && transform.position.x > -8f)
		{
			transform.Translate(Vector3.left * Time.deltaTime * 5);
			PlayerObject.transform.position = new Vector3(this.transform.position.x + 0.4f, this.transform.position.y + 2.5f, this.transform.position.z);
		}

		if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && transform.position.x < 8f)
		{
			transform.Translate(Vector3.right * Time.deltaTime * 5);
			PlayerObject.transform.position = new Vector3(this.transform.position.x + 0.4f, this.transform.position.y + 2.5f, this.transform.position.z);
		}
		//Space key or Enter key to throw
		if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) && time >= interval)
		{
			ThrowOkane();
			AudioSystem.GetComponent<AudioSystem>().PlaySE(0);
			time = 0f;
		}
	}

	void ThrowOkane()
	{
		if (CurrentObject != null) Instantiate(CurrentObject, transform.position, Quaternion.identity);
		SetNextOkane();
	}

	void SetNextOkane()
	{
		CurrentObject = NextObject;
		this.GetComponent<SpriteRenderer>().sprite = CurrentObject.GetComponent<SpriteRenderer>().sprite;
		this.transform.localScale = CurrentObject.transform.localScale;
		switch (RandomOkane())
		{
			case 1:
				NextObject = Resources.Load<GameObject>("Prefab/1yen");
				NextImage.sprite = Resources.Load<Sprite>("Image/1yen");
				break;
			case 5:
				NextObject = Resources.Load<GameObject>("Prefab/5yen");
				NextImage.sprite = Resources.Load<Sprite>("Image/5yen");
				break;
			case 10:
				NextObject = Resources.Load<GameObject>("Prefab/10yen");
				NextImage.sprite = Resources.Load<Sprite>("Image/10yen");
				break;
			case 50:
				NextObject = Resources.Load<GameObject>("Prefab/50yen");
				NextImage.sprite = Resources.Load<Sprite>("Image/50yen");
				break;
		}
	}

	int RandomOkane()
	{
		int random = Random.Range(1, 100);
		if (random <= 50)
		{
			return 1;
		}
		else if (random <= 80)
		{
			return 5;
		}
		else if (random <= 99)
		{
			return 10;
		}
		else
		{
			return 50;
		}
	}
}
