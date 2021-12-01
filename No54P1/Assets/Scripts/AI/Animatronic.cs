using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using System;

public class Animatronic : MonoBehaviour, IAttackable, IProgrammable
{
    public Transform shippingPos;
    private Inventory inventory;
    public ParticleSystem sparks;
    public Color usbColor;
    public ParticleSystem electricP;
    public Collider col;
    public List<Transform> eyes = new List<Transform>();
    public Transform handTransform;
    public Transform faceTransform;
    public List<Animatronic> animatronics;
    public float viewAngle = 40;
    public Transform target;
    public List<Transform> randomPosTarget = new List<Transform>();
    public NavMeshAgent agent;
    public LayerMask obstacleMask;
    public LayerMask playerLayer;
    public Transform player;
    public AudioSource AS;
    public AudioSource loudNoises;
    private float speed = 0.5f;
    [SerializeField] private float nextSpeed = 0.5f;
    public Animator anims;
    private Death death;
    public bool canSeePlayer = false;
    public bool spottedPlayer = false;
    public bool ignoreWander = false;
    [SerializeField] private bool rebooting = false;
    public bool pausing = false;
    public bool fullyProgrammed = false;
    private float wantsToChasePlayer = 30;
    bool programmable = false;
    bool breakingThroughDoor = false;
    private float recalcTime = 0.5f;
    public bool readyToShip = false;

    [Header("Sound Effects")]
    [Space]
    public AudioClip robotFootstep;
    public AudioClip robotWhir;
    public AudioClip screech;
    public AudioClip reboot;
    public AudioClip robotTazing;
    public AudioClip spark;
    public AudioClip[] deathLines;

    float lastDist = 0;
    System.Random t = new System.Random();
    private void Start()
    {
        AS.enabled = true;
        inventory = FindObjectOfType<Inventory>();
        anims = GetComponent<Animator>();
        Initialize();
        player = FindObjectOfType<CPMPlayer>().transform;
        death = player.GetComponent<Death>();
    }
    public void Initialize()
    {
        ignoreWander = false;
        nextSpeed = 0.6f;
        if (fullyProgrammed)
        {
            StopAllCoroutines();
            StartCoroutine(WalkToTarget());
            return;
        }
        StartCoroutine(FindRandomPos());
        StartCoroutine(WalkToTarget());
        StartCoroutine(CheckFOV());
    }
    private void Update()
    {
        if (Death.playerDead || CutScenePlaying.cutscenePlaying)
        {
            StopAllCoroutines();
            AS.enabled = false;
            return;
        }
        if (!rebooting && !pausing)
            ChasePlayer();
        FindNewSpeed();
        if (canSeePlayer && !spottedPlayer && !rebooting && !pausing)
        {
            StopCoroutine(SpottedPlayer());
            StartCoroutine(SpottedPlayer());
        }
        if (rebooting)
        {
            agent.speed = 1.2f;
            agent.isStopped = true;
        }
        if (agent.enabled && !agent.isStopped)
        {
            if (agent.velocity.magnitude == 0 && Floatf.Distance(agent.remainingDistance, lastDist) < 0.01f && !agent.pathPending)
                anims.SetBool("Idle", true);
            else if (agent.velocity.magnitude > 1)
                anims.SetBool("Idle", false);
            lastDist = agent.remainingDistance;
        }
    }

    public void Attack()
    {
        if(!rebooting && !fullyProgrammed)
            StartCoroutine(Reboot());
    }
    void FindNewSpeed()
    {
        speed = Mathf.Lerp(speed, nextSpeed, Time.deltaTime * 5);
        anims.SetFloat("SpeedStateBlend", speed);
    }
    public void PlayFootStep()
    {
        if (Death.playerDead || CutScenePlaying.cutscenePlaying)
            return;
        AS.PlayOneShot(robotFootstep);
    }
    public void PlayWhir()
    {
        if (Death.playerDead || CutScenePlaying.cutscenePlaying)
            return;
        AS.PlayOneShot(robotWhir);
    }
    void ChasePlayer()
    {
        if(spottedPlayer)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 6)
                recalcTime = 0;
            else
                recalcTime = 0.5f;
            float playerDist = Vector3.Distance(transform.position, player.transform.position);
            Vector3 playerDir = (player.transform.position - transform.position).normalized;

