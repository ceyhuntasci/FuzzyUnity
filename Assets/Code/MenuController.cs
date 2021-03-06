﻿//.     .       .  .   . .   .   . .    +  .
//  .     .  :     .    .. :. .___---------___.
//       .  .   .    .  :.:. _".^ .^ ^.  '.. :"-_. .
//    .  :       .  .  .:../:            . .^  :.:\.
//        .   . :: +. :.:/: .   .    .        . . .:\
// .  :    .     . _ :::/:               .  ^ .  . .:\
//  .. . .   . - : :.:./.                        .  .:\
//  .      .     . :..|:                    .  .  ^. .:|
//    .       . : : ..||        .                . . !:|
//  .     . . . ::. ::\(                           . :)/
// .   .     : . : .:.|. ######              .#######::|
//  :.. .  :-  : .:  ::|.#######           ..########:|
// .  .  .  ..  .  .. :\ ########          :######## :/
//  .        .+ :: : -.:\ ########       . ########.:/
//    .  .+   . . . . :.:\. #######       #######..:/
//     :: . . . . ::.:..:.\           .   .   ..:/
//  .   .   .  .. :  -::::.\.       | |     . .:/
//     .  :  .  .  .-:.":.::.\             ..:/
// .      -.   . . . .: .:::.:.\.           .:/
//.   .   .  :      : ....::_:..:\   ___.  :/
//   .   .  .   .:. .. .  .: :.:.:\       :/
//     +   .   .   : . ::. :.:. .:.|\  .:/|
//     .         +   .  .  ...:: ..|  --.:|
//.      . . .   .  .  . ... :..:.."(  ..)"
// .   .       .      :  .   .: ::/  .  .::\


//      __       ___  ___  ___  ___      ___       ___      ___       __        ______    
//     /""\     |"  \/"  ||"  \/"  |    |"  |     |"  \    /"  |     /""\      /    " \   
//    /    \     \   \  /  \   \  /     ||  |      \   \  //   |    /    \    // ____  \  
//   /' /\  \     \\  \/    \\  \/      |:  |      /\\  \/.    |   /' /\  \  /  /    ) :) 
//  //  __'  \    /   /     /   /        \  |___  |: \.        |  //  __'  \(: (____/ //  
// /   /  \\  \  /   /     /   /        ( \_|:  \ |.  \    /:  | /   /  \\  \\        /   
//(___/    \___)|___/     |___/          \_______)|___|\__/|___|(___/    \___)\"_____/    
//--------------------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.Collections.Generic;

//--------------------------------------------------------------------------------
#if UNITY_EDITOR
using UnityEditor;
using GooglePlayGames;
#elif UNITY_IOS
using UnityEngine.SocialPlatforms;
#elif UNITY_ANDROID
using GooglePlayGames;
#endif

//--------------------------------------------------------------------------------
public class MenuController : MonoBehaviour
{
    //--------------------------------------------------------------------------------
    public int m_iScore;
    public Text ScoreText;
    public Text BestScoreText;
    public Text CurrentScoreText;

    public Button muteButton;
    public Sprite mute1;
    public Sprite mute2;

