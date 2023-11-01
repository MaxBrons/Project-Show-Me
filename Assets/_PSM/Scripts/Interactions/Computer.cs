using System;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    [Serializable]
    private class ComputerApplication
    {
        public GameObject Root => root;
        public Button Button => button;

        [SerializeField] private GameObject root;
        [SerializeField] private Button button;

        public ComputerApplication(GameObject root, Button button)
        {
            this.root = root;
            this.button = button;
        }
    }

    [SerializeField] private GameObject _homeScreen;
    [SerializeField] private ComputerApplication _folderScreen;
    [SerializeField] private ComputerApplication _mailScreen;
    [SerializeField] private ComputerApplication _trashbinScreen;
    [SerializeField] private Button _backButton;

    private void Start()
    {
        OnBackButtonPressed();

        _folderScreen.Button.onClick.AddListener(SetFolderScreenEnabled);
        _mailScreen.Button.onClick.AddListener(SetMailScreenEnabled);
        _trashbinScreen.Button.onClick.AddListener(SetTrashbinScreenEnabled);
    }

    public void SetHomeScreenEnabled()
    {
        _backButton.gameObject.SetActive(false);
        SetScreenEnabled(_homeScreen, true);
    }

    public void SetFolderScreenEnabled()
    {
        _backButton.gameObject.SetActive(true);
        SetScreenEnabled(_folderScreen, true);
    }

    public void SetMailScreenEnabled()
    {
        _backButton.gameObject.SetActive(true);
        SetScreenEnabled(_mailScreen, true);
    }

    public void SetTrashbinScreenEnabled()
    {
        _backButton.gameObject.SetActive(true);
        SetScreenEnabled(_trashbinScreen, true);
    }


    private void OnDestroy()
    {
        _folderScreen.Button.onClick.RemoveAllListeners();
        _mailScreen.Button.onClick.RemoveAllListeners();
        _trashbinScreen.Button.onClick.RemoveAllListeners();
    }

    private void OnBackButtonPressed()
    {
        SetScreenEnabled(_homeScreen, true);
        _backButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (!_backButton)
            return;

        _backButton.onClick.AddListener(OnBackButtonPressed);
    }

    private void OnDisable()
    {
        if (!_backButton)
            return;

        _backButton.onClick.RemoveListener(OnBackButtonPressed);
    }

    private void SetScreenEnabled(ComputerApplication screen, bool value)
    {
        _homeScreen.SetActive(!value);
        _folderScreen.Root.SetActive(!value);
        _mailScreen.Root.SetActive(!value);
        _trashbinScreen.Root.SetActive(!value);

        screen.Root.SetActive(value);
    }

    private void SetScreenEnabled(GameObject screen, bool value)
    {
        _homeScreen.SetActive(!value);
        _folderScreen.Root.SetActive(!value);
        _mailScreen.Root.SetActive(!value);
        _trashbinScreen.Root.SetActive(!value);

        screen.SetActive(value);
    }
}
