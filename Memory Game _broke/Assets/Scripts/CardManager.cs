using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Events;
using UnityEngine.UI;
using Newtonsoft.Json;

public class CardManager : MonoBehaviour
{
    //public UnityEvent showSetupButton;
    //public UnityEvent startSplashScreen;
    public UnityEventBool togglePlayButton;
    public UnityEventBool toggleEditButton;
    public UnityEventBool toggleSetupWindow;
    public UnityEventBool toggleSetupMenu;    

    // Items to take in
    public Text ImageLink;
    public Text AudioLink;
    public Text[] Choices; //Input
    //public int currentCard; // Moved to gameWindow

    int correctChoice;
    public Toggle[] cardToggles;

    public List<Card> myCards;

    // Start is called before the first frame update
    public string filepath;
    void Start()
    {
        
        correctChoice = 1;
        filepath = Path.Combine(Application.dataPath, "DataInfo.json");

        
        if (!File.Exists(filepath))
        {
            File.Create(filepath);
            myCards = new List<Card>();
            togglePlayButton.Invoke(false);
            toggleEditButton.Invoke(false);
            toggleSetupWindow.Invoke(false);
            toggleSetupMenu.Invoke(true);
            //showSetupButton.Invoke();
        }
        else
        {
            myCards = JsonConvert.DeserializeObject<List<Card>>(File.ReadAllText(filepath));
            //Debug.Log(myCards[0].choices[3]);
            //startSplashScreen.Invoke();
            if (myCards == null)
            {
                togglePlayButton.Invoke(false);
                toggleEditButton.Invoke(false);
            }
            else
            {
                togglePlayButton.Invoke(true);
                toggleEditButton.Invoke(true);
            }
        }
        //Debug.Log(myCards[0].path_to_audio);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void deleteCard()
    {

        // Check to make sure card deck is not empty
        //if 0 enable button
        // Start the splash screen and stop the initialize screen if the file isn't empty
        if (new FileInfo(filepath).Length == 0)
        {
            //showSetupButton.Invoke();
        }
        else
        {
            //startSplashScreen.Invoke();
        }
    }

    public void submitClick()
    {
        if (checkInput())
        {
            //Take each text field and add to text document, dump to json
            Card tempCard = new Card();
            tempCard.path_to_audio = AudioLink.text;
            tempCard.path_to_img = ImageLink.text;

            string[] choiceStrings = new string[4];

            for (int i = 0; i < Choices.Length; i++)
            {
                choiceStrings[i] = Choices[i].text;
            }
            tempCard.choices = choiceStrings;

            for (int toggle = 0; toggle < (cardToggles.Length); toggle++)
            {
                if (cardToggles[toggle].isOn)
                {
                    correctChoice = toggle + 1;
                    break;
                }
            }

            tempCard.correct_option = correctChoice;

            // Save Card Deck to File
            //Debug.Log(myCards);
            //Debug.Log(tempCard);
            myCards.Add(tempCard);
            File.WriteAllText(filepath, JsonConvert.SerializeObject(myCards));
            //Debug.Log(JsonConvert.SerializeObject(myCards));
            togglePlayButton.Invoke(true);
            toggleEditButton.Invoke(true);
            toggleSetupWindow.Invoke(false);
            toggleSetupMenu.Invoke(true);
        }
    }

    // Return the total number of cards in the deck
    public int totalCards()
    {
        return (myCards.Count);
    }

    // Return the current card (used for: "Card 1/5")
    //public int returnCurrentCardNumber()
    //{
    //    new gameObject.text = 
    //    return (currentCard);
    //}

    public bool checkInput()
    //Check if input is empty
    {
        if (ImageLink.text.Length != 0 && AudioLink.text.Length != 0)
        {
            foreach (Text choice in Choices)
            {
                if (choice.text.Length == 0)
                {
                    return false;
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }
}