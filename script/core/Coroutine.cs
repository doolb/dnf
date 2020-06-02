using System.Collections;
using System.Collections.Generic;
using System.Text;
using Godot;

namespace Core
{
    public class Coroutine : Node
    {
        public override void _EnterTree() {
            Name = nameof(Coroutine);
            Debug.Log(nameof(Coroutine) + " start");
        }

        public override void _Process(float delta) {
            for (int i = 0; i < routines.Count; i++) {
                if (routines[i].IsDefault())
                    continue;
                if (routines[i].Value.Current == null) {
                    if (!routines[i].Value.MoveNext()) {
                        stop(i);
                        continue;
                    }
                }

                CustomYieldInstruction yielder = routines[i].Value.Current as CustomYieldInstruction;
                if (yielder.keepWaiting)
                    continue;
                bool yielded = yielder.MoveNext();
                if (!yielded && !routines[i].Value.MoveNext())
                    stop(i);
            }

            if (OS.IsDebugBuild()) {
                StringBuilder sb = new StringBuilder();
                sb.Append("count").Append(": ").Append(routines.Count).AppendLine();
                for (int i = 0; i < routines.Count; i++) {
                    if (!routines[i].IsDefault())
                        sb.Append("  ").Append(routines[i].Key.Name).Append(routines[i].Value).AppendLine();
                }
                sb.Append("idle").Append(": ").Append(empty_routine.Count).AppendLine();
                EditorDescription = sb.ToString();
            }
        }

        public List<KeyValuePair<Node, IEnumerator>> routines = new List<KeyValuePair<Node, IEnumerator>>();
        public Stack<int> empty_routine = new Stack<int>();

        public void start(Node node, IEnumerator routine) {
            Debug.Log("start coroutine : " + node?.Name + " " + routine.ToString());
            if (empty_routine.Count == 0)
                routines.Add(new KeyValuePair<Node, IEnumerator>(node, routine));
            else
                routines[empty_routine.Pop()] = new KeyValuePair<Node, IEnumerator>(node, routine);
        }
        // stop routine at index
        internal void stop(int index) {
            Debug.Log("stop or finish coroutine : " + routines[index].Key?.Name + " " + routines[index].Value.ToString());
            routines[index] = default;
            empty_routine.Push(index);
        }
        // stop routine on node
        // if routine is null, then stop all on node
        public void stop(Node node, IEnumerator routine) {
            if(node == null && routine == null) // what
                return;
            Debug.Log("stop coroutine : " + node.Name + " " + routine);
            for (int i = 0; i < routines.Count; i++) {
                if (routines[i].Key == node) {
                    if (routine == null || routine == routines[i].Value) // todo: is need return when remove the special routine
                        stop(i);
                }
            }
        }
        // method for no node base
        public void start(IEnumerator _rt){
            start(null, _rt);
        }
        public void stop(IEnumerator _rt){
            stop(null, _rt);
        }
    }

    public static class CoroutineExtension
    {
        public static IEnumerator StartCoroutine(this Node node, IEnumerator routine) {
            GameManager.Instance.Get<Coroutine>().start(node, routine);
            return routine;
        }
        public static bool IsDefault<T>(this T value) where T : struct {
            return value.Equals(default(T));
        }
        public static void StopCoroutine(this Node node, IEnumerator routine) {
            GameManager.Instance.Get<Coroutine>().stop(node, routine);
        }
        public static void StopAllCoroutine(this Node node) {
            GameManager.Instance.Get<Coroutine>().stop(node, null);
        }
    }

    public abstract class CustomYieldInstruction : IEnumerator
    {
        public object Current { get { return null; } }
        public abstract bool keepWaiting { get; }
        public bool MoveNext() {
            return keepWaiting;
        }
        public void Reset() {
        }
        public static implicit operator CustomYieldInstruction(int frame) {
            return new WaitForFrame(frame);
        }
        public static explicit operator CustomYieldInstruction(float second) {
            return new WaitForSeconds(second);
        }
    }

    public sealed class Routine : CustomYieldInstruction
    {
        IEnumerator routine;

        public Routine(IEnumerator routine) {
            this.routine = routine;
        }

        public override bool keepWaiting { get { return routine.MoveNext(); } }
    }
    public class WaitForFrame : CustomYieldInstruction
    {
        public int finishFrame;
        public WaitForFrame(int frame) {
            finishFrame = Time.frameCount + frame;
        }
        public override bool keepWaiting { get { return finishFrame > Time.frameCount; } }
    }
    public class WaitForSeconds : CustomYieldInstruction
    {
        public float finishTime;
        public WaitForSeconds(float seconds) {
            finishTime = Time.time + seconds;
        }
        public override bool keepWaiting { get { return finishTime > Time.time; } }
    }

    public class WaitForSecondsRealtime : CustomYieldInstruction
    {
        public float finishTime;
        public WaitForSecondsRealtime(float seconds) {
            finishTime = Time.realtimeSinceStartup + seconds;
        }
        public override bool keepWaiting { get { return finishTime > Time.realtimeSinceStartup; } }
    }
}