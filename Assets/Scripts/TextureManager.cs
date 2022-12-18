using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (SpriteRenderer))]
public class TextureManager : MonoBehaviour
{
    public SpriteRenderer rend;

    //deprecated
    public Texture2D src;
    private Texture2D tex;

    public Texture2D[] layers;

    private void MakeTexture()
    {  
        
        // array to store the destination texture's pixels
        Color[] colorArray = new Color[layers[0].width * layers[0].height];
        // array of colors derived from the source texture
        Color[][] srcArray = new Color[layers.Length][];
        // populate source array with layer arrays
        for (int i = 0; i < layers.Length; i++)
        {
            srcArray[i] = layers[i].GetPixels();
        }



        for (int x = 0; x < tex.width; x++) {
            for (int y = 0; y < tex.height; y++) {
                int pixelIndex = x + (y * tex.width);
                for (int i = 0; i < layers.Length; i++) { 
                    Color srcPixel = srcArray[i][pixelIndex];
                    if (srcPixel.a > 0) { // only if new layer pixel is not completely transparent
                        colorArray[pixelIndex] = srcPixel;
                    }
                }
            }
        }
        tex.SetPixels(colorArray);
        tex.Apply();

        tex.wrapMode = TextureWrapMode.Clamp;
        tex.filterMode = FilterMode.Point;
    }

    private void MakeSprite() {
        //create a sprite from texture
        Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

        // assign procedural sprite to sprite renderer.sprite
        rend.sprite = newSprite;
    }

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        MakeTexture();
        MakeSprite();
        
    }


    public void firstMethod() {
        rend = GetComponent<SpriteRenderer>();

        //create a texture
        Texture2D tex = new Texture2D(96, 96);

        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                tex.SetPixel(x, y, Color.red);
            }
        }
        tex.Apply();

        //create a sprite from texture
        Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

        // assign procedural sprite to sprite renderer.sprite
        rend.sprite = newSprite;
    }

    public void secondMethod() {

        rend = GetComponent<SpriteRenderer>();

        //create a texture
        Texture2D tex = new Texture2D(96, 96);
        Color[] colorArray = new Color[tex.width * tex.height];

        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                colorArray[x + (y * tex.width)] = Color.red;
            }
        }
        tex.SetPixels(colorArray);
        tex.Apply();

        //create a sprite from texture
        Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

        // assign procedural sprite to sprite renderer.sprite
        rend.sprite = newSprite;
    }

    private void ThirdMethod()
    {
        rend = GetComponent<SpriteRenderer>();

        //create a texture
        Texture2D tex = new Texture2D(96, 96);
        Color[] colorArray = new Color[tex.width * tex.height];

        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                colorArray[x + (y * tex.width)] = Color.Lerp(Color.black, Color.white, (float)y / (float)tex.width);
            }
        }
        tex.SetPixels(colorArray);
        tex.Apply();

        tex.wrapMode = TextureWrapMode.Clamp;
        tex.filterMode = FilterMode.Point;

        //create a sprite from texture
        Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

        // assign procedural sprite to sprite renderer.sprite
        rend.sprite = newSprite;
    }

    private void ForthMethod() {
        rend = GetComponent<SpriteRenderer>();

        //create a texture
        Texture2D tex = new Texture2D(src.width, src.height);
        Color[] colorArray = new Color[tex.width * tex.height];

        Color[] srcArray = src.GetPixels();

        for (int x = 0; x < tex.width; x++)
        {
            for (int y = 0; y < tex.height; y++)
            {
                int pixelIndex = x + (y * tex.width);
                Color srcPixel = srcArray[pixelIndex];
                colorArray[pixelIndex] = srcPixel;
            }
        }
        tex.SetPixels(colorArray);
        tex.Apply();

        tex.wrapMode = TextureWrapMode.Clamp;
        tex.filterMode = FilterMode.Point;

        //create a sprite from texture
        Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

        // assign procedural sprite to sprite renderer.sprite
        rend.sprite = newSprite;
    }

    
}

