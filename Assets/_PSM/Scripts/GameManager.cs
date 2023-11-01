using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _folder;
    [SerializeField] private List<GameObject> _stickyNotes = new();
    [SerializeField] private List<Picture> _pictures = new();
    [SerializeField] private Button _buttonToShowFolder;
    [SerializeField] private Button _buttonToShowStickyNotes;
    [SerializeField] private Image _mainPicture;
    [Min(0)][SerializeField] private int _pictureIndexToReport = 0;
    [SerializeField] private EndScreen _endScreen;
    [SerializeField] private Button _computerEmailButton;

    private bool _computerEmailsRead = false;
    private int _currentSelectedPictureIndex = -1;

    private void Start()
    {
        _pictureIndexToReport = Math.Clamp(_pictureIndexToReport, 0, _pictures.Count);

        _pictures.ForEach(x => x.OnPictureSelected += OnPictureSelected);
        _endScreen.gameObject.SetActive(false);

        _folder.SetActive(false);
        _stickyNotes.ForEach(x => x.SetActive(false));

        _buttonToShowFolder.onClick.AddListener(OnFolderButtonClicked);
        _buttonToShowStickyNotes.onClick.AddListener(OnMailButtonClicked);
        _computerEmailButton.onClick.AddListener(OnComputerEmailButtonClicked);
    }

    private void OnPictureSelected(Picture picture)
    {
        _currentSelectedPictureIndex = _pictures.IndexOf(picture);
        print(_currentSelectedPictureIndex);
    }

    private void OnDestroy()
    {
        _buttonToShowFolder.onClick.RemoveListener(OnFolderButtonClicked);
        _buttonToShowStickyNotes.onClick.RemoveListener(OnMailButtonClicked);
        _computerEmailButton.onClick.RemoveListener(OnComputerEmailButtonClicked);
        _pictures.ForEach(x => x.OnPictureSelected -= OnPictureSelected);
    }

    public void EndGame()
    {
        if (_currentSelectedPictureIndex != _pictureIndexToReport) {
            ShowLosingScreen();
            return;
        }
        ShowWinningScreen();
    }

    private void OnFolderButtonClicked()
    {
        _buttonToShowFolder.onClick.RemoveListener(OnFolderButtonClicked);
        _folder.SetActive(true);
    }

    private void OnMailButtonClicked()
    {
        _buttonToShowStickyNotes.onClick.RemoveListener(OnMailButtonClicked);
        _stickyNotes.ForEach(x => x.SetActive(true));
    }

    private void OnComputerEmailButtonClicked()
    {
        _computerEmailsRead = true;
    }

    private void ShowWinningScreen()
    {
        _endScreen.Initialize(
                "GOOD JOB!",
                "You found the right person.",
                "BOB",
                _pictures[_pictureIndexToReport].Sprite,
                _pictureIndexToReport == _currentSelectedPictureIndex,
                _folder.activeSelf,
                _computerEmailsRead,
                _stickyNotes.Any(x => x.activeSelf),
                Time.realtimeSinceStartup
            );

        _endScreen.gameObject.SetActive(true);
    }

    private void ShowLosingScreen()
    {
        _endScreen.Initialize(
                 "OH NO!",
                 "You have not found the right person.",
                 "BOB",
                 _pictures[_pictureIndexToReport].Sprite,
                 _pictureIndexToReport == _currentSelectedPictureIndex,
                 _folder.activeSelf,
                 _computerEmailsRead,
                 _stickyNotes.Any(x => x.activeSelf),
                 Time.realtimeSinceStartup
             );

        _endScreen.gameObject.SetActive(true);
    }
}
