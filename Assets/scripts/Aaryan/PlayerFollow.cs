using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    public Transform player;
    private Vector3 _velocity = Vector3.zero;

    private float _fixedZ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _fixedZ = transform.position.z;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 target = player.position;
        target.z = _fixedZ;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, 0.25f);
    }
}