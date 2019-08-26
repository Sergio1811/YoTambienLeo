using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Drawing;
using System.Windows.Input;

public class PuzzleCutter : MonoBehaviour
{
    public Texture2D m_ImagePuzzle;
    public SpriteRenderer m_Renderer;

    int m_Difficulty = 4;

    void Start()
    {
        CutIntoPieces();
    }

    public void CutIntoPieces()
    {/*
        int l_NumPieces = 2;
        int l_WidhtPiece = m_ImagePuzzle.width / l_NumPieces;
        int l_HeightPeice = m_ImagePuzzle.height / l_NumPieces;

        for (int i = 0; i < l_NumPieces; i++)
        {
            for (int j = 0; j < l_NumPieces; j++)
            {
                
                Debug.Log(i);
                Debug.Log(j);
                print("new one");
                Sprite l_Sprite;
                Rect rect = new Rect(new Vector2(i * l_WidhtPiece, j * l_HeightPeice), new Vector2((i + 1) * l_WidhtPiece, (j + 1) * l_HeightPeice));
                l_Sprite = Sprite.Create(m_ImagePuzzle, rect, new Vector2(0,0));

                SpriteRenderer l_NewOne = Instantiate(m_Renderer, new Vector3(i * l_WidhtPiece, j * l_HeightPeice), Quaternion.identity);
                l_NewOne.sprite = l_Sprite;
            }
        }
        void Initialize_Tiles()
        {
            //
            // Cut original image to nine rectangular peaces
            // that have identical dimensions and set them to
            // the elements of table 'Puzzle_Tiles'.
            //
            // Placed by it's indexes on fixed positions
            // inside Puzzle_Table, in the below shown order,
            // they constitute original image.
            //
            //       1 2 3
            //       4 5 6
            //       7 8 9
            //

            // Original image
            Bitmap Original_Image;

            // Original image pixel format
            PixelFormat Pixel_Format;

            // Tile rectangle
            RectangleF Tile;

            int Tile_Height;// Tile height
            int Tile_Width; // Tile width

            // Set new original image and it's pixel format
            Original_Image = new Bitmap(BackgroundImage);
            Pixel_Format = Original_Image.PixelFormat;

            // Set new Tile
            Tile_Height = Original_Image.Height / 3;
            Tile_Width = Original_Image.Width / 3;

            Tile = new RectangleF();

            Tile.Width = Tile_Width;
            Tile.Height = Tile_Height;

            //
            // Set values to elements of table Puzzle_Tiles
            //

            // 0. Tile, empty tile
            Puzzle_Tiles[0].Image = null;

            // 1. Tile
            Tile.X = 0;
            Tile.Y = 0;
            Puzzle_Tiles[1].Image = Original_Image.Clone(Tile, Pixel_Format);

            // 2. Tile
            Tile.X = Tile_Width;
            Tile.Y = 0;
            Puzzle_Tiles[2].Image = Original_Image.Clone(Tile, Pixel_Format);

            // 3. Tile
            Tile.X = 2 * Tile_Width;
            Tile.Y = 0;
            Puzzle_Tiles[3].Image = Original_Image.Clone(Tile, Pixel_Format);

            // 4. Tile
            Tile.X = 0;
            Tile.Y = Tile_Height;
            Puzzle_Tiles[4].Image = Original_Image.Clone(Tile, Pixel_Format);

            // 5. Tile
            Tile.X = Tile_Width;
            Tile.Y = Tile_Height;
            Puzzle_Tiles[5].Image = Original_Image.Clone(Tile, Pixel_Format);

            // 6. Tile
            Tile.X = 2 * Tile_Width;
            Tile.Y = Tile_Height;
            Puzzle_Tiles[6].Image = Original_Image.Clone(Tile, Pixel_Format);

            // 7. Tile
            Tile.X = 0;
            Tile.Y = 2 * Tile_Height;
            Puzzle_Tiles[7].Image = Original_Image.Clone(Tile, Pixel_Format);

            // 8. Tile
            Tile.X = Tile_Width;
            Tile.Y = 2 * Tile_Height;
            Puzzle_Tiles[8].Image = Original_Image.Clone(Tile, Pixel_Format);

            // 9. Tile
            Tile.X = 2 * Tile_Width;
            Tile.Y = 2 * Tile_Height;
            Puzzle_Tiles[9].Image = Original_Image.Clone(Tile, Pixel_Format);
        }*/
    }
    }
