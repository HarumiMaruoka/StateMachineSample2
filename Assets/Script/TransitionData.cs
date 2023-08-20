// 日本語対応
using System;

/// <summary>
/// ステートの遷移条件と遷移先を制御するクラス。
/// </summary>
public class TransitionData
{
    public TransitionData(State nextState, params string[] keys)
    {
        if (nextState == null) { throw new ArgumentNullException("nextStateがnullです。"); }
        _nextState = nextState;
        if (keys == null || keys.Length == 0) { throw new ArgumentException("keysが不適切です。"); }
        _keys = keys;
    }

    private readonly string[] _keys;
    private readonly State _nextState;

    public string[] Keys => _keys;
    public State NextState => _nextState;
}
