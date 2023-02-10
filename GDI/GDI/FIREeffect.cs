using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace Cupid
{
    class FireEffect
    {
        public WriteableBitmap bitmap;

        int Width;
        int Height;
        int FireSource = 2;
        int FireChance = 10;
        public int FlameWidth = 35;
        int FlameHeight = 50;
        int Alpha = 255;

        Byte[] pFireBits;
        long[] pIndex;
        Color[] MyImage;
        Color[] pPalletteBuffer;
    

        public FireEffect()
        {
        }

        public FireEffect(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public void InitFire()
        {
            pPalletteBuffer = new Color[256];
            MyImage = new Color[Width * Height];
            for (int i = 0; i < Width * Height; i++)
                MyImage[i] = Colors.Black;

            Height += 3;
            pFireBits = new Byte[Width * Height];
            
            pIndex = new long[Height+1];

            for (int i = 0; i < Width; i++)
                for (int j = 0; j < Height; j++)
                    pFireBits[j * Width + i] = 0;

            for (int i = Height; i > 0; i--)
                pIndex[i] = i * Width;

            InitPallette();
            ClearHotSpots();
        }

        void ClearHotSpots()
        {
            for (int i = 0; i < Width; i++)
            {
                pFireBits[pIndex[FireSource] + i] = 0;
            }
        }

        void InitPallette()
        {
            Color start, end;
            Color[] plate={Colors.Black, Colors.Red, Colors.Yellow, Colors.White};


            int r, g, b;
            float rStep, gStep, bStep;

            int nSteps;
            float fStep;

            for (int i = 1; i < 4; i++)
            {
                start = plate[i - 1];
                end = plate[i];

              

                r = end.R - start.R;
                g = end.G - start.G;
                b = end.B - start.B;

                nSteps = Max(r, g, b);
                fStep = (float)255 / 3 / nSteps;

                rStep = r / (float)nSteps;
                gStep = g / (float)nSteps;
                bStep = b / (float)nSteps;

                r = start.R;
                g = start.G;
                b = start.B;

                for (int j = 0; j < nSteps; j++)
                {
                    Color mycolor = new Color();
                    mycolor.R = (byte)(b + bStep * j);
                    mycolor.G = (byte)(g + gStep * j);
                    mycolor.B = (byte)(r + rStep * j);

                    long index = (int)(j * fStep);

                    if (index + (i - 1) * 85 < 255)
                        pPalletteBuffer[index + (i - 1) * 85] = mycolor;

                }
            }

            start = plate[3];
            end = plate[1];

            for (int i = 0; i < FlameHeight; i++)
                pPalletteBuffer[i] = Colors.Black;
            

            r = end.R - start.R;
            g = end.G - start.G;
            b = end.B - start.B;

            nSteps = Max(r, g, b);
            fStep = (float)(85 - FlameHeight) / (float)nSteps;

            rStep = r / (float)nSteps;
            gStep = g / (float)nSteps;
            bStep = b / (float)nSteps;

            r = start.R;
            g = start.G;
            b = start.B;

            for (int i = 0; i < nSteps; i++)
            {
                Color myColor = new Color();
                myColor.R = (byte)(b + bStep * i);
                myColor.G = (byte)(g + gStep * i);
                myColor.B = (byte)(r + rStep * i);

                long index = (int)(i * fStep);

                pPalletteBuffer[index + (85 - FlameHeight)] = myColor;
            }
        }

        int Max(int a, int b, int c)
        {
            int d;
            if (a > b)
                d = a;
            else
                d = b;
            if (d > c)
                return d;
            else
                return c;
        }

        void SetHotSpots()
        {
            ClearHotSpots();

            long position = 0;

            Random rand=new Random();

            while (position < Width)
            {
                if (rand.Next(0, 100) < FireChance)
                {
                    long w_flame = rand.Next(1, FlameWidth);
                    for (int i = 0; i < w_flame; i++)
                    {
                        if (position < Width)
                        {
                            pFireBits[pIndex[FireSource] + position] = 254;
                            position++;
                        }
                    }
                }
                position++;
            }
        }

        void MakeLines()
        {
            for(int i=0;i<Width;i++)
                for (int j = FireSource; j < Height - 1; j++)
                {
                    int ave = Average(i, j);
                    if (--ave < 0)
                        ave = 0;
                    pFireBits[pIndex[j + 1] + i] = (byte)ave;
                }
        }

        int Average(int x, int y)
        {
            uint ave1;

            if (y == Height)
                ave1 = pFireBits[pIndex[y - 1] + x];
            else
                ave1 = pFireBits[pIndex[y + 1] + x];

            uint ave2 = pFireBits[pIndex[y - 1] + x];
            uint ave3 = pFireBits[pIndex[y] + x + 1];
            uint ave4 = pFireBits[pIndex[y] + x - 1];
            uint ave5 = pFireBits[pIndex[y] + x];

            return (int)(ave1 + ave2 + ave3 + ave4 + ave5) / 5;
        }

        public void Render()
        {
            //draw points
            SetHotSpots();
            MakeLines();


            byte[] color = new byte[4];

            int[] pixels = new int[Width * Height];

            bitmap = new WriteableBitmap(Width, Height, 96, 96, PixelFormats.Bgra32, null);


            for (int i = 0; i < Height - 3; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    color[0] = pPalletteBuffer[pFireBits[pIndex[i + 3] + j]].R;
                    color[1] = pPalletteBuffer[pFireBits[pIndex[i + 3] + j]].G;
                    color[2] = pPalletteBuffer[pFireBits[pIndex[i + 3] + j]].B;
                    

                    color[0]=(byte)(((color[0]-MyImage[Width*i+j].R)*Alpha+(MyImage[Width*i+j].R<<8))>>8);
                    color[1]=(byte)(((color[1]-MyImage[Width*i+j].G)*Alpha+(MyImage[Width*i+j].G<<8))>>8);
                    color[2]=(byte)(((color[2]-MyImage[Width*i+j].B)*Alpha+(MyImage[Width*i+j].B<<8))>>8);

                    if ((int)color[0] == 0 & (int)color[1] == 0 & (int)color[2] == 0)
                        color[3] = 0;
                    else
                        color[3] = 255;

                    MyImage[Width*i+j].R=color[0];
                    MyImage[Width*i+j].G=color[1];
                    MyImage[Width*i+j].B=color[2];

                    int pixvalue = BitConverter.ToInt32(color, 0);

                    pixels[Width * i + j] = pixvalue;

                    
                }
            }
            bitmap.WritePixels(new Int32Rect(0, 0, Width, Height), pixels, Width*4, 0);
        }
    }
}
