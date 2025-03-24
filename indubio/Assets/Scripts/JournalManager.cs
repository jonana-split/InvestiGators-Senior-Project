using TMPro;
using UnityEngine;
using System.IO;

public class JournalManager : MonoBehaviour
{
    public GameObject journalPanel;
    public GameObject journalButton;
    public TextMeshProUGUI journalText;
    public string filePath = "Assets/TextFiles/sample.txt";
    public TMP_InputField journalInputField;

    void Start()
    {
        ReadTextFromFile();
    }
    public void OpenJournal()
    {
        journalPanel.SetActive(true);
        journalButton.SetActive(false);
    }

    public void CloseJournal()
    {
        journalPanel.SetActive(false);
        journalButton.SetActive(true);
    }

    public void ReadTextFromFile()
    {
        if (File.Exists(filePath))
        {
            string fileContent = File.ReadAllText(filePath);
            journalText.text = fileContent;
        }
        else
        {
            Debug.Log("File not found");
        }
    }

    public void WriteTextToFile()
    {
        string textToWrite = journalInputField.text;
        File.AppendAllText(filePath, textToWrite + "\n");
        ReadTextFromFile();
    }
}
