using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("General")]
    public GameObject sentencePanel;
    public GameObject choicesPanel;
    public GameObject wordText;
    public GameObject nextButton;
    public GameObject resultPanel;
    public Text hero1ResultText;
    public Text hero2ResultText;
    public Text hero3ResultText;
    public Text tryNumberText;
    public GameObject gameCanvas;
    public GameObject replayCanvas;
    public Text scoreText;

    [Header("Words lists")]
    public List<string> currentSentence = new List<string>();
    public List<string> subject;
    public List<string> pastVerb;
    public List<string> infVerb;
    public List<string> complement;
    public List<string> complement2;
    public List<string> location;
    public List<string> pPresentVerb;
    public List<string> pPastVerb;
    public List<string> bodypart;
    public List<string> adjective;

    public List<string> hero1WordBaseList;
    public List<string> hero2WordBaseList;
    public List<string> hero3WordBaseList;
    private List<string> hero1WordPlayerList = new List<string>();
    private List<string> hero2WordPlayerList = new List<string>();
    private List<string> hero3WordPlayerList = new List<string>();
    private string hero1ResultSentence;
    private string hero2ResultSentence;
    private string hero3ResultSentence;

    private string chosenWord;
    private string subjectChoosed;
    private string complementChoosed;
    private string complement2Choosed;
    private string infVerbChoosed;
    private string bodypartChoosed;
    private string adjectiveChoosed;

    [Header("Sentences")]
    public string beginSentence;
    public string thenSentence;
    public string withCourageSentence;
    public string efficacitySentence;
    public string rememberSentence;

    private List<GameObject> textBoxList = new List<GameObject>();
    private int index = 0;
    private int boxIndex = 0;

    private int tryNumber = 1;

    private enum STORYPART
    {
        PART_0,
        PART_1,
        PART_2,
        PART_3,
        PART_4,
        PART_5,
        PART_6,
        PART_7,
        PART_8,
        PART_9,
        PART_10,
        PART_11
    }
    private STORYPART storyPart;

    private enum WORDTYPE
    {
        SUBJECT,
        PASTVERB,
        COMPLEMENT,
        COMPLEMENT2,
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

        gameCanvas.SetActive(true);
        replayCanvas.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        GenerateNewStory();
        resultPanel.SetActive(false);

        storyPart = STORYPART.PART_0;

        DisplayNewStoryPartUI(storyPart);
    }

    /**
     * Affiche le bon texte dans les word boutons en fonction de la catégorie de mot à choisir
     */
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
            case WORDTYPE.COMPLEMENT2:
                foreach (Transform buttonGO in choicesPanel.transform)
                {
                    buttonGO.gameObject.GetComponentInChildren<Text>().text = complement2[index];
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

    /**
     * Appelée lorsqu'on clique sur un mot à choisir
     */
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
                    complementChoosed = complement[choiceIndex];
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

                hero1WordPlayerList.Add(chosenWord);
                FillWithNewWord(chosenWord, boxIndex);
                boxIndex++;
                break;
            
            case STORYPART.PART_2:
                break;
            
            case STORYPART.PART_3:
                if (currentWordType == WORDTYPE.INFVERB)
                {
                    currentSentence.Add(infVerb[choiceIndex]);
                    infVerbChoosed = infVerb[choiceIndex];
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
                    bodypartChoosed = bodypart[choiceIndex];
                    chosenWord = bodypart[choiceIndex];
                    currentWordType = WORDTYPE.ADJECTIVE;
                }
                else if (currentWordType == WORDTYPE.ADJECTIVE)
                {
                    currentSentence.Add(adjective[choiceIndex]);
                    adjectiveChoosed = adjective[choiceIndex];
                    chosenWord = adjective[choiceIndex];
                    currentWordType = WORDTYPE.P_PASTVERB;
                    DisableChoiceButtons();
                    nextButton.SetActive(true);
                }

                hero2WordPlayerList.Add(chosenWord);
                FillWithNewWord(chosenWord, boxIndex);
                boxIndex++;
                break;
            case STORYPART.PART_4:
                break;
            case STORYPART.PART_5:
                if (currentWordType == WORDTYPE.P_PASTVERB)
                {
                    currentSentence.Add(infVerb[choiceIndex]);
                    chosenWord = pPastVerb[choiceIndex];
                    currentWordType = WORDTYPE.COMPLEMENT2;
                }
                else if (currentWordType == WORDTYPE.COMPLEMENT2)
                {
                    currentSentence.Add(pPresentVerb[choiceIndex]);
                    complement2Choosed = complement2[choiceIndex];
                    chosenWord = complement2[choiceIndex];
                    DisableChoiceButtons();
                    nextButton.SetActive(true);
                }

                hero3WordPlayerList.Add(chosenWord);
                FillWithNewWord(chosenWord, boxIndex);
                boxIndex++;
                break;
            case STORYPART.PART_6:
                break;
            case STORYPART.PART_7:
                break;
            case STORYPART.PART_8:
                break;
            case STORYPART.PART_9:
                break;
            case STORYPART.PART_10:
                break;
            case STORYPART.PART_11:
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
                storyPart = STORYPART.PART_4;
                break;
            case STORYPART.PART_4:
                storyPart = STORYPART.PART_5;
                break;
            case STORYPART.PART_5:
                storyPart = STORYPART.PART_6;
                break;
            case STORYPART.PART_6:
                storyPart = STORYPART.PART_7;
                break;
            case STORYPART.PART_7:
                storyPart = STORYPART.PART_8;
                break;
            case STORYPART.PART_8:
                storyPart = STORYPART.PART_9;
                break;
            case STORYPART.PART_9:
                storyPart = STORYPART.PART_10;
                break;
            case STORYPART.PART_10:
                storyPart = STORYPART.PART_11;
                break;
            case STORYPART.PART_11:
                if(resultPanel.activeInHierarchy == false)
                {
                    resultPanel.SetActive(true);
                    sentencePanel.SetActive(false);
                    ShowResults();
                }
                else
                {
                    resultPanel.SetActive(false);
                    sentencePanel.SetActive(true);
                    storyPart = STORYPART.PART_0;
                    currentWordType = WORDTYPE.SUBJECT;
                    tryNumber++;
                    tryNumberText.text = tryNumber.ToString();
                    hero1WordPlayerList.Clear();
                    hero2WordPlayerList.Clear();
                    hero3WordPlayerList.Clear();
                }
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
            case STORYPART.PART_4:
                DisableChoiceButtons();
                nextButton.SetActive(true);
                break;
            case STORYPART.PART_5:
                EnableChoiceButtons();
                break;
            case STORYPART.PART_6:
                DisableChoiceButtons();
                nextButton.SetActive(true);
                break;
            case STORYPART.PART_7:
                DisableChoiceButtons();
                nextButton.SetActive(true);
                break;
            case STORYPART.PART_8:
                DisableChoiceButtons();
                nextButton.SetActive(true);
                break;
            case STORYPART.PART_9:
                DisableChoiceButtons();
                nextButton.SetActive(true);
                break;
            case STORYPART.PART_10:
                DisableChoiceButtons();
                nextButton.SetActive(true);
                break;
            case STORYPART.PART_11:
                DisableChoiceButtons();
                nextButton.SetActive(true);
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
                Instantiate(wordText, sentencePanel.transform);
                break;
            case STORYPART.PART_1:
                Instantiate(wordText, sentencePanel.transform);
                for (int i = 0; i < 4; i++)
                {
                    instantiatedObject = Instantiate(wordText, sentencePanel.transform);
                    textBoxList.Add(instantiatedObject);
                }
                break;
            case STORYPART.PART_2:
                Instantiate(wordText, sentencePanel.transform);
                break;
            case STORYPART.PART_3:
                instantiatedObject = Instantiate(wordText, sentencePanel.transform);
                textBoxList.Add(instantiatedObject);
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(wordText, sentencePanel.transform);
                }
                instantiatedObject = Instantiate(wordText, sentencePanel.transform);
                textBoxList.Add(instantiatedObject);
                Instantiate(wordText, sentencePanel.transform);
                for (int i = 0; i < 2; i++)
                {
                    instantiatedObject = Instantiate(wordText, sentencePanel.transform);
                    textBoxList.Add(instantiatedObject);
                }
                break;
            case STORYPART.PART_4:
                Instantiate(wordText, sentencePanel.transform);
                break;
            case STORYPART.PART_5:
                for (int i = 0; i < 2; i++)
                {
                    instantiatedObject = Instantiate(wordText, sentencePanel.transform);
                    textBoxList.Add(instantiatedObject);
                }
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(wordText, sentencePanel.transform);
                }
                break;
            case STORYPART.PART_6:
                Instantiate(wordText, sentencePanel.transform);
                break;
            case STORYPART.PART_7:
                for (int i = 0; i < 5; i++)
                {
                    Instantiate(wordText, sentencePanel.transform);
                }
                break;
            case STORYPART.PART_8:
                Instantiate(wordText, sentencePanel.transform);
                break;
            case STORYPART.PART_9:
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(wordText, sentencePanel.transform);
                }
                break;
            case STORYPART.PART_10:
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(wordText, sentencePanel.transform);
                }
                break;
            case STORYPART.PART_11:
                for (int i = 0; i < 2; i++)
                {
                    Instantiate(wordText, sentencePanel.transform);
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
                sentencePanel.GetComponentsInChildren<Text>()[0].text = "Le";
                break;
            case STORYPART.PART_2:
                sentencePanel.GetComponentInChildren<Text>().text = thenSentence;
                break;
            case STORYPART.PART_3:
                sentencePanel.GetComponentsInChildren<Text>()[1].text = "le " + subjectChoosed;
                sentencePanel.GetComponentsInChildren<Text>()[2].text = "en";
                sentencePanel.GetComponentsInChildren<Text>()[4].text = "son";
                break;
            case STORYPART.PART_4:
                sentencePanel.GetComponentInChildren<Text>().text = withCourageSentence;
                break;
            case STORYPART.PART_5:
                sentencePanel.GetComponentsInChildren<Text>()[2].text = "pour";
                sentencePanel.GetComponentsInChildren<Text>()[3].text = infVerbChoosed;
                sentencePanel.GetComponentsInChildren<Text>()[4].text = "le " + subjectChoosed + " !";
                break;
            case STORYPART.PART_6:
                sentencePanel.GetComponentInChildren<Text>().text = efficacitySentence;
                break;
            case STORYPART.PART_7:
                sentencePanel.GetComponentsInChildren<Text>()[0].text = "d'" + complement2Choosed;
                sentencePanel.GetComponentsInChildren<Text>()[1].text = "face au";
                sentencePanel.GetComponentsInChildren<Text>()[2].text = bodypartChoosed;
                sentencePanel.GetComponentsInChildren<Text>()[3].text = "du";
                sentencePanel.GetComponentsInChildren<Text>()[4].text = subjectChoosed;
                break;
            case STORYPART.PART_8:
                sentencePanel.GetComponentInChildren<Text>().text = rememberSentence;
                break;
            case STORYPART.PART_9:
                sentencePanel.GetComponentsInChildren<Text>()[0].text = "Le " + subjectChoosed;
                sentencePanel.GetComponentsInChildren<Text>()[1].text = "peut causer maints troubles,";
                break;
            case STORYPART.PART_10:
                sentencePanel.GetComponentsInChildren<Text>()[0].text = "mais agit";
                sentencePanel.GetComponentsInChildren<Text>()[1].text = adjectiveChoosed;
                sentencePanel.GetComponentsInChildren<Text>()[2].text = "et sans peur,";
                break;
            case STORYPART.PART_11:
                sentencePanel.GetComponentsInChildren<Text>()[0].text = "et " + complementChoosed;
                sentencePanel.GetComponentsInChildren<Text>()[1].text = "te remerciera !";
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

    private void GenerateNewStory()
    {
        hero1WordBaseList = new List<string>();
        hero2WordBaseList = new List<string>();
        hero3WordBaseList = new List<string>();

        for(int i = 0; i < 4; i++)
        {
            int _index = Random.Range(0, 4);
            switch(i)
            {
                case 0:
                    hero1WordBaseList.Add(subject[_index]);
                    break;
                case 1:
                    hero1WordBaseList.Add(pastVerb[_index]);
                    break;
                case 2:
                    hero1WordBaseList.Add(complement[_index]);
                    break;
                case 3:
                    hero1WordBaseList.Add(location[_index]);
                    break;
            }
        }

        for (int i = 0; i < 4; i++)
        {
            int _index = Random.Range(0, 4);
            switch (i)
            {
                case 0:
                    hero2WordBaseList.Add(infVerb[_index]);
                    break;
                case 1:
                    hero2WordBaseList.Add(pPresentVerb[_index]);
                    break;
                case 2:
                    hero2WordBaseList.Add(bodypart[_index]);
                    break;
                case 3:
                    hero2WordBaseList.Add(adjective[_index]);
                    break;
            }
        }

        for (int i = 0; i < 2; i++)
        {
            int _index = Random.Range(0, 4);
            switch (i)
            {
                case 0:
                    hero3WordBaseList.Add(pPastVerb[_index]);
                    break;
                case 1:
                    hero3WordBaseList.Add(complement2[_index]);
                    break;
            }
        }
    }

    private List<string> CalculHero1Results()
    {
        List<string> validWords = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            if(hero1WordBaseList[i] == hero1WordPlayerList[i])
            {
                validWords.Add(hero1WordBaseList[i]);                
            }
        }
        return validWords;
    }

    private List<string> CalculHero2Results()
    {
        List<string> validWords = new List<string>();
        for (int i = 0; i < 4; i++)
        {
            if (hero2WordBaseList[i] == hero2WordPlayerList[i])
            {
                validWords.Add(hero2WordBaseList[i]);
            }
        }
        return validWords;
    }

    private List<string> CalculHero3Results()
    {
        List<string> validWords = new List<string>();
        for (int i = 0; i < 2; i++)
        {
            if (hero3WordBaseList[i] == hero3WordPlayerList[i])
            {
                validWords.Add(hero3WordBaseList[i]);
            }
        }
        return validWords;
    }

    //Show must go on
    private void ShowResults()
    {
        hero1ResultSentence = "";
        hero2ResultSentence = "";
        hero3ResultSentence = "";

        bool hero1Found = false;
        bool hero2Found = false;
        bool hero3Found = false;

        //HERO 1
        if (CalculHero1Results().Count == 4)
        {
            string firstSentence = "C'est un perfect pour moi scribe ! En même temps, c'est ton métier...";
            hero1ResultSentence += firstSentence;
            hero1Found = true; 
        }
        else if (CalculHero1Results().Count > 0)
        {
            string firstSentence = "Bien joué scribe ! Tu ne t'es pas gourré pour : \n";
            hero1ResultSentence += firstSentence;
            foreach(string _word in CalculHero1Results())
            {
                hero1ResultSentence += "\n         " + _word;
            }
            hero1Found = false;
        } else if(CalculHero1Results().Count == 0)
        {
            string firstSentence = "J'ai la vague impression que t'as pas écouté ce que je t'ai raconté...";
            hero1ResultSentence += firstSentence;
            hero1Found = false;
        }

        //HER0 2
        if (CalculHero2Results().Count == 4)
        {
            string firstSentence = "Voilà ! C'est ce qu'il s'est passé MOT - POUR - MOT pour ma part !";
            hero2ResultSentence += firstSentence;
            hero2Found = true;
        }
        else if (CalculHero2Results().Count > 0)
        {
            string firstSentence = "Perso, on est d'accord pour : \n";
            hero2ResultSentence += firstSentence;
            foreach (string _word in CalculHero2Results())
            {
                hero2ResultSentence += "\n         " + _word;
            }
            hero2Found = false;
        }
        else if (CalculHero2Results().Count == 0)
        {
            string firstSentence = "Mouai, tu ferais mieux de t'appliquer...";
            hero2ResultSentence += firstSentence;
            hero2Found = false;
        }

        //HERO 3
        if (CalculHero3Results().Count == 2)
        {
            string firstSentence = "Chapeau le scribe ! C'est ce que moi ce que j'avais dit complètement !";
            hero3ResultSentence += firstSentence;
            hero3Found = true;
        }
        else if (CalculHero3Results().Count > 0)
        {
            string firstSentence = "Nickel ! C'est exactement ce qu'il s'est passé pour : \n";
            hero3ResultSentence += firstSentence;
            foreach (string _word in CalculHero3Results())
            {
                hero3ResultSentence += "\n         " + _word;
            }
            hero3Found = false;
        }
        else if (CalculHero3Results().Count == 0)
        {
            string firstSentence = "Ta capacité de retranscrire ce que j'ai dit = NEANT...";
            hero3ResultSentence += firstSentence;
            hero3Found = false;
        }

        if(hero1Found && hero2Found && hero3Found)
        {
            //WIN
            StartCoroutine(DisplayReplayScreen());
        }

        hero1ResultText.text = hero1ResultSentence;
        hero2ResultText.text = hero2ResultSentence;
        hero3ResultText.text = hero3ResultSentence;
    }

    IEnumerator DisplayReplayScreen()
    {
        yield return new WaitForSeconds(.1f);
        nextButton.SetActive(false);
        yield return new WaitForSeconds(8f);
        gameCanvas.SetActive(false);
        replayCanvas.SetActive(true);
        scoreText.text = tryNumber.ToString();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
