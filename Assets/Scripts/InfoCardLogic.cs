using TMPro;
using UnityEngine;

public class InfoCardLogic : MonoBehaviour
{
    public GameObject filial;
    public TextMeshProUGUI title,buy, rent, buy1, buy2, buy3, buy4, buy5, sell;
    // Start is called before the first frame update

    public void setValues() {
        if (title != null)
            title.text = filial.GetComponent<FilialLogic>().name;
        if (buy != null)
            buy.text = filial.GetComponent<FilialLogic>().getPriceToBuy().ToString();
        if (rent != null)
            rent.text = filial.GetComponent<FilialLogic>().getPriceToPay().ToString();
        if (buy1 != null)
            buy1.text = (filial.GetComponent<FilialLogic>().getPriceToBuy() * 2).ToString();
        if (buy2 != null)
            buy2.text = (filial.GetComponent<FilialLogic>().getPriceToBuy() * 3).ToString();
        if (buy3 != null)
            buy3.text = (filial.GetComponent<FilialLogic>().getPriceToBuy() * 4).ToString();
        if (buy4 != null)
            buy4.text = (filial.GetComponent<FilialLogic>().getPriceToBuy() * 5).ToString();
        if (buy5 != null)
        buy5.text = (filial.GetComponent<FilialLogic>().getPriceToBuy() * 6).ToString();
        if (sell != null)
            sell.text = filial.GetComponent<FilialLogic>().getPriceToSell().ToString();
    }

}
