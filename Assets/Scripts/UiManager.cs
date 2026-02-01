using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class UiManager : MonoBehaviour
{
    private Animator _animator;
    private MainControls _mainControls;
    [SerializeField] private TextMeshProUGUI _keyText;
    [SerializeField] private string _rightKey = "Right Key";
    [SerializeField] private string _wrongKey = "Wrong Key";
    [SerializeField] private Image _deathScreen;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _mainControls = new MainControls();
        _mainControls.Enable();
        _mainControls.UI.Enter.started += SkiptoNext;
        _deathScreen.gameObject.SetActive(false);
        _keyText.gameObject.SetActive(false);
    }
    private void SkiptoNext(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        _animator.CrossFade("Disappear",0f,0);
    }

    private void OnDestroy()
    {
        _mainControls.UI.Enter.started -= SkiptoNext;
        _mainControls.Disable();
    }

    private static UiManager _instance;
    public static UiManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindFirstObjectByType<UiManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("UiManager");
                    _instance = go.AddComponent<UiManager>();
                }
            }
            return _instance;
        }
    }

    public IEnumerator ShowKeyText(bool isRightKey)
    {
        _keyText.gameObject.SetActive(true);
        if (isRightKey)
        {
            _keyText.text = _rightKey;
        }
        else
        {
            _keyText.text = _wrongKey;
        }
        yield return new WaitForSeconds(2f);
        _keyText.gameObject.SetActive(false);
    }
    public void ShowDeathScreen()
    {
        _deathScreen.gameObject.SetActive(true);
    }
    public void Destroy() => Destroy(gameObject);
}
