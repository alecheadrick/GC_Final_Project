using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : MonoBehaviour {

	//Get tranforms for positions AI navigates to
	private NavMeshAgent agent;
	private Transform playerTransform;
	private Transform cellDoorTransform;
	private Transform stairsTransform;
	private Transform storageTransform;
	private Transform bathroomTransform;
	private Transform recRoomTransform;
	private Transform medRoomTransform;
	public GameObject playerObject;

	//Differents states the AI can be in
	public enum AiMode {Walking, Searching, WaitingToReturn, Returning, WaitingToWalk, SearchingStorage, SearchingBathroom, SearchingRecRoom, SearchingMedRoom};
	public AiMode agentState;

	//Wait time values
	public float returnWaitTime = 5f;
	public float walkWaitTime = 60f;
	public float searchWaitTime = 20f;

	//References for Time
	private float lastWalkTime;

	//Bools to indicate if player is in specific room
	private bool playerInCell;
	private bool playerInStorageRoom;
	private bool playerInMedRoom;
	private bool playerInRecRoom;
	private bool playerInBathroom;

	//Bool to Wait for SteamVR to load and search for player once
	//private bool vrCheck = true;

	//Animator
	private Animator animator;

	// Use this for initialization
	//FInd agent
	//Find all of our position transforms

	void Start () {
		
		agent = GetComponent<NavMeshAgent>();

		animator = GetComponent<Animator> ();

		GameObject stairs = GameObject.FindGameObjectWithTag ("Stairwell");
		stairsTransform = stairs.GetComponent<Transform> ();

		GameObject cellDoor = GameObject.FindGameObjectWithTag ("CellDoor");
		cellDoorTransform = cellDoor.GetComponent<Transform> ();

		GameObject storage = GameObject.FindGameObjectWithTag ("Storage");
		storageTransform = storage.GetComponent<Transform> ();

		GameObject bathroom = GameObject.FindGameObjectWithTag ("Bathroom");
		bathroomTransform = bathroom.GetComponent<Transform> ();

		GameObject recRoom = GameObject.FindGameObjectWithTag ("RecRoom");
		recRoomTransform = recRoom.GetComponent<Transform> ();

		GameObject medRoom = GameObject.FindGameObjectWithTag ("MedRoom");
		medRoomTransform = medRoom.GetComponent<Transform> ();

		agentState = AiMode.Walking;
	}

	// Update is called once per frame

	void Update () {
		//Checking the state, and performing certain tasks like Searching, Walking or Moving

		//Debug.Log ("Current State: " + agentState);
		//Debug.Log ("Path Status: " + agent.pathStatus);
		//Debug.Log ("Remaining Distance " + agent.remainingDistance);
		//Debug.Log ("Stopping Distance" + agent.stoppingDistance);

		if (agentState == AiMode.Searching) {
			//Check if path is pending
			//Check if remaining distance is less than stopping distance
			//Send Player back to cell
			//Set Mode to returning
			playerTransform = playerObject.GetComponent<Transform>();
			animator.SetBool ("Idle", false);
			animator.SetBool ("Walk", true);
			agent.SetDestination (playerTransform.position);

			if (!agent.pathPending) {
				
				if (agent.remainingDistance <= agent.stoppingDistance) {
					if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {

						//Black out Player Screen and Reset SteamVR transform
						playerTransform.parent.transform.position = new Vector3(16f, 0.5f, -10f);
						playerInCell = true;
                        playerInMedRoom = playerObject.GetComponent<Player>().playerInMedRoom;
                        playerInStorageRoom = playerObject.GetComponent<Player>().playerInStorageRoom;
                        playerInRecRoom = playerObject.GetComponent<Player>().playerInRecRoom;
                        playerInBathroom = playerObject.GetComponent<Player>().playerInBathroom;
                        playerInMedRoom = false;
                        playerInStorageRoom = false;
                        playerInRecRoom = false;
                        playerInBathroom = false;

                        agentState = AiMode.Returning;
	
					}
				}
			}

		} else if (agentState == AiMode.Walking) {
			//Play Walking Noise (Combine with Animation)

			//Check if path is pending
			//Check if remaining distance is less than stopping distance
			//Check if player is in cell ? WaitingToReturn : Searching
			animator.SetBool ("Idle", false);
			animator.SetBool ("Walk", true);
			agent.SetDestination (cellDoorTransform.position);

			if (!agent.pathPending) {
				if (agent.remainingDistance <= agent.stoppingDistance) {
					if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {

						playerInCell = playerObject.GetComponent<Player> ().playerInCell;

						if (playerInCell) {
							animator.SetBool ("Idle", true);
							animator.SetBool ("Walk", false);
							lastWalkTime = Time.time;
							agentState = AiMode.WaitingToReturn;
						} else {
							animator.SetBool ("Idle", false);
							animator.SetBool ("Walk", true);
							lastWalkTime = Time.time;
							agentState = AiMode.SearchingStorage;
						}
					}
				}
			}

		} else if (agentState == AiMode.Returning) {
			//Check if path is pending
			//Check if remaining distance is less than stopping distance
			//Set Mode to WaitingToWalk
			animator.SetBool ("Idle", false);
			animator.SetBool ("Walk", true);
			agent.SetDestination (stairsTransform.position);

			if (!agent.pathPending) {
				if (agent.remainingDistance <= agent.stoppingDistance) {
					if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {
						lastWalkTime = Time.time;
						agentState = AiMode.WaitingToWalk;
					}
				}
			}

		} else if (agentState == AiMode.WaitingToWalk) {

			//Wait to start walking again.
			if (Time.time > lastWalkTime + walkWaitTime) {
				//Play RETURN TO CELL NOISE
				agentState = AiMode.Walking;
				lastWalkTime = Time.time;
			}

			return;

		} else if (agentState == AiMode.WaitingToReturn) {

			//Wait to return from cell.
			if (Time.time > lastWalkTime + returnWaitTime) {
				agentState = AiMode.Returning;
				lastWalkTime = Time.time;
			}

			return;

		} else if (agentState == AiMode.SearchingStorage) {
			
			agent.SetDestination (storageTransform.position);

			if (!agent.pathPending) {
				if (agent.remainingDistance <= agent.stoppingDistance) {
					if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {

						animator.SetBool ("Idle", true);
						animator.SetBool ("Walk", false);
						if (Time.time > lastWalkTime + searchWaitTime) {
							playerInStorageRoom = playerObject.GetComponent<Player> ().playerInStorageRoom;

							if (playerInStorageRoom) {
								animator.SetBool ("Idle", false);
								animator.SetBool ("Walk", true);
								agentState = AiMode.Searching;
							} else {
								animator.SetBool ("Idle", false);
								animator.SetBool ("Walk", true);
								agentState = AiMode.SearchingMedRoom;
							}

							lastWalkTime = Time.time;
						}
					}
				}
			}

		} else if (agentState == AiMode.SearchingMedRoom) {
			animator.SetBool ("Idle", false);
			animator.SetBool ("Walk", true);
			agent.SetDestination (medRoomTransform.position);

			if (!agent.pathPending) {
				if (agent.remainingDistance <= agent.stoppingDistance) {
					if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {

						animator.SetBool ("Idle", true);
						animator.SetBool ("Walk", false);
						if (Time.time > lastWalkTime + searchWaitTime) {
							playerInMedRoom = playerObject.GetComponent<Player> ().playerInMedRoom;

							if (playerInMedRoom) {
								animator.SetBool ("Idle", false);
								animator.SetBool ("Walk", true);
								agentState = AiMode.Searching;
							} else {
								animator.SetBool ("Idle", false);
								animator.SetBool ("Walk", true);
								agentState = AiMode.SearchingRecRoom;
							}

							lastWalkTime = Time.time;
						}
					}
				}
			}
		} else if(agentState == AiMode.SearchingRecRoom) {
			animator.SetBool ("Idle", false);
			animator.SetBool ("Walk", true);
			agent.SetDestination (recRoomTransform.position);

			if (!agent.pathPending) {
				if (agent.remainingDistance <= agent.stoppingDistance) {
					if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {

						animator.SetBool ("Idle", true);
						animator.SetBool ("Walk", false);
						if (Time.time > lastWalkTime + searchWaitTime) {
							playerInRecRoom = playerObject.GetComponent<Player> ().playerInRecRoom;

							if (playerInRecRoom) {
								animator.SetBool ("Idle", false);
								animator.SetBool ("Walk", true);
								agentState = AiMode.Searching;
							} else {
								animator.SetBool ("Idle", false);
								animator.SetBool ("Walk", true);
								agentState = AiMode.SearchingBathroom;
							}

							lastWalkTime = Time.time;
						}
					}
				}
			}
		} else if (agentState == AiMode.SearchingBathroom) {
			animator.SetBool ("Idle", false);
			animator.SetBool ("Walk", true);
			agent.SetDestination (bathroomTransform.position);

			if (!agent.pathPending) {
				if (agent.remainingDistance <= agent.stoppingDistance) {
					if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f) {

						animator.SetBool ("Idle", true);
						animator.SetBool ("Walk", false);
						if (Time.time > lastWalkTime + searchWaitTime) {
							playerInBathroom = playerObject.GetComponent<Player> ().playerInBathroom;

							if (playerInBathroom) {
								animator.SetBool ("Idle", false);
								animator.SetBool ("Walk", true);
								agentState = AiMode.Searching;
							} else {
								animator.SetBool ("Idle", false);
								animator.SetBool ("Walk", true);
								agentState = AiMode.Returning;
								//Adjust Walk Wait Time to Speed up Enemy's searching
							}

							lastWalkTime = Time.time;
						}
					}
				}
			}
		} else {
			return;
		}
	}



}