using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;

public class GameManager : NetworkBehaviour {

	public List<ContenderController> contenders;

	[SyncVar(hook="OnWinnerDeclared")]
	public string winner;

	private TextMesh winnerText;
	private void Start() {
		winnerText = GetComponentInChildren<TextMesh>();
	}

	private void Update() {
		if (isServer) {
			contenders = GameObject.FindObjectsOfType<ContenderController>().ToList();

			if(contenders.Count > 1 && AllContendersSubmitted()) {
				DeclareWinner(contenders);
			}
		}
	}

	public bool AllContendersSubmitted() {
		return contenders.All(contender => contender.HasChosen());
	}

	public void DeclareWinner(List<ContenderController> contenders) {
		ContenderController.GAME_CHOICE contender0Choice = contenders[0].gameChoice;
		ContenderController.GAME_CHOICE contender1Choice = contenders[1].gameChoice;

		if (contender0Choice != contender1Choice) {
			if ((int) contender0Choice % 3 > (int) contender1Choice % 3){
				winner = contenders[0].playerName;
			} else {
				winner = contenders[1].playerName;
			}
		}
		
		contenders.ForEach(contender => contender.RpcShowChoice());
		StartCoroutine(ReturnToLobby());
	}
	
	public void OnWinnerDeclared(string winner) {
		winnerText.text = "Winner: " + winner;
	}

	IEnumerator ReturnToLobby() {
		yield return new WaitForSeconds(3.0f);
		Prototype.NetworkLobby.LobbyManager.s_Singleton.ServerReturnToLobby();
	}
}
