using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameController : MonoBehaviour
{
	
	public int baseHealth=10;
	
	public string gameState="place";
	public float placmentTime=10f;
	private float startTime;
	
	private float nextEnemySpawn=0f;
	private float enemySpawnRate=1f;
	public GameObject unit;
	
	public List<GameObject> myUnits;
	public int maxUnits = 5;
	private int myUnitCount = 0;
	
	public GameObject enemyUnit;
	
	public  List<GameObject> enemyUnits;
	public int maxEnemyUnits = 1;
	private int myEnemyUnitCount = 0;
	
	
	private int unitNameInt = 0;
	
	
	//................................................................	
	
	void Start ()
	{
		myUnits = new List<GameObject> ();
		enemyUnits= new List<GameObject> ();
		startTime=Time.time;
		
	}
		
	//................................................................	
	
	
	void Update ()
	{
		
		switch(gameState) {
    case "place":
		
		
		if (Input.GetMouseButtonDown (0)) {	
			this.placeCannon ();	
		}
			
		if(Time.time-startTime>placmentTime){
				Debug.Log ("time out");
				gameState="sendingEnemy";
				
			}
			break;
			
		case "sendingEnemy":
		
			  if ( Time.time > nextEnemySpawn) {
        //nextEnemySpawn = Time.time + enemySpawnRate;
				
				nextEnemySpawn = Time.time+Random.Range(1.0f, 3.0f);
				this.spawEnemyUnit();
			}
			
			break;
				 
				
		}
	}
			
	//................................................................	
	
	
	void placeCannon ()
	{
		// is there any units left to place?
		//Debug.Log ("want to place unit nr:" + myUnitCount);
		if (myUnitCount < maxUnits) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			RaycastHit hit = new RaycastHit ();
		

			if (Physics.Raycast (ray, out hit)) {
				//Debug.Log ("hit");
				
				// is the collider my gameboard
				
				if (hit.collider.name == "gameboard") {
					//Debug.Log ("hit gameboard " + myUnitCount);
				
					if (myUnitCount > 0) {
						
						foreach (GameObject myTarget in myUnits) {
							float myDist = Vector3.Distance (hit.point, myTarget.transform.position);
							//Debug.Log ("distance to " + 1 + " : " + myDist);
							
							
							if (myDist < 2f) {
								//Debug.Log ("too close: " + myDist);
								return;
								// exit code
							}
      
							
						}
						

						
					}
					/*	
					// place unit
					Debug.Log ("hit point: "+hit.point);
					GameObject unitSpawn = (GameObject)Instantiate (unit, hit.point, transform.rotation);
					
					
					MyUnitController myUnit = unitSpawn.GetComponent ("MyUnitController") as MyUnitController;
					
					myUnit.turnRate = Random.Range (1, 3);
					
					myUnit.transform.position=hit.point;
					
					unitSpawn.name="friendlyUnit"+unitNameInt;
					
					myUnits.Add (unitSpawn);
					unitNameInt++;
					
					//Debug.Log ("after add " + myUnits [myUnitCount]);
					myUnitCount = myUnitCount + 1;
					//Debug.Log (myUnitCount);
					*/	
				}
				
			}
			
				
		} 
		
	}
	
//................................................................
	

	
	void spawEnemyUnit ()
	{
	/*	
	if (enemyUnits.Count<maxEnemyUnits){
		GameObject enemyUnitSpawn = (GameObject)Instantiate (enemyUnit, new Vector3 (0, 0, -8f), transform.rotation);
					
					
		MyUnitController enemyUnitController = enemyUnitSpawn.GetComponent ("MyUnitController") as MyUnitController;
			
		enemyUnitSpawn.name="enemyUnit"+unitNameInt;
			
		enemyUnits.Add (enemyUnitSpawn);
			
			myEnemyUnitCount=myEnemyUnitCount+1;
			unitNameInt=unitNameInt+1;
			
		}
		*/			
	}
	
	//................................................................
	
	public void reciveDamage(int myDamage){
		
		baseHealth=baseHealth-myDamage;
		
		if (baseHealth<=0)
		{
			Debug.Log ("game over");
			Application.LoadLevel("menu");
		}
		
	}
	
	//................................................................
	
	
	public void removeEnemy(string unitName)
	{
		//Debug.Log ("destroy unit: "+unitName);
		
		// find name
		int i=0;
			foreach (GameObject myTarget in enemyUnits) {
			
							if (myTarget.name==unitName) {
				
							enemyUnits.RemoveAt(i);
				myEnemyUnitCount=myEnemyUnitCount-1;
								return;
							}
      
							i++;
						}
	
	}
	
}

//................................................................


