using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {
	
	public void load10() {
		SceneManager.LoadScene("Game10");
	}

	public void load42() {
		SceneManager.LoadScene("Game42");
	}
}
