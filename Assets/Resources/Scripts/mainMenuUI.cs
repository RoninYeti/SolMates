using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace solmates {
	public class mainMenuUI : MonoBehaviour {

		void Start () {
		}
		
		void Update () {	
		}

		public void StartGame()
		{
			SceneManager.LoadScene (1);
		}

		public void EndGame()
		{
			Application.Quit ();
		}
	}
}