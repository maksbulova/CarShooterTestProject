using UnityEngine;

namespace BitToolSet.ProceduralEasing.Springs
{
	public static class Spring
	{
		private struct DampedSpringMotionParams
		{
			// newPos = posPosCoef*oldPos + posVelCoef*oldVel
			public float posPosCoef, posVelCoef;

			// newVel = velPosCoef*oldPos + velVelCoef*oldVel
			public float velPosCoef, velVelCoef;
		}

		private static DampedSpringMotionParams CalcDampedSpringMotionParams(
			float deltaTime, // time step to advance
			float angularFrequency, // angular frequency of motion
			float dampingRatio) // damping ratio of motion
		{
			const float epsilon = 0.0001f;
			DampedSpringMotionParams pOutParams;

			// force values into legal range
			if (dampingRatio < 0.0f) dampingRatio = 0.0f;
			if (angularFrequency < 0.0f) angularFrequency = 0.0f;

			// if there is no angular frequency, the spring will not move and we can
			// return identity
			if (angularFrequency < epsilon)
			{
				pOutParams.posPosCoef = 1.0f;
				pOutParams.posVelCoef = 0.0f;
				pOutParams.velPosCoef = 0.0f;
				pOutParams.velVelCoef = 1.0f;
				return pOutParams;
			}

			if (dampingRatio > 1.0f + epsilon)
			{
				// over-damped
				float za = -angularFrequency * dampingRatio;
				float zb = angularFrequency * Mathf.Sqrt(dampingRatio * dampingRatio - 1.0f);
				float z1 = za - zb;
				float z2 = za + zb;
				// Value e (2.7) raised to a specific power
				float e1 = Mathf.Exp(z1 * deltaTime);
				float e2 = Mathf.Exp(z2 * deltaTime);

				float invTwoZb = 1.0f / (2.0f * zb); // = 1 / (z2 - z1)

				float e1_Over_TwoZb = e1 * invTwoZb;
				float e2_Over_TwoZb = e2 * invTwoZb;

				float z1e1_Over_TwoZb = z1 * e1_Over_TwoZb;
				float z2e2_Over_TwoZb = z2 * e2_Over_TwoZb;

				pOutParams.posPosCoef = e1_Over_TwoZb * z2 - z2e2_Over_TwoZb + e2;
				pOutParams.posVelCoef = -e1_Over_TwoZb + e2_Over_TwoZb;

				pOutParams.velPosCoef = (z1e1_Over_TwoZb - z2e2_Over_TwoZb + e2) * z2;
				pOutParams.velVelCoef = -z1e1_Over_TwoZb + z2e2_Over_TwoZb;
			}
			else if (dampingRatio < 1.0f - epsilon)
			{
				// under-damped
				float omegaZeta = angularFrequency * dampingRatio;
				float alpha = angularFrequency * Mathf.Sqrt(1.0f - dampingRatio * dampingRatio);

				float expTerm = Mathf.Exp(-omegaZeta * deltaTime);
				float cosTerm = Mathf.Cos(alpha * deltaTime);
				float sinTerm = Mathf.Sin(alpha * deltaTime);

				float invAlpha = 1.0f / alpha;

				float expSin = expTerm * sinTerm;
				float expCos = expTerm * cosTerm;
				float expOmegaZetaSin_Over_Alpha = expTerm * omegaZeta * sinTerm * invAlpha;

				pOutParams.posPosCoef = expCos + expOmegaZetaSin_Over_Alpha;
				pOutParams.posVelCoef = expSin * invAlpha;

				pOutParams.velPosCoef = -expSin * alpha - omegaZeta * expOmegaZetaSin_Over_Alpha;
				pOutParams.velVelCoef = expCos - expOmegaZetaSin_Over_Alpha;
			}
			else
			{
				// critically damped
				float expTerm = Mathf.Exp(-angularFrequency * deltaTime);
				float timeExp = deltaTime * expTerm;
				float timeExpFreq = timeExp * angularFrequency;

				pOutParams.posPosCoef = timeExpFreq + expTerm;
				pOutParams.posVelCoef = timeExp;

				pOutParams.velPosCoef = -angularFrequency * timeExpFreq;
				pOutParams.velVelCoef = -timeExpFreq + expTerm;
			}

			return pOutParams;
		}

		private static void UpdateDampedSpringMotion(
			ref float pPos, // position value to update
			ref float pVel, // velocity value to update
			float equilibriumPos, // position to approach
			DampedSpringMotionParams parameters) // motion parameters to use
		{
			float oldPos = pPos - equilibriumPos; // update in equilibrium relative space
			float oldVel = pVel;

			pPos = oldPos * parameters.posPosCoef + oldVel * parameters.posVelCoef + equilibriumPos;
			pVel = oldPos * parameters.velPosCoef + oldVel * parameters.velVelCoef;
		}

