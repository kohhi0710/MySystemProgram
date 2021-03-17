using UnityEngine;

/// <summary>
/// 参考:https://qiita.com/kazuma_f/items/e524b3dfffbb100893e1
/// </summary>
public class LookAtGameObject : MonoBehaviour
{
    public Transform[] _Position;
    int _TargetNumber = 0;
    float _RotatePosition = 0f;
    float _RotateSpeed = 0.1f;
    Vector3 _Direction;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _TargetNumber++;

            if (_TargetNumber >= _Position.Length)
                _TargetNumber = 0;

            _Direction = _Position[_TargetNumber].position - transform.position;
            _RotatePosition = 0f;
        }

        if (_RotatePosition < 1f)
        {
            _RotatePosition += _RotateSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Lerp(transform.rotation,
                                                 Quaternion.LookRotation(_Direction),
                                                 _RotatePosition);
        }
    }
}
