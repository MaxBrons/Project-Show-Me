using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour
{
    public event Action<Picture> OnPictureSelected;

    public Sprite Sprite => _picture;
    public string Name => _name;

    [SerializeField] private Sprite _picture;
    [SerializeField] private Image _pictureUI;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private string _name;

    private void Start()
    {
        _nameText.text = _name;
        _pictureUI.sprite = Sprite;
    }

    public void Select()
    {
        OnPictureSelected?.Invoke(this);
    }
}