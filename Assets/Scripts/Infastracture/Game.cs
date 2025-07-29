using Infastracture.Services;
using Infastracture.States;
using UI;
using UI.Elements;

namespace Infastracture
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}