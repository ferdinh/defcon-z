using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefconZ;

namespace DefconZ
{
    public class GameSound : MonoBehaviour
    {
        // Start is called before the first frame update
        private AudioSource audioSource;
        public AudioClip attackswing;
        public GameManager player;


        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        public void playAttack()
        {
            audioSource.clip = attackswing;
            audioSource.Play();
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
