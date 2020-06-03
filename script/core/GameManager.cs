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

        Node coreNode;
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
            coreNode = new Node(){Name = "Core"};
            AddChild(coreNode);
            addModule(new Time(), coreNode);
            addModule(new Coroutine(), coreNode);
            addModule(new ResourceManager(), coreNode);
            addModule(new ConfigManager(), coreNode);
        }
        
        internal void addModule(Node ins, Node parent){
            ins.Name = ins.GetType().Name;
            modules.Add(ins.GetType(), ins);
            parent.AddChild(ins);
        }
    }
}