using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class GameWindow : MonoBehaviour
{

    // Items to display in game
    public Text counterText;
    public Text[] choicesText;
    public GameObject[] checkMarks;
    public GameObject[] xMarks;
    public Image cardImageBox;
    //public Texture2D cardImageBoxTexture;

    public int currentCard; // Used for counter of which card in the deck.

    public CardManager cardManager;

    // Start is called before the first frame update
    void Start()
    {
        currentCard = 1;
        currentCardUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void currentCardUpdate()
    {
        counterText.text = currentCard.ToString() + " / " + cardManager.myCards.Count.ToString();

        //Debug.Log("choicesText.Length = " + choicesText.Length + "  *should be (4)");
        for (int i = 0; i < choicesText.Length; i++)
        {
            choicesText[i].text = cardManager.myCards[currentCard - 1].choices[i];
        }

        // Audio Setup
        string audioPath = cardManager.myCards[currentCard-1].path_to_audio;
        //DownloadHandlerAudioClip audioReference = new DownloadHandlerAudioClip(audioPath, AudioType.MPEG);
        //audioReference.streamAudio;

        // Image setup
        string imagePath = cardManager.myCards[currentCard - 1].path_to_img;
        //cardImageBox.sprite(imagePath);
        StartCoroutine(downloadTexture());


       // StartCoroutine(GetTexture(imagePath));
    }
    /*
    IEnumerator GetTexture(string imagePath)
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(imagePath);
        yield return www.SendWebRequest();
        
        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Texture2D myTexture = DownloadHandlerTexture.GetContent(www);
            Sprite webSprite = SpriteFromTexture2D(myTexture);
            cardImageBox.sprite = webSprite;
            cardImageBox.material.mainTexture = myTexture;
        }
    }

    Sprite SpriteFromTexture2D(Texture2D texture)
    {
        return Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    } */

    /* IEnumerator downloadTexture()
    {
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(cardManager.myCards[currentCard - 1].path_to_img);
        yield return www.SendWebRequest();
        Texture2D tex = new Texture2D(1, 1);
        tex = ((DownloadHandlerTexture)www.downloadHandler).texture;
        Sprite image = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f));

        cardImageBox.sprite = image;

    } */

    public void buttonChoiceClicked(int buttonID)
    {
        if (buttonID == cardManager.myCards[currentCard - 1].correct_option)
        {
            checkMarks[buttonID-1].SetActive(true);
        }
        else
        {
            xMarks[buttonID-1].SetActive(true);
        }
    }

    public void resetValidationMarks()
    {
        for (int i = 0; i < checkMarks.Length; i++)
        {
            checkMarks[i].SetActive(false);
            xMarks[i].SetActive(false);
        }
    }

    public void nextCard()
    {
        
        if (currentCard < cardManager.myCards.Count)
        {
            currentCard++;
            resetValidationMarks();
            currentCardUpdate();
        }
        
    }
    public void previousCard()
    {
        if (currentCard > 1)
        {
            currentCard--;
            resetValidationMarks();
            currentCardUpdate();
        }
        
    }

}
