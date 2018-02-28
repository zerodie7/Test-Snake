using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

    public EmergentMenu CurrentMenu;

	// Use this for initialization
	public void Start () {
        ShowMenu(CurrentMenu);
	}
	
    public void ShowMenu (EmergentMenu menu)    {
        if (CurrentMenu != null)
            CurrentMenu.IsOpen = false;

        CurrentMenu = menu;
        CurrentMenu.IsOpen = true;
    }
}
