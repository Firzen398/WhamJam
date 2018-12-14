using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private SongManager songManager;


    private void StartGame()
    {
        songManager.GameStart();
    }
}
