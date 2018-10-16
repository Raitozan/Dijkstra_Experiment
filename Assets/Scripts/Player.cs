using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
	
	public Node startNode;
	public Node endNode;
	public float startTime;
	public float valuation;

	public SpriteRenderer sr;
	public Sprite playerSprite;
	public Sprite stunnedSprite;
	public Sprite speededSprite;

	public bool stunned;
	public float stunTimer;
	public bool speeded;
	public bool speedEnded;
	public float speedTimer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!stunned) {
			if (!GameManager.instance.paused) {
				float duration = Time.time - startTime;
				float fraction = duration / (valuation / 4);
				if (speeded) {
					fraction = duration / (valuation / 8);
					speedTimer -= Time.deltaTime;
					if (speedTimer <= 0.0f)
						speedEnded = true;
				}
				transform.position = Vector3.Lerp (startNode.transform.position, endNode.transform.position, fraction);

				if (transform.position == endNode.transform.position) {
					if (speedEnded) {
						speedEnded = false;
						speeded = false;
						sr.sprite = playerSprite;
					}
					if (endNode.state != "empty") {
						if (endNode.state == "bomb") {
							stunned = true;
							stunTimer = 1f;
							sr.sprite = stunnedSprite;
							if (speeded)
								speeded = false;
						} else if (endNode.state == "speed") {
							speeded = true;
							speedTimer = 2.0f;
							sr.sprite = speededSprite;
							endNode.state = "empty";
							endNode.sr.sprite = endNode.nodeSprite;
							endNode.timer = Random.Range (endNode.minTimer, endNode.maxTimer);
						} else if (endNode.state == "teleporter") {
							endNode.state = "empty";
							endNode.sr.sprite = endNode.nodeSprite;
							endNode.timer = Random.Range (endNode.minTimer, endNode.maxTimer);
							startNode = endNode.teleportTarget;
							endNode = startNode;
							transform.position = endNode.transform.position;
							endNode.state = "empty";
							endNode.sr.sprite = endNode.nodeSprite;
							endNode.timer = Random.Range (endNode.minTimer, endNode.maxTimer);
							GameManager.instance.teleporterOn = false;
						}
					}
					else
						GameManager.instance.paused = true;
				}
			}
		} else {
			stunTimer -= Time.deltaTime;
			if (stunTimer <= 0.0f) {
				stunned = false;
				sr.sprite = playerSprite;
				endNode.state = "empty";
				endNode.sr.sprite = endNode.nodeSprite;
				endNode.timer = Random.Range (endNode.minTimer, endNode.maxTimer);
			}
		}
	}
}
