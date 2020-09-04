using System.Collections.Generic;
using Core;
using Game.Config;
using Godot;

namespace Game.Render
{
    public class SceneRender : Node2D, ISceneRender
    {
        Node objectRoot;
        AnimeSprite spritePrefab;
        Camera2D camera;


        public override void _EnterTree() {
            objectRoot = GetNode("Object");
            spritePrefab = objectRoot.GetNode<AnimeSprite>("prefab");
            spritePrefab.Visible = false;
            camera = GetNode<Camera2D>("Camera");
        }

        readonly Dictionary<string, Dictionary<SceneObject, AnimeSprite>> spriteDic = new Dictionary<string, Dictionary<SceneObject, AnimeSprite>>();

        public void _CreateObject(SceneObject obj) {
            string layer = obj.layer;
            if (!spriteDic.ContainsKey(layer)) {
                spriteDic.Add(layer, new Dictionary<SceneObject, AnimeSprite>());
                if (objectRoot.GetNode(layer) == null) {
                    var n = new Node { Name = layer };
                    objectRoot.AddChild(n);
                }
            }
            if (!spriteDic[layer].ContainsKey(obj)) {
                var sp = spritePrefab.Duplicate() as AnimeSprite;
                sp.Material = sp.Material.Duplicate() as Material; // also duplicate material
                sp.Name = obj.name;
                spriteDic[layer].Add(obj, sp);
                objectRoot.GetNode(layer).AddChild(sp);
            }
        }

        public void _UpdateObject(SceneObject obj) {
            var sp = spriteDic[obj.layer][obj];
            sp.Visible = true;
            var cfg = ConfigManager.Instance.GetRes<AnimeConfig>(obj.anime);
            sp.Show(cfg);
        }
    }
}