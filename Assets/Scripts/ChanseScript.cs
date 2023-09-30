using System.Collections;
using UnityEngine;

public class ChanseScript : MonoBehaviour
{
    // Start is called before the first frame update
    private string[] actions = {"Add", "Move"};
    private int[] toAdd = { 50, 100, 200, 300 }, toMove = { 4, 6, 9, 10, 12 };
    private GameObject curPlayer;
    private float timer = 0;
    
    public GameObject playerControl, cuberRuler;

    private IEnumerable waiter() {
        yield return new WaitForSeconds(2);
    }

    void Update() {
           
        if (curPlayer != null) {
            cuberRuler.GetComponent<CubeS>().canShuffle = false;
            timer += Time.deltaTime;
        }

        if (timer > 1.5) {
            cuberRuler.GetComponent<CubeS>().canShuffle = false;
            int act = Random.Range(0, actions.Length);

            if (act == 0) {
                int plus = Random.Range(0, 2);
                int amound = Random.Range(0, toAdd.Length);
                amound = toAdd[amound];
                if (plus == 1) amound *= -1;
                curPlayer.GetComponent<PlayerScript>().money += amound;
                cuberRuler.GetComponent<CubeS>().canShuffle = true;
            } else {
                int steps = toMove[Random.Range(0, toMove.Length)];

                playerControl.GetComponent<PlayerControl>().playerIndx = curPlayer.GetComponent<PlayerScript>().playerNumber;
                playerControl.GetComponent<PlayerControl>().steps = steps;
            }
            curPlayer = null;
            timer = 0;
        }
    }

    public void doActions(GameObject Player) {
        curPlayer = Player;
        cuberRuler.GetComponent<CubeS>().canShuffle = false;
    }
}
