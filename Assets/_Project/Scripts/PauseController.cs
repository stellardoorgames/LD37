using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour {

    [SerializeField]
    private Canvas pausePanel;

    [SerializeField]
    private Rigidbody tentacleTracking;    

    public bool pauseEnabled = false;


    void Start()
        {
            pausePanel = pausePanel.GetComponent<Canvas>();
            pausePanel.enabled = false;
            pauseEnabled = false;
            Time.timeScale = 1;
            AudioListener.volume = 1;
            UnityEngine.Cursor.visible = false;
    }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (pauseEnabled == true)
                {
                    ContinueGame();
                    Debug.Log("unpause");
                }
                else if (pauseEnabled == false)
                {
                    PauseGame();
                    Debug.Log("pause");
                }

            }
        }

        private void PauseGame()
        {
            Time.timeScale = 0;
            pausePanel.enabled = true;
            pauseEnabled = true;
            // DisableControllers?

            AudioListener.volume = 0;
            UnityEngine.Cursor.visible = true;
        }

        private void ContinueGame()
        {

            Time.timeScale = 1;
            pausePanel.enabled = false;
            pauseEnabled = false;

            AudioListener.volume = 1;
            UnityEngine.Cursor.visible = false;
        }

        public void Exit()
        {
            //SceneManager.LoadScene("MainMenu");
        }
}