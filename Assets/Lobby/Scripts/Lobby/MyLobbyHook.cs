using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class MyLobbyHook : LobbyHook {
	public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer) {
		LobbyPlayer lobbyPlayerComponent = lobbyPlayer.GetComponent<LobbyPlayer>();
		ContenderController contenderController = gamePlayer.GetComponent<ContenderController>();

		contenderController.playerColor = lobbyPlayerComponent.playerColor;
		contenderController.playerName = lobbyPlayerComponent.playerName;	
	}
}
