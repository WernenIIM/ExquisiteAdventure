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

    private List<GameObject> textBoxList = new List<GameObject>();
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
            case WORDTYPE.INFVERB:
                foreach (Transform buttonGO in choicesPanel.transform)
                {
                    buttonGO.gameObject.GetComponentInChildren<Text>().text = infVerb[index];
                    index++;
                }
                break;
            case WORDTYPE.P_PRESENTVERB:
                foreach (Transform buttonGO in choicesPanel.transform)
                {
                    buttonGO.gameObject.GetComponentInChildren<Text>().text = pPresentVerb[index];
                    index++;
                }
                break;
            case WORDTYPE.BODYPART:
                foreach (Transform buttonGO in choicesPanel.transform)
                {
                    buttonGO.gameObject.GetComponentInChildren<Text>().text = bodypart[index];
                    index++;
                }
                break;
            case WORDTYPE.ADJECTIVE:
                foreach (Transform buttonGO in choicesPanel.transform)
                {
                    buttonGO.gameObject.GetComponentInChildren<Text>().text = adjective[index];
                    index++;
                }
                break;
            case WORDTYPE.P_PASTVERB:
                foreach (Transform buttonGO in choicesPanel.transform)
                {
                    buttonGO.gameObject.GetComponentInChildren<Text>().text = pPastVerb[index];
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
                    currentWordType = WORDTYPE.INFVERB;
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
                    currentSentence.Add(infVerb[choiceIndex]);
                    chosenWord = infVerb[choiceIndex];
                    currentWordType = WORDTYPE.P_PRESENTVERB;
                }
                else if (currentWordType == WORDTYPE.P_PRESENTVERB)
                {
                    currentSentence.Add(pPresentVerb[choiceIndex]);
                    chosenWord = pPresentVerb[choiceIndex];
                    currentWordType = WORDTYPE.BODYPART;
                }
                else if (currentWordType == WORDTYPE.BODYPART)
                {
                    currentSentence.Add(bodypart[choiceIndex]);
                    chosenWord = bodypart[choiceIndex];
                    currentWordType = WORDTYPE.ADJECTIVE;
                }
                else if (currentWordType == WORDTYPE.ADJECTIVE)
                {
                    currentSentence.Add(adjective[choiceIndex]);
                    chosenWord = adjective[choiceIndex];
                    currentWordType = WORDTYPE.P_PASTVERB;
                    DisableChoiceButtons();
                    nextButton.SetActive(true);
                }

                FillWithNewWord(chosenWord, boxIndex);
                boxIndex++;
                break;
        }

        UpdateButtonText();
    }

    /**
     * Fonction lancée par le bouton NextButton, et permet de passer à l'étape suivante de l'histoire
     */
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

    /**
     * Affiche pour une nouvelle étape de l'histoire, les cases vides de mots à remplir, et place les mots déjà préfaits
     */
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
        textBoxList.Clear();
        while(sentencePanel.transform.childCount != 0)
            ClearSentencePanel();
        GameObject instantiatedObject;
        switch (_storyPart)
        {
            case STORYPART.PART_0:
                instantiatedObject = Instantiate(premadeSentencePrefab, sentencePanel.transform);
                //textBoxList.Add(instantiatedObject);
                break;
            case STORYPART.PART_1:
                for (int i = 0; i < 4; i++)
                {
                    instantiatedObject = Instantiate(wordPrefab, sentencePanel.transform);
                    textBoxList.Add(instantiatedObject);
                }
                break;
            case STORYPART.PART_2:
                instantiatedObject = Instantiate(premadeSentencePrefab, sentencePanel.transform);
                //textBoxList.Add(instantiatedObject);
                break;
            case STORYPART.PART_3:
                instantiatedObject = Instantiate(wordPrefab, sentencePanel.transform);
                textBoxList.Add(instantiatedObject);
                for (int i = 0; i < 2; i++)
                {
                    instantiatedObject = Instantiate(premadeSentencePrefab, sentencePanel.transform);
                    //textBoxList.Add(instantiatedObject);
                }
                for (int i = 0; i < 3; i++)
                {
                    instantiatedObject = Instantiate(wordPrefab, sentencePanel.transform);
                    textBoxList.Add(instantiatedObject);
                }
                break;
        }
    }


    private void FillPremadeSentence(STORYPART _storyPart)
    {
        switch (_storyPart)
        {
            case STORYPART.PART_0:
                //textBoxList[0].GetComponent<Text>().text = beginSentence;
                sentencePanel.GetComponentInChildren<Text>().text = beginSentence;
                break;
            case STORYPART.PART_1:
                break;
            case STORYPART.PART_2:
                //textBoxList[0].GetComponent<Text>().text = thenSentence;
                sentencePanel.GetComponentInChildren<Text>().text = thenSentence;
                break;
            case STORYPART.PART_3:
                //textBoxList[1].GetComponent<Text>().text = subjectChoosed;
                //textBoxList[2].GetComponent<Text>().text = "en";
                sentencePanel.GetComponentsInChildren<Text>()[1].text = subjectChoosed;
                sentencePanel.GetComponentsInChildren<Text>()[2].text = "en";
                break;
        }
    }

    private void FillWithNewWord(string _chosenWord, int _textBoxIndex)
    {
        textBoxList[_textBoxIndex].GetComponentInChildren<Text>().text = _chosenWord;
    }

    private void ClearSentencePanel()
    {
        foreach (Transform child in sentencePanel.transform)
        {
            DestroyImmediate(child.gameObject);
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
