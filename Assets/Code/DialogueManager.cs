using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

	private Queue<string> sentences;

	private float displayNextTimer = 0;
	private float displayNextTime = 3.0f;

	public Text dialogueText;
	public RectTransform textStartPosition;
	public RectTransform textPosition;
	public AudioManager audioManager;

	DialogueTrigger dialogueTrigger;

    // Start is called before the first frame update
    void Start()
    {
		sentences = new Queue<string>();
		dialogueTrigger = GetComponent<DialogueTrigger>();
    }

	private void Update() {

		if(sentences.Count > 0) {
			displayNextTimer += Time.deltaTime;
			if (displayNextTimer > displayNextTime) {
				displayNextTimer = 0f;
				DisplayNextSentence();
			}
		}
	}

	public void StartDialogue(Dialogue dialogue) {
		Debug.Log("Started conversionation: " + dialogue.name);

		sentences.Clear();
		textPosition.position = textStartPosition.position;

		foreach(string sentence in dialogue.sentences) {
			sentences.Enqueue(sentence);
		}

		DisplayNextSentence();
		displayNextTimer = 0f;
	}

	public void DisplayNextSentence() {

		dialogueTrigger.ResetDisobeyTimer();

		if(sentences.Count == 0) {
			EndDialogue();
		}

		string sentence = sentences.Dequeue();
		StopAllCoroutines();
		StartCoroutine(TypeSentence(sentence));
	}

	IEnumerator TypeSentence(string sentence) {
		dialogueText.text = "";
		textPosition.position += new Vector3(0, -1.5f, 0);
		foreach (char letter in sentence.ToCharArray()) {
			dialogueText.text += letter;
			audioManager.Play("Letter");
			yield return new WaitForSeconds((1.0f / 60.0f) * 5);
		}
	}

	public void EndDialogue() {
		Debug.Log("End of conversation");
	}
}
