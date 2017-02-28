using UnityEngine;
using UnityEngine.Networking;

public class ContenderController : NetworkBehaviour {

	[SyncVar]
	public GAME_CHOICE gameChoice;

	private TextMesh textChoice;
	private TextMesh textPlayerName;

	[SyncVar]
	public string playerName;

	[SyncVar]
	public Color playerColor;

	private void Awake() {
		textChoice = transform.FindChild("Choice").gameObject.GetComponent<TextMesh>();
		textPlayerName = transform.FindChild("PlayerName").gameObject.GetComponent<TextMesh>();
	}

	private void Start() {
		textPlayerName.text = playerName;
		textPlayerName.color = playerColor;
		textChoice.color = playerColor;
	}

	private void Update() {

		if (!isLocalPlayer) {
			return;
		}

		if (!HasChosen()) {
			if (Input.GetKeyDown(KeyCode.R)) {
				CmdSetChoice(GAME_CHOICE.ROCK);
			} else if (Input.GetKeyDown(KeyCode.P)) {
				CmdSetChoice(GAME_CHOICE.PAPER);
			} else if (Input.GetKeyDown(KeyCode.S)) {
				CmdSetChoice(GAME_CHOICE.SCISSORS);
			}
		}
	}

	[Command]
	public void CmdSetChoice(GAME_CHOICE gameChoice) {
		this.textChoice.text = gameChoice.ToString();
		this.gameChoice = gameChoice; 		
	}

	[ClientRpc]
	public void RpcShowChoice() {
		this.textChoice.text = gameChoice.ToString();
	}

	public bool HasChosen() {
		return gameChoice != GAME_CHOICE.NONE;
	}

	public enum GAME_CHOICE {
		NONE,
		ROCK,
		PAPER,
		SCISSORS
	}
}
