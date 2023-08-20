// 日本語対応
using System;

public class State
{
    public void Initialize(StateMachine owner, params TransitionData[] transitionData)
    {
        if (owner == null) throw new ArgumentNullException("引数ownerがnullです。");
        _owner = owner;
        if (transitionData == null || transitionData.Length == 0)
            throw new ArgumentNullException("引数transitionConditionListが無効です。");
        _transitionData = transitionData;
    }

    private StateMachine _owner; // 実行元となるステートマシン
    private TransitionData[] _transitionData; // 遷移条件リスト

    public event Action Enter;
    public event Action Update;
    public event Action Exit;

    public void ExecuteEnter() { Enter?.Invoke(); }
    public void ExecuteUpdate() { Update?.Invoke(); }
    public void ExecuteExit() { Exit?.Invoke(); }

    /// <summary> 遷移を試みる。 </summary>
    /// <param name="nextState"> 遷移に成功した場合 遷移先の参照を返す。そうでない場合、nullを返す。 </param>
    /// <returns> 遷移条件が満たされたとき true、そうで無い場合falseを返す。 </returns>
    public bool TryTransition(out State nextState)
    {
        nextState = null;
        for (int i = 0; i < _transitionData.Length; i++)
        {
            // 「Met」は英語で、「満たされた」「達成された」「到達した」といった意味を持つ動詞。
            bool allConditionsMet = true; // 全ての条件が満たされたかどうか表現する値。
            for (int j = 0; j < _transitionData[i].Keys.Length; j++)
            {
                // もし、条件が満たされていなければこのループは無効。
                if (!_owner.GetCondition(_transitionData[i].Keys[j]))
                {
                    allConditionsMet = false;
                    break;
                }
            }
            if (allConditionsMet)
            {
                nextState = _transitionData[i].NextState;
                return true;
            }
        }
        return false;
    }
}