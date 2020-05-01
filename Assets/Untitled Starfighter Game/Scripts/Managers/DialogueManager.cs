using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

[System.Serializable]
public class Dialogue
{
    public SVGImage characterPicture;
    public string name;
    public string text;
}

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Parameters")]
    [Range(0.1f, 1.0f)] public float animationSpeed = 1.0f;
    public float textSpeed = 0.25f;
    public float textDisplayDuration = 10;

    [Header("Dialogues")]
    public Dialogue dSS_Var1;
    public Dialogue dBB_Var1;
    public Dialogue dBB_Var2;
    public Dialogue dR3X_Var1;
    public Dialogue dR3X_Var2;
    public Dialogue dAW_Var1;
    public Dialogue dKE_Var1;
    public Dialogue dKE_Var2;
    public AudioSource source;

    [Header("Dialogue Animation [Character Picture Elements]")]
    public SVGImage characterPictureBackground;
    public SVGImage characterPictureForeground;
    [Header("Dialogue Animation [Character Name Elements]")]
    public SVGImage characterNameBackground;
    public TextMeshProUGUI characterName;
    [Header("Dialogue Animation [Character Notifier Elements]")]
    public SVGImage characterNotifierBackground;
    [Header("Dialogue Animation [Character Speech Elements]")]
    public SVGImage characterSpeechBackground;
    public TextMeshProUGUI characterSpeech;

    public Coroutine DialogueCoroutine { get; private set; }

    private Dialogue lastDialogue;

    private bool isAnim;
    private Coroutine curCoroutine;

    public IEnumerator DialogueTemplate(Dialogue dialogue)
    {
        if (isAnim) {
            yield return curCoroutine;
        }

        isAnim = true;

        if (lastDialogue!=null) {
            ClearDialogue();
        }
        lastDialogue = dialogue;

        characterName.DOText("", 0);
        characterSpeech.DOText("", 0);
        characterName.DOFade(1, 0);
        characterSpeech.DOFade(1, 0);

        Tween d_CharPicture_Tween = characterPictureBackground.DOFade(1, 1f * animationSpeed).SetAutoKill(false);
                                    dialogue.characterPicture.DOFade(1, 1f * animationSpeed).SetAutoKill(false);
                                    characterPictureForeground.DOFade(1, 1f * animationSpeed).SetAutoKill(false);
        yield return d_CharPicture_Tween.WaitForCompletion();

        Tween d_CharNameBackground_Tween = characterNameBackground.DOFade(1, 0.2f * animationSpeed).SetAutoKill(false);
        yield return d_CharNameBackground_Tween.WaitForCompletion();
        Tween d_CharName_Tween = characterName.DOText(dialogue.name, 0.2f * animationSpeed).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
        yield return d_CharName_Tween.WaitForCompletion();
        Tween d_CharSpeechBackground_Tween = characterSpeechBackground.DOFade(1, 0.5f * animationSpeed).SetAutoKill(false);
        yield return d_CharSpeechBackground_Tween.WaitForCompletion();
        float textFinalSpeed = dialogue.text.Split(' ').Length * animationSpeed * textSpeed;
        Tween d_CharSpeech_Tween = characterSpeech.DOText(dialogue.text, textFinalSpeed).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
        AudioManager.Instance.PlayBeepClip();
        Tween d_CharNotifier_Tween = characterNotifierBackground.DOFade(1, 0.5f * animationSpeed).SetLoops((int)(textDisplayDuration / 0.5f), LoopType.Yoyo).SetAutoKill(false);
        yield return new WaitForSeconds(textDisplayDuration);

        Tween d_CharEnd_Tween = characterPictureBackground.DOFade(0, 1f * animationSpeed).SetAutoKill(false);
                                dialogue.characterPicture.DOFade(0, 1f * animationSpeed).SetAutoKill(false);
                                characterPictureForeground.DOFade(0, 1f * animationSpeed).SetAutoKill(false);
                                characterNameBackground.DOFade(0, 1f * animationSpeed).SetAutoKill(false);
                                characterName.DOFade(0, 1f * animationSpeed).SetAutoKill(false);
                                characterNotifierBackground.DOFade(0, 1f * animationSpeed).SetAutoKill(false);
                                characterSpeechBackground.DOFade(0, 1f * animationSpeed).SetAutoKill(false);
                                characterSpeech.DOFade(0, 1f * animationSpeed).SetAutoKill(false);

        isAnim = false;
    }

    private void ClearDialogue()
    {
        characterPictureBackground.DOFade(0, 0).SetAutoKill(false);
        lastDialogue.characterPicture.DOFade(0, 0).SetAutoKill(false);
        characterPictureForeground.DOFade(0, 0).SetAutoKill(false);
        characterNameBackground.DOFade(0, 0).SetAutoKill(false);
        characterNotifierBackground.DOFade(0, 0).SetAutoKill(false);
        characterSpeechBackground.DOFade(0, 0).SetAutoKill(false);
    }
    //-------------SALVADOR SCRAPBEARD ANIMATIONS-------------
    public void Dialogue_SS_Var1()
    {
        curCoroutine = StartCoroutine(DialogueTemplate(dSS_Var1));
    }
    //-------------BETTY BLACKSCREW ANIMATIONS-------------
    public void Dialogue_BB_Var1()
    {
        curCoroutine = StartCoroutine(DialogueTemplate(dBB_Var1));
    }
    public void Dialogue_BB_Var2()
    {
        curCoroutine = StartCoroutine(DialogueTemplate(dBB_Var2));
    }
    //-------------R3X ANIMATIONS-------------
    public void Dialogue_R3X_Var1()
    {
        curCoroutine = StartCoroutine(DialogueTemplate(dR3X_Var1));
    }
    public void Dialogue_R3X_Var2()
    {
        curCoroutine = StartCoroutine(DialogueTemplate(dR3X_Var2));
    }
    //-------------ALFONSO WHIZBANG ANIMATIONS-------------
    public void Dialogue_AW_Var1()
    {
        curCoroutine = StartCoroutine(DialogueTemplate(dAW_Var1));
    }
    //-------------KRONK ERROR009 ANIMATIONS-------------
    public void Dialogue_KE_Var1()
    {
        curCoroutine = StartCoroutine(DialogueTemplate(dKE_Var1));
    }
    public void Dialogue_KE_Var2()
    {
        curCoroutine = StartCoroutine(DialogueTemplate(dKE_Var2));
    }
    //-------------CANNONBALL BOB ANIMATIONS-------------
    // public IEnumerator Dialogue_CB_Var1()
    // ﻿﻿﻿﻿﻿﻿﻿{
    // ﻿﻿﻿﻿﻿﻿﻿  Tween d_CharPicture_Tween = characterPictureBackground.DOFade(1, 1.5f).SetAutoKill(false);
    //                               characterPicture.DOFade(1, 1.5f).SetAutoKill(false);
    //                               characterPictureForeground.DOFade(1, 1.5f).SetAutoKill(false);
    // ﻿﻿﻿﻿﻿﻿﻿  yield return d_CharPicture_Tween.WaitForCompletion();
    //   Tween d_CharNameBackground_Tween = characterNameBackground.DOFade(1, 1.5f).SetAutoKill(false);
    //   yield return d_CharNameBackground_Tween.WaitForCompletion();
    //   Tween d_CharName_Tween = characterName.DOText("Cannonball Bob", 1f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
    //   yield return d_CharName_Tween.WaitForCompletion();
    //   Tween d_CharNotifier_Tween = characterNotifierBackground.DOFade(1, 0.5f).SetLoops(5, LoopType.Yoyo).SetAutoKill(false);
    //   yield return d_CharNotifier_Tween.WaitForCompletion();
    //   Tween d_CharSpeechBackground_Tween = characterSpeechBackground.DOFade(1, 1.5f).SetAutoKill(false);
    //   yield return d_CharSpeechBackground_Tween.WaitForCompletion();
    //   Tween d_CharSpeech_Tween = characterSpeech.DOText("*explosion*", 0.5f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
    //   yield return d_CharSpeech_Tween.WaitForCompletion();
    //   Tween d_CharReName_Tween = characterName.DOText("Kronk ERROR009", 1f).SetEase(Ease.Linear).SetDelay(2.5f).SetAutoKill(false);
    //   yield return d_CharReName_Tween.WaitForCompletion();
    //   Tween d_CharReSpeech_Tween = characterSpeech.DOText("Oh for God's sake Bob *Error* I'm gonna have to clean that up!", 3.75f).SetEase(Ease.Linear).SetAutoKill(false);
    // ﻿﻿﻿﻿﻿﻿﻿}
}
