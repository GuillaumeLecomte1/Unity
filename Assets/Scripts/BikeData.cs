using UnityEngine;

[System.Serializable]
public class BikeData
{
    [Tooltip("Nom de la moto affiché dans le menu")]
    public string bikeName = "Nouvelle Moto";

    [Tooltip("Prefab ou modèle 3D de la moto")]
    public GameObject bikeModel;

    [Tooltip("Image de prévisualisation (optionnel)")]
    public Sprite bikePreview;
} 