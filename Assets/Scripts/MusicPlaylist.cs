using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlaylist : MonoBehaviour
{

	public AudioSource track1;

	public AudioSource track2;

	public AudioSource track3;

	public AudioSource track4;

	public AudioSource track5;

	public int trackSelecter;

	public int trackHistory;

	private void Start() {
		trackSelecter = Random.Range(0, 5);

		if(trackSelecter == 0) {
			track1.Play();
		}
	}


	void Update()
    {
        if (!track1.isPlaying && !track2.isPlaying && !track3.isPlaying && !track4.isPlaying && !track5.isPlaying) {

		}
    }
}
