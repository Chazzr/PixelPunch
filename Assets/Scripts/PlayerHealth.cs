using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour {

	public int maxHealth;

	[SyncVar(hook = "OnChangeHealth")]
	public int currentHealth;

	public RectTransform healthBar;

	void Awake(){
		currentHealth = maxHealth;
	}

	public void TakeDamage (int amount){

		if (!isServer){
			return;
		}

		currentHealth -= amount;

		if (currentHealth <= 0) {
			
			currentHealth = maxHealth;

			RpcRespawn ();
		}
	}

	void OnChangeHealth(int health){
		healthBar.sizeDelta = new Vector2 (health, healthBar.sizeDelta.y);
	}

	[ClientRpc]
	void RpcRespawn(){
		if (isLocalPlayer) {
			transform.position = Vector3.zero;
		}
	}
}
