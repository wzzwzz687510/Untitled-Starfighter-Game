using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueType : MonoBehaviour
{
  public TextMeshProUGUI textDisplay;
  public string[] dialogueLines;
  private int index;
  public float typingSpeed;

    void Start()
    {
      StartCoroutine(LetterType());
    }

    IEnumerator LetterType()
    {
      yield return new WaitForSeconds(3.75f); //Waiting for dialogue anim to finish.

      foreach(char letter in dialogueLines[index].ToCharArray())
      {
        textDisplay.text += letter;
        yield return new WaitForSeconds(typingSpeed);
      }
    }
}
