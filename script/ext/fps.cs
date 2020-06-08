using Godot;
using System;
using Core;

public class fps : Label
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        var mem = $"mem : {OS.GetStaticMemoryUsage()}/{OS.GetStaticMemoryPeakUsage()} -- {OS.GetDynamicMemoryUsage()} \n";
        var cs = $"code : {GC.GetTotalMemory(false)}";
        Text = GameManager.Instance.Get<Time>().ToString() + delta.ToString() +"\n" + mem + cs;
    }
}
