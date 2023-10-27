using UnityEngine;

public class yenMerge : MonoBehaviour
{
	private string thisTag;
	private int thisMerge = 1;
	private int needMerge = 0;
	private int objectID;
	private GameSystem gameSystem;
	// Start is called before the first frame update
	void Start()
	{
		gameSystem = GameObject.FindWithTag("GameSystem").gameObject.GetComponent<GameSystem>();
		objectID = gameSystem.Identity++;
		thisTag = this.gameObject.tag;
		//Debug.Log(this.gameObject.name + " " + objectID);
		if (thisTag == "1yen" || thisTag == "10yen" || thisTag == "100yen" || thisTag == "1000yen")
		{
			needMerge = 5;
		}
		else
		{
			needMerge = 2;
		}
	}

	//property
	public int ThisMerge
	{
		get { return thisMerge; }
		set { thisMerge = value; }
	}

	public int ObjectID
	{
		get { return objectID; }
		set { objectID = value; }
	}


	void OnCollisionEnter2D(Collision2D other)
	{
		if (thisTag == other.gameObject.tag)
		{
			var otherPos = other.gameObject.transform.position;
			if (objectID < other.gameObject.GetComponent<yenMerge>().objectID)
			{
				thisMerge += other.gameObject.GetComponent<yenMerge>().thisMerge;
				if (thisMerge > needMerge)
				{
					while (thisMerge != needMerge)
					{
						thisMerge--;
						var prefab = Resources.Load<GameObject>("Prefab/" + thisTag);
						Instantiate(prefab, otherPos, Quaternion.identity);
					}
				}
				Destroy(other.gameObject);
				//Debug.Log(this.gameObject.name + " " + ObjectID + " " + thisMerge);
			}
			if (thisMerge == needMerge)
			{
				var betPos = (this.transform.position + otherPos) / 2;
				GameObject prefab;
				switch (thisTag)
				{
					case "1yen":
						prefab = Resources.Load<GameObject>("Prefab/5yen");
						Instantiate(prefab, betPos, Quaternion.identity);
						gameSystem.Score += 5;
						break;
					case "5yen":
						prefab = Resources.Load<GameObject>("Prefab/10yen");
						Instantiate(prefab, betPos, Quaternion.identity);
						gameSystem.Score += 10;
						break;
					case "10yen":
						prefab = Resources.Load<GameObject>("Prefab/50yen");
						Instantiate(prefab, betPos, Quaternion.identity);
						gameSystem.Score += 50;
						break;
					case "50yen":
						prefab = Resources.Load<GameObject>("Prefab/100yen");
						Instantiate(prefab, betPos, Quaternion.identity);
						gameSystem.Score += 100;
						break;
					case "100yen":
						prefab = Resources.Load<GameObject>("Prefab/500yen");
						Instantiate(prefab, betPos, Quaternion.identity);
						gameSystem.Score += 500;
						break;
					case "500yen":
						prefab = Resources.Load<GameObject>("Prefab/1000yen");
						Instantiate(prefab, betPos, Quaternion.identity);
						gameSystem.Score += 1000;
						break;
					case "1000yen":
						prefab = Resources.Load<GameObject>("Prefab/5000yen");
						Instantiate(prefab, betPos, Quaternion.identity);
						gameSystem.Score += 5000;
						break;
					case "5000yen":
						prefab = Resources.Load<GameObject>("Prefab/10000yen");
						Instantiate(prefab, betPos, Quaternion.identity);
						gameSystem.Score += 10000;
						break;
					case "10000yen":
						gameSystem.Score += 20000;
						break;
				}
				Destroy(this.gameObject);
			}
		}
	}
}
