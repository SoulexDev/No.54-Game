using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MountAnimatronic : MonoBehaviour
{
    [SerializeField] private Transform mountPos;
    [SerializeField] private Transform returnPos;
    private ProgrammingComputer computer;
    public Animatronic mech;
    //private void Start()
    //{
    //    computer = FindObjectOfType<ProgrammingComputer>();
    //}
    //private void OnTriggerEnter(Collider other)
    //{
    //    if(!animatronicMounted && other.TryGetComponent<Animatronic>(out mech))
    //    {
    //        StartCoroutine(Mount());
    //        animatronicMounted = true;
    //        computer.mountedAnimatronic = mech;
    //        computer.UpdateComputer();
    //    }
    //}
    IEnumerator Mount()
    {
        mech.StopAllCoroutines();
        mech.agent.speed = 1.2f;
        mech.agent.enabled = false;
        mech.spottedPlayer = false;
        mech.canSeePlayer = false;
        //mech.mounted = true;
        mech.pausing = true;
        //mech.anims.SetBool("Mounted", true);
        mech.transform.position = mountPos.position;
        mech.transform.rotation = Quaternion.Euler(180, 0, 180);
        yield return new WaitForSeconds(80);
        //mech.mounted = false;
        mech.pausing = false;
        //mech.anims.SetBool("Mounted", false);
        mech.transform.position = returnPos.position;
        mech.agent.enabled = true;
        mech.Initialize();
        mech = null;
    }
}