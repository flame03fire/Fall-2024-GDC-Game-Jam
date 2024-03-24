using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScream : MonoBehaviour
{
    // This exists so the game over screams at you.
    public AudioClip screamingSound;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = screamingSound;
    }

    void Update()
    {
        audioSource.Play();
    }
}
