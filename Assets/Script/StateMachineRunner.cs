// 日本語対応
using UnityEngine;
using System;
using System.Collections.Generic;

public class StateMachineRunner : MonoBehaviour
{
    [SerializeField]
    private StateMachineCondition[] conditionsField;

    private Dictionary<string, bool> conditionsValue;

    private StateMachine stateMachine = null;
    private State stateA = null;
    private State stateB = null;
    private State stateC = null;

    public void Start()
    {
        conditionsValue = ConditionArrayToDinctionary(conditionsField);

        stateMachine = new StateMachine();

        stateA = new State();
        stateB = new State();
        stateC = new State();

        stateA.Initialize(stateMachine, new TransitionData(stateB, "toB"));
        stateB.Initialize(stateMachine, new TransitionData(stateC, "toC"));
        stateC.Initialize(stateMachine, new TransitionData(stateA, "toA"));

        stateMachine.Initialize(stateA, conditionsValue);
    }

    public void Update()
    {
        stateMachine.SetCondition("toA", false);
        stateMachine.SetCondition("toB", false);
        stateMachine.SetCondition("toC", false);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (stateMachine.CurrentState == stateA) stateMachine.SetCondition("toB", true);
            else if (stateMachine.CurrentState == stateB) stateMachine.SetCondition("toC", true);
            else if (stateMachine.CurrentState == stateC) stateMachine.SetCondition("toA", true);
        }

        if (stateMachine.CurrentState == stateA) Debug.Log("CurrentState is A");
        if (stateMachine.CurrentState == stateB) Debug.Log("CurrentState is B");
        if (stateMachine.CurrentState == stateC) Debug.Log("CurrentState is C");
    }

    private Dictionary<string, bool> ConditionArrayToDinctionary(StateMachineCondition[] array)
    {
        var result = new Dictionary<string, bool>();

        for (int i = 0; i < array.Length; i++)
        {
            result.Add(array[i].Key, array[i].Value);
        }

        return result;
    }

}

[Serializable]
public class StateMachineCondition
{
    [SerializeField]
    private string _key;
    [SerializeField]
    private bool _value;

    public string Key => _key;
    public bool Value => _value;
}
