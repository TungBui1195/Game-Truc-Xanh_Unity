using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    // Declare variables to manage button
    [SerializeField]
    private Sprite backgroundImg;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text timesToPlay;
    public float gameTimer = 0f;

    public List<Button> btnList = new List<Button>();

    public Sprite[] SourceSprites;
    public List<Sprite> GameSprites = new List<Sprite>();

    public bool firstGuess, secondGuess = false;
    public string firstName, secondName;
    public int firstIndex, secondIndex;
    public int numberOfSelections, ExpectTimes = 0;

    void Awake()
    {
        SourceSprites = Resources.LoadAll<Sprite>("Sprites/GameImg");
    }

    // Start is called before the first frame update
    void Start()
    {
        GetButtons();
        AddSprites();
        Shuffle(GameSprites);
        AddListener();
       
    }

    void Update()
    {
        
        gameTimer += Time.deltaTime;
        int remainingTimes = (int)(30 - gameTimer);
        timesToPlay.text = "Remaining Times(Second): " + remainingTimes.ToString();
        if (remainingTimes == 0 || ExpectTimes == (btnList.Count) / 2)
        {
            SceneManager.LoadScene("EndScene");
            Debug.Log("Actual number of times to complete this game is " + numberOfSelections);
            Debug.Log("Expect number of times to complete this game is " + ExpectTimes);
        }
    }

    void AddSprites()
    {
        int size = btnList.Count;
        int index = 0;
        for (int i = 0; i < size; i++)
        {
            if (i == size / 2)
                index = 0;
            GameSprites.Add(SourceSprites[index]);
            index++;
        }
    }

    void GetButtons()
    {
        // lay het cac button hien co them vao list
        GameObject[] objects = GameObject.FindGameObjectsWithTag("PuzzleButton");
        for (int i = 0; i < objects.Length; i++)
        {
            btnList.Add(objects[i].GetComponent<Button>());
            btnList[i].image.sprite = backgroundImg;
        }
    }

    void AddListener()
    {
        foreach (Button btn in btnList)
        {
            btn.onClick.AddListener(() => PickPuzzle());
        }
    }

    void PickPuzzle()
    {
        //string name = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;
        //Debug.Log("This button was clicked: " + name);
        if (!firstGuess)
        {
            firstGuess = true;
            firstIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            firstName = GameSprites[firstIndex].name;
            btnList[firstIndex].image.sprite = GameSprites[firstIndex];
            Debug.Log("1st index: " + firstIndex + " 1st name= " + firstName);
        }
        else if (!secondGuess)
        {
            numberOfSelections++;
            secondGuess = true;
            secondIndex = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name);
            secondName = GameSprites[secondIndex].name;
            btnList[secondIndex].image.sprite = GameSprites[secondIndex];
            Debug.Log("2st index: " + secondIndex + " 2st name= " + secondName);
            StartCoroutine(CheckPuzzleMatch());

        }
        

    }
    IEnumerator CheckPuzzleMatch()
    {
        yield return new WaitForSeconds(0.5f);
        if (firstName == secondName && firstIndex != secondIndex)
        {
            ExpectTimes++;
            scoreText.text = "Score:" +  ExpectTimes.ToString();
            btnList[firstIndex].interactable = false;
            btnList[secondIndex].interactable = false;
            btnList[firstIndex].image.color = Color.clear;
            btnList[secondIndex].image.color = Color.clear;

        }
        else
        {
            btnList[firstIndex].image.sprite = backgroundImg;
            btnList[secondIndex].image.sprite = backgroundImg;
        }
        firstGuess = secondGuess = false;
    }

    void Shuffle(List<Sprite> shuffleList)
    {
        Sprite temp;
        for (int i =0; i < btnList.Count; i++)
        {
            temp = shuffleList[i];
            int random = Random.Range(i, btnList.Count);
            shuffleList[i] = shuffleList[random];
            shuffleList[random] = temp;
        }
    }
}