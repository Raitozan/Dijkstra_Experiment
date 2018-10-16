using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AI : MonoBehaviour {

	public Graph g;
	public Player p;

	public Node startNode;
	public Node endNode;
	public float valuation;
	public float startTime;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.paused) {
			if (transform.position == endNode.transform.position) {
				startNode = endNode;
				if (startNode.id != p.endNode.id)
					endNode = g.nodes [dijkstra (startNode.id, p.endNode.id)];
				else
					endNode = p.startNode;
				valuation = g.adjacencyMatrix [startNode.id, endNode.id];
				startTime = Time.time;
			}

			float duration = Time.time - startTime;
			float fraction = duration / (valuation / 4);
			transform.position = Vector3.Lerp (startNode.transform.position, endNode.transform.position, fraction);
		} 
		else {
			startTime += Time.deltaTime;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if(other.gameObject.name == "Player")
		{
			SceneManager.LoadScene ("GameOver");
		}
	}

	public int dijkstra(int startID, int endID) {
		int[] done = new int[g.nodes.Count];
		int[] distance = new int[g.nodes.Count];
		int[] predecessor = new int[g.nodes.Count];

		for (int i = 0; i < g.nodes.Count; i++) {
			done [i] = 0;
			distance [i] = -1;
			predecessor [i] = -1;
		}
		done [startID] = 1;
		distance [startID] = 0;

		int actualNode = startID;

		while (done [endID] != 1) {
			//MISE A JOUR DES DISTANCES DES SOMMETS
			for(int i=0; i<g.nodes.Count; i++){
				if (g.adjacencyMatrix [actualNode, i] != 0 && done[i] != 1) {
					bool change = true;
					if (distance [i] != -1)
						if (distance [actualNode] + g.adjacencyMatrix [actualNode, i] > distance [i])
							change = false;
					if (change) {
						distance [i] = distance [actualNode] + g.adjacencyMatrix [actualNode, i];
						predecessor [i] = actualNode;
					}
				}
			}

			//CHOIX DU PROCHAIN NODE
			int idMin = actualNode;
			int min = 999;
			for (int i = 0; i < g.nodes.Count; i++) {
				if (distance [i] != -1 && done [i] != 1) {
					if (distance [i] < min) {
						idMin = i;
						min = distance [i];
					}
				}
			}
			done [idMin] = 1;
			actualNode = idMin;
		}

		//récupération de l'ID du prochain move
		while (predecessor [actualNode] != startID) {
			actualNode = predecessor [actualNode];
		}
		return actualNode;
	}
}
