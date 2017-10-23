using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P2MovementLaser : MonoBehaviour {

	CharacterController playerOne;
	private float speed ;
	private Vector3 playerOnePosition = Vector3.zero;	
	public static float energy;
	private bool energyZone;
	private SpriteRenderer spriteR;
	private bool invisOn;
	int x;
	[SerializeField] private Transform Center;
    [SerializeField] private LayerMask visibleObjects;
	public GameObject deathAnimTarget;
	public GameObject deathAnimPlayers;
	int playerKillPoint;
	int innocentKillPoint;
	float distance = 100.0f;
	int layerMaskPlayer = 1<< 8;
	
	LineRenderer linerenderer;
		
	void SetupLine(){
	//	linerenderer.numPosition(2);
		//linerenderer.SetPosition(0,Center.position);
		linerenderer.startWidth = 0.1f;
		linerenderer.endWidth = 0.1f;

	}
	
	void Start () {
		
		playerOne = GameObject.FindGameObjectWithTag("Player2").GetComponent<CharacterController>();
		spriteR = gameObject.GetComponent<SpriteRenderer>();
		speed = 5;
		energy = 100;
		energyZone = false;
		invisOn = false;
		playerKillPoint = 2;
		innocentKillPoint = -1;
		
		linerenderer = GetComponentInChildren<LineRenderer>();
		linerenderer.enabled = true;
	}
		
	
	
	
	void Update () {
	
		GameObject.Find("P2energyBar").transform.localScale = new Vector3(energy/50,0.25f,1);
		
		if(energy >= 100.0f){
			
			energy = 100.0f;
			
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			energy = 100;
		}
		
		if(Input.GetAxis("P2_Horizontal") != 0 || Input.GetAxis("P2_Vertical") != 0){
			
			playerOnePosition = new Vector3(Input.GetAxisRaw("P2_Horizontal"), Input.GetAxisRaw("P2_Vertical"), 0 );
		
			playerOnePosition.Normalize();
		
			playerOnePosition *= speed;
			playerOne.Move(playerOnePosition * Time.deltaTime);	
		
		}
		
		else{
			
			playerOnePosition = Vector3.zero;
		}
		
		
		if (Input.GetButtonDown("P2_FireUp") && energy >= 15){
			energy -= 15;
			linerenderer.enabled = true;
			LookForTargetUp();
			
		}
		
		if (Input.GetButtonDown("P2_FireDown") && energy >= 15){
			
			energy -= 15;
			linerenderer.enabled = true;			
			LookForTargetDown();
		}
	/*	else{
			linerenderer.enabled = false;
		}
	*/	
		if (Input.GetButtonDown("P2_FireLeft") && energy >= 15){
			
			energy -= 15;
			linerenderer.enabled = true;
			LookForTargetLeft();
		}

		
		if (Input.GetButtonDown("P2_FireRight") && energy >= 15){
			
			energy -= 15;
			linerenderer.enabled = true;
			LookForTargetRight();

		}

		
		if(energyZone){
			
			energy = energy + Time.deltaTime * 10;
		
		}
		
		if (Input.GetButtonDown("P2_Invisibility")){
			
			if ( invisOn == false){

				invisOn = true;
				
			}
			
			else {
				
				invisOn = false;
			}
			
			
		}
		
		if ( energy <= 0){
				
				invisOn = false;
		}
			
		if (invisOn){

			energy = energy - Time.deltaTime * 20;
			spriteR.color = new Color(1f,1f,1f,0f);	
			
		}
		
		else{
			
			spriteR.color = new Color(1f,1f,1f,1f);
			
		}
		
		if ( this.gameObject.transform.position.x < -11.5 || this.gameObject.transform.position.x > 11.5 ||
			this.gameObject.transform.position.y > 5.5 || this.gameObject.transform.position.y < -5.5	){
			
			x = Random.Range(0,19);
			
			this.gameObject.transform.position = GameObject.Find("NPC" + x).transform.position;
			Destroy(GameObject.Find("NPC"+x));
			
		}
	
		
	}
	
	void LookForTargetUp(){
		
		var hitNPCup = Physics2D.Raycast((Center.position + transform.up*0.225f), Vector2.up, distance, visibleObjects);
		var hitPlayerup = Physics2D.Raycast((Center.position + transform.up*0.225f), Vector2.up, distance, layerMaskPlayer);
		
		linerenderer.SetPosition(0,Vector3.zero); //sets line position to where the object is.
		
		if(hitPlayerup && (hitPlayerup.transform.tag =="Player1"))
        {	
			linerenderer.SetPosition(1, new Vector3(0, hitPlayerup.distance,0));			

			x = Random.Range(0,19);
			Instantiate(deathAnimPlayers, hitPlayerup.transform.position, Quaternion.identity);
			hitPlayerup.transform.parent.gameObject.transform.position = GameObject.Find("NPC" + x).transform.position;
			Destroy(GameObject.Find("NPC"+x));
			PointSystem.AddScoreP1(innocentKillPoint);
			PointSystem.AddScoreP2(playerKillPoint);
        }
		
		if(hitPlayerup && (hitPlayerup.transform.tag =="Player3"))
        {	
			linerenderer.SetPosition(1, new Vector3(0, hitPlayerup.distance,0));			

			x = Random.Range(0,19);
			Instantiate(deathAnimPlayers, hitPlayerup.transform.position, Quaternion.identity);
			hitPlayerup.transform.parent.gameObject.transform.position = GameObject.Find("NPC" + x).transform.position;
			Destroy(GameObject.Find("NPC"+x));
			PointSystem.AddScoreP3(innocentKillPoint);
			PointSystem.AddScoreP2(playerKillPoint);
        }
		
		if(hitPlayerup && (hitPlayerup.transform.tag =="Player4"))
        {	
			linerenderer.SetPosition(1, new Vector3(0, hitPlayerup.distance,0));			

			x = Random.Range(0,19);
			Instantiate(deathAnimPlayers, hitPlayerup.transform.position, Quaternion.identity);
			hitPlayerup.transform.parent.gameObject.transform.position = GameObject.Find("NPC" + x).transform.position;
			Destroy(GameObject.Find("NPC"+x));
			PointSystem.AddScoreP4(innocentKillPoint);
			PointSystem.AddScoreP2(playerKillPoint);
        }
		
		if (hitNPCup && (hitNPCup.transform.tag == "NPC"))
        {	
			
			linerenderer.SetPosition(1, new Vector3(0, hitNPCup.distance,0));			
			
			Instantiate(deathAnimPlayers, hitNPCup.transform.position, Quaternion.identity);
			Destroy(hitNPCup.transform.gameObject);
			PointSystem.AddScoreP2(innocentKillPoint);
        } 
		
		if (hitNPCup && (hitNPCup.transform.tag == "Target"))
        {	
			linerenderer.SetPosition(1, new Vector3(0, hitNPCup.distance,0));			

			Instantiate(deathAnimTarget, hitNPCup.transform.position, Quaternion.identity);
			GameObject.Find("Target").transform.position = GameObject.Find("Respawn").transform.position;
			NPC_Spawner.timerActive();
			PointSystem.AddScoreP2(playerKillPoint);
			
        } 
		
		if (hitNPCup && (hitNPCup.transform.tag == "Wall")){
			Debug.Log("Hit the Wall");
			linerenderer.SetPosition(1, new Vector3(0,distance,0));			

		}
		}
	
	
	void LookForTargetRight(){

		var hitNPCright = Physics2D.Raycast((Center.position + transform.right*0.225f), Vector2.right, distance, visibleObjects);
		var hitPlayerright = Physics2D.Raycast((Center.position  + transform.right*0.225f), Vector2.right, distance, layerMaskPlayer);
	
		linerenderer.SetPosition(0,Vector3.zero); //sets line position to where the object is.
		
		if(hitPlayerright && (hitPlayerright.transform.tag =="Player1"))
        {	
			linerenderer.SetPosition(1, new Vector3(hitPlayerright.distance,0,0));			
			
			x = Random.Range(0,19);
			Instantiate(deathAnimPlayers, hitPlayerright.transform.position, Quaternion.identity);
			hitPlayerright.transform.parent.gameObject.transform.position = GameObject.Find("NPC" + x).transform.position;
			Destroy(GameObject.Find("NPC"+x));
			PointSystem.AddScoreP1(innocentKillPoint);
			PointSystem.AddScoreP2(playerKillPoint);
        }
		
		if(hitPlayerright && (hitPlayerright.transform.tag =="Player3"))
        {	
			linerenderer.SetPosition(1, new Vector3(hitPlayerright.distance,0,0));			
			
			x = Random.Range(0,19);
			Instantiate(deathAnimPlayers, hitPlayerright.transform.position, Quaternion.identity);
			hitPlayerright.transform.parent.gameObject.transform.position = GameObject.Find("NPC" + x).transform.position;
			Destroy(GameObject.Find("NPC"+x));
			PointSystem.AddScoreP3(innocentKillPoint);
			PointSystem.AddScoreP2(playerKillPoint);
        }
		
		if(hitPlayerright && (hitPlayerright.transform.tag =="Player4"))
        {	
			linerenderer.SetPosition(1, new Vector3(hitPlayerright.distance,0,0));			
			
			x = Random.Range(0,19);
			Instantiate(deathAnimPlayers, hitPlayerright.transform.position, Quaternion.identity);
			hitPlayerright.transform.parent.gameObject.transform.position = GameObject.Find("NPC" + x).transform.position;
			Destroy(GameObject.Find("NPC"+x));
			PointSystem.AddScoreP4(innocentKillPoint);
			PointSystem.AddScoreP2(playerKillPoint);
        }
		
		if (hitNPCright && (hitNPCright.transform.tag == "NPC"))
        {	
			linerenderer.SetPosition(1, new Vector3(hitNPCright.distance,0,0));			
			
			Instantiate(deathAnimPlayers, hitNPCright.transform.position, Quaternion.identity);
			Destroy(hitNPCright.transform.gameObject);
			PointSystem.AddScoreP2(innocentKillPoint);
        } 
		
		if (hitNPCright && (hitNPCright.transform.tag == "Target"))
        {	
			linerenderer.SetPosition(1, new Vector3(hitNPCright.distance,0,0));			
			
			Instantiate(deathAnimTarget, hitNPCright.transform.position, Quaternion.identity);
			hitNPCright.transform.position = GameObject.Find("Respawn").transform.position;
			NPC_Spawner.timerActive();
			PointSystem.AddScoreP2(playerKillPoint);
			
        }
		
		if (hitNPCright && (hitNPCright.transform.tag == "Wall")){
			Debug.Log("Hit the Wall");
			linerenderer.SetPosition(1, new Vector3(distance,0,0));			

		}	
				
	}
	
	
	void LookForTargetLeft(){
		

		var hitNPCleft = Physics2D.Raycast((Center.position - transform.right*0.225f), Vector2.left, distance, visibleObjects);
		var hitPlayerleft = Physics2D.Raycast((Center.position - transform.right*0.225f), Vector2.left, distance, layerMaskPlayer);
	
		linerenderer.SetPosition(0,Vector3.zero); //sets line position to where the object is.	
	
		if(hitPlayerleft && (hitPlayerleft.transform.tag =="Player1"))
        {	
			linerenderer.SetPosition(1, new Vector3(-hitPlayerleft.distance,0,0));			
			
			x = Random.Range(0,19);
			Instantiate(deathAnimPlayers, hitPlayerleft.transform.position, Quaternion.identity);
			hitPlayerleft.transform.parent.gameObject.transform.position = GameObject.Find("NPC" + x).transform.position;
			Destroy(GameObject.Find("NPC"+x));
			PointSystem.AddScoreP1(innocentKillPoint);
			PointSystem.AddScoreP2(playerKillPoint);
        }
		
		if(hitPlayerleft && (hitPlayerleft.transform.tag =="Player3"))
        {	
			linerenderer.SetPosition(1, new Vector3(-hitPlayerleft.distance,0,0));			

			x = Random.Range(0,19);
			Instantiate(deathAnimPlayers, hitPlayerleft.transform.position, Quaternion.identity);
			hitPlayerleft.transform.parent.gameObject.transform.position = GameObject.Find("NPC" + x).transform.position;
			Destroy(GameObject.Find("NPC"+x));
			PointSystem.AddScoreP3(innocentKillPoint);
			PointSystem.AddScoreP2(playerKillPoint);
        }
		
		if(hitPlayerleft && (hitPlayerleft.transform.tag =="Player4"))
        {	
			linerenderer.SetPosition(1, new Vector3(-hitPlayerleft.distance,0,0));			

			x = Random.Range(0,19);
			Instantiate(deathAnimPlayers, hitPlayerleft.transform.position, Quaternion.identity);
			hitPlayerleft.transform.parent.gameObject.transform.position = GameObject.Find("NPC" + x).transform.position;
			Destroy(GameObject.Find("NPC"+x));
			PointSystem.AddScoreP4(innocentKillPoint);
			PointSystem.AddScoreP2(playerKillPoint);
        }
		
		if (hitNPCleft && (hitNPCleft.transform.tag == "NPC"))
        {	
			linerenderer.SetPosition(1, new Vector3(-hitNPCleft.distance,0,0));			

			Instantiate(deathAnimPlayers, hitNPCleft.transform.position, Quaternion.identity);
			Destroy(hitNPCleft.transform.gameObject);
			PointSystem.AddScoreP2(innocentKillPoint);
        } 
		
		if (hitNPCleft && (hitNPCleft.transform.tag == "Target"))
        {	
			linerenderer.SetPosition(1, new Vector3(-hitNPCleft.distance,0,0));			

			Instantiate(deathAnimTarget, hitNPCleft.transform.position, Quaternion.identity);
			hitNPCleft.transform.position = GameObject.Find("Respawn").transform.position;
			NPC_Spawner.timerActive();
			PointSystem.AddScoreP2(playerKillPoint);
			
        } 
		if (hitNPCleft && (hitNPCleft.transform.tag == "Wall")){
			Debug.Log("Hit the Wall");
			linerenderer.SetPosition(1, new Vector3(-distance,0,0));			

		}		
	
	}
	
	void LookForTargetDown(){
		
		var hitNPCdown = Physics2D.Raycast((Center.position - transform.up*0.225f), Vector2.down, distance, visibleObjects);
		var hitPlayerdown = Physics2D.Raycast((Center.position - transform.up*0.225f), Vector2.down, distance, layerMaskPlayer);
	
		if(hitPlayerdown && (hitPlayerdown.transform.tag =="Player1"))
        {	
			linerenderer.SetPosition(1, new Vector3(0, -hitPlayerdown.distance,0));

			x = Random.Range(0,19);
			Instantiate(deathAnimPlayers, hitPlayerdown.transform.position, Quaternion.identity);
			hitPlayerdown.transform.parent.gameObject.transform.position = GameObject.Find("NPC" + x).transform.position;
			Destroy(GameObject.Find("NPC"+x));
			PointSystem.AddScoreP1(innocentKillPoint);
			PointSystem.AddScoreP2(playerKillPoint);
        }
		
		if(hitPlayerdown && (hitPlayerdown.transform.tag =="Player3"))
        {	
			linerenderer.SetPosition(1, new Vector3(0, -hitPlayerdown.distance,0));

			x = Random.Range(0,19);
			Instantiate(deathAnimPlayers, hitPlayerdown.transform.position, Quaternion.identity);
			hitPlayerdown.transform.parent.gameObject.transform.position = GameObject.Find("NPC" + x).transform.position;
			Destroy(GameObject.Find("NPC"+x));
			PointSystem.AddScoreP3(innocentKillPoint);
			PointSystem.AddScoreP2(playerKillPoint);
        }
		
		if(hitPlayerdown && (hitPlayerdown.transform.tag =="Player4"))
        {	
			linerenderer.SetPosition(1, new Vector3(0, -hitPlayerdown.distance,0));

			x = Random.Range(0,19);
			Instantiate(deathAnimPlayers, hitPlayerdown.transform.position, Quaternion.identity);
			hitPlayerdown.transform.parent.gameObject.transform.position = GameObject.Find("NPC" + x).transform.position;
			Destroy(GameObject.Find("NPC"+x));
			PointSystem.AddScoreP4(innocentKillPoint);
			PointSystem.AddScoreP2(playerKillPoint);
        }
		
		if (hitNPCdown && (hitNPCdown.transform.tag == "NPC"))
        {	
			linerenderer.SetPosition(1, new Vector3(0, -hitNPCdown.distance,0));

			Instantiate(deathAnimPlayers, hitNPCdown.transform.position, Quaternion.identity);
			Destroy(hitNPCdown.transform.gameObject);
			PointSystem.AddScoreP2(innocentKillPoint);
        } 
		
		if (hitNPCdown && (hitNPCdown.transform.tag == "Target"))
        {	
			linerenderer.SetPosition(1, new Vector3(0, -hitNPCdown.distance,0));

			Instantiate(deathAnimTarget, hitNPCdown.transform.position, Quaternion.identity);
			hitNPCdown.transform.position = GameObject.Find("Respawn").transform.position;
			NPC_Spawner.timerActive();
			PointSystem.AddScoreP2(playerKillPoint);
			
        } 
		if (hitNPCdown && (hitNPCdown.transform.tag == "Wall")){
			Debug.Log("Hit the Wall");
			linerenderer.SetPosition(1, new Vector3(0,-distance,0));			

		}		
	
	}
	
	public void energyZoneActive(){
		
		energyZone = true;
		
	}
	
	public void energyZoneInActive(){
		
		energyZone = false;
		
	}
	
	public void Wall(){
	
		
	}
	
	public static float energyGet(){
		
		return energy;
		
	}
	
	
}
