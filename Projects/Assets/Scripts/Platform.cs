using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.tag == "Player") {
			Invoke ("FallDown",1.2f);
		}
	}

	private void FallDown() {
		GameManager.instance.PlatformerPoolingList.Add(this.gameObject.transform.parent.gameObject);

	}
}