            if (Quaternion.Angle(Quaternion.LookRotation(playerDir), Quaternion.LookRotation(transform.forward)) < viewAngle / 2 && playerDist < 20)
            {
                if (!Physics.Raycast(transform.position, playerDir, playerDist, obstacleMask))
                {
                    if (Vector3.Distance(transform.position, player.transform.position) < 2)
                    {
                        if (Physics.Raycast(transform.position, playerDir, playerDist, playerLayer))
                        {
                            if (!Death.playerDead)
                            {
                                anims.SetBool("Punching", false);
                                anims.SetBool("Idle", false);
                                agent.isStopped = true;
                                foreach (var _animatronic in animatronics)
                                {
                                    if (_animatronic)
                                        Destroy(_animatronic.gameObject);
                                }
                                col.enabled = false;
                                System.Random r = new System.Random();
                                int rn = r.Next(0, 3);
                                if (rn > 2)
                                    rn = 2;
                                Debug.Log(rn);
                                AudioClip chosenClip = deathLines[rn];
                                death.StartCoroutine(death.JumpscareSequence(faceTransform, handTransform, anims, transform, chosenClip));
                            }
                        }
                    }
                }
                else if (NearDoor.playerNearDoor && NearDoor.currentRobotPos != null && !breakingThroughDoor && NearDoor.currentDoor != null && playerDist < 6)
                    StartCoroutine(BreakThroughDoor());
                else
                    wantsToChasePlayer = Mathf.Clamp(wantsToChasePlayer - 1 * Time.deltaTime, 0, 30);
                if (wantsToChasePlayer == 0)
                {
                    spottedPlayer = false;
                    canSeePlayer = false;
                    ignoreWander = false;
                    nextSpeed = 0.6f;
                    agent.speed = 1.2f;
                    wantsToChasePlayer = 30;
                }
            }
        }
    }
    IEnumerator BreakThroughDoor()
    {
        breakingThroughDoor = true;
        agent.isStopped = true;
        transform.position = NearDoor.currentRobotPos.position;
        Vector3 dir = (player.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        anims.SetBool("Idle", false);
        anims.SetBool("Punching", true);
        yield return new WaitForSeconds(0.3f);
        NearDoor.Break(dir);
        yield return new WaitForSeconds(0.2f);
        anims.SetBool("Punching", false);
        agent.isStopped = false;
        breakingThroughDoor = false;
    }
    IEnumerator CheckFOV()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            if(!spottedPlayer)
                FieldOfVisionCheck();
        }
    }
    IEnumerator WalkToTarget()
    {
        while (true)
        {
            if(target != null)
                agent.destination = target.position;
            yield return new WaitForSeconds(recalcTime);
        }
    }
    IEnumerator FindRandomPos()
    {
        while (true)
        {
            if (!spottedPlayer && !fullyProgrammed)
            {
                if (!ignoreWander)
                {
                    int tn = t.Next(0, randomPosTarget.Count);
                    if (tn == randomPosTarget.Count)
                        tn = randomPosTarget.Count - 1;
                    target = randomPosTarget[tn];
                    while (Vector3.Distance(transform.position, target.position) > 3)
                    {
                        yield return null;
                    }
                    yield return null;
                }
            }
            yield return null;
        }
    }
    void FieldOfVisionCheck()
    {
        float playerDist = Vector3.Distance(transform.position, player.transform.position);
        Vector3 playerDir = (player.transform.position - transform.position).normalized;

        if (Quaternion.Angle(Quaternion.LookRotation(playerDir), Quaternion.LookRotation(transform.forward)) < viewAngle / 2 && playerDist < 20)
        {
            if (!Physics.Raycast(transform.position, playerDir, playerDist, obstacleMask))
            {
                if (Physics.Raycast(transform.position, playerDir, playerDist, playerLayer))
                {
                    canSeePlayer = true;
                }
            }
        }
    }
    IEnumerator SpottedPlayer()
    {
        nextSpeed = 0.3f;
        loudNoises.PlayOneShot(screech);
        agent.speed = 10;
        spottedPlayer = true;
        target = player;
        agent.isStopped = true;
        pausing = true;
        AS.maxDistance = 21;
        yield return new WaitForSeconds(1.5f);
        pausing = false;
        agent.isStopped = false;
        if(!rebooting)
            nextSpeed = 1;
    }
    IEnumerator Reboot()
    {
        anims.SetBool("Idle", false);
        AS.maxDistance = 8;
        StopCoroutine(SpottedPlayer());
        electricP.Play();
        anims.SetBool("Tazed", true);
        rebooting = true;
        AS.PlayOneShot(robotTazing);
        agent.speed = 1.2f;
        yield return new WaitForSeconds(2.3f);
        anims.SetBool("Tazed", false);
        anims.SetBool("Down", true);
        electricP.Stop();
        programmable = true;
        yield return new WaitForSeconds(10);
        AlertAnimatronics();
        programmable = false;
        AS.PlayOneShot(reboot);
        yield return new WaitForSeconds(9.5f);
        anims.SetBool("Down", false);
        nextSpeed = 0.6f;
        yield return new WaitForSeconds(0.5f);
        rebooting = false;
        agent.isStopped = false;
        spottedPlayer = false;
        canSeePlayer = false;
        ignoreWander = false;
        if (fullyProgrammed)
        {
            pausing = false;
            agent.enabled = true;
            Initialize();
            StartCoroutine(WaitForShipping());
        }
    }
    IEnumerator WaitForShipping()
    {
        target = shippingPos;
        while(Vector3.Distance(transform.position, shippingPos.position) > 1)
        {
            yield return null;
        }
        transform.SetPositionAndRotation(shippingPos.position, Quaternion.LookRotation(-Vector3.forward, Vector3.up));
        anims.SetBool("Idle", false);
        anims.SetBool("Down", true);
        readyToShip = true;
    }
    public void AlertAnimatronics()
    {
        foreach (var animatronic in animatronics)
        {
            if (animatronic)
            {
                animatronic.StopCoroutine(animatronic.SetTemporaryTarget(player));
                animatronic.StartCoroutine(animatronic.SetTemporaryTarget(player));
            }
        }
    }
    public IEnumerator SetTemporaryTarget(Transform targ)
    {
        if (!fullyProgrammed)
        {
            target = targ;
            ignoreWander = true;
            yield return new WaitForSeconds(20);
            ignoreWander = false;
        }
    }
    public void Program(USB usb)
    {
        if (programmable)
        {
            if (usb.usbColor == usbColor)
            {
                inventory.usb.Remove(usb);
                fullyProgrammed = true;
                sparks.Play();
                AS.PlayOneShot(spark);
            }
        }
    }
}