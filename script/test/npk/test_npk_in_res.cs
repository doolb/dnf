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
    public override void _Ready() {
        image = GetNode(nameof(image)) as TextureRect;
        albumList = GetNode(nameof(albumList)) as ScrollList;
        //albumList.Connect("item_selected", this, nameof(_selectAlbum));
        spriteList = GetNode(nameof(spriteList)) as ItemList;
        //spriteList.Connect("item_selected", this, nameof(setImage));

        albumList.Init(() =>
        {
            return new Button() { Align = Button.TextAlign.Left };
        }, (_c, _i) =>
        {
            (_c as Button).Text = npkKeys[_i];
            (_c as Button).Name = _i.ToString();
        }, new Vector2(0, 24));

        ResourceManager.Instance.Connect(nameof(ResourceManager.load_npk_header_), this, nameof(_show_npk_list));
    }

    ScrollList albumList;
    int selectAlbum;
    String[] npkKeys;
    void _show_npk_list(int @count) {
        npkKeys = ResourceManager.Instance.allNpkData.Keys.ToArray();
        albumList.Show(npkKeys.Length);

        //selectAlbum = 0;
        //if (albums.Count > 0)
        //    setImage(0);
        //GC.Collect();
    }
    int selectSprite;
    ItemList spriteList;
    //void _selectAlbum(int index) {
    //    spriteList.Clear();
    //    selectAlbum = index;
    //    var album = albums[selectAlbum];
    //    for (int i = 0; i < album.Count; i++) {
    //        spriteList.AddItem($"{album.List[i].Index} {album.List[i].Version} {album.List[i].Type} {album.List[i].Height} {album.List[i].Width} {album.List[i].FrameSize} {album.List[i].Location}");
    //    }
    //    if (album.Count > 0)
    //        setImage(0);
    //}
    //
    //void setImage(int index) {
    //    selectSprite = Mathf.Min(index, albums[selectAlbum].List.Count);
    //    image.Texture?.Dispose();
    //    ImageTexture tex = new ImageTexture();
    //    Debug.Log($"select : {albums[selectAlbum].Path} {selectAlbum} {selectSprite}");
    //    var godotText = albums[selectAlbum].List[selectSprite].Picture as GodotTexture;
    //    if (godotText != null) {
    //        tex.CreateFromImage(godotText.Image);
    //    }
    //    image.Texture = tex;
    //}
}