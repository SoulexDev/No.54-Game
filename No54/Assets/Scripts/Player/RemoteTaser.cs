using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemoteTaser : MonoBehaviour
{
    private Transform cam;
    private CPMPlayer player;
    public LayerMask animatronicLayer;
    public Image taserCooldown;
    private bool coolingDown = false;
    private void Start()
    {
        player = FindObjectOfType<CPMPlayer>();
        cam = player.playerView;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && !coolingDown)
        {
            RaycastHit hit;
            if (Physics.SphereCast(cam.position, 0.1f, cam.forward, out hit, 10))
            {
                IAttackable attackable;
                if(hit.collider.TryGetComponent<IAttackable>(out attackable))
                {
                    StartCoroutine(CoolDown());
                    attackable.Attack();
                }
            }
        }
    }
    IEnumerator CoolDown()
    {
        float timer = 80;
        coolingDown = true;
        while (timer > 0)
        {
            taserCooldown.fillAmount = timer / 80;
            timer = Mathf.Clamp(timer - 1 * Time.deltaTime, 0, 80);
            yield return null;
        }
        taserCooldown.fillAmount = 1;
        coolingDown = false;
    }
}