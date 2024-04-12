using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 1f;
    [SerializeField] private Transform _viewContainer;
    private MeshRenderer _meshRenderer;
    private float _angleHelper = 0f;
    private Vector3 _rotationHelperVec = Vector3.zero;
    private BallSkinData currentSkinData;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ChangeSkin(BallSkinData newData)
    {
        if (currentSkinData == newData) return;
        currentSkinData = newData;
        _meshRenderer.sharedMaterial.mainTexture = newData.texture;
    }

    public void RotateView(bool isForwardMove)
    {
        if (isForwardMove)
        {
            _angleHelper += Time.deltaTime * _rotationSpeed;
            _viewContainer.localEulerAngles = new Vector3(_angleHelper, 0, 0);
        }
        else
        {
            _angleHelper += Time.deltaTime * -_rotationSpeed;
            _viewContainer.localEulerAngles = new Vector3(0, 0, _angleHelper);
        }
    }

    public void OnBallTurning()
    {
        _angleHelper = 0;
        _rotationHelperVec = transform.eulerAngles;
        _viewContainer.localEulerAngles = Vector3.zero;
        transform.eulerAngles = _rotationHelperVec;
    }

    public void ChangeViewRotationSpeed(float newBallSpeed)
    {
        _rotationSpeed = newBallSpeed*100;
    }
}
