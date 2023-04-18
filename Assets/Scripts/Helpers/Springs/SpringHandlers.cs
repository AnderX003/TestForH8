using System;
using UnityEngine;

namespace Helpers.Springs
{
    [Serializable]
    public abstract class SpringBaseHandler<T>
    {
        protected T value;
        protected T velocity;
        protected T goalValue;

        [field: SerializeField] public float Frequency { get; set; } = 15;
        [field: SerializeField] public float Damping { get; set; } = 0.35f;
        [field: SerializeField] public bool Unscaled { get; set; } = false;

        protected float DeltaTime => Unscaled
            ? Time.unscaledDeltaTime
            : Time.deltaTime;

        public T Value
        {
            get => value;
            set => this.value = value;
        }

        public T Velocity
        {
            get => velocity;
            set => velocity = value;
        }

        public T GoalValue
        {
            get => goalValue;
            set => goalValue = value;
        }
    }

    [Serializable]
    public class SpringFloatHandler : SpringBaseHandler<float>
    {
        public void Update()
        {
            Spring.Calc(
                ref value,
                ref velocity,
                GoalValue,
                DeltaTime,
                Frequency,
                Damping);
        }
    }

    [Serializable]
    public class SpringVector2Handler : SpringBaseHandler<Vector2>
    {
        public void Update()
        {
            Spring.Calc(
                ref value,
                ref velocity,
                GoalValue,
                DeltaTime,
                Frequency,
                Damping);
        }
    }

    [Serializable]
    public class SpringVector3Handler : SpringBaseHandler<Vector3>
    {
        public void Update()
        {
            Spring.Calc(
                ref value,
                ref velocity,
                GoalValue,
                DeltaTime,
                Frequency,
                Damping);
        }
    }

    [Serializable]
    public class SpringVector4Handler : SpringBaseHandler<Vector4>
    {
        public void Update()
        {
            Spring.Calc(
                ref value,
                ref velocity,
                GoalValue,
                DeltaTime,
                Frequency,
                Damping);
        }
    }

    [Serializable]
    public class SpringQuaternionHandler : SpringBaseHandler<Quaternion>
    {
        public void Update()
        {
            Spring.Calc(
                ref value,
                ref velocity,
                GoalValue,
                DeltaTime,
                Frequency,
                Damping);
        }
    }
}

//Usage example

/*public class SpringVector3Example : MonoBehaviour
{
	[SerializeField] private SpringVector3Handler spring;

	private void Update()
	{
		spring.Value = transform.position;
		spring.Update();
		transform.position = spring.Value;
	}

	public void MoveTo(Vector3 pos) => spring.GoalValue = pos;
}*/
