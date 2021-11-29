using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IInteractable
{
    public void Interact();
}
public interface IAttackable
{
    public void Attack();
}
public interface IHoldinteract
{
    public void HoldInteract();
}
public interface IProgrammable
{
    public void Program(USB usb);
}