using System.Collections;
using UnityEngine;

public class MoveCommand : ICommand
{

    private Vector3 _direction;
    private Transform _target;
    private Transform _originalTargetParent;


    public MoveCommand(Transform target, Transform originalTargetParent, Vector3 direction)
    {
        _target = target;
        _direction = direction;
        _originalTargetParent = originalTargetParent;
    }
    public void Execute()
    {
        Move(_direction, _direction);
    }
    public void Undo()
    {
        _target.parent = _originalTargetParent;
        Move(-_direction, _direction);
    }

    private void Move(Vector3 direction, Vector3 facing)
    {
        Vector3 destination = _target.position + direction;
        _target.rotation = Quaternion.LookRotation(Vector3.forward, facing);
        _target.position = destination;
    }
}