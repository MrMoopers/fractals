using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FractalsCS
{
    public static class ColorPaletteFactory
    {
        public static List<Color> GetColorPalette1(Color[] Colors)
        {
            List<Color> colorPalette = new List<Color>();
            for (int i = 0; i < Colors.Length; i++)
            {

                if (Colors[i]!= null)
                {
                    colorPalette.Add((Colors[i]));
                   
                }
                
            }
            return colorPalette;
        }

        public static List<Color> GetColorPalette2()
        {
            List<Color> colorPalette = new List<Color>();
            for (int i = 0; i < 10; i++)
            {
                colorPalette.Add(Color.FromArgb(0, (i * 53) % 256, 0));
            }
            return colorPalette;
        }

        public static List<Color> GetRandomColorPalette()
        {
            List<Color> colorPalette = new List<Color>();
            Random rand = new Random();
            int[] randomNumber = new int[3];
            for (int i = 0; i < 10; i++)
            {
                for (int f = 0; f < 3; f++)
                {
                    randomNumber[f] = rand.Next(0, 255);
                }

                colorPalette.Add(Color.FromArgb(randomNumber[0], randomNumber[1], randomNumber[2]));//random colour here
                //Debug.WriteLine("255,{0},{1},{2}", randomNumber[0], randomNumber[1], randomNumber[2]);
            }
            return colorPalette;
        }



        public static List<Color> GetColorPalette3()
        {
            List<Color> colorPalette = new List<Color>();
            for (int i = 0; i < 10; i++)
            {
                colorPalette.Add(Color.FromArgb((i * 57) % 256, (i * 17) % 256, (i * 13) % 256));
                //colorPalette.Add(Color.FromArgb((i * 13) % 256, (i * 13) % 256, (i * 13) % 256)); dark_Grey
                //colorPalette.Add(Color.FromArgb((i * 13) % 256, (i * 7) % 256, (i * 17) % 256)); dark_Magenta
                //colorPalette.Add(Color.FromArgb((i * 13) % 256, (i * 7) % 256, (i * 17) % 256)); dark_Purple
            }
            for (int i = 10; i < 30; i++)
            {
                colorPalette.Add(Color.Wheat);
            }
            return colorPalette;
        }


        public static List<Color> GetColorPaletteFromFile(string filePath)
        {
            List<Color> colorList = new List<Color>();
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                if (line.StartsWith("!"))
                {
                    break;
                }
                string[] parts = line.Split(',');
                int a = Convert.ToInt32(parts[0]);
                int r = Convert.ToInt32(parts[1]);
                int g = Convert.ToInt32(parts[2]);
                int b = Convert.ToInt32(parts[3]);
                Color c = Color.FromArgb(a, r, g, b);
                colorList.Add(c);
              
            }
            return colorList;
        }
        public static List<Double> GetCoordinates(string filePath)
        {
            string[] lines = File.ReadAllLines(filePath);
            List<Double> coords = new List<Double>();
            foreach (string line in lines)
            {
                if (line.StartsWith("!"))
                {
                    string ln = line.TrimStart(new char[] { '!' });
                    string[] parts = ln.Split(',');
                    coords.Add(Convert.ToDouble(parts[0]));
                    coords.Add(Convert.ToDouble(parts[1])); 
                    coords.Add(Convert.ToDouble(parts[2]));
                    coords.Add(Convert.ToDouble(parts[3]));
                    
                }
            }
            return coords;
        }
        public static List<Color> GenerateColorGradientPalette(Color start, Color end, int size)
        {
            var colorList = new List<Color>();
            int rMin = start.R;
            int rMax = end.R;
            int gMin = start.G;
            int gMax = end.G;
            int bMin = start.B;
            int bMax = end.B;
            for (int i = 0; i < size; i++)
            {
                var rAverage = rMin + (int)((rMax - rMin) * i / size);
                var gAverage = gMin + (int)((gMax - gMin) * i / size);
                var bAverage = bMin + (int)((bMax - bMin) * i / size);
                colorList.Add(Color.FromArgb(rAverage, gAverage, bAverage));
            }

            return colorList;
        }
        public static List<Color> GenerateTHREEColorGradientPalettes(Color[] start, Color[] end, int size)
        {
            var colorList = new List<Color>();

            for (int f = 0; f < 3; f++)
            {
                int rMin = start[f].R;
                int rMax = end[f].R;
                int gMin = start[f].G;
                int gMax = end[f].G;
                int bMin = start[f].B;
                int bMax = end[f].B;
                for (int i = 0; i < size; i++)
                {
                    var rAverage = rMin + (int)((rMax - rMin) * i / size);
                    var gAverage = gMin + (int)((gMax - gMin) * i / size);
                    var bAverage = bMin + (int)((bMax - bMin) * i / size);
                    colorList.Add(Color.FromArgb(rAverage, gAverage, bAverage));
                }
            }



            return colorList;
        }

    }
}

