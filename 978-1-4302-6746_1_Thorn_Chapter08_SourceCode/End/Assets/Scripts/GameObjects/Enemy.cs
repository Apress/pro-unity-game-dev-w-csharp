//Sets up FSM for enemy AI
//------------------------------------------------
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//------------------------------------------------
public class Enemy : MonoBehaviour
{
	//Enemy types
	public enum ENEMY_TYPE {Drone = 0, ToughGuy = 1, Boss=2};
	
	//Type of this enemy
	public ENEMY_TYPE Type = ENEMY_TYPE.Drone;
	
	//Custom ID of this enemy
	public int EnemyID = 0;
	
	//Current health of this enemy
	public int Health = 100;
	
	//Attack Damage - amount of damage this enemy deals to player when attacking
	public int AttackDamage = 10;
	
	//Recovery delay in seconds after launching an attack
	public float RecoveryDelay = 1.0f;
	
	//Enemy cached transform
	protected Transform ThisTransform = null;
	
	//------------------------------------------------
	//AI Properties
	
	//Reference to NavMesh Agent component
	protected NavMeshAgent Agent = null;
	
	//Reference to active PlayerController component for player
	protected PlayerController PC = null;
	
	//Reference to Player Transform
	protected Transform PlayerTransform = null;
	
	//Total distance in Unity Units from current position that agent can wander when patrolling
	public float PatrolDistance = 10.0f;
	
	//Total distance enemy must be from player, in Unity Units, before chasing them (entering chase state)
	public float ChaseDistance = 10.0f;
	
	//Total distance enemy must be from player before attacking them
	public float AttackDistance = 0.1f;
	
	//Enum of states for FSM
	public enum ENEMY_STATE {PATROL = 0, CHASE = 1, ATTACK=2};
	
	//Current state of enemy - default is patrol
	public ENEMY_STATE ActiveState = ENEMY_STATE.PATROL;
	
	//------------------------------------------------
	//Called on object start
	protected virtual void Start()
	{
		//Get NavAgent Component
		Agent = GetComponent<NavMeshAgent>();
		
		//Get Player Controller Component
		GameObject PlayerObject = GameObject.Find("Player");
		PC = PlayerObject.GetComponentInChildren<PlayerController>();
		
		//Get Player Transform
		PlayerTransform = PC.transform;
		
		//Get Enemy Transform
		ThisTransform = transform;
		
		//Set default state
		ChangeState(ActiveState);
	}
	//------------------------------------------------
	//Change AI State
	public void ChangeState(ENEMY_STATE State)
	{
		//Stops all AI Processing
		StopAllCoroutines();
		
		//Set new state
		ActiveState = State;
		
		//Activates new state
		switch(ActiveState)
		{
		case ENEMY_STATE.ATTACK:
			StartCoroutine(AI_Attack());
			SendMessage("Attack", SendMessageOptions.DontRequireReceiver); //Notify Game Object
			return;
			
		case ENEMY_STATE.CHASE:
			StartCoroutine(AI_Chase());
			SendMessage("Chase", SendMessageOptions.DontRequireReceiver); //Notify Game Object
			return;
			
		case ENEMY_STATE.PATROL:
			StartCoroutine(AI_Patrol());
			SendMessage("Patrol", SendMessageOptions.DontRequireReceiver); //Notify Game Object
			return;
		}
	}
	//------------------------------------------------
	//AI Function to handle patrol behaviour for enemy
	//Can exit this state and enter chase
	IEnumerator AI_Patrol()
	{
		//Stop Agent
		Agent.Stop();
		
		//Loop forever while in patrol state
		while(ActiveState == ENEMY_STATE.PATROL)
		{
			//Get random destination on map
			Vector3 randomPosition = Random.insideUnitSphere * PatrolDistance;
			
			//Add as offset from current position
			randomPosition += ThisTransform.position;
			
			//Get nearest valid position
			NavMeshHit hit;
			NavMesh.SamplePosition(randomPosition, out hit, PatrolDistance, 1);
			
			//Set destination
			Agent.SetDestination(hit.position);
			
			//Set distance range between object and destination to classify as 'arrived'
			float ArrivalDistance = 2.0f;
			
			//Set timeout before new path is generated (5 seconds)
			float TimeOut = 5.0f;
			
			//Elapsed Time
			float ElapsedTime = 0;
			
			//Wait until enemy reaches destination or times-out, and then get new position
			while(Vector3.Distance(ThisTransform.position, hit.position) > ArrivalDistance && ElapsedTime < TimeOut)
			{
				//Update ElapsedTime
				ElapsedTime += Time.deltaTime;
				
				//Check if should enter chase state
				if(Vector3.Distance(ThisTransform.position, PlayerTransform.position) < ChaseDistance)
				{
					//Exit patrol and enter chase state
					ChangeState(ENEMY_STATE.CHASE);
					yield break;
				}
				
				yield return null;
			}
		}
	}
	//------------------------------------------------
	//AI Function to handle chase behaviour for enemy
	//Can exit this state and enter either patrol or attack
	IEnumerator AI_Chase()
	{
		//Stop Agent
		Agent.Stop();
		
		//Loop forever while in chase state
		while(ActiveState == ENEMY_STATE.CHASE)
		{
			//Set destination to player
			Agent.SetDestination(PlayerTransform.position);
			
			//Check distances and state exit conditions
			float DistanceFromPlayer = Vector3.Distance(ThisTransform.position, PlayerTransform.position);
			
			//If within attack range, then change to attack state
			if(DistanceFromPlayer < AttackDistance) {ChangeState(ENEMY_STATE.ATTACK); yield break;}
			
			//If outside chase range, then revert to patrol state
			if(DistanceFromPlayer > ChaseDistance) {ChangeState(ENEMY_STATE.PATROL); yield break;}
			
			//Wait until next frame
			yield return null;
		}
	}
	//------------------------------------------------
	//AI Function to handle attack behaviour for enemy
	//Can exit this state and enter either patrol or chase
	IEnumerator AI_Attack()
	{
		//Stop Agent
		Agent.Stop();
		
		//Elapsed time - to calculate strike intervals
		float ElapsedTime = RecoveryDelay;
		
		//Loop forever while in chase state
		while(ActiveState == ENEMY_STATE.ATTACK)
		{
			//Update elapsed time
			ElapsedTime += Time.deltaTime;
			
			//Check distances and state exit conditions
			float DistanceFromPlayer = Vector3.Distance(ThisTransform.position, PlayerTransform.position);
			
			//If outside chase range, then revert to patrol state
			if(DistanceFromPlayer > ChaseDistance) {ChangeState(ENEMY_STATE.PATROL); yield break;}
			
			//If within attack range, then change to attack state
			if(DistanceFromPlayer > AttackDistance) {ChangeState(ENEMY_STATE.CHASE); yield break;}
			
			//Make strike
			if(ElapsedTime >= RecoveryDelay)
			{
				//Reset elapsed time
				ElapsedTime = 0;
				SendMessage("Strike",SendMessageOptions.DontRequireReceiver);
			}
			
			//Wait until next frame
			yield return null;
		}
	}
}
//------------------------------------------------