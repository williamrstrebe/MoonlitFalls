using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[RequireComponent(typeof(SpriteRenderer))]
public class TextureManager : MonoBehaviour
{
    public SpriteRenderer rend;

    //deprecated
    public Texture2D src;
    private Texture2D tex;

    public Texture2D[] textureArray;
    public Color[] colorArray;

    private Texture2D ClearTexture(int width, int height) {
        Texture2D clearTex = new Texture2D(width, height);
        Color[] clearPixels = new Color[width * height];

        clearTex.SetPixels(clearPixels);
        
        return clearTex;
    }

    public Texture2D MakeTexture(Texture2D[] layers, Color[] layerColors)
    {

        if (layers.Length == 0) {
            Debug.LogError("No image layer information in array");
            return Texture2D.whiteTexture;
        } else if (layers.Length == 1) {
            Debug.Log("Only one image layer present.");
            return layers[0];
        }

        // create the base texture
        Texture2D newTexture = new Texture2D(layers[0].width, layers[0].height);
        // array to store the destination texture's pixels
        Color[] colorArray = new Color[newTexture.width * newTexture.height];
        // array of colors derived from the source texture
        Color[][] adjustedLayers = new Color[layers.Length][];
        // populate array with cropped or expanded layer arrays
        for (int i = 0; i < layers.Length; i++)
        {
            // layer with same height/width or the first (base layer)
            if (i == 0 || (layers[i].width == newTexture.width && layers[i].height == newTexture.height))
                adjustedLayers[i] = layers[i].GetPixels();
            else {
                // handle different sized layers
                int getX, getWidth, setX, setWidth;
                getX = (layers[i].width > newTexture.width) ? (layers[i].width - newTexture.width) / 2 : 0;
                getWidth = (layers[i].width > newTexture.width) ? newTexture.width : layers[i].width;
                setX = (layers[i].width < newTexture.width) ? (newTexture.width - layers[i].width) / 2 : 0;
                setWidth = (layers[i].width < newTexture.width) ? layers[i].width : newTexture.width;

                int getY, getHeight, setY, setHeight;
                getY = (layers[i].height > newTexture.height) ? (layers[i].height - newTexture.height) / 2 : 0;
                getHeight = (layers[i].height > newTexture.height) ? newTexture.height : layers[i].height;
                setY = (layers[i].height < newTexture.height) ? (newTexture.height - layers[i].height) / 2 : 0;
                setHeight = (layers[i].height < newTexture.height) ? layers[i].height : newTexture.height;

                Color[] getPixels = layers[i].GetPixels(getX, getY, getWidth, getHeight);
                if (layers[i].width >= newTexture.width && layers[i].height >= newTexture.height)
                {
                    adjustedLayers[i] = getPixels;
                }
                else { 
                    Texture2D sizedLayer = ClearTexture(newTexture.width, newTexture.height);
                    sizedLayer.SetPixels(setX, setY, setWidth, setHeight, getPixels);
                    adjustedLayers[i] = sizedLayer.GetPixels();
                }
            }
        }

        // set each color layer to alpha 1 if it isn't already
        for (int i = 0; i < layerColors.Length; i ++) {
            if (layerColors[i].a < 1) {
                layerColors[i] = new Color(layerColors[i].r,layerColors[i].g,layerColors[i].b,1f);
            }
        }

        for (int x = 0; x < newTexture.width; x++)
        {
            for (int y = 0; y < newTexture.height; y++)
            {
                int pixelIndex = x + (y * newTexture.width);
                for (int i = 0; i < layers.Length; i++)
                {

                    Color srcPixel = adjustedLayers[i][pixelIndex];
                    //                //Apply layer color if necessary (if not black or transparent) and there is color to fill
                    if (srcPixel.r != 0 && srcPixel.a != 0 && i < layerColors.Length) {
                    srcPixel = ApplyColorToPixel(srcPixel, layerColors[i]);
                    }

                    // Normal Blending based on alpha
                    if (srcPixel.a == 1)
                    { // only if new layer pixel is completely opaque
                        colorArray[pixelIndex] = srcPixel;
                    }
                    else if (srcPixel.a > 0)
                    {
                        colorArray[pixelIndex] = NormalBlend(colorArray[pixelIndex], srcPixel);
                    }
                }
            }
        }
        newTexture.SetPixels(colorArray);
        newTexture.Apply();

        newTexture.wrapMode = TextureWrapMode.Clamp;
        newTexture.filterMode = FilterMode.Point;

        return newTexture;
    }

    private Color ApplyColorToPixel(Color pixel, Color applyColor)
    {

        if (pixel.r == 1f)
        {
            return applyColor;
        }

        return pixel * applyColor;
    }

    private Color NormalBlend(Color dest, Color src)
    {

    float srcAlpha = src.a;
    float destAlpha = (1 - srcAlpha) * dest.a;
    Color destLayer = dest * destAlpha;
    Color srcLayer = src * srcAlpha;

    return destLayer + srcLayer;
    //return dest * destAlpha + src * srcAlpha;

}

public Sprite MakeSprite(Texture2D texture)
{
    //create a sprite from texture
    return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);

