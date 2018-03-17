using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : MonoBehaviour {

	private NavMeshAgent agent;
	private Transform playerTransform;
	private Transform cellDoorTransform;
	private Transform stairsTransform;
	private GameObject playerObject;

	public enum AiMode {Walking, Searching, WaitingToReturn, Returning, WaitingToWalk};
	public AiMode agentState = AiMode.Walking;

	public float returnWaitTime = 5f;
	public float walkWaitTime = 60f;

	private float lastWalkTime;
	private float lastReturnTime;

	private bool playerInCell;

	// Use this for initialization
	void Start () {
		
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		playerTransform = playerObject.GetComponent<Transform>();
		agent = GetComponent<NavMeshAgent>();

		GameObject cellDoor = GameObject.FindGameObjectWithTag ("CellDoor");
		cellDoorTransform = cellDoor.GetComponent<Transform> ();

		GameObject stairWell = GameObject.FindGameObjectWithTag ("Stairwell");
		stairsTransform = stairWell.GetComponent<Transform> ();

		playerInCell = playerObject.GetComponent<Player> ().playerInCell;
	}

	// Update is called once per frame
	void Update () {

		Debug.Log ("Current State: " + agentState);
		Debug.Log ("Path Status: " + agent.pathStatus);
		Debug.Log ("Remaining Distance " + agent.remainingDistance);
		Debug.Log ("Stopping Distance" + agent.stoppingDistance);

		if (agentState == AiMode.Searching) {
			//Check if path is pending
			//Check if remaining distance is less than stopping distance
			//Send Player back to cell
			//Set Mode to returning
			agent.SetDestination (playerTransform.position);

			if (!agent.pathPending) {
				if (agent.remainingDistance <= agent.stoppingDistance) {
					if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
						agentState = AiMode.Returning;
					}
				}
			}


		} else if (agentState == AiMode.Walking) {
			//Check if path is pending
			//Check if remaining distance is less than stopping distance
			//Check if player is in cell ? WaitingToReturn : Searching

			agent.SetDestination (cellDoorTransform.position);

			if (!agent.pathPending) {
				if (agent.remainingDistance <= agent.stoppingDistance) {
					if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
						
						playerInCell = playerObject.GetComponent<Player> ().playerInCell;

						if (playerInCell) {
							agentState = AiMode.WaitingToReturn;
						} else {
							agentState = AiMode.Searching;
						}
					}
				}
			}

		} else if (agentState == AiMode.Returning) {
			//Check if path is pending
			//Check if remaining distance is less than stopping distance
			//Set Mode to WaitingToWalk

			agent.SetDestination (stairsTransform.position);

			if (!agent.pathPending) {
				if (agent.remainingDistance <= agent.stoppingDistance) {
					if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
						agentState = AiMode.WaitingToWalk;
					}
				}
			}

		} else if (agentState == AiMode.WaitingToWalk) {

			//Wait to start walking again.
			if (Time.time > lastWalkTime + walkWaitTime) {
				agentState = AiMode.Walking;
				lastWalkTime = Time.time;
			}

			return;

		} else if (agentState == AiMode.WaitingToReturn) {

			//Wait to return from cell.
			if (Time.time > lastReturnTime + returnWaitTime) {
				agentState = AiMode.Returning;
				lastReturnTime = Time.time;
			}

			return;

		}else {
			return;
		}
	}



}