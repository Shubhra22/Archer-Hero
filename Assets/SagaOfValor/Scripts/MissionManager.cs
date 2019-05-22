using UnityEngine;
using System.Collections;

public class MissionManager : MonoBehaviour 
{
	public Transform Container;
	public GameObject endLevel;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Container.childCount<=0)
		{
			endLevel.SetActive(true);
		}
	}
}
