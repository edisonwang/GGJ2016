using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour {
    
    public GameObject[] obs;
    
  
	// Use this for initialization
	void Start () { 
        
        SceneManager.LoadScene(1,LoadSceneMode.Additive);
    SceneManager.LoadScene(2,LoadSceneMode.Additive);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
