using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prisoner : MonoBehaviour {
	public float activePeriod = 3f;
	public float activeVariance = 2f;
	public float dormantPeriod = 5f;
	public float dormantVariance = 10f;
	private float waitTime = 0f;
	private float direction = 1f;
	private bool isDormant = true;


	private Rigidbody2D rb;
	private Movement move;
	private BoxCollider2D box;

	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody2D>();
		move = GetComponent<Movement>();
		box = GetComponent<BoxCollider2D>();

		// Removing collisions with prisoners
		GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
		GameObject[] guards = GameObject.FindGameObjectsWithTag("Enemy");
		foreach (GameObject go in guards)
		{
			Physics2D.IgnoreCollision(box, go.GetComponent<BoxCollider2D>());
		}
		foreach (GameObject go in players)
		{
			Physics2D.IgnoreCollision(box, go.GetComponent<BoxCollider2D>());
		}

		StartWait();
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Active Behaviour
		if (!isDormant)
		{
			move.Move(direction, 0f);
		}
	}

	private void StartWait()
	{
		if (isDormant)
			waitTime = dormantPeriod + Random.Range(-dormantVariance/2f, dormantVariance/2f);
		else
			waitTime = activePeriod + Random.Range(-activeVariance/2f, activeVariance/2f);

		StartCoroutine("Waiting");
	}

	IEnumerator Waiting()
	{
		yield return new WaitForSeconds(waitTime);

		// Toggle dormancy, set a random direction
		isDormant = !isDormant;
		direction = Random.Range(0f, 1f) > 0.5f ? -1f : 1f;
		if (direction < 0)
			direction = 0f;
		
		// Start a new timer
		StartWait();
	}
}
