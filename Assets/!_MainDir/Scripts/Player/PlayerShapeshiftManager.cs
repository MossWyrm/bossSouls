using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerShapeshiftManager : MonoBehaviour
{
    [SerializeField] private Character baseCharacter;
    private PlayerStats _playerStats;
    private AbilityManager _abilityManager;
    private Character[] _absorbedCharacters = new Character[4];
    private GameObject[] _absorbedCharacterModels = new GameObject[4];
    private CinemachineCamera _camera;
    
    public Character CurrentCharacter { get; private set; }

    internal int CurrentCharacterIndex = -1;
    private void Awake()
    {
        baseCharacter = Instantiate(baseCharacter);
        _absorbedCharacters[0] = baseCharacter;
        CurrentCharacter ??= _absorbedCharacters[0];
        _playerStats ??= GetComponent<PlayerStats>();
        _abilityManager ??= GetComponent<AbilityManager>();
        TransformInto(0);
        _camera = GetComponent<CinemachineCamera>();
    }
    
    /// <summary>
    /// Use 0 for default character, 1, 2 or 3 for absorbed souls. Returns true when form is changed.
    /// </summary>
    /// <param name="charNum"></param>
    /// <returns></returns>
    public bool RequestTransform(int charNum)
    {
        if (charNum < -1 || charNum > _absorbedCharacters.Length) return false;
        if (charNum == CurrentCharacterIndex) return false;
        if(_absorbedCharacters[charNum] == null) return false;
        TransformInto(charNum);
        return true;
    }

    private void TransformInto(int charNum)
    {
        CurrentCharacterIndex = charNum;
        CurrentCharacter = _absorbedCharacters[charNum];
        foreach (var t in _absorbedCharacterModels)
        {
            t?.SetActive(false);
        }

        if (_absorbedCharacterModels[charNum] == null)
        {
            _absorbedCharacterModels[charNum] = Instantiate(_absorbedCharacters[charNum].characterPrefab, transform.position, transform.rotation);
            _absorbedCharacterModels[charNum].transform.SetParent(transform);
        }
        _absorbedCharacterModels[charNum].SetActive(true);
        _abilityManager.ChangeCharacter(CurrentCharacter);
    }
}