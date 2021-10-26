/************************************************************************************

Filename    :   CVirtHapticEmitter.cs
Content     :   The HapticEmitter emitts an animated haptic effect to be received from a HapticListener.
Created     :   August 8, 2014
Last Updated:	May 26, 2018
Authors     :   Lukas Pfeifhofer
				Stefan Radlwimmer

Copyright   :   Copyright 2020 Cyberith GmbH

Licensed under the AssetStore Free License and the AssetStore Commercial License respectively.

************************************************************************************/

using UnityEngine;

namespace CybSDK
{

    public class CVirtHapticEmitter : MonoBehaviour
    {

        [Tooltip("Reference to the Haptic Listener receiving Haptic Feedback. If not set will find one in scene")]
        public CVirtHapticListener hapticListener;

        [Tooltip("AutoStart haptic effect when enabled.")]
        [Rename("AutoStart Playing")]
        [SerializeField]
        protected bool autoStart = false;

        [Tooltip("Is currently playing haptic feedback.")]
        protected bool playing = false;

        [Tooltip("Restart the feedback after it ended.")]
        public bool loop = false;

        [Tooltip("Duration in seconds for a feedback loop.")]
        [SerializeField]
        private float duration = 3.0f;

        public float Duration
        {
            get { return duration; }
            set { duration = value; }
        }

        [Tooltip("Radius in meter the haptic feedback should spread.")]
        [Rename("Range")]
        [SerializeField]
        private float distance = 4.0f;

        public float Distance
        {
            get { return distance; }
            set { distance = value; }
        }

        [Tooltip("Frequency for the haptic unit.")]
        [Range(10.0f, 80.0f)]
        [SerializeField]
        private float frequency = 80.0f;

        public float Frequency
        {
            get { return frequency; }
            set
            {
                if (value < 10)
                    frequency = 10;
                if (value > 80)
                    frequency = 80;
            }
        }

        /// <summary>
        /// Keep Haptic Emitter active. Can be used to keep emitter active ignoring the emitters distance value
        /// </summary>
        [HideInInspector]
        public bool keepActive = false;

        [Tooltip("Animation Curve for feedback intensity over time [Normalized, volume/s].")]
        [Rename("Volume over Time")]
        public AnimationCurve forceOverTime = AnimationCurve.Linear(0, 1, 1, 1);

        [Tooltip("Animation Curve for feedback intensity distance [Normalized, volume/m].")]
        public AnimationCurve forceOverDistance = AnimationCurve.Linear(0, 1, 1, 0);

        private float timeStart;

        // Use this for initialization
        protected virtual void Start()
        {
            if (hapticListener == null)
            {
                hapticListener = FindObjectOfType<CVirtHapticListener>().GetComponent<CVirtHapticListener>();

                if (hapticListener == null)
                {
                    CLogger.LogWarning(string.Format("No CVirtHapticListener-Object set in CVirtHapticEmitter@'{0}'.", gameObject.name));
                    return;
                }
            }

            if (autoStart)
                Play();
        }

        void Update()
        {
            if (!playing)
                return;

            if (!loop)
            {
                float timeDelta = Time.time - timeStart;

                if (timeDelta > duration)
                {
                    Stop();
                }
            }
        }

        void OnEnable()
        {
            if (autoStart)
                Play();
        }

        void OnDisable()
        {
            Stop();
        }

        // Use this for deinitialization
        void OnDestroy()
        {
            Stop();
        }

        /// <summary>
        /// Start playing the Haptic Emitter by adding it to the Haptic Listener
        /// </summary>
        public void Play()
        {
            timeStart = Time.time;

            if (playing)
                return;

            playing = true;
            if (hapticListener != null)
                hapticListener.AddEmitter(this);
        }

        /// <summary>
        /// Stop playing the Haptic Emitter by removing it from the Haptic Listener
        /// </summary>
        public void Stop()
        {
            if (!playing)
                return;

            playing = false;
            if (hapticListener != null)
                hapticListener.RemoveEmitter(this);
        }

        /// <summary>
        /// Calculates the Volume (Strength) for this Emitter
        /// The volume depends on forceOverTime and forceOverDistance Animation Curves
        /// </summary>
        /// <param name="listenerPosition"> Position of the listener used for forceOverDistance calculation </param>
        /// <returns></returns>
        public virtual float EvaluateForce(Vector3 listenerPosition)
        {
            if (!isActiveAndEnabled || !playing)
                return 0f;

            float timeDelta = Time.time - timeStart;
            float timeInLoop = Mathf.Repeat(timeDelta, duration);

            //Evaluate time
            float forceTimeP = forceOverTime.Evaluate(timeInLoop / duration);

            //Evaluate distance
            float dist = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(listenerPosition.x, 0, listenerPosition.z)) / distance;
            float forceDistP = forceOverDistance.Evaluate(Mathf.Clamp01(dist));

            return forceTimeP * forceDistP;
        }
    }

}

