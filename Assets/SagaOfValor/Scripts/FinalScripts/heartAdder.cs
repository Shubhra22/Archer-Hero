using UnityEngine;
using System.Collections;

public class heartAdder : MonoBehaviour {
	public GameObject heartGUI;
	public Vector2 startPoint = new Vector2(0.05f,0.95f);
	public float distance = 0.04f;
	
	void addHearts (int amount){
		for(int i = 0;i < amount;i++){
			Vector3 pos = new Vector3(startPoint.x+(distance*i), startPoint.y,0);
			GameObject heartPrefab = Instantiate(heartGUI, pos, Quaternion.Euler(0,0,0)) as GameObject;
			heartPrefab.transform.name = "heart"+(i+1).ToString();
			heartPrefab.transform.parent = transform;
		}
	}
}
