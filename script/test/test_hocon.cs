using Godot;
using Hocon;
using Core;

public class test_hocon : Node{
    public override void _Ready(){
        var cfg = HoconParser.Parse(EditorDescription);
        //var cfg = HoconConfigurationFactory.ParseString(EditorDescription);
        cfg.ToString().log();
        //cfg.GetString("debug");
    }
}