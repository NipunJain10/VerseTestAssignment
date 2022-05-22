using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3(15,20,45) * Time.deltaTime);

        if (GameManager.instance.Player.transform.position.z - this.transform.position.z > 4 && !GameManager.instance.GemsPoolingList.Contains(this.gameObject))

        
        {
			GameManager.instance.GemsPoolingList.Add(this.gameObject);
			this.gameObject.SetActive(false);

		}
	}
}
