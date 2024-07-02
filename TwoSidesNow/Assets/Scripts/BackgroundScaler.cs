using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    public GameObject _background;
    private ProCamera2D _proCamera;
    private Vector3 _initialScale;

    [SerializeField] private float _scaleMultiplier = 1f;

    void Start()
    {
        _proCamera = GetComponent<ProCamera2D>();
        _initialScale = _background.transform.localScale;
    }

    void Update()
    {
        if (_proCamera != null && _background != null)
        {
            float orthographicSize = _proCamera.GameCamera.orthographicSize;
            float scaleFactor = orthographicSize * _scaleMultiplier;

            _background.transform.localScale = _initialScale * scaleFactor / 10;
        }
    }
}
