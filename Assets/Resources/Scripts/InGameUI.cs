using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace solmates {
	public class InGameUI : MonoBehaviour {

		public float timeToWait=6f;
        
		void OnEnable() {
			StartCoroutine(GotoFirstScene());
		}

		IEnumerator GotoFirstScene() {
			yield return new WaitForSeconds(timeToWait);
            SceneManager.LoadScene(0);
        }
	}
}