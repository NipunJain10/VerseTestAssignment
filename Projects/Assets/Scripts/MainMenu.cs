using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	[SerializeField]
	GameObject Loading;

    public void StartGame() {
		Loading.SetActive(true);
		SceneManager.LoadScene("Customisation");
	}

	public void Quit() {
		Application.Quit();
	}

    public void OnApplicationQuit()
    {

        Application.Quit();
	}
	

}
