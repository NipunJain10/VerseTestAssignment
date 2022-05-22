using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	// Initialize variables.
	private Rigidbody rb;
	private bool isMovingRight = false;
	private bool hasPlayerStarted = false;
	 [SerializeField]
    MeshRenderer PlayerRenderer;
	[SerializeField]
	Transform PlayerParent;
	[SerializeField]
	float speed = 4f;


	[SerializeField]
	AudioSource audioSourceMusic,tapsound,gemCollect;

	public bool canMove = false;

	[SerializeField]
	GameObject particle;

	[SerializeField]
	GameObject gems;

	private bool isGameover = false;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		PlayerRenderer.material.color = Constants.ColorProp;
		PlayerRenderer.material.SetColor("_EmissionColor", Constants.ColorProp);
		PlayerParent.localScale = Constants.PlayerScale;
	}

	public void PlayMusic(bool flag)
    {
		if (flag)
		{
			audioSourceMusic.Play();
		}
        else
        {
			audioSourceMusic.Stop();
		}
	}
	public void OnClickChangeDirection()
	{
		if (canMove == true)
		{

			if (hasPlayerStarted == false)
			{
				hasPlayerStarted = true;

				StartCoroutine(ShowGems(2.5f));
				
			}

			PlayTapSound();
			ChangeBoolean();
			ChangeDirection();

		}

		
	}
	void PlayTapSound()
    {
		tapsound.Play();
	}

	// Hide the gems for the first 2.5 seconds (avoiding bugs).
	IEnumerator ShowGems (float count) {
		yield return new WaitForSeconds (count);

		// Checks if player hasn't fallen off before showing grms.
		if (canMove == true) {
			gems.SetActive (true);
		}
	}
		

	// Control player direction.
	private void ChangeBoolean() {
		isMovingRight = !isMovingRight;
	}
	private void ChangeDirection() {
		if (isMovingRight == true) {
			rb.velocity = new Vector3 (speed, 0f, 0f);
		} 
		else {
			rb.velocity = new Vector3 (0f,0f,speed);
		}
	}

    private void FixedUpdate()
    {
		if (canMove == true)
		{
			rb.velocity = new Vector3(0f, 0f, speed);
		}
        if (mLeftPressed)
        {
			this.transform.position += Vector3.left * 0.05f;
		}
		if (mRightPressed)
		{
			this.transform.position += Vector3.right * 0.05f;
		}
		if (Physics.Raycast(this.transform.position, Vector3.down * 2) == false)
		{
			FallDown();
		}
	}

	bool mLeftPressed = false;
	bool mRightPressed = false;
	public void MoveplayerLeft(bool flag)
    {
		if (canMove == true)
		{
			mLeftPressed = flag;
		}
		
    }

	public void MoveplayerRight(bool flag)
	{
		if (canMove == true)
		{
			mRightPressed = flag;
		}
	}
	
	public void Jump()
    {
		if (canMove == true)
		{
			rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
		}
	//	rb.velocity = new Vector3(rb.transform.position.x, rb.transform.position.y+2.0f, rb.transform.position.z);
	}

	// When the player falls off the platform.
	private void FallDown() {
		canMove = false;
		rb.velocity = new Vector3 (0f,-4f,0f);
		PlayMusic(false);
		if (!isGameover) {
			isGameover = true;
			audioSourceMusic.Pause();
			audioSourceMusic.loop = false;
			audioSourceMusic.Play(); }
	
		StartCoroutine(ReturnToMainMenu (2.8f));
	}
	// Return to main menu.
	IEnumerator ReturnToMainMenu (float count) {
		yield return new WaitForSeconds (count);
		
		Application.LoadLevel("Menu");
	}


	// When the player hits a gem.
	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Gem") {
			other.gameObject.SetActive(false);
			GameManager.instance.GemsPoolingList.Add(other.gameObject);
			GameObject _particle = Instantiate (particle) as GameObject;
			_particle.transform.position = this.transform.position;
			Destroy (_particle, 1f);
			gemCollect.Play();
		}
	}

}
