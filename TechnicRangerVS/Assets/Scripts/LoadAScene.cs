using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAScene : MonoBehaviour 
{
	public string scene;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("test2");

        if (other.gameObject.tag == ("Player") )
		{
				SceneManager.LoadScene (scene, LoadSceneMode.Single);

            Debug.Log("test");
		}
        
    }
}
