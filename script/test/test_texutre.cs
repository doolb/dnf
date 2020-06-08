using Core;
using Godot;
using System;

public class test_texutre : TextureRect
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public override void _Ready() {
        var img = new Image();
        img.Create(64, 64, false, Image.Format.Rgba8);
        img.Lock();
        for (int r = 0; r < img.GetSize().x; r++) {
            for (int c = 0; c < img.GetSize().y; c++)
                img.SetPixel(r, c, new Color(r / 64.0f, c / 64.0f, 1, 1));
        }
        img.Unlock();
        //img.Load(GameManager.Instance.PrjPath + "../texture/baa.jpg").ToString().log();

        var img_tex = new ImageTexture();
        img_tex.CreateFromImage(img);
        this.Texture = img_tex;
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
