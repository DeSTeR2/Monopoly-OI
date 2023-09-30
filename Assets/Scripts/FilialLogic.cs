using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FilialLogic : MonoBehaviour
{
    public int priceToBuy = 0;
    public int[] updates;
    public int priceToPay = 0;
    public int priceToSell = 0;
    public int numberOfUpdates = 0;

    public string name = "BoarHeights";
    public GameObject Owner = null, UI, buttons, playerStay, filial;
    public Canvas infoCard, updateCanva, sellCanva, canvaForSellStationDepends;
    public bool canUpdate = false;
    public TextMeshProUGUI textUpdateButton;

    public List<GameObject> updatesHouse = new List<GameObject>(), oneColorFilials = new List<GameObject>();

    private List<Color> colorList = new List<Color>();
    private SpriteRenderer playerColor;
    private GameObject updateBtn, sellBtn;
    private Button refBtn;
    // Start is called before the first frame update
    void Start()
    {

        sellBtn = GameObject.FindGameObjectWithTag("refBTN");
        sellBtn.active = true;
        refBtn = sellBtn.GetComponent<Button>();
        refBtn.onClick.RemoveAllListeners();
        //sellBtn.GetComponent<RectTransform>().position = new Vector3(-40,36,0);
        //sellBtn.enabled = true;

        colorList.Add(new Color(255f, 0f, 0f));
        colorList.Add(new Color(66f, 255f, 0f));
        colorList.Add(new Color(0f, 255f, 255f));
        colorList.Add(new Color(0f, 0f, 255f));

        playerColor = this.gameObject.transform.GetChild(5).GetComponent<SpriteRenderer>();
        playerColor.enabled = false;
        playerColor.sortingOrder = 4;
        playerColor.color = colorList[0];

        if (updateCanva != null) {
            updateCanva.enabled = true;
            updateBtn = updateCanva.gameObject.transform.GetChild(0).gameObject;
            updateBtn.active = false;
            sellCanva.enabled = false;
            GameObject child = this.gameObject.transform.GetChild(2).gameObject;
            for (int i=0; i<5;i++) {
                updatesHouse.Add(child.transform.GetChild(i).gameObject);
                updatesHouse[i].active = false;
            }
        }

        for (int i=0; i<updates.Length;i++)
        {
            updates[i] = priceToBuy * (i + 1);
        }

        priceToPay = updates[numberOfUpdates] * 20 / 100;
        priceToSell = updates[0] * 50 / 100;

        infoCard.GetComponent<InfoCardLogic>().setValues();

        updatePrices();
    }

    void Update() {
        if (numberOfUpdates>=0 && updatesHouse.Count > 0 ) {

            for (int i = 0; i < 5; i++) {
                updatesHouse[i].active = false;
            }

            for (int i=1; i<=numberOfUpdates;i++) {
                updatesHouse[i-1].active = true;
            }
            
            if (numberOfUpdates==5) {
                for (int i=0; i<updatesHouse.Count-1; i++) {
                    updatesHouse[i].active = false;
                }
            }

            priceToPay = updates[numberOfUpdates] * 20 / 100;
        }

        if (Owner != null) {
            bool f = true;
            playerColor.color = colorList[Owner.GetComponent<PlayerScript>().playerNumber];
            playerColor.color -= new Color(0, 0, 0, 0.5f);
            playerColor.enabled = true;
            for (int i=0; i<oneColorFilials.Count; i++) {
                if (oneColorFilials[i].GetComponent<FilialLogic>().Owner != Owner) {
                    f = false;
                    break;
                }
            }
            canUpdate = f;
            
        }

        if (Owner == null) playerColor.enabled = false;

    }

    public void showUpdateCanva() {
        if (canUpdate == true) {
            if (numberOfUpdates < 3) {
                textUpdateButton.text = "Update";
                updateBtn.active = true;
            }
            if (numberOfUpdates > 0) {
                sellCanva.enabled = true;
            }
            infoCard.enabled = true;
        }
    }

    public void hideUpdateCanva() {
        if (sellCanva != null) { 
            updateBtn.active = false;
            infoCard.enabled = true;
            sellCanva.enabled = false;
        } 
    }

    public void addUpdate() {
        if (numberOfUpdates < 5 && Owner.GetComponent<PlayerScript>().money >= updates[numberOfUpdates]) {
            numberOfUpdates++;
            Owner.GetComponent<PlayerScript>().money -= updates[numberOfUpdates - 1];
        }
    }

    public void sellUpdate() {
        if (numberOfUpdates > 0) {
            numberOfUpdates--;
            Owner.GetComponent<PlayerScript>().money += updates[numberOfUpdates];
        }
    }

    public void updatePrices()
    {
        priceToPay = updates[numberOfUpdates] * 20 / 100;
        priceToSell = updates[0] * 50 / 100;
    }
    public int getPriceToBuy()
    {
        return priceToBuy;
    }
    public int getPriceToSell()
    {
        return priceToSell;
    }

    public int getPriceToPay()
    {
        return priceToPay;
    }

    public void buyLogic() {
        UI.active = true;
        if (Owner == null) {
            //buttons.active = true;
            int money = playerStay.GetComponent<PlayerScript>().money;
            if (money >= priceToBuy) buttons.active = true;
        }
    }

    public void skipLogic() {
        hideBUTTONS();
        hideIU();
        playerStay.GetComponent<PlayerScript>().curFilial = null;
    }


    public void getBought() {
        GameObject player = playerStay;
        if (Owner != null) {
            return;
        }

        int playerMoney = player.GetComponent<PlayerScript>().getMoney();
        if (playerMoney >= priceToBuy && Owner == null) {
            Owner = player;
            player.GetComponent<PlayerScript>().money -= priceToBuy;
            player.GetComponent<PlayerScript>().filials.Add(filial);
            hideBUTTONS();
            return;
        }
        return;
    }

    public void showUI() {
        UI.active = true;
    }
    public void hideIU() {
        UI.active = false;
    }

    public void showBUTTONS() {
        buttons.active = true;
    }
    public void hideBUTTONS() {
        buttons.active = false;
    }

    private void sellMe() {
        if (Owner != null) {
            numberOfUpdates = 0;
            Owner.GetComponent<PlayerScript>().money += priceToSell;
            Owner.GetComponent<PlayerScript>().filials.Remove(filial);
            Owner = null;
        }
    }

    private void OnMouseDown() {
        if (filial.tag != "DependsOnCube" && filial.tag != "Station") {
            if (updateCanva.gameObject.transform.childCount == 1 && Owner != null && Owner.GetComponent<PlayerScript>().playerNumber == Owner.GetComponent<PlayerScript>().controller.GetComponent<PlayerControl>().playerIndx) {
                var button = Instantiate(refBtn, Vector3.zero, Quaternion.identity) as Button;
                button.GetComponent<RectTransform>().SetParent(updateCanva.transform);
                button.GetComponent<RectTransform>().position = Input.mousePosition + new Vector3(4f, 0, 0);
                //button.onClick.RemoveListener(addUpdate);
                button.onClick.AddListener(sellMe);
                updateCanva.gameObject.transform.GetChild(1).gameObject.active = true;

            }
        } else {
            if (canvaForSellStationDepends.gameObject.transform.childCount == 3 && Owner != null && Owner.GetComponent<PlayerScript>().playerNumber == Owner.GetComponent<PlayerScript>().controller.GetComponent<PlayerControl>().playerIndx) {
                var button = Instantiate(refBtn, Vector3.zero, Quaternion.identity) as Button;
                button.GetComponent<RectTransform>().SetParent(canvaForSellStationDepends.transform);
                button.GetComponent<RectTransform>().position = Input.mousePosition + new Vector3(4f, 0, 0);
                //button.onClick.RemoveListener(addUpdate);
                button.onClick.AddListener(sellMe);
                canvaForSellStationDepends.gameObject.transform.GetChild(3).gameObject.active = true;

            }
        }
    }

    private void OnMouseExit() {
        if (filial.tag != "DependsOnCube" && filial.tag != "Station") {
            if (updateCanva != null)
                if (updateCanva.gameObject.transform.childCount > 1) Destroy(updateCanva.gameObject.transform.GetChild(1).gameObject);
        }
        else {
            if (canvaForSellStationDepends.gameObject.transform.childCount > 3) Destroy(canvaForSellStationDepends.gameObject.transform.GetChild(3).gameObject);
        }
    }
}
