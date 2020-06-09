using Core;
using ExtractorSharp.Core.Coder;
using ExtractorSharp.Core.Model;
using Godot;
using System;
using ExtractorSharp.Core;
using System.Collections.Generic;

public class test_npk : Control
{
    Button fileBtn;
    TextureRect image;
    public override void _Ready() {
        fileBtn = GetNode(nameof(fileBtn)) as Button;
        fileBtn.Connect("pressed", this, "onClickFile");
        image = GetNode(nameof(image)) as TextureRect;
        dialog = GetNode(nameof(dialog)) as FileDialog;
        dialog.Connect("file_selected", this, nameof(_openFile));
        albumList = GetNode(nameof(albumList)) as ItemList;
        albumList.Connect("item_selected", this, nameof(_selectAlbum));
        spriteList = GetNode(nameof(spriteList)) as ItemList;
        spriteList.Connect("item_selected", this, nameof(setImage));
    }

    List<Album> albums;
    FileDialog dialog;
    void onClickFile() {
        dialog.Popup_();
        //albums= NpkCoder.Load(GameManager.Instance.PrjPath + "sprite/sprite.npk");
    }
    int selectAlbum;
    ItemList albumList;
    void _openFile(string path) {
        path = path.Replace("res://", GameManager.Instance.PrjPath);
        albums = NpkCoder.Load(path);
        albums.Count.ToString().log();

        albumList.Clear();
        for (int i = 0; i < albums.Count; i++)
            albumList.AddItem(albums[i].Name);

        selectAlbum = 0;
        if (albums.Count > 0)
            setImage(0);
        GC.Collect();
    }

    int selectSprite;
    ItemList spriteList;
    void _selectAlbum(int index) {
        spriteList.Clear();
        selectAlbum = index;
        var album = albums[selectAlbum];
        for (int i = 0; i < album.Count; i++) {
            spriteList.AddItem($"{album.List[i].Index} {album.List[i].Version} {album.List[i].Type} {album.List[i].Height} {album.List[i].Width} {album.List[i].FrameSize} {album.List[i].Location}");
        }
        if (album.Count > 0)
            setImage(0);
    }

    void setImage(int index) {
        selectSprite = Mathf.Min(index, albums[selectAlbum].List.Count);
        image.Texture?.Dispose();
        ImageTexture tex = new ImageTexture();
        Debug.Log($"select : {dialog.CurrentPath} {selectAlbum} {selectSprite}");
        var godotText = albums[selectAlbum].List[selectSprite].Picture as GodotTexture;
        if (godotText != null) {
            tex.CreateFromImage(godotText.Image);
        }
        else {
            var bmptex = albums[selectAlbum].List[selectSprite].Picture as BitmapTexture;
            tex.CreateFromImage(bmptex.Image.ConvertToImage());
        }
        image.Texture = tex;
    }
}