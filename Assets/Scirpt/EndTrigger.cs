using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		GameObject.FindWithTag("GameSystem").gameObject.GetComponent<GameSystem>().IsGameOver = true;
		Debug.Log("Game Over");
	}
}