    // assign procedural sprite to sprite renderer.sprite
    // rend.sprite = newSprite;
}

    public void SaveTextureToFile(Texture2D texture, string fileName)
    {
        string file_path = Application.dataPath+"/Resources/"+fileName;
        Debug.Log(file_path);
        byte[] fileData = texture.EncodeToPNG();
        using (var fs = new FileStream(file_path, FileMode.Create, FileAccess.Write))
        {
            fs.Write(fileData, 0, fileData.Length);
        }
    }
      
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();

        //Texture2D headTexture = Resources.Load<Texture2D>("Test/Head");
        //Texture2D eyesTexture = Resources.Load<Texture2D>("Test/Eyes");
        //Texture2D mouthTexture = Resources.Load<Texture2D>("Test/Mouth");
        //Texture2D[] loadedTextures = new Texture2D[] { headTexture, eyesTexture, mouthTexture};
        Texture2D baseSprite = Resources.Load<Texture2D>("Test/Sprite/Female/Head");
        Texture2D cabelo = Resources.Load<Texture2D>("Test/Sprite/Female/Eyes");
        Texture2D inferior = Resources.Load<Texture2D>("Test/Sprite/Female/Mouth");
        Texture2D superior = Resources.Load<Texture2D>("Test/Sprite/Female/Mouth");
        //Texture2D[] loadedTextures = new Texture2D[] { headTexture, eyesTexture, mouthTexture };
        Texture2D[] loadedTextures = new Texture2D[] { baseSprite };

        tex = MakeTexture(loadedTextures, colorArray);
                
        // assign procedural sprite to sprite renderer.sprite
        Sprite finalSprite = MakeSprite(tex);
        rend.sprite = finalSprite;
        
        //Debug.Log("save to disk comentado");
        //SaveTextureToFile(finalSprite.texture, "SpriteTeste.png");
        SaveTextureToFile(finalSprite.texture, "FemaleIdle.png");

    }


//public void firstMethod()
//{
//    rend = GetComponent<SpriteRenderer>();

//    //create a texture
//    Texture2D tex = new Texture2D(96, 96);

//    for (int x = 0; x < tex.width; x++)
//    {
//        for (int y = 0; y < tex.height; y++)
//        {
//            tex.SetPixel(x, y, Color.red);
//        }
//    }
//    tex.Apply();

//    //create a sprite from texture
//    Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

//    // assign procedural sprite to sprite renderer.sprite
//    rend.sprite = newSprite;
//}

//public void secondMethod()
//{

//    rend = GetComponent<SpriteRenderer>();

//    //create a texture
//    Texture2D tex = new Texture2D(96, 96);
//    Color[] colorArray = new Color[tex.width * tex.height];

//    for (int x = 0; x < tex.width; x++)
//    {
//        for (int y = 0; y < tex.height; y++)
//        {
//            colorArray[x + (y * tex.width)] = Color.red;
//        }
//    }
//    tex.SetPixels(colorArray);
//    tex.Apply();

//    //create a sprite from texture
//    Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

//    // assign procedural sprite to sprite renderer.sprite
//    rend.sprite = newSprite;
//}

//private void ThirdMethod()
//{
//    rend = GetComponent<SpriteRenderer>();

//    //create a texture
//    Texture2D tex = new Texture2D(96, 96);
//    Color[] colorArray = new Color[tex.width * tex.height];

//    for (int x = 0; x < tex.width; x++)
//    {
//        for (int y = 0; y < tex.height; y++)
//        {
//            colorArray[x + (y * tex.width)] = Color.Lerp(Color.black, Color.white, (float)y / (float)tex.width);
//        }
//    }
//    tex.SetPixels(colorArray);
//    tex.Apply();

//    tex.wrapMode = TextureWrapMode.Clamp;
//    tex.filterMode = FilterMode.Point;

//    //create a sprite from texture
//    Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

//    // assign procedural sprite to sprite renderer.sprite
//    rend.sprite = newSprite;
//}

//private void ForthMethod()
//{
//    rend = GetComponent<SpriteRenderer>();

//    //create a texture
//    Texture2D tex = new Texture2D(src.width, src.height);
//    Color[] colorArray = new Color[tex.width * tex.height];

//    Color[] srcArray = src.GetPixels();

//    for (int x = 0; x < tex.width; x++)
//    {
//        for (int y = 0; y < tex.height; y++)
//        {
//            int pixelIndex = x + (y * tex.width);
//            Color srcPixel = srcArray[pixelIndex];
//            colorArray[pixelIndex] = srcPixel;
//        }
//    }
//    tex.SetPixels(colorArray);
//    tex.Apply();

//    tex.wrapMode = TextureWrapMode.Clamp;
//    tex.filterMode = FilterMode.Point;

//    //create a sprite from texture
//    Sprite newSprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.one * 0.5f);

//    // assign procedural sprite to sprite renderer.sprite
//    rend.sprite = newSprite;
//}
}

