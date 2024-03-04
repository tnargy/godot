using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Reflection.Metadata;

public partial class UIController : Control
{
    private Dictionary<ContainerType, UIContainer> containers;
    private bool canPause = false;

    public override void _Ready()
    {
        containers = GetChildren().Where(
            (element) => element is UIContainer
        ).Cast<UIContainer>().ToDictionary(
            (element) => element.container
        );

        containers[ContainerType.Start].Visible = true;
        containers[ContainerType.Start].ButtonNode.Pressed += HandleStartPressed;
        containers[ContainerType.Pause].ButtonNode.Pressed += HandlePausePressed;
        containers[ContainerType.Reward].ButtonNode.Pressed += HandleRewardPressed;
        containers[ContainerType.Victory].ButtonNode.Pressed += HandleVictoryPressed;
        containers[ContainerType.Defeat].ButtonNode.Pressed += HandleDefeatPressed;
        
        GameEvents.OnEndGame += HandleEndGame;
        GameEvents.OnVictory += HandleVictory;
        GameEvents.OnReward += HandleReward;
    }

    public override void _Input(InputEvent @event)
    {
        // Start Game
        if (containers[ContainerType.Start].Visible && Input.IsActionJustPressed(GameConstants.INPUT_ATTACK)) {
            HandleStartPressed();
        }

        // Reward Screen
        if (containers[ContainerType.Reward].Visible && Input.IsActionJustPressed(GameConstants.INPUT_ATTACK)) {
            HandleRewardPressed();
        }

        // Pause Game
        if (!canPause) { return; }
        if (!Input.IsActionJustPressed(GameConstants.INPUT_PAUSE))
        {
            return;
        }
        containers[ContainerType.Stats].Visible = GetTree().Paused;
        containers[ContainerType.Minimap].Visible = GetTree().Paused;
        GetTree().Paused = !GetTree().Paused;
        containers[ContainerType.Pause].Visible = GetTree().Paused;
    }

    private void HandleStartPressed()
    {
        canPause = true;
        GetTree().Paused = false;
        containers[ContainerType.Start].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        containers[ContainerType.Minimap].Visible = true;
        
        GameEvents.RaiseStartGame();
    }

    private void HandleEndGame()
    {
        canPause = false;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Minimap].Visible = false;
        containers[ContainerType.Defeat].Visible = true;
    }

    private void HandleVictory()
    {
        canPause = false;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Minimap].Visible = false;
        containers[ContainerType.Victory].Visible = true;
        GetTree().Paused = true;
    }
    
    private void HandlePausePressed()
    {
        GetTree().Paused = false;

        containers[ContainerType.Pause].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        containers[ContainerType.Minimap].Visible = true;
    }

    private void HandleReward(RewardResource resource)
    {
        canPause = false;
        GetTree().Paused = true;
        containers[ContainerType.Stats].Visible = false;
        containers[ContainerType.Minimap].Visible = false;
        containers[ContainerType.Reward].Visible = true;

        containers[ContainerType.Reward].TextureNode.Texture = resource.SpriteTexture;
        containers[ContainerType.Reward].LabelNode.Text = resource.Description;
    }

    private void HandleRewardPressed()
    {
        canPause = true;
        GetTree().Paused = false;

        containers[ContainerType.Reward].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        containers[ContainerType.Minimap].Visible = true;
    }

    private void HandleVictoryPressed()
    {
        canPause = true;
        GetTree().Paused = false;

        containers[ContainerType.Reward].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        containers[ContainerType.Minimap].Visible = true;

        GameEvents.RaiseNextLevel();
    }
    
    private void HandleDefeatPressed()
    {
        canPause = true;
        GetTree().Paused = false;

        containers[ContainerType.Reward].Visible = false;
        containers[ContainerType.Stats].Visible = true;
        containers[ContainerType.Minimap].Visible = true;

        GameEvents.RaiseRestartLevel();
    }
}
