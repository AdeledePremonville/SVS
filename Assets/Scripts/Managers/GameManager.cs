using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Managers
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameObject player1;
        public GameObject player2;
        public GameObject player1HealthBar;
        public GameObject player2HealthBar;

        public Button leaveButton;
        public Button restartButton;


        public InputDevice Player1Device;
        public InputDevice Player2Device;
        public bool player1Ready;
        public bool player2Ready;
        public string player1Character;
        public string player2Character;

        public string gameMode; // "Solo", "Versus"

        private int roundNb = 1;
        private int maxRound = 3;
        public TextMeshProUGUI roundText;
        public TextMeshProUGUI mainText;
        private int newRoundCount = 3;
        public TextMeshProUGUI roundCountText;
        private bool isGameRunning = true;


        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                roundText.text = roundNb.ToString();
                player1HealthBar.GetComponent<Slider>().value = 100;
                leaveButton.gameObject.SetActive(false);
                restartButton.gameObject.SetActive(false);


                player2HealthBar.GetComponent<Slider>().value = 100;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Update()
        {
            Slider player1Health = player1HealthBar.GetComponent<Slider>();
            Slider player2Health = player2HealthBar.GetComponent<Slider>();

            if (isGameRunning)
                player1Health.value -= 1;
            if (player1Health.value <= 0 && isGameRunning)
            {
                mainText.text = "Player 1 won!";
                newRound();
            } else if (player2Health.value <= 0 && isGameRunning)
            {
                mainText.text = "Player 2 won!";
                newRound();

            }
        }
        private IEnumerator CountdownToNextRound()
        {
            int countdown = newRoundCount;
            while (countdown > 0)
            {
                roundCountText.text = "New round starting in " + countdown;
                yield return new WaitForSeconds(1f);
                countdown--;
            }

            // Start the new round after countdown
            roundNb += 1;
            roundText.text = roundNb.ToString();
            player1HealthBar.GetComponent<Slider>().value = 100;
            player2HealthBar.GetComponent<Slider>().value = 100;
            roundCountText.text = ""; // Clear countdown text
            mainText.text = ""; // Optionally clear winner text
            isGameRunning = true;
            StopCoroutine(CountdownToNextRound());
        }

        private void newRound() {
            if (roundNb < maxRound)
            {
                isGameRunning = false;
                StartCoroutine(CountdownToNextRound());
                //roundCountText.text = "New round starting in " + newRoundCount;
                //roundNb += 1;
                //player1HealthBar.GetComponent<Slider>().value = 100;
                //player2HealthBar.GetComponent<Slider>().value = 100;
                //roundText.text = roundNb.ToString();
            } else
            {
                //mainText.text = "End";
                leaveButton.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(true);

            }
        }

    }
    
}

