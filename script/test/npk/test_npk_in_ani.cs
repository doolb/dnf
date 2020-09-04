using Core;
using Godot;
using UI;
using System;
using System.Collections.Generic;
using System.Linq;
using Game.Config;
using Game.Render;

public class test_npk_in_ani : Control
{
    AnimeRect image;
    List<ButtonBase<Button>> buttons = new List<UI.ButtonBase<Button>>();
    public override void _Ready() {
        image = GetNode(nameof(image)) as AnimeRect;
        albumList = GetNode(nameof(albumList)) as ScrollList;
        spriteList = GetNode(nameof(spriteList)) as ItemList;
        spriteList.Connect("item_selected", this, nameof(setImage));

        albumList.Init(() =>
        {
            var btn = new Button() { Align = Button.TextAlign.Left };
            var btnbase = new ButtonBase<Button>(btn);
            btnbase.onButtonClick += _selectAlbum;
            buttons.Add(btnbase);
            return btn;
        }, (_c, _i, _t) =>
        {
            (_c as Button).Text = npkKeys[_i];
            buttons[_t].Id = _i;
            buttons[_t].VId = _t;
            _c.Name = _i.ToString();
            buttons[_t].target.Flat = _i == selectAlbum;
        }, new Vector2(0, 24));

        ConfigManager.Instance.Connect(nameof(ConfigManager.load_config_ok), this, nameof(_show_npk_list));
    }

    ScrollList albumList;
    String[] npkKeys;
    void _show_npk_list() {
        npkKeys = ConfigManager.Instance.ResCfgs[typeof(AnimeConfig)].Keys.ToArray();
        Debug.Log("anime config count: " + npkKeys.Length);
        albumList.Show(npkKeys.Length);

        //selectAlbum = 0;
        //if (albums.Count > 0)
        //    setImage(0);
        //GC.Collect();
    }
    int selectAlbum = -1;
    int selectBtn = -1;
    ItemList spriteList;
    void _selectAlbum(ButtonBase<Button> _button) {
        selectAlbum = _button.Id;
        selectBtn = _button.VId;
        var cfg = ConfigManager.Instance.GetRes<AnimeConfig>(npkKeys[selectAlbum]);
        $"{_button.Id} {npkKeys[_button.Id]} {cfg.key}".log();

        spriteList.Clear();
        if (selectBtn > 0 && selectBtn != _button.VId) {
            buttons[selectBtn].target.Flat = false;
            ConfigManager.Instance.UnloadRes(ConfigManager.Instance.GetRes<AnimeConfig>(npkKeys[buttons[selectBtn].Id]));
        }
        //albumList.ScrollIndex(selectAlbum, _button.Id);

        buttons[_button.VId].target.Flat = true;
        var album = cfg.Frames;
        for (int i = 0; i < album.Length; i++) {
            spriteList.AddItem($"{album[i].Image} {album[i].ImageIdx} {album[i].Delay}");
        }
        if (album.Length > 0)
            setImage(0);
    }

    // use texutrerect
    void setImage_on_image(int index) {
        var cfg = ConfigManager.Instance.GetRes<AnimeConfig>(npkKeys[selectAlbum]);
        image.Show(cfg, index);
    }


    SceneObject sobj;
    // use scene object
    void setImage(int index) {
        if(sobj == null){
            sobj = new SceneObject();
            SceneManager.Instance.AddObject(sobj);
        }
        sobj.anime = npkKeys[selectAlbum];
        SceneManager.Instance.UpdateObject(sobj);
    }
}