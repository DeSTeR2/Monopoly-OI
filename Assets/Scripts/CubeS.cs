using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class CubeS : MonoBehaviour
{
    public SpriteRenderer cubeL, cubeR;
    public Sprite[] spriteArray;
    public float timeAnim = 0f;
    public GameObject playerControl, buttonShuffle;
    public bool canShuffle = true, start = true;

    public Animator cubeAnimator;

    void Start()
    {
        cubeAnimator = GetComponent<Animator>();
        cubeL.gameObject.GetComponent<SpriteRenderer>();
        cubeR.gameObject.GetComponent<SpriteRenderer>();
    }

    void FixedUpdate() {
        if (canShuffle) {
            buttonShuffle.active = true;
        }    
        else {
            buttonShuffle.active = false;
        }
    }
    private IEnumerable waiter() {
        yield return new WaitForSeconds(2);
    }
    public void startShuffle()
    {
        if (!canShuffle) return;
        /*cubeAnimator.SetTrigger("CubeAnim");
        waiter();
        cubeAnimator.SetTrigger("goToEntry");
        cubeAnimator.ResetTrigger("CubeAnim");*/
        int cubeL, cubeR;
        if (!start) playerControl.GetComponent<PlayerControl>().goToAnotherPlayer();
        start = false;
        Shuffle(out cubeL, out cubeR);
        playerControl.GetComponent<PlayerControl>().shuflled = true;
        playerControl.GetComponent<PlayerControl>().steps = cubeL + cubeR;
        playerControl.GetComponent<PlayerControl>().stepsAgain = cubeL == cubeR;
    }


    private void Shuffle(out int CubeL, out int CubeR)
    {
        int indx = Random.Range(0, spriteArray.Length);
        cubeL.sprite = spriteArray[indx];

        CubeL = indx+1; 

        indx = Random.Range(0, spriteArray.Length);
        cubeR.sprite = spriteArray[indx];
        
        CubeR = indx+1;
    }
}
