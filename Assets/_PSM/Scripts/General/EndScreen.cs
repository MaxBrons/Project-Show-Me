using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{

    [SerializeField] private TMP_Text _title;
    [SerializeField] private TMP_Text _subtitle;
    [SerializeField] private Image _picture;
    [SerializeField] private TMP_Text _suspectedPerson;
    [SerializeField] private TMP_Text _documentsRead;
    [SerializeField] private TMP_Text _emailsRead;
    [SerializeField] private TMP_Text _trashbinSearched;
    [SerializeField] private TMP_Text _totalTimeSpend;
    [SerializeField] private TMP_Text _chosenPerson;
    [SerializeField] private Color _titleColor = new Color(1, 0.5f, 0);
    [SerializeField] private Color _defaultColor;

    private const string SUSPECTED_PERSON_TEXT = "Suspected person: ";
    private const string DOCUMENTS_READ_TEXT = "Documents read: ";
    private const string EMAILS_READ_TEXT = "Emails read: ";
    private const string TRASHBIN_SEARCHED_TEXT = "Trashbin Searched: ";
    private const string TOTAL_TIME_TEXT = "Total time spend: ";
    private const string CHOSEN_PERSON_TEXXT = "You chose: ";
    private const string DONE_TEXT = "DONE";
    private const string NOT_DONE_TEXT = "NOT DONE";

    public void Initialize(string titleText, string subtitleText, string chosenPerson, string suspectedPerson, Sprite picture, bool suspectedPersonFound, bool documentsRead, bool emailsRead, bool trashBinSearched, float timeSpend)
    {
        _title.color = suspectedPersonFound ? _titleColor : Color.red;
        _title.text = titleText;
        _subtitle.text = subtitleText;

        _suspectedPerson.text = SUSPECTED_PERSON_TEXT + GetColoredText(suspectedPerson, _defaultColor);

        _picture.sprite = picture;
        SetStatisticText(_documentsRead, DOCUMENTS_READ_TEXT, documentsRead);
        SetStatisticText(_emailsRead, EMAILS_READ_TEXT, emailsRead);
        SetStatisticText(_trashbinSearched, TRASHBIN_SEARCHED_TEXT, trashBinSearched);

        _chosenPerson.text = SUSPECTED_PERSON_TEXT + GetColoredText(chosenPerson, _defaultColor);

        TimeSpan time = TimeSpan.FromSeconds(timeSpend);
        _totalTimeSpend.text = TOTAL_TIME_TEXT + GetColoredText(time.ToString("mm':'ss"), _defaultColor);
    }

    private void SetStatisticText(TMP_Text text, string body, bool value)
    {
        Color color = value ? Color.green : Color.red;
        text.text = body + GetColoredText(value ? DONE_TEXT : NOT_DONE_TEXT, color);
    }

    private string GetColoredText(string body, Color color)
    {
        return $"<color=#{color.ToHexString()}>{body}</color>";
    }
}
