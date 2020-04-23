using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAScene : MonoBehaviour 
{
	public string scene;
    PlayerController coin;
    private void Start()
    {
        coin = GameObject.Find("Prefab_FinalCharacterModel").GetComponent<PlayerController>();
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log("test2");

        if (other.gameObject.tag == ("Player") )
		{
            Paused.Save(coin.count);//coin.count should be it's input  might need to import paused as static
            SceneManager.LoadScene (scene, LoadSceneMode.Single);

            Debug.Log("test");
		}
        
    }
}
