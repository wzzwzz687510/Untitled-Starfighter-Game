using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialogueManager : MonoBehaviour
{
  [Header("Dialogue Animation [Character Picture Elements]")]
  public SVGImage characterPictureBackground;
  public SVGImage characterPictureSS;
  public SVGImage characterPictureAW;
  public SVGImage characterPictureBB;
  public SVGImage characterPictureR3X;
  public SVGImage characterPictureKE;
  public SVGImage characterPictureForeground;
  [Header("Dialogue Animation [Character Name Elements]")]
  public SVGImage characterNameBackground;
  public TextMeshProUGUI characterName;
  [Header("Dialogue Animation [Character Notifier Elements]")]
  public SVGImage characterNotifierBackground;
  [Header("Dialogue Animation [Character Speech Elements]")]
  public SVGImage characterSpeechBackground;
  public TextMeshProUGUI characterSpeech;

    void Start()
    {
      StartCoroutine(Dialogue_KE_Var2());
    }
//-------------SALVADOR SCRAPBEARD ANIMATIONS-------------
    public IEnumerator Dialogue_SS_Var1()
    ﻿﻿﻿﻿﻿﻿﻿{
    ﻿﻿﻿﻿﻿﻿﻿  Tween d_CharPicture_Tween = characterPictureBackground.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureSS.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureForeground.DOFade(1, 1.5f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿  yield return d_CharPicture_Tween.WaitForCompletion();
      Tween d_CharNameBackground_Tween = characterNameBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharNameBackground_Tween.WaitForCompletion();
      Tween d_CharName_Tween = characterName.DOText("Salvador Scrapbeard", 1.5f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return d_CharName_Tween.WaitForCompletion();
      Tween d_CharNotifier_Tween = characterNotifierBackground.DOFade(1, 0.5f).SetLoops(5, LoopType.Yoyo).SetAutoKill(false);
      yield return d_CharNotifier_Tween.WaitForCompletion();
      Tween d_CharSpeechBackground_Tween = characterSpeechBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharSpeechBackground_Tween.WaitForCompletion();
      Tween d_CharSpeech_Tween = characterSpeech.DOText("There she is... the flagship, let's take her down Iron-Shanks and get our plunder!", 4f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return new WaitForSeconds(10);
      Tween d_CharEnd_Tween = characterPictureBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureSS.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureForeground.DOFade(0, 1f).SetAutoKill(false);
                              characterNameBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterName.DOFade(0, 1f).SetAutoKill(false);
                              characterNotifierBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeechBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeech.DOFade(0, 1f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿}
//-------------BETTY BLACKSCREW ANIMATIONS-------------
    public IEnumerator Dialogue_BB_Var1()
    ﻿﻿﻿﻿﻿﻿﻿{
    ﻿﻿﻿﻿﻿﻿﻿  Tween d_CharPicture_Tween = characterPictureBackground.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureBB.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureForeground.DOFade(1, 1.5f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿  yield return d_CharPicture_Tween.WaitForCompletion();
      Tween d_CharNameBackground_Tween = characterNameBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharNameBackground_Tween.WaitForCompletion();
      Tween d_CharName_Tween = characterName.DOText("Betty Blackscrew", 1f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return d_CharName_Tween.WaitForCompletion();
      Tween d_CharNotifier_Tween = characterNotifierBackground.DOFade(1, 0.5f).SetLoops(5, LoopType.Yoyo).SetAutoKill(false);
      yield return d_CharNotifier_Tween.WaitForCompletion();
      Tween d_CharSpeechBackground_Tween = characterSpeechBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharSpeechBackground_Tween.WaitForCompletion();
      Tween d_CharSpeech_Tween = characterSpeech.DOText("Performing evasive maneuvers!", 2f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return new WaitForSeconds(10);
      Tween d_CharEnd_Tween = characterPictureBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureBB.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureForeground.DOFade(0, 1f).SetAutoKill(false);
                              characterNameBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterName.DOFade(0, 1f).SetAutoKill(false);
                              characterNotifierBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeechBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeech.DOFade(0, 1f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿}
    public IEnumerator Dialogue_BB_Var2()
    ﻿﻿﻿﻿﻿﻿﻿{
    ﻿﻿﻿﻿﻿﻿﻿  Tween d_CharPicture_Tween = characterPictureBackground.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureBB.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureForeground.DOFade(1, 1.5f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿  yield return d_CharPicture_Tween.WaitForCompletion();
      Tween d_CharNameBackground_Tween = characterNameBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharNameBackground_Tween.WaitForCompletion();
      Tween d_CharName_Tween = characterName.DOText("Betty Blackscrew", 1f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return d_CharName_Tween.WaitForCompletion();
      Tween d_CharNotifier_Tween = characterNotifierBackground.DOFade(1, 0.5f).SetLoops(5, LoopType.Yoyo).SetAutoKill(false);
      yield return d_CharNotifier_Tween.WaitForCompletion();
      Tween d_CharSpeechBackground_Tween = characterSpeechBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharSpeechBackground_Tween.WaitForCompletion();
      Tween d_CharSpeech_Tween = characterSpeech.DOText("Woooo! Barrel roll!", 1.25f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return new WaitForSeconds(10);
      Tween d_CharEnd_Tween = characterPictureBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureBB.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureForeground.DOFade(0, 1f).SetAutoKill(false);
                              characterNameBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterName.DOFade(0, 1f).SetAutoKill(false);
                              characterNotifierBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeechBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeech.DOFade(0, 1f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿}
//-------------R3X ANIMATIONS-------------
    public IEnumerator Dialogue_R3X_Var1()
    ﻿﻿﻿﻿﻿﻿﻿{
    ﻿﻿﻿﻿﻿﻿﻿  Tween d_CharPicture_Tween = characterPictureBackground.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureR3X.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureForeground.DOFade(1, 1.5f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿  yield return d_CharPicture_Tween.WaitForCompletion();
      Tween d_CharNameBackground_Tween = characterNameBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharNameBackground_Tween.WaitForCompletion();
      Tween d_CharName_Tween = characterName.DOText("R3X", 0.5f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return d_CharName_Tween.WaitForCompletion();
      Tween d_CharNotifier_Tween = characterNotifierBackground.DOFade(1, 0.5f).SetLoops(5, LoopType.Yoyo).SetAutoKill(false);
      yield return d_CharNotifier_Tween.WaitForCompletion();
      Tween d_CharSpeechBackground_Tween = characterSpeechBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharSpeechBackground_Tween.WaitForCompletion();
      Tween d_CharSpeech_Tween = characterSpeech.DOText("Turrets down boss!", 1f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return new WaitForSeconds(10);
      Tween d_CharEnd_Tween = characterPictureBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureR3X.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureForeground.DOFade(0, 1f).SetAutoKill(false);
                              characterNameBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterName.DOFade(0, 1f).SetAutoKill(false);
                              characterNotifierBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeechBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeech.DOFade(0, 1f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿}
    public IEnumerator Dialogue_R3X_Var2()
    ﻿﻿﻿﻿﻿﻿﻿{
    ﻿﻿﻿﻿﻿﻿﻿  Tween d_CharPicture_Tween = characterPictureBackground.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureR3X.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureForeground.DOFade(1, 1.5f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿  yield return d_CharPicture_Tween.WaitForCompletion();
      Tween d_CharNameBackground_Tween = characterNameBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharNameBackground_Tween.WaitForCompletion();
      Tween d_CharName_Tween = characterName.DOText("R3X", 0.4f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return d_CharName_Tween.WaitForCompletion();
      Tween d_CharNotifier_Tween = characterNotifierBackground.DOFade(1, 0.5f).SetLoops(5, LoopType.Yoyo).SetAutoKill(false);
      yield return d_CharNotifier_Tween.WaitForCompletion();
      Tween d_CharSpeechBackground_Tween = characterSpeechBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharSpeechBackground_Tween.WaitForCompletion();
      Tween d_CharSpeech_Tween = characterSpeech.DOText("Boom! Target down!", 1f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return new WaitForSeconds(10);
      Tween d_CharEnd_Tween = characterPictureBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureR3X.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureForeground.DOFade(0, 1f).SetAutoKill(false);
                              characterNameBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterName.DOFade(0, 1f).SetAutoKill(false);
                              characterNotifierBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeechBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeech.DOFade(0, 1f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿}
//-------------ALFONSO WHIZBANG ANIMATIONS-------------
    public IEnumerator Dialogue_AW_Var1()
    ﻿﻿﻿﻿﻿﻿﻿{
    ﻿﻿﻿﻿﻿﻿﻿  Tween d_CharPicture_Tween = characterPictureBackground.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureAW.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureForeground.DOFade(1, 1.5f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿  yield return d_CharPicture_Tween.WaitForCompletion();
      Tween d_CharNameBackground_Tween = characterNameBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharNameBackground_Tween.WaitForCompletion();
      Tween d_CharName_Tween = characterName.DOText("Alfonso Whizbang", 1f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return d_CharName_Tween.WaitForCompletion();
      Tween d_CharNotifier_Tween = characterNotifierBackground.DOFade(1, 0.5f).SetLoops(5, LoopType.Yoyo).SetAutoKill(false);
      yield return d_CharNotifier_Tween.WaitForCompletion();
      Tween d_CharSpeechBackground_Tween = characterSpeechBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharSpeechBackground_Tween.WaitForCompletion();
      Tween d_CharSpeech_Tween = characterSpeech.DOText("Repairs incoming!", 1f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return new WaitForSeconds(10);
      Tween d_CharEnd_Tween = characterPictureBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureAW.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureForeground.DOFade(0, 1f).SetAutoKill(false);
                              characterNameBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterName.DOFade(0, 1f).SetAutoKill(false);
                              characterNotifierBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeechBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeech.DOFade(0, 1f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿}
//-------------KRONK ERROR009 ANIMATIONS-------------
    public IEnumerator Dialogue_KE_Var1()
    ﻿﻿﻿﻿﻿﻿﻿{
    ﻿﻿﻿﻿﻿﻿﻿  Tween d_CharPicture_Tween = characterPictureBackground.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureKE.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureForeground.DOFade(1, 1.5f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿  yield return d_CharPicture_Tween.WaitForCompletion();
      Tween d_CharNameBackground_Tween = characterNameBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharNameBackground_Tween.WaitForCompletion();
      Tween d_CharName_Tween = characterName.DOText("Kronk ERROR009", 1f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return d_CharName_Tween.WaitForCompletion();
      Tween d_CharNotifier_Tween = characterNotifierBackground.DOFade(1, 0.5f).SetLoops(5, LoopType.Yoyo).SetAutoKill(false);
      yield return d_CharNotifier_Tween.WaitForCompletion();
      Tween d_CharSpeechBackground_Tween = characterSpeechBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharSpeechBackground_Tween.WaitForCompletion();
      Tween d_CharSpeech_Tween = characterSpeech.DOText("We've suffered critical *Error* damage captain", 3.5f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return new WaitForSeconds(10);
      Tween d_CharEnd_Tween = characterPictureBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureKE.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureForeground.DOFade(0, 1f).SetAutoKill(false);
                              characterNameBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterName.DOFade(0, 1f).SetAutoKill(false);
                              characterNotifierBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeechBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeech.DOFade(0, 1f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿}
    public IEnumerator Dialogue_KE_Var2()
    ﻿﻿﻿﻿﻿﻿﻿{
    ﻿﻿﻿﻿﻿﻿﻿  Tween d_CharPicture_Tween = characterPictureBackground.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureKE.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureForeground.DOFade(1, 1.5f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿  yield return d_CharPicture_Tween.WaitForCompletion();
      Tween d_CharNameBackground_Tween = characterNameBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharNameBackground_Tween.WaitForCompletion();
      Tween d_CharName_Tween = characterName.DOText("Kronk ERROR009", 1f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return d_CharName_Tween.WaitForCompletion();
      Tween d_CharNotifier_Tween = characterNotifierBackground.DOFade(1, 0.5f).SetLoops(5, LoopType.Yoyo).SetAutoKill(false);
      yield return d_CharNotifier_Tween.WaitForCompletion();
      Tween d_CharSpeechBackground_Tween = characterSpeechBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharSpeechBackground_Tween.WaitForCompletion();
      Tween d_CharSpeech_Tween = characterSpeech.DOText("New upgrades are available captain!", 2.25f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return new WaitForSeconds(10);
      Tween d_CharEnd_Tween = characterPictureBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureKE.DOFade(0, 1f).SetAutoKill(false);
                              characterPictureForeground.DOFade(0, 1f).SetAutoKill(false);
                              characterNameBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterName.DOFade(0, 1f).SetAutoKill(false);
                              characterNotifierBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeechBackground.DOFade(0, 1f).SetAutoKill(false);
                              characterSpeech.DOFade(0, 1f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿}
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
