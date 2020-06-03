using System.Collections.Generic;
using Core;
using Godot;
using Game;

public class debug_view : Label, IConfigable{

    public override void _EnterTree(){
        Debug.OnDebugCallback += on_debug_msg;
        LoadCfg(EditorDescription);
    }

    int maxCount = 10;
    
    Queue<int> msgLen = new Queue<int>();

    System.Text.StringBuilder debugMsgs = new System.Text.StringBuilder();

    bool on_debug_msg(Debug.Level level, string msg){
        debugMsgs.AppendLine(msg);
        msgLen.Enqueue(msg.Length);

        if(msgLen.Count > maxCount){
            debugMsgs.Remove(0, msgLen.Dequeue() + 2);
        }
        Text = debugMsgs.ToString();
        return true;
    }

    public void LoadCfg(string str) {
        if(string.IsNullOrEmpty(str)) return;
        var cfg = Hocon.HoconParser.Parse(str);
        maxCount = cfg.GetInt("max");
        Debug.Log("set max debug: " + maxCount);
    }
}