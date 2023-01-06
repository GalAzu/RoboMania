using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Utilities;
using Photon.Pun;
public class SpawnPlayers : MonoBehaviour
{
    [SerializeField]
    private GameObject robotPlayerPrefab , magicPlayerPrefab;

    private enum playerType { Robot , Magic}
    private void Start()
    {
        PhotonNetwork.Instantiate(robotPlayerPrefab.name, Vector2Utilities.NewSpawnPos(), Quaternion.identity);
    }
}
