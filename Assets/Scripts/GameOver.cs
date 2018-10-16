using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour {

	public Text score;

	// Use this for initialization
	void Start () {
		score.text = "Your Score: " + GameManager.score;
	}
	
	public void loadMenu() {
		SceneManager.LoadScene("Menu");
	}
}
