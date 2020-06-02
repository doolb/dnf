using System;
using System.Collections.Generic;
using Godot;

namespace Core
{
    public class GameManager : Node
    {
        public static GameManager Instance { get; private set; }
        private readonly Dictionary<Type, Node> modules = new Dictionary<Type, Node>();

        public T Get<T>() where T : Node {
            return modules.ContainsKey(typeof(T)) ? (T)modules[typeof(T)] : null;
        }

        public override void _EnterTree() {
            if (Instance != null) {
                Debug.LogError("there is a GameManager in scene.");
                this.QueueFree();
                return;
            }
            Instance = this;
            Name = nameof(GameManager);
            Debug.Log(nameof(GameManager) + " start");

            // add modules
            modules.Add(typeof(Time), new Time());
            modules.Add(typeof(Coroutine), new Coroutine());
            modules.Add(typeof(Game.Main), new Game.Main());

            var mod = modules.Values.GetEnumerator();
            while (mod.MoveNext())
                AddChild(mod.Current);
        }

        #region engine code
        public override void _ExitTree() {
            var mod = modules.Values.GetEnumerator();
            while (mod.MoveNext())
                mod.Current._ExitTree();
        }

        public override void _Ready() {
            var mod = modules.Values.GetEnumerator();
            while (mod.MoveNext())
                mod.Current._Ready();
        }
        public override void _Input(InputEvent @event) {
            var mod = modules.Values.GetEnumerator();
            while (mod.MoveNext())
                mod.Current._Input(@event);
        }
        public override void _Notification(int what) {
            var mod = modules.Values.GetEnumerator();
            while (mod.MoveNext())
                mod.Current._Notification(what);
        }
        public override void _Process(float delta) {
            var mod = modules.Values.GetEnumerator();
            while (mod.MoveNext())
                mod.Current._Process(delta);
        }
        public override void _PhysicsProcess(float delta) {
            var mod = modules.Values.GetEnumerator();
            while (mod.MoveNext())
                mod.Current._PhysicsProcess(delta);
        }
        #endregion
    }
}