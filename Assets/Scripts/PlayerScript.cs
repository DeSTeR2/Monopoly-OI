using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public GameObject player, controller;
    public List<GameObject> filials = new List<GameObject>();
    public Vector2 dir;
    public int playerNumber = 0, dependsOnCubeFilials = 0, cubeNumber = 0, money = 0;
    public bool paid = false, inJail = false, haveToPay = false, moveToJail = false, Move = false;
    public GameObject curFilial=null;
    public TextMeshProUGUI addMoneyText;

    private int prefMoney = 0;
    private float timer = 0, addMoneyTimer = 0;
    private bool addMoneyTextBool = false;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sprt = this.GetComponent<SpriteRenderer>();
        sprt.sortingOrder = 11;
        dir = new Vector2(-2, 0);
        prefMoney = money;
    }

    // Update is called once per frame
    void Update()
    {
        DeletePersone();
        if (player != null) {
            if (haveToPay == true) {
                GameObject filial = curFilial;
                if (filial.tag == "Untagged" || filial.tag == "Station" || filial.tag == "DependsOnCube") {

                    if (controller.GetComponent<PlayerControl>().steps == 0) {
                        //Debug.Log(filial.GetComponent<FilialLogic>().Owner);
                        if (filial.GetComponent<FilialLogic>().Owner != null && filial.GetComponent<FilialLogic>().Owner != player) {
                            //Debug.Log(1);
                            haveToPay = true;

                            if (filial.tag == "DependsOnCube") {
                                if (filial.GetComponent<FilialLogic>().Owner != null) {
                                    haveToPay = true;
                                    int dependsOnCube1 = filial.GetComponent<FilialLogic>().Owner.GetComponent<PlayerScript>().dependsOnCubeFilials;
                                    if (money >= cubeNumber * 25 * dependsOnCube1) {
                                        filial.GetComponent<FilialLogic>().Owner.GetComponent<PlayerScript>().money += cubeNumber * 25 * dependsOnCube1;
                                        money -= cubeNumber * 25 * dependsOnCube1;
                                        paid = true;
                                        haveToPay = false;
                                    }
                                }
                            } else
                            if (money >= filial.GetComponent<FilialLogic>().priceToPay) {
                                filial.GetComponent<FilialLogic>().Owner.GetComponent<PlayerScript>().money += filial.GetComponent<FilialLogic>().priceToPay;
                                money -= filial.GetComponent<FilialLogic>().priceToPay;
                                paid = true;
                                haveToPay = false;
                            }
                        }
                    }
                }
            }

            if (moveToJail == true) {
                timer += Time.deltaTime;
                if (timer > 1.5) {
                    controller.GetComponent<PlayerControl>().steps = 20;
                    timer = 0;
                    moveToJail = false;
                }
            }

            if (money != prefMoney) {
                addMoneyTextBool = true;
            }

            if (addMoneyTextBool) {
                addMoneyTimer += Time.deltaTime;

                int addMoney = money - prefMoney;
                if (addMoney != 0) {
                    addMoneyText.enabled = true;
                    addMoneyText.text = (addMoney).ToString();

                    if (addMoney > 0) {
                        string addMoneyString = '+' + (addMoney).ToString();
                        addMoneyText.text = addMoneyString;
                        addMoneyText.color = Color.green;
                    } else addMoneyText.color = Color.red;
                }
            } else {
                addMoneyText.enabled = false;
            }

            if (addMoneyTimer > 4) {
                addMoneyTextBool = false;
                addMoneyTimer = 0;
            }
            prefMoney = money;
        }
    }

    public int getMoney() {
        return money;
    }


    public void ClickedToSkip() {
        curFilial.GetComponent<FilialLogic>().hideBUTTONS();
        curFilial.GetComponent<FilialLogic>().hideIU();
        curFilial = null;
    }
    /* 1b 2a 3b 4a 5b*/
    public void deleteFilialInfo() {
        if (curFilial != null)
        if (curFilial.tag == "Untagged") {
            GameObject filial = curFilial.gameObject;
            filial.GetComponent<FilialLogic>().hideBUTTONS();
            filial.GetComponent<FilialLogic>().hideIU();
            curFilial = null;
        }
    }

    private void DeletePersone() {
        if (player != null && money < 0) {
            if (filials.Count > 0) {
                for (int i=0; i<filials.Count;i++) {
                    GameObject fil = filials[i];
                    fil.GetComponent<FilialLogic>().Owner = null;
                    filials.Remove(fil);
                }
            }
            Destroy(player);
            controller.GetComponent<PlayerControl>().cubeLogic.GetComponent<CubeS>().canShuffle = true;
            controller.GetComponent<PlayerControl>().goToAnotherPlayer();
        }
    }

    public void DeletePlayerButton() {
        money = -1;
        DeletePersone();
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject filial = collider.gameObject;

        controller.GetComponent<PlayerControl>().steps--;

        if (filial.tag == "Finish") {
            dir = new Vector2(-2, 0);
            money += 200;
            if (controller.GetComponent<PlayerControl>().steps == 0) money += 200;
        } else if (filial.tag == "Jail") {
            if (controller.GetComponent<PlayerControl>().steps == 0)
                inJail = true;

            dir = new Vector2(0, 2);
        } else if (filial.tag == "Park") {
            dir = new Vector2(2, 0);
        } else if (filial.tag == "GoJail") {
            dir = new Vector2(0, -2);
            if (controller.GetComponent<PlayerControl>().steps == 0) {
                moveToJail = true;
            }
        } else if (filial.tag == "Tax" && (controller.GetComponent<PlayerControl>().steps == 0)) {
            money -= filial.GetComponent<TaxScript>().taxAmound;
        } else if (filial.tag == "Chanse") {
            if (controller.GetComponent<PlayerControl>().steps == 0) {
                controller.GetComponent<ChanseScript>().doActions(player);
            }
        } else if (filial.tag == "Untagged" || filial.tag == "Station" || filial.tag == "DependsOnCube") {

            if (controller.GetComponent<PlayerControl>().steps == 0) {
                //Debug.Log(filial.GetComponent<FilialLogic>().Owner);
                if (filial.GetComponent<FilialLogic>().Owner != null && filial.GetComponent<FilialLogic>().Owner != player) {
                    //Debug.Log(1);
                    haveToPay = true;

                    if (filial.tag == "DependsOnCube") {
                        if (filial.GetComponent<FilialLogic>().Owner != null) {
                            haveToPay = true;
                            int dependsOnCube1 = filial.GetComponent<FilialLogic>().Owner.GetComponent<PlayerScript>().dependsOnCubeFilials;
                            if (money >= cubeNumber * 25 * dependsOnCube1) {
                                filial.GetComponent<FilialLogic>().Owner.GetComponent<PlayerScript>().money += cubeNumber * 25 * dependsOnCube1;
                                money -= cubeNumber * 25 * dependsOnCube1;
                                paid = true;
                                haveToPay = false;
                            }
                        }
                    } else
                    if (money >= filial.GetComponent<FilialLogic>().priceToPay) {
                        filial.GetComponent<FilialLogic>().Owner.GetComponent<PlayerScript>().money += filial.GetComponent<FilialLogic>().priceToPay;
                        money -= filial.GetComponent<FilialLogic>().priceToPay;
                        paid = true;
                        haveToPay = false;
                    }
                }

                if (filial.GetComponent<FilialLogic>().Owner == player) {
                    filial.GetComponent<FilialLogic>().showUpdateCanva();

                }

                filial.GetComponent<FilialLogic>().playerStay = player;
                curFilial = filial;
                filial.GetComponent<FilialLogic>().buyLogic();

                int stationCount = 0, dependsOnCube = 0;
                for (int i = 0; i < filials.Count; i++) {
                    if (filials[i].tag == "Station") {
                        stationCount++;
                    }

                    if (filials[i].tag == "DependsOnCube") {
                        dependsOnCube++;
                    }
                }

                dependsOnCubeFilials = dependsOnCube;

                for (int i = 0; i < filials.Count; i++) {
                    if (filials[i].tag == "Station") {
                        filials[i].GetComponent<FilialLogic>().numberOfUpdates = stationCount - 1;
                        filials[i].GetComponent<FilialLogic>().updatePrices();
                    }
                }
            }



        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        GameObject filial = collision.gameObject;
        

    }

    private void OnTriggerExit2D(Collider2D collision) {

        if (collision.tag == "Untagged" || collision.tag == "Station" || collision.tag == "DependsOnCube") {
            GameObject filial = collision.gameObject;
            filial.GetComponent<FilialLogic>().playerStay = null;
            filial.GetComponent<FilialLogic>().hideBUTTONS();
            filial.GetComponent<FilialLogic>().hideIU();
            filial.GetComponent<FilialLogic>().hideUpdateCanva();
            curFilial = null;
        }
    }
}
