using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace solmates {
	public class mainMenuUI : MonoBehaviour {

        public AudioSource aSource;
        public AudioClip buttonPress;

        void Start () {
		}
		
		void Update () {	
		}

		public void StartGame() {
            aSource.PlayOneShot(buttonPress);
            SceneManager.LoadScene(1);
		}

		public void EndGame() {
            aSource.PlayOneShot(buttonPress);
            Application.Quit();
		}
	}
}