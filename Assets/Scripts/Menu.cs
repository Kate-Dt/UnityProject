using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void ChooseLevel()
    {
        SceneManager.LoadScene(1);
    }
}
