using System;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerShapeshiftManager : MonoBehaviour
{
    [SerializeField] private Character baseCharacter;
    private Player _player;
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
        _player ??= GetComponent<Player>();
        _abilityManager ??= GetComponent<AbilityManager>();
        TransformInto(0);
        _camera = GetComponent<CinemachineCamera>();
    }

    private void OnEnable()
    {
        CustomPlayerInputManager.TransformNextPerformed += TransformNext;
        CustomPlayerInputManager.TransformPreviousPerformed += TransformPrevious;
    }

    private void OnDisable()
    {
        CustomPlayerInputManager.TransformNextPerformed -= TransformNext;
        CustomPlayerInputManager.TransformPreviousPerformed -= TransformPrevious;
    }

    public void TransformNext()
    {
        var characterTransformRequest = (CurrentCharacterIndex + 1) % 3;
        RequestTransform(characterTransformRequest);
    }
    
    public void TransformPrevious()
    {
        var characterTransformRequest = (CurrentCharacterIndex + 2) % 3;
        RequestTransform(characterTransformRequest);
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
        _player.NewPlayerModel(_absorbedCharacterModels[charNum]);
        _abilityManager.ChangeCharacter(CurrentCharacter);
    }
}