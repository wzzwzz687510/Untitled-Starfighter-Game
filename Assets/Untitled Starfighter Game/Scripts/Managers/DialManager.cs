using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class DialManager : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
      StartCoroutine(Dialogue_SS_Var1());
    }

    public IEnumerator Dialogue_SS_Var1()
    ﻿﻿﻿﻿﻿﻿﻿{
    ﻿﻿﻿﻿﻿﻿﻿  Tween d_CharPicture_Tween = characterPictureBackground.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureSS.DOFade(1, 1.5f).SetAutoKill(false);
                                  characterPictureForeground.DOFade(1, 1.5f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿  yield return d_CharPicture_Tween.WaitForCompletion();
      Tween d_CharNameBackground_Tween = characterNameBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharNameBackground_Tween.WaitForCompletion();
      Tween d_CharName_Tween = characterName.DOText("Salvador Scrapbeard", 1f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return d_CharName_Tween.WaitForCompletion();
      Tween d_CharNotifier_Tween = characterNotifierBackground.DOFade(1, 0.5f).SetLoops(5, LoopType.Yoyo).SetAutoKill(false);
      yield return d_CharNotifier_Tween.WaitForCompletion();
      Tween d_CharSpeechBackground_Tween = characterSpeechBackground.DOFade(1, 1.5f).SetAutoKill(false);
      yield return d_CharSpeechBackground_Tween.WaitForCompletion();
      Tween d_CharSpeech_Tween = characterSpeech.DOText("There she is... the flagship, let's take her down Iron-Shanks and get our plunder!", 4f).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
      yield return d_CharSpeech_Tween.WaitForCompletion();
      Tween d_CharEnd_Tween = characterPictureBackground.DOFade(0, 1.5f).SetAutoKill(false);
                              characterPictureSS.DOFade(0, 1.5f).SetAutoKill(false);
                              characterPictureForeground.DOFade(0, 1.5f).SetAutoKill(false);
                              characterNotifierBackground.DOFade(0, 1.5f).SetAutoKill(false);
    ﻿﻿﻿﻿﻿﻿﻿}
}
