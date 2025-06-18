using UnityEngine;

namespace Infastracture.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                Vector2 axis = SimpleInputAxis();

                if (axis == Vector2.zero)
                    axis = UnityAxis();

                return axis;
            }
        }

        private static Vector2 UnityAxis()
            => new(UnityEngine.Input.GetAxis(Constants.InputService.Horizontal), UnityEngine.Input.GetAxis(Constants.InputService.Vertical));
    }
}