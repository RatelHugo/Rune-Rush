using System.Collections.Generic;
using UnityEngine;

public class ValidationRunePoints : MonoBehaviour
{
    [Header("Rune Manager")]
    public RuneManager runeManager;

    [Header("Drawer")]
    public DrawerOnPanel drawer;

    [Header("Validation Settings")]
    public float toleranceValide = 100f;
    public float toleranceTrap = 75f;
    public float speedByObstacle = 0.33f;

    private List<Transform> validZones = new List<Transform>();
    private List<Transform> trapZones = new List<Transform>(); 
    private HashSet<int> validatedZones = new HashSet<int>();

    public void InitializeValidation()
    {
        if (runeManager == null)
        {
            Debug.LogError("Aucune rune sélectionnée !");
            return;
        }
        validZones.Clear();
        trapZones.Clear();

        foreach (Transform child in runeManager.validZoneParent)
        {
            validZones.Add(child);
        }

        foreach (Transform child in runeManager.trapZoneParent)
        {
            trapZones.Add(child);
        }

        validatedZones.Clear();

        Debug.Log($"Validation initialisée avec {validZones.Count} zones valides et {trapZones.Count} zones pièges.");
    }

    public void ValidatePoint(Vector3 localDrawnPoint)
    {
        for (int i = 0; i < validZones.Count; i++)
        {
            if (validatedZones.Contains(i)) continue;

            Vector3 localZonePosition = validZones[i].localPosition;

            float distance = Vector3.Distance(localDrawnPoint, localZonePosition);
            if (distance <= toleranceValide)
            {
                Debug.Log($"Zone {i} validée !");
                validatedZones.Add(i);
                break;
            }
        }

        foreach (Transform trap in trapZones)
        {
            Vector3 localTrapPosition = trap.localPosition;
            if (Vector3.Distance(localDrawnPoint, localTrapPosition) <= toleranceTrap)
            {
                Debug.Log("Zone piège activée !");
                drawer.FinishDrawing();
                return;
            }
        }

        if (validatedZones.Count == validZones.Count)
        {
            Debug.Log("Rune validée !");
            drawer.FinishDrawing();
            runeManager.ClearZones();
            GameManager.Instance.canvas.SetActive(false);
            GameManager.Instance.speedPlayer += speedByObstacle;
            GameManager.Instance.isValidationRune = true;
            GameManager.Instance.score++;
        }
    }

    public void ResetValidation()
    {
        validatedZones.Clear();
        Debug.Log("Validation réinitialisée.");
    }
}
