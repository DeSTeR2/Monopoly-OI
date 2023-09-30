using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.UI;

public class TradeScropt : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject imgPL1, imgPL2;
    // Update is called once per frame
    void Update()
    {
        
    }

    public void startStrade(GameObject Player1, GameObject Player2) {
        imgPL1.GetComponent<SpriteRenderer>().sprite = Player1.GetComponent<SpriteRenderer>().sprite;
        imgPL2.GetComponent<SpriteRenderer>().sprite = Player2.GetComponent<SpriteRenderer>().sprite;
    }

}
