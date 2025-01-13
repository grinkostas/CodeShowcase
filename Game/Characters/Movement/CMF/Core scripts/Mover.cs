using System;
using UnityEngine;
using System.Collections;
using Game.Characters.Api;

namespace CMF
{
	public class Mover : MonoBehaviour {
		
		[Header("Mover Options :")]
		[Range(0f, 1f)][SerializeField] float stepHeightRatio = 0.25f;
		[Header("Collider Options :")]
		[SerializeField] float colliderHeight = 2f;
		[SerializeField] float colliderThickness = 1f;
		[SerializeField] Vector3 colliderOffset = Vector3.zero;
		
		[Header("Sensor Options :")]
		[SerializeField] public Sensor.CastType sensorType = Sensor.CastType.Raycast;
		private float sensorRadiusModifier = 0.8f;
		private int currentLayer;
		[SerializeField] bool isInDebugMode = false;
		[Header("Sensor Array Options :")]
		[SerializeField] [Range(1, 5)] int sensorArrayRows = 1;
		[SerializeField] [Range(3, 10)] int sensorArrayRayCount = 6;
		[SerializeField] bool sensorArrayRowsAreOffset = false;

		[HideInInspector] public Vector3[] raycastArrayPreviewPositions;

		bool isGrounded = false;

		bool IsUsingExtendedSensorRange  = true;
		float baseSensorRange = 0f;

		Vector3 currentGroundAdjustmentVelocity = Vector3.zero;

		private IRigidBodyUser _rigidBodyUser;
		public IRigidBodyUser rbUser => _rigidBodyUser ??= GetComponentInParent<IRigidBodyUser>(true);

		private Collider col => rbUser.col;
		private Rigidbody rig => rbUser.rb;
		private Transform tr => rbUser.rb.transform;
		
		private Sensor sensor;

		private void Start()
		{
			Setup();
			
			sensor = new Sensor(this.tr, col);
			RecalculateColliderDimensions();
			RecalibrateSensor();
		}

		private void Reset () {
			Setup();
		}

		private void OnValidate()
		{
			if(this.gameObject.activeInHierarchy)
				RecalculateColliderDimensions();

			if(sensorType == Sensor.CastType.RaycastArray)
				raycastArrayPreviewPositions = 
					Sensor.GetRaycastStartPositions(sensorArrayRows, sensorArrayRayCount, sensorArrayRowsAreOffset, 1f);
		}

		private void Setup()
		{
			rig.freezeRotation = true;
			rig.useGravity = false;
		}

		private void LateUpdate()
		{
			if(isInDebugMode)
				sensor.DrawDebug();
		}

		public void RecalculateColliderDimensions()
		{
			if(col == null)
			{
				Setup();

				if(col == null)
				{
					Debug.LogWarning("There is no collider attached to " + this.gameObject.name + "!");
					return;
				}				
			}

			if(col is BoxCollider)
			{
				var boxCollider = (BoxCollider)col;
				Vector3 size = Vector3.zero;
				size.x = colliderThickness;
				size.z = colliderThickness;

				boxCollider.center = colliderOffset * colliderHeight;

				size.y = colliderHeight * (1f - stepHeightRatio);
				boxCollider.size = size;

				boxCollider.center = boxCollider.center + new Vector3(0f, stepHeightRatio * colliderHeight/2f, 0f);
			}
			else if(col is SphereCollider)
			{
				var sphereCollider = (SphereCollider)col;
				sphereCollider.radius = colliderHeight/2f;
				sphereCollider.center = colliderOffset * colliderHeight;

				sphereCollider.center = sphereCollider.center + new Vector3(0f, stepHeightRatio * sphereCollider.radius, 0f);
				sphereCollider.radius *= (1f - stepHeightRatio);
			}
			else if(col is CapsuleCollider)
			{
				var capsuleCollider = (CapsuleCollider)col;
				capsuleCollider.height = colliderHeight;
				capsuleCollider.center = colliderOffset * colliderHeight;
				capsuleCollider.radius = colliderThickness/2f;

				capsuleCollider.center = capsuleCollider.center + new Vector3(0f, stepHeightRatio * capsuleCollider.height/2f, 0f);
				capsuleCollider.height *= (1f - stepHeightRatio);

				if(capsuleCollider.height/2f < capsuleCollider.radius)
					capsuleCollider.radius = capsuleCollider.height/2f;
			}

			if(sensor != null)
				RecalibrateSensor();
		}

