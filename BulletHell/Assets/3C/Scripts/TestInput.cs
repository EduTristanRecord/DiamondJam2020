using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class TestInput : MonoBehaviour
{
	public int playerId = 0;
	public float moveSpeed = 3.0f;
	private Vector3 moveVector;
	private Player player;
	private Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		player = ReInput.players.GetPlayer(playerId);
	}

    // Update is called once per frame
    void Update()
    {
		moveVector.x = player.GetAxis("Move Horizontal"); // get input by name or action id
		moveVector.y = player.GetAxis("Move Vertical");

		rb.velocity = moveVector * moveSpeed;
	}
}
