using System.Collections.Generic;
using System.Diagnostics;

namespace Core
{
    public class SceneManager : Godot.Node
    {
        [DebuggerHidden]
        public static SceneManager Instance => GameManager.Instance.Get<SceneManager>();

        ISceneRender sceneRender;
        public override void _EnterTree() {
            sceneRender = GameManager.Instance.GetNode<ISceneRender>("Scene");
        }

        public Dictionary<string, List<SceneObject>> sceneDic;
        public const string default_layer = "normal";
        public void AddObject(SceneObject obj) {
            if (string.IsNullOrEmpty(obj.layer))
                obj.layer = default_layer;
            sceneRender._CreateObject(obj);
        }
        public void UpdateObject(SceneObject obj) {
            sceneRender._UpdateObject(obj);
        }
    }

    public interface ISceneRender
    {
        void _CreateObject(SceneObject obj);
        void _UpdateObject(SceneObject obj);
    }
}