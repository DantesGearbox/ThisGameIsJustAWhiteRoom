using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillField : MonoBehaviour
{

	public Transform respawnPosition;
	private NewMovement player;
	public DialogueTrigger dialogueTrigger;
	public AudioManager audioManager;

	public float deathDelay = (1.0f / 60.0f) * 60;
	private float deathTimer = 0;
	private bool dead = false;
	private bool deathAnim = false;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {

			if (!dead) {
				player = collision.gameObject.GetComponent<NewMovement>();
				dead = true;
				deathAnim = true;

				audioManager.Play("Death");
				//collision.gameObject.GetComponent<NewMovement>().Die(respawnPosition.position);
			}
		}
	}

    // Update is called once per frame
    void Update()
    {
		if (deathAnim) {
			player.DeathAnimation();
			deathAnim = false;
		}


		if (dead) {
			deathTimer += Time.deltaTime;
			if(deathTimer > deathDelay) {
				deathTimer = 0;
				dead = false;
				player.Die(respawnPosition.position);
				dialogueTrigger.IncreaseDeathCounter();
			}
		}
    }
}
