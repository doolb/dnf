using System;
using System.Collections.Generic;
using System.Threading;
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

        static GameManager() {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
        }
        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e) {
            // Log the exception, display it, etc
            Debug.LogError(e.Exception.Message);
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            // Log the exception, display it, etc
            var msg = e.ExceptionObject is GameException ? (e.ExceptionObject as GameException).Message : ((e.ExceptionObject as Exception).Message + "\n" +
                (e.ExceptionObject as Exception).StackTrace);
            if (!OS.IsDebugBuild())
                System.Diagnostics.Process.Start(OS.GetExecutablePath(), "scene/error/error.tscn \"" + msg + "\"");
            Debug.LogError(msg);
        }

        Node coreNode;
        public string ExePath { get; } = OS.GetExecutablePath();
        public string CodePath { get; } = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public string PrjPath { get; } = System.Reflection.Assembly.GetExecutingAssembly().Location.GetLastStringOf('/', 5);
        public override void _EnterTree() {
            if (Instance != null) {
                Debug.LogError("there is a GameManager in scene.");
                this.QueueFree();
                return;
            }
            Instance = this;
            Name = nameof(GameManager);
            Debug.Log(nameof(GameManager) + " start");
            Debug.Log("godot path: " + ExePath);
            Debug.Log("code path: " + CodePath);
            Debug.Log("project path: " + PrjPath);

            // add modules
            coreNode = new Node() { Name = "Core" };
            AddChild(coreNode);
            addModule(new Time(), coreNode);
            addModule(new Coroutine(), coreNode);
            addModule(new ResourceManager(), coreNode);
            addModule(new ConfigManager(), coreNode);
        }

        internal void addModule(Node ins, Node parent) {
            ins.Name = ins.GetType().Name;
            modules.Add(ins.GetType(), ins);
            parent.AddChild(ins);
        }
    }
}
