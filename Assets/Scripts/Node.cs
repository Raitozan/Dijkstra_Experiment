using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {

	public int id;
	public bool selectable;
	public GameObject select;

	public float minTimer=2.0f;
	public float maxTimer=8.0f;
	public float timer;

	public string state = "empty";
	public float bombProb = 0.07f;
	public float speedProb = 0.04f;
	public float teleporterProb = 0.04f;

	public SpriteRenderer sr;
	public Sprite nodeSprite;
	public Sprite bombSprite;
	public Sprite speedSprite;
	public Sprite teleporterSprite;

	public Node teleportTarget;

	// Use this for initialization
	void Start () {
		timer = Random.Range (minTimer, maxTimer);
	}

	// Update is called once per frame
	void Update () {
		if (state == "empty" && !GameManager.instance.paused) {
			timer -= Time.deltaTime;
			if (timer <= 0.0f) {
				float p = Random.Range (0f, 1f);
				if (p <= bombProb) {
					sr.sprite = bombSprite;
					state = "bomb";
				} else if (p <= bombProb+speedProb) {
					sr.sprite = speedSprite;
					state = "speed";
				} else if (p <= bombProb+speedProb+teleporterProb && !GameManager.instance.teleporterOn) {
					GameManager.instance.teleporterOn = true;
					sr.sprite = teleporterSprite;
					state = "teleporter";
					Node outNode = GameManager.instance.getTeleporterOutNode (this);
					outNode.sr.sprite = teleporterSprite;
					outNode.state = "teleporter";
					teleportTarget = outNode;
					outNode.teleportTarget = this;
				} else {
					timer = Random.Range (minTimer, maxTimer);
					state = "empty";
				}
			}
		}
	}

	void OnMouseDown() {
		if (selectable) {
			GameManager.instance.changePlayerTarget (id);
		}
	}
}