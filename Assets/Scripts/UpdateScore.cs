using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class UpdateScore : MonoBehaviour
{
    Score scoreScript;
    public AudioClip CoinAudio;
    // Start is called before the first frame update
    void Start()
    {
        scoreScript = GameObject.Find("GameManager").GetComponent<Score>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(CoinAudio, transform.position);
            scoreScript.AddScore();
            Destroy(gameObject);
        }
    }
}
