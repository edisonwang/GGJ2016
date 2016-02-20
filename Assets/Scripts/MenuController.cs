﻿using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour
{

    public GameObject[] selections;
    int pointer;
    public GameObject selectionMask;
    public Sprite start;
    public Sprite resume;
    public Sprite restart;
    public Sprite Win;
    public Sprite Lose;
    public Sprite WinningTurn;
    
    public SpriteRenderer Title;
    public SpriteRenderer HowToPlay;
    private bool isShowing = true;
    
    public GameObject scoretext;
    public GameObject score;
    private bool gameOverAudioPlayed = false;
    public AudioClip winAudio;
    public AudioClip loseAudio;

    // Use this for initialization
    void Start()
    {
        GameController.instance.setMenuCOntroller(this);
        pointer = 0;
        selectionMask.transform.position = selections[pointer].transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.instance.isWin()){
            Title.sprite = Win;
            HowToPlay.sprite = WinningTurn;
            if (!gameOverAudioPlayed)
            {
                gameOverAudioPlayed = true;
                GetComponent<AudioSource>().PlayOneShot(winAudio);
            }
        }
        if(GameController.instance.isLose()){
            Title.sprite = Lose;
            if (!gameOverAudioPlayed)
            {
                gameOverAudioPlayed = true;
                GetComponent<AudioSource>().PlayOneShot(loseAudio);
            }
        }
        if (isShowing)
        {


            if (Input.GetKeyDown("left"))
            {
                if (pointer > 0)
                {
                    pointer--;
                    selectionMask.transform.position = selections[pointer].transform.position;

                }
                else
                {
                    pointer = selections.Length - 1;
                    selectionMask.transform.position = selections[pointer].transform.position;

                }
                Debug.Log("Pointer: " + pointer);
            }
            if (Input.GetKeyDown("right"))
            {
                if (pointer < selections.Length - 1)
                {
                    pointer++;
                    selectionMask.transform.position = selections[pointer].transform.position;

                }
                else
                {
                    pointer = 0;
                    selectionMask.transform.position = selections[pointer].transform.position;

                }
            }
            if (Input.GetKeyDown("space"))
            {
                switch (pointer)
                {
                    case 0:
                        if(GameController.instance.isStarted()){
                            Restart();
                            return;
                        }
                        PlayGame();
                        break;
                    case 1:
                        Application.Quit();
                        break;
                    default:
                        return;

                }
            }
            if (Input.GetKeyDown("escape"))
            {
                if (GameController.instance.isStarted())
                {
                    PlayGame();
                    //GameController.instance.restartGame();
                }
            }
        }
    }

    void PlayGame()
    {
        GameController.instance.setRunning(true);
        gameObject.SetActive(false);
        //changeToResume();
        changeToRestart();
    }
    
    public void changeToResume(){
        selections[0].GetComponent<SpriteRenderer>().sprite = resume;
    }
    public void changeToRestart(){
        selections[0].GetComponent<SpriteRenderer>().sprite = restart;
    }
    void Restart(){
        GameController.instance.restartGame();
    }
    
}
