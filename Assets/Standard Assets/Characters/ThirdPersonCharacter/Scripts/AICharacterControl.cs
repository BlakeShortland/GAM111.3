using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for

		public GameObject[] waypoints;
		public GameObject player;

		int targetID = 0;

		AudioSource audioSource;

		[SerializeField] AudioClip intro;
		[SerializeField] AudioClip outro;
		[SerializeField] AudioClip[] objectiveComplete;
		[SerializeField] AudioClip[] takingTooLong;

		private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
			audioSource = GetComponent<AudioSource>();

	        agent.updateRotation = false;
	        agent.updatePosition = true;

			player = GameObject.FindGameObjectWithTag("Player");

			NextTarget();

			StartCoroutine(TakingTooLong());
		}


        private void Update()
        {
            if (target != null)
                agent.SetDestination(target.position);

            if (agent.remainingDistance > agent.stoppingDistance)
                character.Move(agent.desiredVelocity, false, false);
            else
                character.Move(Vector3.zero, false, false);
        }

		private void OnTriggerEnter(Collider collision)
		{
			if (collision.transform.name == "Waypoint1")
			{
				PlayAudio(intro);
			}

			if (collision.transform.name == "Waypoint1 (7)")
			{
				PlayAudio(outro);
				StartCoroutine(EndGame());
			}
		}

		private void OnTriggerStay(Collider collision)
		{
			if (collision.transform.tag == "Waypoint")
			{
				Vector3 targetDir = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z) - transform.position;

				float step = agent.angularSpeed * Time.deltaTime;

				Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);

				transform.rotation = Quaternion.LookRotation(newDir);
			}
		}

		private void OnTriggerExit(Collider collision)
		{
			if (collision.transform.name == "Waypoint1")
			{
				PlayAudio(objectiveComplete[0]);
			}

			if (collision.transform.name == "Waypoint1 (3)")
			{
				PlayAudio(objectiveComplete[1]);
			}
		}

		public void SetTarget(Transform target)
        {
            this.target = target;
        }


		public void NextTarget()
		{
			SetTarget(waypoints[targetID].transform);
			if (targetID <= waypoints.Length)
				targetID++;
		}

		IEnumerator TakingTooLong()
		{
			foreach (AudioClip audioClip in takingTooLong)
			{
				yield return new WaitForSeconds(15);

				yield return new WaitWhile(IsAudioPlaying);

				PlayAudio(audioClip);
			}
		}

		bool IsAudioPlaying()
		{
			if (audioSource.isPlaying)
				return true;
			else
				return false;
		}

		void PlayAudio(AudioClip audioClip)
		{
			audioSource.Stop();
			audioSource.clip = audioClip;
			audioSource.Play();
		}

		IEnumerator EndGame()
		{
			yield return new WaitWhile(IsAudioPlaying);
			Application.Quit();
		}
    }
}
