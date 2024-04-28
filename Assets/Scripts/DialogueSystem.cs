using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] private string _nameOfCharacter;
    private GameObject _dialogueUI;
    private List<Dialogue> _dialogues = new();
    private int _currentDialogueIndex = 0;
    private bool _waitingForUserInput;

    // Start is called before the first frame update
    void Start()
    {
        _dialogueUI = GameObject.Find("Canvas").GetComponent<UI>()._dialogue;
        LoadDialogues();
    }

    // Update is called once per frame
    [System.Obsolete]
    void Update()
    {
        if (_dialogueUI.active)
        {
            if (!_waitingForUserInput)
            {
                if (_currentDialogueIndex != -1)
                {
                    DisplayDialogue();
                }
                else
                {
                    _dialogueUI.SetActive(false);
                    _waitingForUserInput = false;
                    _currentDialogueIndex = 0;
                    Destroy(GameObject.Find($"{_nameOfCharacter}Temp"));
                }
                _waitingForUserInput = true;
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _currentDialogueIndex = _dialogues[_currentDialogueIndex].TargetForResponse[0];
                    _waitingForUserInput = false;
                }

                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _currentDialogueIndex = _dialogues[_currentDialogueIndex].TargetForResponse[1];
                    _waitingForUserInput = false;
                }

                if (Input.GetKeyDown(KeyCode.Alpha3))
                {
                    _currentDialogueIndex = _dialogues[_currentDialogueIndex].TargetForResponse[2];
                    _waitingForUserInput = false;
                }

            }

        }
    }

    void LoadDialogues()
    {
        TextAsset textAsset = (TextAsset)Resources.Load("dialogues");
        XmlDocument doc = new();
        doc.LoadXml(textAsset.text);

        foreach (XmlNode character in doc.SelectNodes("dialogues/character"))
        {
            if (character.Attributes.GetNamedItem("name").Value.Equals(_nameOfCharacter))
            {
                foreach (XmlNode dialogueFromXML in doc.SelectNodes("dialogues/character/dialogue"))
                {
                    // Debug.Log(character.Attributes.GetNamedItem("name").Value);
                    var message = dialogueFromXML.Attributes.GetNamedItem("content").Value;
                    int choiceIndex = 0;
                    var response = new string[3];
                    var targetForResponse = new int[3];



                    foreach (XmlNode choice in dialogueFromXML)
                    {
                        response[choiceIndex] = choice.Attributes.GetNamedItem("content").Value;
                        targetForResponse[choiceIndex] = int.Parse(choice.Attributes.GetNamedItem("target").Value);
                        choiceIndex++;
                    }

                    _dialogues.Add(new Dialogue(_nameOfCharacter, message, response, targetForResponse));
                }
            }
        }
    }

    public void DisplayDialogue()
    {
        string textToDisplay = $"[1] > {_dialogues[_currentDialogueIndex].Response[0]}" +
                             $"\n[2] > {_dialogues[_currentDialogueIndex].Response[1]}" +
                             $"\n[3] > {_dialogues[_currentDialogueIndex].Response[2]}";

        GameObject.Find("dialogueTitle").GetComponent<TextMeshProUGUI>().text = $"[{_nameOfCharacter}]";
        GameObject.Find("dialogueBox").GetComponent<TextMeshProUGUI>().text = textToDisplay;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _dialogueUI.SetActive(true);
        DisplayDialogue();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _dialogueUI.SetActive(false);
    }
}
