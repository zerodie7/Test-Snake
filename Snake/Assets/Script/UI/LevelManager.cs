using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;




	void Start () {
		if (autoLoadNextLevelAfter <= 0) {
			print ("Level auto load disabled, use a postive number is seconds");
		} else {
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}
	}

    void Update()
    {
        Time.timeScale = 1f;
    }

    public void LoadLevel(string name){
        Time.timeScale = 1f;
		print ("New Level load: " + name);
		 SceneManager.LoadScene(name);
    }


    public void QuitRequest(){
		print ("Quit requested");
		Application.Quit ();
	}
	
	public void LoadNextLevel() {
        Time.timeScale = 1f;
        //SceneManager.LoadScene(SceneManager.LoadScene + 1);
        Application.LoadLevel(Application.loadedLevel + 1);    
    }



}


