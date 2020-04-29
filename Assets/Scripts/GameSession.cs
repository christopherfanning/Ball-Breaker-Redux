using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{

    [Range(0.1f, 10f)] [SerializeField] float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 83;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] int currentScore = 0;
    [SerializeField] bool isAutoplayEnabled;
    
private void Awake() 
{
    // Singleton Method 
   int  gameStatusCount = FindObjectsOfType<GameSession>().Length;
    // Debug.Log(gameStatusCount);
   if (gameStatusCount > 1){
       gameObject.SetActive(false);
       Destroy(gameObject);
   }
   else{
       DontDestroyOnLoad(gameObject);
   }
}

    private void Start() {
        scoreText.text = currentScore.ToString();
        Application.targetFrameRate = 60;
    }



    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore(){
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();

    }
    
    public void RestartGame(){
        Destroy(gameObject);
    }

    public bool IsAutoPlayEnabled(){
        return isAutoplayEnabled;
    }
    

}
