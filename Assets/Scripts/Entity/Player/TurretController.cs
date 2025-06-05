using BitToolSet.Extensions;
using BitToolSet.ProceduralEasing.Springs;
using UnityEngine;

namespace Player
{
    public class TurretController : MonoBehaviour
    {
        [SerializeField] private TurretInputController inputController;
        [SerializeField] private Transform aimPivot;
        [SerializeField] private ShotController shotController;
        [Header("Rotation")]
        [SerializeField] private SpringVector3Handler rotationSpring;
        [SerializeField] private float maxRotationAngle = 45;

        private Vector3 _currentAimDirection = Vector3.forward;

        private void Update()
        {
            AimTick();
        }

        public void Init()
        {
            inputController.OnPointerDown += OnInputPointerDown;
            rotationSpring.Init(Vector3.forward);
        }

        public void Deinit()
        {
            inputController.OnPointerDown -= OnInputPointerDown;
        }

        private void OnInputPointerDown(Vector2 relativeScreenPosition)
        {
            var aimAngle = relativeScreenPosition.x.Remap(
                0, 1,
                -maxRotationAngle, maxRotationAngle);
            _currentAimDirection = Quaternion.Euler(0, aimAngle, 0) * Vector3.forward;
        
            shotController.TryShot();
        }

        private void AimTick()
        {
            var direction = rotationSpring.Update(_currentAimDirection, Time.fixedDeltaTime);
            aimPivot.localRotation = Quaternion.LookRotation(direction);
        }
    }
}
