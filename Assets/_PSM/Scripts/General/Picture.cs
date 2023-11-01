using System;
using UnityEngine;
using UnityEngine.UI;

public class Picture : MonoBehaviour
{
    public event Action<Picture> OnPictureSelected;

    public Sprite Sprite => _picture;

    [SerializeField] private Sprite _picture;
    [SerializeField] private Image _pictureUI;

    private void Start()
    {
        _pictureUI.sprite = Sprite;
    }

    public void Select()
    {
        OnPictureSelected?.Invoke(this);
    }
}