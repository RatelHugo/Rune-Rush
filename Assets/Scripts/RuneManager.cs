using UnityEngine;
using UnityEngine.UI;

public class RuneManager : MonoBehaviour
{
    [Header("Rune Settings")]
    public RuneData[] runesLevel0;
    public RuneData[] runesLevel1;
    public Image runeDisplay;

    private RuneData currentRune;

    [Header("Zone Settings")]
    public Transform validZoneParent;
    public Transform trapZoneParent; 
    public GameObject zonePrefab;

    public void Update()
    {
        if(GameManager.Instance.isTriggerRune)
        {
            SelectRandomRune();
            GameManager.Instance.isTriggerRune = false;
        }
    }

    public void SelectRandomRune()
    {
        if (runesLevel0.Length == 0 && runesLevel1.Length == 0)
        {
            Debug.LogError("Aucune rune disponible dans le tableau !");
            return;
        }

        if (GameManager.Instance.score < 10)
        {
            currentRune = runesLevel0[Random.Range(0, runesLevel0.Length)];
        }
        else if (GameManager.Instance.score > 10)
        {
            currentRune = runesLevel1[Random.Range(0, runesLevel1.Length)];
        }

        runeDisplay.sprite = currentRune.runeSprite;
        GenerateZones(currentRune);
        FindObjectOfType<ValidationRunePoints>().InitializeValidation();
    }

    private void GenerateZones(RuneData rune)
    {
        foreach (Vector2 position in rune.validZonePositions)
        {
            CreateZone(position, validZoneParent, Color.green);
        }

        foreach (Vector2 position in rune.trapZonePositions)
        {
            CreateZone(position, trapZoneParent, Color.red);
        }
    }

    public void ClearZones()
    {
        foreach (Transform child in validZoneParent)
        {
            Destroy(child.gameObject);
        }

        foreach (Transform child in trapZoneParent)
        {
            Destroy(child.gameObject);
        }
    }

    private void CreateZone(Vector2 position, Transform parent, Color color)
    {
        GameObject zone = Instantiate(zonePrefab, parent); 
        zone.transform.localPosition = new Vector3(position.x, position.y, 0f); 
        zone.GetComponentInChildren<Renderer>().enabled = false;
        zone.GetComponent<Renderer>().material.color = color;
    }
}
