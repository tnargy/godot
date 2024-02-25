using Godot;
using System;

public partial class UIContainer : VBoxContainer
{
    [Export] public ContainerType container { get; private set; }
}
