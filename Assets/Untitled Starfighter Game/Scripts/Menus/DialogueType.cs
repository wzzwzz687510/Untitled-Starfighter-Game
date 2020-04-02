using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueType : MonoBehaviour
{
  public TextMeshProUGUI textDisplay;
  public string[] sentence;
  private int index;
  public float typingSpeed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Type());
    }

    IEnumerator Type()
    {
      yield return new WaitForSeconds(3.75f);

      foreach(char letter in sentence[index].ToCharArray())
      {
        textDisplay.text += letter;
        yield return new WaitForSeconds(typingSpeed);
      }
    }
}
