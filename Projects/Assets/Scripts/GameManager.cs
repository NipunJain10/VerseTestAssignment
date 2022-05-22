 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;
	// Initialize variables.
	private float size;

	[SerializeField]
	GameObject platform,Canvas,Pooltransform;
	[SerializeField]
	Player player;
	
	private Vector3 lastPosition;

	[SerializeField]
	GameObject gems;
	public List<GameObject> PlatformerPoolingList=new List<GameObject>();
	public List<GameObject> GemsPoolingList = new List<GameObject>();

    public Player Player { get => player; set => player = value; }

    private void Awake()
    {
        if (instance == null)
        {
			instance = this;

		}
    }
    void Start () {
		
		size = platform.transform.localScale.x;
		lastPosition = platform.transform.position;

		// Create the first initial platforms.
		for (int x = 0; x < 10; x++) {
			int random = Random.Range(0, 10);
			if (random % 2 == 0)
			{
				SpawnX();
			}
			else
			{
				SpawnZ();
			}
		}
		StartGame();
			InvokeRepeating("SpawnPlatform", 2f, 0.75f);
		
	}


	// Spawn a random platform.
	private void SpawnPlatform() {
		if (!Player.canMove)
		{
			return;
		}
			int random = Random.Range (0, 6);
		int gemsRandom = Random.Range (0, 7);

		if (random < 3) {
			SpawnX ();
		}

		if (random >= 3) {
			SpawnZ ();
		}

		if (gemsRandom < 4) {
			SpawnGem ();
		}
	}
	private void SpawnX() {
		Debug.Log("SpawnX");
		GameObject _platform;
		if (PlatformerPoolingList.Count == 0)
		{
			 _platform = Instantiate(platform) as GameObject;
        }
        else
        {
			_platform = PlatformerPoolingList[PlatformerPoolingList.Count-1];
			PlatformerPoolingList.RemoveAt(PlatformerPoolingList.Count - 1);

		}
		_platform.transform.position = lastPosition + new Vector3 (0, 0f, size);
		_platform.transform.SetParent(Pooltransform.transform);
		lastPosition = _platform.transform.position;
		_platform.GetComponent<MeshRenderer>().material.color = Color.green;
	}

	private void SpawnZ() {
		Debug.Log("SpawnZ");
		GameObject _platform;
		if (PlatformerPoolingList.Count == 0)
		{
			_platform = Instantiate(platform) as GameObject;

		}
		else
		{
			_platform = PlatformerPoolingList[PlatformerPoolingList.Count - 1];
			PlatformerPoolingList.RemoveAt(PlatformerPoolingList.Count - 1);

		}
		_platform.transform.position = lastPosition + new Vector3 (0f,0f,size);
		_platform.transform.SetParent(Pooltransform.transform);
		lastPosition = _platform.transform.position;
		_platform.GetComponent<MeshRenderer>().material.color = Color.red;
	}


	// Spawn a random gem.
	private void SpawnGem() {
		float random = Random.Range(-1.0f, 1.0f);
        if (GemsPoolingList.Count > 0)
        {
			GemsPoolingList[GemsPoolingList.Count - 1].transform.position = lastPosition + new Vector3(random, 0.7f, 0f);
			GemsPoolingList[GemsPoolingList.Count - 1].gameObject.SetActive(true);
			GemsPoolingList.RemoveAt(GemsPoolingList.Count - 1);
        }
        else
        {
			GameObject gem =Instantiate(gems, lastPosition + new Vector3(random, 0.7f, 0f), gems.transform.rotation);
			gem.transform.SetParent(Pooltransform.transform);

		}
	}

	public void StartGame()
	{
		Player.canMove = true;
		Player.PlayMusic(true);

	}

	public void OnClickBack()
	{
		LoadAsset.assetBundle.Unload(true);
		Application.LoadLevel("Menu");
	}
}
