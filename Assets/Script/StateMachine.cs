// 日本語対応
using System;
using System.Collections.Generic;

public class StateMachine
{
    private State _currentState = null;

    private Dictionary<string, bool> _condition = null;

    public State CurrentState => _currentState;

    public void Initialize(State firstState, Dictionary<string, bool> condition)
    {
        if (firstState == null) throw new ArgumentNullException("引数firstStateがnullです。");
        _currentState = firstState;
        if (condition.Count == 0 || condition == null) throw new ArgumentException("引数conditionが無効です。");
        _condition = condition;
    }
    public void Update()
    {
        if (_currentState == null) { throw new ArgumentNullException(""); }

        _currentState.ExecuteUpdate();
    }

    public void SetCondition(string key, bool value)
    {
        var oldValue = _condition[key];
        _condition[key] = value;
        if (_condition[key] != oldValue) OnConditionChanged();
    }
    public bool GetCondition(string key)
    {
        return _condition[key];
    }

    /// <summary> ステートマシンの状態が変化したとき実行する。 </summary>
    private void OnConditionChanged()
    {
        TryStateTransition();
    }
    /// <summary> ステートの遷移を試みる。 </summary>
    private void TryStateTransition()
    {
        if (_currentState.TryTransition(out State nextState))
        {
            _currentState.ExecuteExit(); // 遷移前ステートのExit実行
            _currentState = nextState;
            _currentState.ExecuteEnter(); // 遷移後ステートのEnter実行

            TryStateTransition(); // 一応次のステートも確認する。
        }
    }
}