		private void RecalibrateSensor()
		{
			sensor.SetCastOrigin(GetColliderCenter());
			sensor.SetCastDirection(Sensor.CastDirection.Down);

			RecalculateSensorLayerMask();

			sensor.castType = sensorType;

			float radius = colliderThickness/2f * sensorRadiusModifier;

			float safetyDistanceFactor = 0.001f;

			if(col is BoxCollider)
				radius = Mathf.Clamp(radius, safetyDistanceFactor, ((BoxCollider)col).size.y/2f) * (1f - safetyDistanceFactor);
			else if(col is SphereCollider)
				radius = Mathf.Clamp(radius, safetyDistanceFactor, ((SphereCollider)col).radius * (1f - safetyDistanceFactor));
			else if(col is CapsuleCollider)
				radius = Mathf.Clamp(radius, safetyDistanceFactor, (((CapsuleCollider)col).height/2f) * (1f - safetyDistanceFactor));

			sensor.sphereCastRadius = radius * tr.localScale.x;

			float length = 0f;
			length += (colliderHeight * (1f - stepHeightRatio)) * 0.5f;
			length += colliderHeight * stepHeightRatio;
			baseSensorRange = length * (1f + safetyDistanceFactor) * tr.localScale.x;
			sensor.castLength = length * tr.localScale.x;

			sensor.ArrayRows = sensorArrayRows;
			sensor.arrayRayCount = sensorArrayRayCount;
			sensor.offsetArrayRows = sensorArrayRowsAreOffset;
			sensor.isInDebugMode = isInDebugMode;

			sensor.calculateRealDistance = true;
			sensor.calculateRealSurfaceNormal = true;

			sensor.RecalibrateRaycastArrayPositions();
		}

		private void RecalculateSensorLayerMask()
		{
			int layerMask = 0;
			int objectLayer = this.gameObject.layer;
 
            for (int i = 0; i < 32; i++)
            {
                if (!Physics.GetIgnoreLayerCollision(objectLayer, i)) 
					layerMask = layerMask | (1 << i);
			}

			if(layerMask == (layerMask | (1 << LayerMask.NameToLayer("Ignore Raycast"))))
			{
				layerMask ^= (1 << LayerMask.NameToLayer("Ignore Raycast"));
			}
 
            sensor.layermask = layerMask;

			currentLayer = objectLayer;
		}

		private Vector3 GetColliderCenter()
		{
			if(col == null)
				Setup();

			return col.bounds.center;
		}

		private void Check()
		{
			currentGroundAdjustmentVelocity = Vector3.zero;

			if(IsUsingExtendedSensorRange)
				sensor.castLength = baseSensorRange + (colliderHeight * tr.localScale.x) * stepHeightRatio;
			else
				sensor.castLength = baseSensorRange;
			
			sensor.Cast();

			if(!sensor.HasDetectedHit())
			{
				isGrounded = false;
				return;
			}

			isGrounded = true;

			float distance = sensor.GetDistance();

			float upperLimit = ((colliderHeight * tr.localScale.x) * (1f - stepHeightRatio)) * 0.5f;
			float middle = upperLimit + (colliderHeight * tr.localScale.x) * stepHeightRatio;
			float distanceToGo = middle - distance;

			currentGroundAdjustmentVelocity = tr.up * (distanceToGo/Time.fixedDeltaTime);
		}
		
		public void CheckForGround()
		{
			if(currentLayer != this.gameObject.layer)
				RecalculateSensorLayerMask();

			Check();
		}

		public void SetVelocity(Vector3 _velocity)
		{
			rig.velocity = _velocity + currentGroundAdjustmentVelocity;	
		}	

		public bool IsGrounded()
		{
			return isGrounded;
		}
		
		public void SetExtendSensorRange(bool isExtended)
		{
			IsUsingExtendedSensorRange = isExtended;
		}

		public void SetColliderHeight(float newColliderHeight)
		{
			if(colliderHeight == newColliderHeight)
				return;

			colliderHeight = newColliderHeight;
			RecalculateColliderDimensions();
		}
		
		public void SetColliderThickness(float newColliderThickness)
		{
			if(Math.Abs(colliderThickness - newColliderThickness) < 0.01f)
				return;

			if(newColliderThickness < 0f)
				newColliderThickness = 0f;

			colliderThickness = newColliderThickness;
			RecalculateColliderDimensions();
		}

		public void SetStepHeightRatio(float newStepHeightRatio)
		{
			newStepHeightRatio = Mathf.Clamp(newStepHeightRatio, 0f, 1f);
			stepHeightRatio = newStepHeightRatio;
			RecalculateColliderDimensions();
		}

		public Vector3 GetGroundNormal()
		{
			return sensor.GetNormal();
		}

		public Vector3 GetGroundPoint()
		{
			return sensor.GetPosition();
		}

		public Collider GetGroundCollider()
		{
			return sensor.GetCollider();
		}
		
	}
}
