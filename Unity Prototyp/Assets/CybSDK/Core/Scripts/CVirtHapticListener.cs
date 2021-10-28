/************************************************************************************

Filename    :   CVirtHapticListener.cs
Content     :   The HapticListener maintains a list of all playing HapticEmitters and evaluates the resulting haptic feedback.
Created     :   August 8, 2014
Last Updated:	May 26, 2019
Authors     :   Lukas Pfeifhofer
				Stefan Radlwimmer

Copyright   :   Copyright 2020 Cyberith GmbH

Licensed under the AssetStore Free License and the AssetStore Commercial License respectively.

************************************************************************************/

using UnityEngine;
using System.Collections.Generic;

namespace CybSDK
{
	[RequireComponent(typeof(CVirtDeviceController))]
	public class CVirtHapticListener : MonoBehaviour
	{
		private List<CVirtHapticEmitter> emitters = new List<CVirtHapticEmitter>();

		private CVirtDeviceController deviceController;

        [Tooltip("Maximum range of the listener. All emitters inside this range are considered by this listener.")]
        public int maxRange = 60;

		void Awake()
		{
			//Check if this object has a CVirtDeviceController attached
			deviceController = GetComponent<CVirtDeviceController>();
			if (deviceController == null)
			{
				CLogger.LogError("CVirtHapticListener gameobject does not have a CVirtDeviceController attached.");
				this.enabled = false;
				return;
			}

			deviceController.OnCVirtDeviceControllerCallback += OnCVirtDeviceControllerCallback;
        }

		private void OnCVirtDeviceControllerCallback(IVirtDevice virtDevice, CVirtDeviceController.CVirtDeviceControllerCallbackType callbackType)
		{
			if (deviceController == null || !deviceController.IsHapticEnabled())
				return;

			if (!virtDevice.HasHaptic())
				return;

			switch (callbackType)
			{
                case CVirtDeviceController.CVirtDeviceControllerCallbackType.Connect:
                    virtDevice.HapticSetGain(3);
                    virtDevice.HapticSetFrequency(0);
                    virtDevice.HapticSetVolume(0);
                    virtDevice.HapticPlay();
					break;

				case CVirtDeviceController.CVirtDeviceControllerCallbackType.Disconnect:
					virtDevice.HapticStop();
					break;
			}
		}

		// Update is called once per frame
		void Update()
		{
			IVirtDevice virtDevice = deviceController.GetDevice();

			if (virtDevice == null || !virtDevice.IsOpen())
				return;

			if (!virtDevice.HasHaptic())
				return;

            if (emitters.Count == 0)
                return;

            float sumForce = 0;
            float sumFrequency = 0;
            int emittersInRange = 0;

            foreach (CVirtHapticEmitter emitter in emitters)
			{
                float distance = Vector3.Distance(transform.position, emitter.transform.position);
                // Check if emitter is inside the listeners range and inside the emitters range or the emitter is kept alive ignoring the distance
                if (distance < maxRange && (distance < emitter.Distance || emitter.keepActive))
                {
                    float force = emitter.EvaluateForce(transform.position) * 100;
				    sumForce += force;

                    sumFrequency += emitter.Frequency;

                    ++emittersInRange;
                }

            }

            if (emittersInRange == 0)
            {
                virtDevice.HapticSetVolume(0);
                return;
            }

            sumFrequency /= emittersInRange;

            //Haptic Unit has resonance frequency between 35 - 45Hz. Therefore this frequencies are filtered out and set to 34 or 46Hz respectively
            if ((int)sumFrequency >= 35 && (int)sumFrequency <= 39)
                sumFrequency = 34;
            if ((int)sumFrequency >= 40 && (int)sumFrequency <= 45)
                sumFrequency = 46;

            virtDevice.HapticSetFrequency((int)sumFrequency);
            virtDevice.HapticSetVolume((int)sumForce / emittersInRange);
        }

        void OnEnable()
        {
            deviceController = GetComponent<CVirtDeviceController>();

            deviceController.OnCVirtDeviceControllerCallback += OnCVirtDeviceControllerCallback;
        }

		// This function is called when the behaviour becomes disabled or inactive
		void OnDisable()
		{
			deviceController.OnCVirtDeviceControllerCallback -= OnCVirtDeviceControllerCallback;
		}

		private float SumUpDecibel(float a, float b)
		{
			float powA = Mathf.Pow(a / 10.0f, 10.0f);
			float powB = Mathf.Pow(b / 10.0f, 10.0f);

			float powSum = powA + powB;
			return 10.0f * Mathf.Log10(powSum);
		}

        /// <summary>
        /// Adds a Haptic Emitter to the list of active emitters for this Haptic Listener
        /// </summary>
        /// <param name="emitter"> Emitter to add to this listener </param>
		public void AddEmitter(CVirtHapticEmitter emitter)
		{

            emitters.Add(emitter);
        }

        /// <summary>
        /// Remove a Haptic Emitter from the list of active emitters for this Haptic Listener
        /// </summary>
        /// <param name="emitter"> Emitter to remove from this listener </param>
		public void RemoveEmitter(CVirtHapticEmitter emitter)
		{
            emitters.Remove(emitter);

            if (emitters.Count == 0)
            {
                IVirtDevice virtDevice = deviceController.GetDevice();

                if (virtDevice == null || !virtDevice.IsOpen())
                    return;

                if (!virtDevice.HasHaptic())
                    return;

                virtDevice.HapticSetVolume(0);
            }
        }
	}
}
