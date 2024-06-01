using System.Collections;
using UnityEngine;

public class Buff2 : MonoBehaviour
{
    private PlayerController playerController;

    private void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void ActivateBuff()
    {
        playerController.hasBuff2 = true;
    }
}
