using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject allTowersSpawned;
    public GameObject shotMonstersText;
    public GameObject GameOverPanel;
    public GameObject inGameUIs;
    public GameObject shotCountInfo;
    public int shotMonsterCount;
    public GameObject countDownText;
    public GameObject beginningInfoPanel;
    public Animator animator;
    bool happenedOnce;
   
    void Start()
    {
     
        allTowersSpawned.SetActive(false);
        GameOverPanel.SetActive(false);
       
        //It should be set to "No" so that the state of the game changes after reload.
        PlayerPrefs.SetString("isGameEnded", "No");
        happenedOnce = false;
       
    }

    // Update is called once per frame
    void Update()
    {
        //Display countdown UI after the game info animation is ended.
        if (happenedOnce == false)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Beginning Animation") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                StartCoroutine(CountdownWait());
                happenedOnce = true;
            }
        }
       

        //StartCoroutine(CountdownWait());
        shotMonstersText.GetComponent<Text>().text = "Monster Shot: " + shotMonsterCount.ToString();

        //When a monster reaches end of the path, Game Over panel is displayed, in-game UIs are disabled as become redundant.
        if (PlayerPrefs.HasKey("isGameEnded"))
        {
            string result = PlayerPrefs.GetString("isGameEnded");
            if(result=="Yes")
            {
               GameOverPanel.SetActive(true);
               inGameUIs.SetActive(false);
               shotCountInfo.GetComponent<Text>().text = "You shot " + shotMonsterCount.ToString() + " aliens until one reached to the spaceship";

            }
        }
    }

    //Called onButtonClick
    public void TryAgainButton()
    {
        SceneManager.LoadScene("Tower Defense Game");
    }

    //Countdown animation once the game info panel is disabled. 
    IEnumerator CountdownWait()
    {
        //yield return new WaitForSeconds(1f);
        countDownText.GetComponent<Text>().text = "3";
        countDownText.SetActive(true);

        yield return new WaitForSeconds(1f);
        countDownText.SetActive(false);
        countDownText.GetComponent<Text>().text = "2";
        countDownText.SetActive(true);

        yield return new WaitForSeconds(1f);
        countDownText.SetActive(false);
        countDownText.GetComponent<Text>().text = "1";
        countDownText.SetActive(true);

        yield return new WaitForSeconds(1f);
        countDownText.SetActive(false);

    }
}
