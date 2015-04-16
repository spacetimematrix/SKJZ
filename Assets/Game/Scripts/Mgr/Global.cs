using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour
{
	public static Global _instance;
	public static Global Instance
	{
		get{return _instance;}
	}

	void Awake()
	{
		Debug.Log (typeof(Global));

		_instance = GetComponent<Global> ();

		DontDestroyOnLoad (_instance);
	}


	void Start()
	{
		
	}


}
