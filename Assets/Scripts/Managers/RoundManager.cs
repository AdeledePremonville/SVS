using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class RoundManager : MonoBehaviour
    {
        public GameObject player1;
        public GameObject player2;

        public Button leaveButton;
        public Button restartButton;

        private int roundNb = 1;
        private int maxRound = 3;
        public TextMeshProUGUI roundText;
        public TextMeshProUGUI mainText;
        private int newRoundCount = 3;
        public TextMeshProUGUI roundCountText;
        private bool isGameRunning = true;


        private void Awake()
        {
            roundText.text = roundNb.ToString();
            leaveButton.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(false);


            //player1HealthBar.GetComponent<Slider>().value = 100;
            //player2HealthBar.GetComponent<Slider>().value = 100;
            // DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            int player1Health = player1.GetComponent<Health>().currentHealth;
            int player2Health = player2.GetComponent<Health>().currentHealth;

            Debug.Log("player 2 helath " + player2Health.ToString() + " is game running" + isGameRunning);
            //if (isGameRunning)
            //    player1Health.value -= 1;
            if (player1Health <= 0 && isGameRunning)
            {
                mainText.text = player2.name + " won!";
                NewRound();
            }
            else if (player2Health <= 0 && isGameRunning)
            {
                mainText.text = player1.name + " won!";
                NewRound();

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
            //player1HealthBar.GetComponent<Slider>().value = 100;
            //player2HealthBar.GetComponent<Slider>().value = 100;
            roundCountText.text = ""; // Clear countdown text
            mainText.text = ""; // Optionally clear winner text
            isGameRunning = true;
            StopCoroutine(CountdownToNextRound());
        }

        private void NewRound()
        {
            player1.GetComponent<Health>().currentHealth = 100;
            player2.GetComponent<Health>().currentHealth = 100;
            if (roundNb < maxRound)
            {
                isGameRunning = false;
                StartCoroutine(CountdownToNextRound());
            }
            else
            {
                leaveButton.gameObject.SetActive(true);
                restartButton.gameObject.SetActive(true);

            }
        }
    }
}