using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("General")]
    public GameObject sentencePanel;
    public GameObject choicesPanel;
    public Text wordPrefab; 
    
    [Header("Words lists")]
    public List<string> originalSentence;
    public List<string> currentSentence = new List<string>();
    public List<string> subject;
    public List<string> pastVerb;
    public List<string> infinitiveVerb;
    public List<string> complement;
    public List<string> location;
    public List<string> pPresentVerb;
    public List<string> pPastVerb;
    public List<string> bodypart;
    public List<string> adjective;

    private string subjectChoosed;
    private string complementChoosed;
    private string infVerbChoosed;
    private string bodypartChoosed;

    [Header("Sentences")]
    public string beginSentence;
    public string thenSentence;
    public string withCourageSentence;
    public string efficacitySentence;

    private enum CHOICE
    {
        WORD_1,
        WORD_2,
        WORD_3,
        WORD_4
    }
    private CHOICE choice;

    private enum STORYPART
    {
        PART_1,
        PART_2,
        PART_3
    }
    private STORYPART storyPart;


    // Start is called before the first frame update
    void Start()
    {
        choice = CHOICE.WORD_1;
        storyPart = STORYPART.PART_1;
        
        currentSentence.Add(beginSentence);
        ShowText(currentSentence);
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    private void ShowText(List<string> sentenceToShow)
    {
        ClearText();

        if (sentenceToShow.Count > 0)
        {
            foreach (string word in sentenceToShow)
            {
                Text _word = Instantiate(wordPrefab, sentencePanel.transform);
                _word.text = word;
            }
        }
        else
        {
            Debug.Log("Pas de mots à afficher");
        }

        sentenceToShow.Clear();
    }

    private void ClearText()
    {
        foreach (Transform child in sentencePanel.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