    AudioSource audio;
    bool muted;
    private bool outOfLoop = false;
    //--------------------------------------------------------------------------------
    public GameLogic m_GameLogic;
    public AchievementHandler m_AchvHandler;
    //--------------------------------------------------------------------------------
    bool achievementsProcessed;
    int gamesPlayed;
    public void Start()
    {
#if UNITY_ANDROID 
        PlayGamesPlatform.Activate(); 
#endif
        Social.localUser.Authenticate((bool success) =>
        {

        });
        muted = false;
        audio = GetComponent<AudioSource>();
        achievementsProcessed = false;

        if (PlayerPrefs.GetInt("Mute", 0) == 1)
        {
            ToogleMute();
        }

        m_iScore = 0;


        gamesPlayed = PlayerPrefs.GetInt("GamesPlayed", 0);


       
        BestScoreText.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        print(gamesPlayed.ToString());
    }
    //--------------------------------------------------------------------------------
    void Update()
    {

        if (GameLogic.State == GameLogic.GameState.Ended && !outOfLoop)
        {
            audio.Play();

            gamesPlayed = PlayerPrefs.GetInt("GamesPlayed", 0);
            gamesPlayed += 1;
            PlayerPrefs.SetInt("GamesPlayed", gamesPlayed);

            outOfLoop = true;
            HandleAchievements();

            int wth = PlayerPrefs.GetInt("WTH", 0);


            //-------------------------------------
            if (wth == 0)
            {
#if UNITY_ANDROID
                Social.ReportProgress("CgkIzs-alcMYEAIQDQ", 100.0f, (bool success) =>
                       {
                           if (success)
                           {
                               PlayerPrefs.SetInt("WTH", 1);
                           }
                       }); 
#endif
            }
        }

    }
    //--------------------------------------------------------------------------------
    public void RestartLevel()
    {
        m_GameLogic.Restart();
        outOfLoop = false;
    }
    //--------------------------------------------------------------------------------
    public void UpdateScoreboard(int iScore)
    {
        m_iScore = iScore;
        CurrentScoreText.text = m_iScore.ToString();
        ScoreText.text = CurrentScoreText.text;
        if (m_iScore > System.Convert.ToInt32(BestScoreText.text))
        {
            PlayerPrefs.SetInt("BestScore", m_iScore);
            BestScoreText.text = PlayerPrefs.GetInt("BestScore").ToString();

        }

        HandleAchievements();
    }
    public void HandleAchievements()
    {
        //------------------------------------
        if (m_iScore > 9 && m_iScore <= 24)
        {
            m_AchvHandler.Process(m_AchvHandler.Achievements.Find(x => x.Name == "Oh! I got this - Highscore 10"));
        }
        if (m_iScore > 24 && m_iScore <= 74)
        {
            m_AchvHandler.Process(m_AchvHandler.Achievements.Find(x => x.Name == "Getting the hang of it"));
        }
        if (m_iScore > 74 && m_iScore <= 149)
        {
            m_AchvHandler.Process(m_AchvHandler.Achievements.Find(x => x.Name == "Ambitious"));
        }
        if (m_iScore > 149 && m_iScore <= 249)
        {
            m_AchvHandler.Process(m_AchvHandler.Achievements.Find(x => x.Name == "Tier 0"));
        }
        if (m_iScore > 249 && m_iScore <= 999)
        {
            m_AchvHandler.Process(m_AchvHandler.Achievements.Find(x => x.Name == "Superior Kind"));
        }
        if (m_iScore > 999)
        {
            m_AchvHandler.Process(m_AchvHandler.Achievements.Find(x => x.Name == "Freak"));
        }

        //---------------------------------------

        //---------------------------------------
        if (gamesPlayed > 4 && gamesPlayed < 20)
        {
            m_AchvHandler.Process(m_AchvHandler.Achievements.Find(x => x.Name == "Newcomer"));
        }
        if (gamesPlayed > 19 && gamesPlayed < 50)
        {
            m_AchvHandler.Process(m_AchvHandler.Achievements.Find(x => x.Name == "Rookie"));
        }
        if (gamesPlayed > 49 && gamesPlayed < 100)
        {
            m_AchvHandler.Process(m_AchvHandler.Achievements.Find(x => x.Name == "Regular"));
        }
        if (gamesPlayed > 99 && gamesPlayed < 250)
        {
            m_AchvHandler.Process(m_AchvHandler.Achievements.Find(x => x.Name == "Citizen"));
        }
        if (gamesPlayed > 249)
        {
            m_AchvHandler.Process(m_AchvHandler.Achievements.Find(x => x.Name == "Leecher"));
        }
    }
    public void ToogleMute()
    {

        audio.volume = muted ? 100 : 0;
        muteButton.image.sprite = muted ? mute1 : mute2;
        if (!muted)
        {
            PlayerPrefs.SetInt("Mute", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Mute", 0);
        }

        muted = !muted;



    }
    //--------------------------------------------------------------------------------

    public void Rate()
    {
#if UNITY_ANDROID
        Application.OpenURL("market://details?id=com.gramgames.Form8");
#elif UNITY_IPHONE
        Application.OpenURL("itms-apps://itunes.apple.com/app/idYOUR_ID");
#endif
    }
}
