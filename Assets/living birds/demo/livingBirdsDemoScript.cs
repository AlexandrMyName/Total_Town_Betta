using UnityEngine;
using System.Collections;

public class livingBirdsDemoScript : MonoBehaviour {
	public lb_BirdController birdControl;
	public Camera camera1;
	 

	Camera currentCamera;
	bool cameraDirections = true;
	Ray ray;
	RaycastHit[] hits;

	void Start(){
		 
		StartCoroutine( SpawnSomeBirds());
	}

	// Update is called once per frame
	void Update () {
	 
		//if(Input.GetMouseButtonDown(0)){
		//	ray = currentCamera.ScreenPointToRay(Input.mousePosition);
		//	hits = Physics.RaycastAll (ray);
		//	foreach(RaycastHit hit in hits){
		//		if (hit.collider.tag == "lb_bird"){
		//			hit.transform.SendMessage ("KillBirdWithForce",ray.direction*500);
		//			break;
		//		}
		//	}
		//}
	}

	 

	IEnumerator SpawnSomeBirds(){
		while (true)
		{
			yield return new WaitForSeconds(2);
			birdControl.SendMessage("SpawnAmount", 10);
			Debug.Log("Спавню");
		}
	}

	 
}
