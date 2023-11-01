using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PictureSelectionManager : MonoBehaviour
{
    [SerializeField] private Image _mainPicture;

    private List<Picture> _pictureSelections = new();

    private void Start()
    {
        _pictureSelections = FindObjectsOfType<Picture>().ToList();
        _pictureSelections.ForEach(x => x.OnPictureSelected += OnPictureSelected);
    }

    private void OnDestroy()
    {
        _pictureSelections.ForEach(x => x.OnPictureSelected -= OnPictureSelected);
    }

    private void OnPictureSelected(Picture pictureInteraction)
    {
        _mainPicture.sprite = pictureInteraction.Sprite;
    }
}