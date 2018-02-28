using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScorePrefab : MonoBehaviour {

	public GameObject score, rank, scoreNombre;

	public void SetScore(string scoreNombre, string score, string rank)
	{
		this.rank.GetComponent<Text>().text = rank;
		this.score.GetComponent<Text>().text = score;
		this.scoreNombre.GetComponent<Text>().text = scoreNombre;
	}
}
