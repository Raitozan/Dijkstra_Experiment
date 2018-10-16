using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public bool paused;

	public bool teleporterOn;

	public static float score;

	public Text scoreTxt;

	public Graph g;
	public Player p;

	void Awake() {
		if (instance == null)
			instance = this;
		else
			Destroy (gameObject);

		score = 0;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		scoreTxt.text = "Your Score: " + score;
		if (paused) {
			for (int i = 0; i < g.nodes.Count; i++) {
				if (g.adjacencyMatrix [p.endNode.id, i] != 0) {
					g.nodes [i].selectable = true;
					g.nodes [i].select.SetActive (true);
				}
			}
		}
	}

	public void changePlayerTarget(int id) {
		p.startNode = p.endNode;
		p.endNode = g.nodes [id];
		p.valuation = g.adjacencyMatrix [p.startNode.id, p.endNode.id];
		score += p.valuation;

		for (int i = 0; i < g.nodes.Count; i++) {
			g.nodes [i].select.SetActive (false);
			g.nodes [i].selectable = false;
		}

		p.startTime = Time.time;
		paused = false;
	}

	public Node getTeleporterOutNode(Node inNode) {
		Node outNode = g.nodes [Random.Range (0, g.nodes.Count)];
		while(outNode == inNode)
			outNode = g.nodes [Random.Range (0, g.nodes.Count)];
		return outNode;
	}
}
