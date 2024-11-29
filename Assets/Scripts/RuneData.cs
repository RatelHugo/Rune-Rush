using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRune", menuName = "Runes/RuneData")]
public class RuneData : ScriptableObject
{
    public Sprite runeSprite; 
    public List<Vector2> validZonePositions; 
    public List<Vector2> trapZonePositions;
    public int level;
}
