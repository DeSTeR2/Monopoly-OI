using UnityEngine;

public class StartGame : MonoBehaviour
{

    public void chooseOnePlayer() {
        PlayerControl.numberPlayer = 1;
    }
    public void chooseTwoPlayer() {
        PlayerControl.numberPlayer = 2;
    }
    public void chooseThreePlayer() {
        PlayerControl.numberPlayer = 3;
    }
    public void chooseFourPlayer() {
        PlayerControl.numberPlayer = 4;
    }

}
