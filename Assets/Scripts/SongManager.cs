using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource song;

    protected void Awake()
    {
        song = GetComponent<AudioSource>();
    }

    public void GameStart()
    {
        song.Play();
    }

    public void StopGame()
    {
        song.Stop();
    }
}
