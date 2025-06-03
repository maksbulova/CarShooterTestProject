using System;
using UnityEngine;

namespace BitToolSet.ProceduralEasing.Springs
{
    [Serializable]
    public abstract class SpringBaseHandler<T>
    {
        [Header("Spring params")]
        [SerializeField] protected float frequency = 15;
        [SerializeField] protected float damping = 0.35f;
        [SerializeField] protected float epsilon = 0.001f;

        protected T Value;
        protected T velocity;
        protected T Target;

        public T Velocity => velocity;

        public abstract bool IsCalm
        {
            get;
        }

        public void Init(T initialValue, T targetValue)
        {
            Value = initialValue;
            Target = targetValue;
            velocity = default;
        }
        public void Init(T initialValue) => Value = initialValue;
        public void SetTarget(T target) => Target = target;
        public T Update(float deltaTime) => Update(Target, deltaTime);
        public abstract T Update(T target, float deltaTime);
    }

    [Serializable]
    public class SpringFloatHandler : SpringBaseHandler<float>
    {
        public override bool IsCalm => velocity < epsilon;

        public override float Update(float target, float deltaTime)
        {
            Spring.Calc(
                ref Value,
                ref velocity,
                target,
                deltaTime,
                frequency,
                damping);

            return Value;
        }
    }


    [Serializable]
    public class SpringVector2Handler : SpringBaseHandler<Vector2>
    {
        public override bool IsCalm => velocity.sqrMagnitude < epsilon * epsilon;
        
        public override Vector2 Update(Vector2 target, float deltaTime)
        {
            Spring.Calc(
                ref Value,
                ref velocity,
                target,
                deltaTime,
                frequency,
                damping);

            return Value;
        }
    }

    [Serializable]
    public class SpringVector3Handler : SpringBaseHandler<Vector3>
    {
        public override bool IsCalm => velocity.sqrMagnitude < epsilon * epsilon;
        
        public override Vector3 Update(Vector3 target, float deltaTime)
        {
            Spring.Calc(
                ref Value,
                ref velocity,
                target,
                deltaTime,
                frequency,
                damping);

            return Value;
        }
    }

    [Serializable]
    public class SpringVector4Handler : SpringBaseHandler<Vector4>
    {
        public override bool IsCalm => velocity.sqrMagnitude < epsilon * epsilon;
        
        public override Vector4 Update(Vector4 target, float deltaTime)
        {
            Spring.Calc(
                ref Value,
                ref velocity,
                target,
                deltaTime,
                frequency,
                damping);

            return Value;
        }
    }

    [Serializable]
    public class SpringQuaternionHandler : SpringBaseHandler<Quaternion>
    {
        public override bool IsCalm => Quaternion.Angle(velocity, Quaternion.identity) < epsilon * epsilon;
        
        public override Quaternion Update(Quaternion target, float deltaTime)
        {
            Spring.Calc(
                ref Value,
                ref velocity,
                target,
                deltaTime,
                frequency,
                damping);
            
            return Value;
        }
    }
}
