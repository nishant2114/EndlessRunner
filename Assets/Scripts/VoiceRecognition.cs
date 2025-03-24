using UnityEngine;
using UnityEngine.Windows.Speech;

public class VoiceRecognition : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        string[] keywords = { "jump", "move left", "move right" };
        keywordRecognizer = new KeywordRecognizer(keywords);
        keywordRecognizer.OnPhraseRecognized += OnPhraseRecognized;
        keywordRecognizer.Start();
    }

    private void OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        Debug.Log("Command: " + args.text);
        switch (args.text)
        {
            case "jump":
                playerController.Jump();
                break;
            case "move left":
                playerController.MoveLeft();
                break;
            case "move right":
                playerController.MoveRight();
                break;
        }
    }

    void OnDestroy()
    {
        if (keywordRecognizer != null)
        {
            keywordRecognizer.Stop();
            keywordRecognizer.Dispose();
        }
    }
}
