using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControl : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    public List<Image> playerInfoBG = new List<Image>();
    public Material mater;
    public int playerIndx = 0;
    public int steps = 0;
    public float speed = 0;
    public GameObject cubeLogic, winCard;
    public int playerCount = 0;
    public bool stepsAgain = false, shuflled = false;
    public TextMeshProUGUI winText;
    
    public static int numberPlayer = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<numberPlayer;i++) {
            players[i].active = true;
        }
        //playerCount = numberPlayer;

        for (int i=0; i<players.Count; i++) {
            if (players[i].active) playerCount++;
        }
        playerCount = numberPlayer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int countAlivePlayers = 0;
        int number = 0;
        for (int i=0; i<players.Count;i++) {
            if (players[i] != null) {
                if (players[i].active == true) { number = players[i].GetComponent<PlayerScript>().playerNumber; countAlivePlayers++; }
            }
        }

        if (countAlivePlayers == 1) {
            winCard.SetActive(true);
            winText.text = ("Player " + (number+1).ToString() + " Win");
            cubeLogic.GetComponent<CubeS>().canShuffle = false;
        }

        GameObject curPlayer = players[playerIndx];
        if (curPlayer != null) {
            if (shuflled == true) {
                curPlayer.GetComponent<PlayerScript>().cubeNumber = steps;
            }

            if (curPlayer.GetComponent<PlayerScript>().inJail == true && shuflled == true) {
                for (int i = 0; i < playerInfoBG.Count; i++) {
                    if (i != playerIndx) playerInfoBG[i].material = null;
                }

                playerInfoBG[playerIndx].material = mater;
                if (stepsAgain == false) {
                    //goToAnotherPlayer();
                    /*playerIndx--;
                    if (playerIndx < 0) {
                        playerIndx = playerCount;
                        while (players[playerIndx] == null) playerIndx++;
                    }*/
                    steps = 0;
                } else {
                    curPlayer.GetComponent<PlayerScript>().inJail = false;
                    stepsAgain = false;
                }
            }

            shuflled = false;
        } else steps = 0;
        if (steps>0) {
            if (curPlayer == null) steps = 0;
            else {
                Vector2 dir = curPlayer.GetComponent<PlayerScript>().dir;
                curPlayer.transform.Translate(dir * speed);
                cubeLogic.GetComponent<CubeS>().canShuffle = false;
                curPlayer.GetComponent<PlayerScript>().Move = true;



                for (int i=0; i<playerInfoBG.Count;i++) {
                    if (i!=playerIndx) playerInfoBG[i].material = null;
                }

                /*int prefIndex = playerIndx - 1;
                if (prefIndex < 0) prefIndex = playerCount - 1;
                if (prefIndex < 0 || prefIndex >= playerCount) ;
                else playerInfoBG[prefIndex].material = null;*/
                playerInfoBG[playerIndx].material = mater;
            }
        } else {
            if (curPlayer != null && curPlayer.GetComponent<PlayerScript>().moveToJail == false) {
                if (curPlayer.GetComponent<PlayerScript>().haveToPay == false) {
                    if (curPlayer != null) {
                        curPlayer.GetComponent<PlayerScript>().Move = false;
                        curPlayer.GetComponent<PlayerScript>().paid = false;
                        curPlayer.GetComponent<PlayerScript>().haveToPay = false;
                    }
                    cubeLogic.GetComponent<CubeS>().canShuffle = true;
                }
            }
        }
    }

    public void goToAnotherPlayer() {
        if (players[playerIndx].GetComponent<PlayerScript>().inJail==true) {
            stepsAgain = false;
        }
        if (stepsAgain == false) {
            steps = -1;
            GameObject curPlayer = players[playerIndx];
            if (curPlayer != null) {
                curPlayer.GetComponent<PlayerScript>().deleteFilialInfo();
            }
            playerIndx++;
            playerIndx %= playerCount;
            while (players[playerIndx] == null) {
                playerIndx++;
                playerIndx %= playerCount;
               
            }
        }
    }


}
