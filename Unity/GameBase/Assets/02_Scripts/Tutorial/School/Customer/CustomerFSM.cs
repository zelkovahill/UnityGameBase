using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
using UnityEngine;
using UnityEngine.AI;


public enum ECustomerState
{
    Idle,
    WalkingToShelf,
    PickingItem,
    WalkingToCounter,
    PlacingItem,
}

public class Timer
{
    private float timeRemaining;

    public void Set(float time)
    {
        timeRemaining = time;
    }

    public void Update(float deltaTime)
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= deltaTime;
        }
    }

    public bool IsFinished()
    {
        return timeRemaining <= 0;
    }
}

[RequireComponent(typeof(NavMeshAgent))]
public class CustomerFSM : MonoBehaviour
{
    public ECustomerState currentState;
    public Timer timer;
    private NavMeshAgent agent;
    public bool isMoveDone = false;

    [Space(10)]

    public Transform target;

    [Space(10)]

    public Transform counter;
    public List<GameObject> targetPos = new List<GameObject>();
    public List<GameObject> myBox = new List<GameObject>();

    private static int nextProiority = 0;
    private static readonly object priorityLock = new object();

    public int boxesToPick = 5;
    private int boxesPicked = 0;

    private void Start()
    {
        timer = new Timer();
        agent = GetComponent<NavMeshAgent>();
        AssignPriority();
        currentState = ECustomerState.Idle;
    }

    private void Update()
    {
        timer.Update(Time.deltaTime);

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
            {
                isMoveDone = true;
            }
        }

        switch (currentState)
        {
            case ECustomerState.Idle:
                Idle();
                break;

            case ECustomerState.WalkingToShelf:
                WalkingToShelf();
                break;

            case ECustomerState.PickingItem:
                PickingItem();
                break;

            case ECustomerState.WalkingToCounter:
                WalkingToCounter();
                break;

            case ECustomerState.PlacingItem:
                PlacingItem();
                break;
        }
    }

    private void AssignPriority()
    {
        lock (priorityLock)
        {
            agent.avoidancePriority = nextProiority;
            nextProiority = (nextProiority + 1) % 100;
        }
    }

    private void MoveToTarget()
    {
        isMoveDone = false;

        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    private void ChangeState(ECustomerState nextState, float waitTime = 0.0f)
    {
        currentState = nextState;
        timer.Set(waitTime);
    }

    private void Idle()
    {
        if (timer.IsFinished())
        {
            target = targetPos[Random.Range(0, targetPos.Count)].transform;
            MoveToTarget();

            ChangeState(ECustomerState.WalkingToShelf, 2.0f);
        }
    }

    private void WalkingToShelf()
    {
        if (timer.IsFinished() && isMoveDone)
        {
            ChangeState(ECustomerState.PickingItem, 2.0f);
        }
    }

    private void PickingItem()
    {
        if (timer.IsFinished())
        {
            if (boxesPicked < boxesToPick)
            {
                GameObject box = GameObject.CreatePrimitive(PrimitiveType.Cube);
                myBox.Add(box);
                box.transform.parent = gameObject.transform;
                box.transform.localEulerAngles = Vector3.zero;
                box.transform.localPosition = new Vector3(0, boxesPicked * 2f, 0);

                boxesPicked++;
                timer.Set(0.5f);
            }
            else
            {
                target = counter;
                MoveToTarget();
                ChangeState(ECustomerState.WalkingToCounter, 2.0f);
            }

        }

    }

    private void WalkingToCounter()
    {
        if (timer.IsFinished() && isMoveDone)
        {
            ChangeState(ECustomerState.PlacingItem, 2.0f);
        }
    }

    private void PlacingItem()
    {
        if (timer.IsFinished())
        {
            if (myBox.Count != 0)
            {
                myBox[0].transform.position = counter.transform.position;
                myBox[0].transform.parent = counter.transform;
                myBox.RemoveAt(0);

                timer.Set(1.0f);
            }
            else
            {
                ChangeState(ECustomerState.Idle, 2.0f);
            }
        }
    }
}
