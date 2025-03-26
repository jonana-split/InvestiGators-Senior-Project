using TMPro;
using UnityEngine;
using System.IO;

public class JournalManager : MonoBehaviour
{
    public GameObject backgroundPanel;
    public GameObject journalPanel;
    public GameObject journalButton;
    public TextMeshProUGUI journalText;
    public string filePath;
    public TMP_InputField journalInputField;
    public TextMeshProUGUI pageNumText;
    public GameObject prevButton;
    public GameObject nextButton;

    private int pageNum;

    void Start()
    {
        pageNum = 1;
        ReadTextFromFile();
    }

    void Update()
    {
        pageNumText.text = pageNum.ToString();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            WriteTextToFile();
        }

        if (pageNum == 1)
        {
            prevButton.SetActive(false);
        }
        else if (pageNum == 10)
        {
            nextButton.SetActive(false);
        }
        else
        {
            prevButton.SetActive(true);
            nextButton.SetActive(true);
        }
    }
    public void OpenJournal()
    {
        journalPanel.SetActive(true);
        journalButton.SetActive(false);
        backgroundPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void CloseJournal()
    {
        journalPanel.SetActive(false);
        journalButton.SetActive(true);
        backgroundPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReadTextFromFile()
    {
        filePath = "Assets/TextFiles/page" + pageNum + ".txt";
        if (File.Exists(filePath))
        {
            string fileContent = File.ReadAllText(filePath);
            journalText.text = fileContent;
        }
        else
        {
            journalText.text = "This page has not been created yet";
            //create a new txt file at the file path "Assets/TextFiles" called page[pageNum].txt
            File.Create(filePath).Dispose();  // Automatically create the new file
            journalText.text = "";
            Debug.Log("File not found");
        }
    }

    public void WriteTextToFile()
    {
        string textToWrite = journalInputField.text;
        File.AppendAllText(filePath, textToWrite + "\n");
        journalInputField.text = "";
        ReadTextFromFile();
    }

    public void SwitchPageUp()
    {
        pageNum++;
        ReadTextFromFile();
    }

    public void SwitchPageDown()
    {
        pageNum--;
        ReadTextFromFile();
    }
}
