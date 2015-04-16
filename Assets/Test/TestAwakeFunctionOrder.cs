using UnityEngine;
using System.Collections;

public class TestAwakeFunctionOrder : MonoBehaviour {

	// Use this for initialization
	void Awake () 
	{
		Debug.Log (typeof(TestAwakeFunctionOrder));
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
