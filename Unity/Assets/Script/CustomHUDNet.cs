using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UI;

public class CustomHUDNet : NetworkManager
{
   public void StartHost()
   {
      SetPort();
      NetworkManager.singleton.StartHost();
   }

   public void JoinGame()
   {
      SetIPAddress();
      SetPort();
      NetworkManager.singleton.StartClient();
   }

   void SetIPAddress()
   {
      string IpAddress = GameObject.Find("InputFieldIPAddress").transform.Find("Text").GetComponent<Text>().text;
      NetworkManager.singleton.networkAddress = IpAddress;
   }

   void SetPort()
   {
      NetworkManager.singleton.networkPort = 7777;
   }
}
