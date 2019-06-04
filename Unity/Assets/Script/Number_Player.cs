using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Number_Player : NetworkBehaviour
{
    private int numOfConnectedPlayers = Network.connections.Length;
}
