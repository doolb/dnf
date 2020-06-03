using System.Text;
using Godot;

namespace Core
{
    public class Time : Node
    {
        public static float time { get; internal set; }
        public static float realtimeSinceStartup { get { return OS.GetTicksMsec() / 1000.0f; } }
        public static float timeScale { get { return Engine.TimeScale; } set { Engine.TimeScale = value; } }
        public static int frameCount { get { return Engine.GetFramesDrawn(); } }
        public override void _EnterTree() {
            //time = frameCount / Engine.GetFramesPerSecond();
            Engine.TargetFps = 60;
            time = (float)frameCount / Engine.TargetFps;
            Debug.Log(nameof(Time) + " start " + time);
        }
        public override void _Process(float delta) {
            time += delta;
            //time = (float)frameCount / Engine.TargetFps; // use targetfps because GetFramesPerSecond == 1 on start
            //Debug.Log("time " + Time.time + " " + Time.frameCount + " " + Engine.TargetFps + " " + Engine.GetIdleFrames() + " " + Engine.GetFramesPerSecond());

            if (OS.IsDebugBuild()) {
                EditorDescription = this.ToString() + delta.ToString();
            }
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            sb.Append(nameof(time)).Append(": ").Append(time).AppendLine()
                .Append("realtime: ").Append(realtimeSinceStartup).AppendLine()
                .Append(nameof(timeScale)).Append(": ").Append(timeScale).AppendLine()
                .Append(nameof(frameCount)).Append(": ").Append(frameCount).AppendLine()
                .Append("fps: ").Append(Engine.GetFramesPerSecond()).AppendLine();
            return sb.ToString();
        }
    }
}