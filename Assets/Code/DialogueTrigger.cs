using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
	public Dialogue startDialogue;
	public Dialogue death1Dialogue;
	public Dialogue death2Dialogue;
	public Dialogue death3Dialogue;
	public Dialogue disobeyDialogue;
	public Dialogue disobey2Dialogue;
	public Dialogue disobey3Dialogue;
	public Dialogue obeyDialogue;

	private DialogueManager dialogueManager;

	//start dialogue trigger
	public bool hasPlayedStartDialogue = false;

	//obey dialogue trigger
	private int deathCounter = 0;

	//disobey dialogue trigger
	public float disobeyTimer = 0;
	public float disobey1Time = 5.0f;
	private bool hasPlayedDisobey1 = false;
	public float disobey2Time = 1.0f;
	private bool hasPlayedDisobey2 = false;
	public float disobey3Time = 1.0f;
	private bool hasPlayedDisobey3 = false;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.CompareTag("Player")) {
			if (!hasPlayedStartDialogue) {
				TriggerDialogue(startDialogue);
				hasPlayedStartDialogue = true;
			}
		}
	}

	public void IncreaseDeathCounter() {
		deathCounter++;
		if(deathCounter == 1) {
			TriggerDialogue(death1Dialogue);
		}
		if (deathCounter == 2) {
			TriggerDialogue(death2Dialogue);
		}
		if (deathCounter > 2 && deathCounter < 6) {
			TriggerDialogue(death3Dialogue);
		}
		if (deathCounter == 6) {
			TriggerDialogue(obeyDialogue);
		}
		disobeyTimer = 0f;
	}

	private void Update() {
		if (hasPlayedStartDialogue) {
			disobeyTimer += Time.deltaTime;
			if (disobeyTimer > disobey1Time && !hasPlayedDisobey1) {
				hasPlayedDisobey1 = true;
				TriggerDialogue(disobeyDialogue);
			}
			if (disobeyTimer > disobey2Time && !hasPlayedDisobey2 && hasPlayedDisobey1) {
				hasPlayedDisobey2 = true;
				TriggerDialogue(disobey2Dialogue);
			}
			if (disobeyTimer > disobey3Time && !hasPlayedDisobey3 && hasPlayedDisobey1 && hasPlayedDisobey2) {
				hasPlayedDisobey3 = true;
				TriggerDialogue(disobey3Dialogue);
				StartCoroutine(EndGame());
			}
		}
	}

	IEnumerator EndGame(){
		yield return new WaitForSeconds(3);
		Application.Quit();
		AppHelper.Quit();
		//Debug.Break();
	}

	public void ResetDisobeyTimer() {
		disobeyTimer = 0f;
	}

	private void Start() {
		dialogueManager = GetComponent<DialogueManager>();
	}

	public void TriggerDialogue(Dialogue dialogue) {
		dialogueManager.StartDialogue(dialogue);
	}

}

public static class AppHelper {
#if UNITY_WEBPLAYER
     public static string webplayerQuitURL = "http://google.com";
#endif
	public static void Quit() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
	}
}
