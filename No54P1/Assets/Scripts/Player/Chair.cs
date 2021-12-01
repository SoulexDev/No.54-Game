using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chair : MonoBehaviour, IInteractable
{
    [SerializeField] private ProgrammingComputer computer;
    private CPMPlayer player;
    [SerializeField] private Transform sitPos;
    private Transform playerView;
    bool sittingDown = false;
    private void Start()
    {
        player = FindObjectOfType<CPMPlayer>();
        playerView = player.playerView;
    }
    private void Update()
    {
        if(sittingDown)
            SitDown();
    }
    public void Interact()
    {
        if (sittingDown)
        {
            computer.playerSittingDown = false;
            sittingDown = false;
            StandUp();
        }
        else
        {
            computer.playerSittingDown = true;
            player.ownsView = false;
            sittingDown = true;
        }
    }
    void SitDown()
    {
        playerView.position = Vector3.Slerp(playerView.position, sitPos.position, Time.deltaTime * 5);
    }
    void StandUp()
    {
        player.ownsView = true;
    }
}