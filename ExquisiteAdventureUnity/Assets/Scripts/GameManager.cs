using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("General")]
    public GameObject sentencePanel;
    public GameObject choicesPanel;
    public GameObject wordPrefab;
    public GameObject premadeSentencePrefab;
    public GameObject nextButton;
    
    [Header("Words lists")]
    public List<string> originalSentence;
    public List<string> currentSentence = new List<string>();
    public List<string> subject;
    public List<string> pastVerb;
    public List<string> infVerb;
    public List<string> complement;
    public List<string> location;
    public List<string> pPresentVerb;
    public List<string> pPastVerb;
    public List<string> bodypart;
    public List<string> adjective;

    private string chosenWord;
    private string subjectChoosed;
    private string complementChoosed;
    private string infVerbChoosed;
    private string bodypartChoosed;

    [Header("Sentences")]
    public string beginSentence;
    public string thenSentence;
    public string withCourageSentence;
    public string efficacitySentence;

    private int index = 0;
    private int boxIndex = 0;

    private enum CHOICE
    {
        WORD_1,
        WORD_2,
        WORD_3,
        WORD_4,
        DEFAULT
    }
    private CHOICE choice;

    private enum STORYPART
    {
        PART_0,
        PART_1,
        PART_2,
        PART_3
    }
    private STORYPART storyPart;

    private enum WORDTYPE
    {
        SUBJECT,
        PASTVERB,
        COMPLEMENT,
        LOCATION,
        INFVERB,
        P_PASTVERB,
        P_PRESENTVERB,
        ADJECTIVE,
        BODYPART
    }
    private WORDTYPE currentWordType;

    #region Singleton Pattern
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }
    #endregion

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        choice = CHOICE.DEFAULT;
        storyPart = STORYPART.PART_0;

        DisplayNewStoryPartUI(storyPart);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void UpdateButtonText()
    {
        index = 0;
        switch (currentWordType)
        {
            case WORDTYPE.SUBJECT:
                foreach(Transform buttonGO in choicesPanel.transform)
                {
                    buttonGO.gameObject.GetComponentInChildren<Text>().text = subject[index];
                    index++;
                }
                break;
            case WORDTYPE.PASTVERB:
                foreach (Transform buttonGO in choicesPanel.transform)
                {
                    buttonGO.gameObject.GetComponentInChildren<Text>().text = pastVerb[index];
                    index++;
                }
                break;
            case WORDTYPE.COMPLEMENT:
                foreach (Transform buttonGO in choicesPanel.transform)
                {
                    buttonGO.gameObject.GetComponentInChildren<Text>().text = complement[index];
                    index++;
                }
                break;
            case WORDTYPE.LOCATION:
                foreach (Transform buttonGO in choicesPanel.transform)
                {
                    buttonGO.gameObject.GetComponentInChildren<Text>().text = location[index];
                    index++;
                }
                break;
        }
    }

    public void ChooseWord(int choiceIndex)
    {
        switch (storyPart)
        {
            case STORYPART.PART_0:
                break;
            
            case STORYPART.PART_1:
                if (currentWordType == WORDTYPE.SUBJECT)
                {
                    currentSentence.Add(subject[choiceIndex]);
                    subjectChoosed = subject[choiceIndex];
                    chosenWord = subject[choiceIndex];
                    currentWordType = WORDTYPE.PASTVERB;
                }
                else if (currentWordType == WORDTYPE.PASTVERB)
                {
                    currentSentence.Add(pastVerb[choiceIndex]);
                    chosenWord = pastVerb[choiceIndex];
                    currentWordType = WORDTYPE.COMPLEMENT;
                }
                else if (currentWordType == WORDTYPE.COMPLEMENT)
                {
                    currentSentence.Add(complement[choiceIndex]);
                    chosenWord = complement[choiceIndex];
                    currentWordType = WORDTYPE.LOCATION;
                }
                else if (currentWordType == WORDTYPE.LOCATION)
                {
                    currentSentence.Add(location[choiceIndex]);
                    chosenWord = location[choiceIndex];
                    currentWordType = WORDTYPE.PASTVERB;
                    DisableChoiceButtons();
                    nextButton.SetActive(true);
                }

                FillWithNewWord(chosenWord, boxIndex);
                boxIndex++;
                break;
            
            case STORYPART.PART_2:
                break;
            
            case STORYPART.PART_3:
                if (currentWordType == WORDTYPE.INFVERB)
                {
                    currentSentence.Add(subject[choiceIndex]);
                    currentWordType = WORDTYPE.P_PRESENTVERB;
                }
                else if (currentWordType == WORDTYPE.P_PRESENTVERB)
                {
                    currentSentence.Add(pastVerb[choiceIndex]);
                    currentWordType = WORDTYPE.BODYPART;
                }
                else if (currentWordType == WORDTYPE.BODYPART)
                {
                    currentSentence.Add(complement[choiceIndex]);
                    currentWordType = WORDTYPE.ADJECTIVE;
                }
                else if (currentWordType == WORDTYPE.ADJECTIVE)
                {
                    currentSentence.Add(location[choiceIndex]);
                    currentWordType = WORDTYPE.P_PASTVERB;
                    nextButton.SetActive(true);
                }

                FillWithNewWord(chosenWord, boxIndex);
                boxIndex++;
                break;
        }

        UpdateButtonText();
    }

    public void NextStep()
    {
        switch(storyPart)
        {
            case STORYPART.PART_0:
                storyPart = STORYPART.PART_1;
                break;
            case STORYPART.PART_1:
                storyPart = STORYPART.PART_2;
                break;
            case STORYPART.PART_2:
                storyPart = STORYPART.PART_3;
                break;
            case STORYPART.PART_3:
                Debug.Log("Plus de storypart à afficher");
                break;
        }

        nextButton.SetActive(false);
        DisplayNewStoryPartUI(storyPart);
    }
    private void DisplayNewStoryPartUI(STORYPART _storyPart)
    {
        DisplaySlots(_storyPart);
        FillPremadeSentence(_storyPart);
        boxIndex = 0;

        //Active ou desactive les boutons de choix
        switch (_storyPart)
        {
            case STORYPART.PART_0:
                DisableChoiceButtons();
                nextButton.SetActive(true);
                break;
            case STORYPART.PART_1:
                EnableChoiceButtons();
                break;
            case STORYPART.PART_2:
                DisableChoiceButtons();
                nextButton.SetActive(true);
                break;
            case STORYPART.PART_3:
                EnableChoiceButtons();
                break;

        }

        UpdateButtonText();
    }

    private void DisplaySlots(STORYPART _storyPart)
    {
        ClearText();

        switch (_storyPart)
        {
            case STORYPART.PART_0:
                Instantiate(premadeSentencePrefab, sentencePanel.transform);
                break;
            case STORYPART.PART_1:
                for(int i = 0; i < 4; i++)
                {
                    Instantiate(wordPrefab, sentencePanel.transform);
                }
                break;
            case STORYPART.PART_2:
                Instantiate(premadeSentencePrefab, sentencePanel.transform);
                break;
            case STORYPART.PART_3:
                Instantiate(wordPrefab, sentencePanel.transform);
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(premadeSentencePrefab, sentencePanel.transform);
                }
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(wordPrefab, sentencePanel.transform);
                }
                break;
        }
    }

    private List<GameObject> GetTextBoxList()
    {
        List<GameObject> textBoxList = new List<GameObject>();
        foreach(Transform textBox in sentencePanel.transform)
        {
            textBoxList.Add(textBox.gameObject);
        }
        return textBoxList;
    }

    private void FillPremadeSentence(STORYPART _storyPart)
    {
        switch (_storyPart)
        {
            case STORYPART.PART_0:
                GetTextBoxList()[0].GetComponent<Text>().text = beginSentence;
                break;
            case STORYPART.PART_1:
                break;
            case STORYPART.PART_2:
                if(GetTextBoxList()[0] != null)
                    GetTextBoxList()[0].GetComponent<Text>().text = thenSentence;
                break;
            case STORYPART.PART_3:
                GetTextBoxList()[1].GetComponent<Text>().text = subjectChoosed;
                GetTextBoxList()[2].GetComponent<Text>().text = "en";
                break;
        }
    }

    private void FillWithNewWord(string _chosenWord, int _textBoxIndex)
    {
        GetTextBoxList()[_textBoxIndex].GetComponentInChildren<Text>().text = _chosenWord;
    }

    private void ClearText()
    {
        foreach (Transform child in sentencePanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
    

    private void DisableChoiceButtons()
    {
        foreach(Transform button in choicesPanel.transform)
        {
            button.gameObject.SetActive(false);
        }
    }

    private void EnableChoiceButtons()
    {
        foreach (Transform button in choicesPanel.transform)
        {
            button.gameObject.SetActive(true);
        }
    }

}
