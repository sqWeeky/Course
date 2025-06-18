using UnityEngine;

namespace Infastracture.Services.Input
{
    public abstract class InputService : IInputService
    {
        public abstract Vector2 Axis { get; }

        public bool IsAttackButtonUp()
            => SimpleInput.GetButtonUp(Constants.InputService.ButtonFire);

        protected static Vector2 SimpleInputAxis()
            => new Vector2(SimpleInput.GetAxis(Constants.InputService.Horizontal), SimpleInput.GetAxis(Constants.InputService.Vertical));
    }
}