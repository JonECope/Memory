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
    public UnityEventBool toggleSetupMenu; //Manage Window
    public UnityEventBool toggleEditWindow;

    // Items to take in
    public Text ImageLink;
    public Text AudioLink;
    public Text[] Choices; //Input
    //public int currentCard; // Moved to gameWindow

    // Items for the Editing Card Menu
    public int currentEditingCard;
    public InputField TakeEditImageLink;
    public InputField TakeEditAudioLink;
    public InputField[] TakeEditChoices; //Input
    int TakeEditcorrectChoice;
    public Toggle[] TakeEditcardToggles;
    // Items above are for the Editing Card Menu

    int correctChoice;
    public Toggle[] cardToggles;

    public List<Card> myCards;

    // Start is called before the first frame update
    public string filepath;
    void Start()
    {
        currentEditingCard = 0;

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

    public void displayEditableFields()
    {
        TakeEditAudioLink.text = myCards[currentEditingCard].path_to_audio;
        //TakeEditAudioLink.text = "Hello";
        TakeEditImageLink.text = myCards[currentEditingCard].path_to_img;
        Debug.Log("current card = " + currentEditingCard);
        Debug.Log(myCards[currentEditingCard].path_to_audio);
        for (int i = 0; i < TakeEditChoices.Length; i++)
        {
            TakeEditChoices[i].text = myCards[currentEditingCard].choices[i];
        }

        TakeEditcorrectChoice = myCards[currentEditingCard].correct_option;
        for (int toggle = 0; toggle < (TakeEditcardToggles.Length); toggle++)
        {
            
            if (toggle+1 == TakeEditcorrectChoice)
            {
                TakeEditcardToggles[toggle].isOn = true;
                //break;
            }
            else
            {
                TakeEditcardToggles[toggle].isOn = false;
            }
        }
        

    }

    // If arrow button clicked, recall the displayeditable...

    public void EditApplyClick()
    {
        if (checkEditInput())
        {
            //Take each text field and add to text document, dump to json
            Card editableCard = new Card();
            editableCard.path_to_audio = TakeEditAudioLink.text;
            editableCard.path_to_img = TakeEditImageLink.text;

            string[] TakeEditchoiceStrings = new string[4];

            for (int i = 0; i < TakeEditChoices.Length; i++)
            {
                TakeEditchoiceStrings[i] = TakeEditChoices[i].text;
            }
            editableCard.choices = TakeEditchoiceStrings;

            for (int toggle = 0; toggle < (TakeEditcardToggles.Length); toggle++)
            {
                if (TakeEditcardToggles[toggle].isOn)
                {
                    TakeEditcorrectChoice = toggle + 1;
                    break;
                }
            }

            editableCard.correct_option = TakeEditcorrectChoice;

            // Save Card Deck to File
            //Debug.Log(myCards);
            //Debug.Log(tempCard);
            myCards[currentEditingCard] = (editableCard);
            File.WriteAllText(filepath, JsonConvert.SerializeObject(myCards));
            //Debug.Log(JsonConvert.SerializeObject(myCards));
            togglePlayButton.Invoke(true);
            toggleEditButton.Invoke(true);
            toggleEditWindow.Invoke(false);
            toggleSetupMenu.Invoke(true);
        }
    }

    public void nextCard()
    {

        if (currentEditingCard < (myCards.Count-1))
        {
            currentEditingCard++;
            displayEditableFields();
        }

    }
    public void previousCard()
    {
        if (currentEditingCard > 0)
        {
            currentEditingCard--;
            displayEditableFields();
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

    public bool checkEditInput()
    //Check if input is empty
    {
        if (TakeEditImageLink.text.Length != 0 && TakeEditAudioLink.text.Length != 0)
        {
            foreach (InputField choice in TakeEditChoices)
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

// ON DELETE, CHECK IF CARDS. IF SO, KEEP BUTTON, OTHERWISE DISABLE