		/// <summary>
		/// Calculate a spring motion development for a given deltaTime
		/// </summary>
		/// <param name="position">"Live" position value</param>
		/// <param name="velocity">"Live" velocity value</param>
		/// <param name="equilibriumPosition">Goal (or rest) position</param>
		/// <param name="deltaTime">Time to update over</param>
		/// <param name="angularFrequency">Angular frequency of motion</param>
		/// <param name="dampingRatio">Damping ratio of motion</param>
		public static void Calc(ref float position, ref float velocity,
			float equilibriumPosition, float deltaTime, float angularFrequency, float dampingRatio)
		{
			var motionParams = CalcDampedSpringMotionParams(deltaTime, angularFrequency, dampingRatio);
			UpdateDampedSpringMotion(ref position, ref velocity, equilibriumPosition, motionParams);
		}

		/// <summary>
		/// Calculate a spring motion development for a given deltaTime
		/// </summary>
		/// <param name="position">"Live" position value</param>
		/// <param name="velocity">"Live" velocity value</param>
		/// <param name="equilibriumPosition">Goal (or rest) position</param>
		/// <param name="deltaTime">Time to update over</param>
		/// <param name="angularFrequency">Angular frequency of motion</param>
		/// <param name="dampingRatio">Damping ratio of motion</param>
		public static void Calc(ref Vector2 position, ref Vector2 velocity,
			Vector2 equilibriumPosition, float deltaTime, float angularFrequency, float dampingRatio)
		{
			var motionParams = CalcDampedSpringMotionParams(deltaTime, angularFrequency, dampingRatio);
			UpdateDampedSpringMotion(ref position.x, ref velocity.x, equilibriumPosition.x, motionParams);
			UpdateDampedSpringMotion(ref position.y, ref velocity.y, equilibriumPosition.y, motionParams);
		}

		/// <summary>
		/// Calculate a spring motion development for a given deltaTime
		/// </summary>
		/// <param name="position">"Live" position value</param>
		/// <param name="velocity">"Live" velocity value</param>
		/// <param name="equilibriumPosition">Goal (or rest) position</param>
		/// <param name="deltaTime">Time to update over</param>
		/// <param name="angularFrequency">Angular frequency of motion</param>
		/// <param name="dampingRatio">Damping ratio of motion</param>
		public static void Calc(ref Vector3 position, ref Vector3 velocity,
			Vector3 equilibriumPosition, float deltaTime, float angularFrequency, float dampingRatio)
		{
			var motionParams = CalcDampedSpringMotionParams(deltaTime, angularFrequency, dampingRatio);
			UpdateDampedSpringMotion(ref position.x, ref velocity.x, equilibriumPosition.x, motionParams);
			UpdateDampedSpringMotion(ref position.y, ref velocity.y, equilibriumPosition.y, motionParams);
			UpdateDampedSpringMotion(ref position.z, ref velocity.z, equilibriumPosition.z, motionParams);
		}

		/// <summary>
		/// Calculate a spring motion development for a given deltaTime
		/// </summary>
		/// <param name="position">"Live" position value</param>
		/// <param name="velocity">"Live" velocity value</param>
		/// <param name="equilibriumPosition">Goal (or rest) position</param>
		/// <param name="deltaTime">Time to update over</param>
		/// <param name="angularFrequency">Angular frequency of motion</param>
		/// <param name="dampingRatio">Damping ratio of motion</param>
		public static void Calc(ref Vector4 position, ref Vector4 velocity,
			Vector4 equilibriumPosition, float deltaTime, float angularFrequency, float dampingRatio)
		{
			var motionParams = CalcDampedSpringMotionParams(deltaTime, angularFrequency, dampingRatio);
			UpdateDampedSpringMotion(ref position.x, ref velocity.x, equilibriumPosition.x, motionParams);
			UpdateDampedSpringMotion(ref position.y, ref velocity.y, equilibriumPosition.y, motionParams);
			UpdateDampedSpringMotion(ref position.z, ref velocity.z, equilibriumPosition.z, motionParams);
			UpdateDampedSpringMotion(ref position.w, ref velocity.w, equilibriumPosition.z, motionParams);
		}

		/// <summary>
		/// Calculate a spring motion development for a given deltaTime
		/// </summary>
		/// <param name="position">"Live" position value</param>
		/// <param name="velocity">"Live" velocity value</param>
		/// <param name="equilibriumPosition">Goal (or rest) position</param>
		/// <param name="deltaTime">Time to update over</param>
		/// <param name="angularFrequency">Angular frequency of motion</param>
		/// <param name="dampingRatio">Damping ratio of motion</param>
		public static void Calc(ref Quaternion position, ref Quaternion velocity,
			Quaternion equilibriumPosition, float deltaTime, float angularFrequency, float dampingRatio)
		{
			var motionParams = CalcDampedSpringMotionParams(deltaTime, angularFrequency, dampingRatio);
			UpdateDampedSpringMotion(ref position.x, ref velocity.x, equilibriumPosition.x, motionParams);
			UpdateDampedSpringMotion(ref position.y, ref velocity.y, equilibriumPosition.y, motionParams);
			UpdateDampedSpringMotion(ref position.z, ref velocity.z, equilibriumPosition.z, motionParams);
			UpdateDampedSpringMotion(ref position.w, ref velocity.w, equilibriumPosition.z, motionParams);
		}
	}
}
