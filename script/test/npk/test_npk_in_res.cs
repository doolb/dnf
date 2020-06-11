using Core;
using ExtractorSharp.Core.Coder;
using ExtractorSharp.Core.Model;
using Godot;
using UI;
using System;
using ExtractorSharp.Core;
using System.Collections.Generic;
using System.Linq;

public class test_npk_in_res : Control
{
    TextureRect image;
    List<ButtonBase<Button>> buttons = new List<UI.ButtonBase<Button>>();
    public override void _Ready() {
        image = GetNode(nameof(image)) as TextureRect;
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

        ResourceManager.Instance.Connect(nameof(ResourceManager.load_npk_header_), this, nameof(_show_npk_list));
    }

    ScrollList albumList;
    String[] npkKeys;
    void _show_npk_list(int @count) {
        npkKeys = ResourceManager.Instance.allNpkData.Keys.ToArray();
        albumList.Show(npkKeys.Length);

        //selectAlbum = 0;
        //if (albums.Count > 0)
        //    setImage(0);
        //GC.Collect();
    }
    int selectAlbum = -1;
    int selectBtn = -1;
    int selectSprite;
    ItemList spriteList;
    void _selectAlbum(ButtonBase<Button> _button) {
        var npk = ResourceManager.Instance.allNpkData[npkKeys[_button.Id]];
        var path = System.IO.Path.GetFileName(npk.filePath);
        $"{_button.Id} {npkKeys[_button.Id]} {path} {npk.index}".log();
        npk.album.LoadImage(npk.filePath);
        spriteList.Clear();
        if (selectBtn > 0 && selectBtn != _button.VId){
            buttons[selectBtn].target.Flat = false;
            ResourceManager.Instance.allNpkData[npkKeys[buttons[selectBtn].Id]].album.UnloadImage();
        }
        //albumList.ScrollIndex(selectAlbum, _button.Id);
        selectAlbum = _button.Id;
        selectBtn = _button.VId;
        buttons[_button.VId].target.Flat = true;
        var album = npk.album;
        for (int i = 0; i < album.List.Count; i++) {
            spriteList.AddItem($"{album.List[i].Index} {album.List[i].Version} {album.List[i].Type} {album.List[i].Height} {album.List[i].Width} {album.List[i].FrameSize} {album.List[i].Location}");
        }
        if (album.Count > 0)
            setImage(0);
    }
    void setImage(int index) {
        var album = ResourceManager.Instance.allNpkData[npkKeys[selectAlbum]].album;
        selectSprite = Mathf.Min(index, album.List.Count);
        image.Texture?.Dispose();
        ImageTexture tex = new ImageTexture();
        Debug.Log($"select : {album.Path} {selectAlbum} {selectSprite}");
        var godotText = album.List[selectSprite].Picture as GodotTexture;
        if (godotText != null) {
            tex.CreateFromImage(godotText.Image);
        }
        image.Texture = tex;
    }
}