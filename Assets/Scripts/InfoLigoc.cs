using UnityEngine;
using TMPro;

public class InfoLigoc : MonoBehaviour
{
    public GameObject player;
    public TextMeshProUGUI moneyText;
    public Canvas selfCanva;

    private float timer = 0;
    private bool Checked = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Checked)
            timer += Time.deltaTime;
        
        if (timer > 0.5 && !Checked) {
            selfCanva.enabled = player.active;
            Checked = true;
            timer = 0;
        }
        
        if (player != null) {
            int pMoney = player.GetComponent<PlayerScript>().money;
            moneyText.text = pMoney.ToString();
        }
    }
}
