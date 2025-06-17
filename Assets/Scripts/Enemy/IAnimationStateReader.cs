using UnityEditor.Animations;
using AnimatorState = Logic.AnimatorState;

namespace Enemy
{
    public interface IAnimationStateReader
    {
        void EnteredState(int stateHash);
        void ExitedState(int stateHash);
        AnimatorState State { get; }
    }
}