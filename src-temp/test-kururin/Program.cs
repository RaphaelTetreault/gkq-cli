using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using System.Numerics;

namespace test_kururin
{
    internal class Program
    {
        static float maxX = 0, maxY = 0;
        static PointF[] points;

        static void Main(string[] args)
        {
            ProcessFolder("C:\\gkqj01-working\\files\\frame\\");
            //ProcessFolder("C:\\gkqj01-working\\files\\procs\\");
        }

        public static void ProcessFolder(string folder)
        {
            string[] filePaths = GetPaths(folder);
            foreach (var path in filePaths)
            {
                Console.WriteLine(path);
                maxX = 0;
                maxY = 0;
                points = null;
                CreateGeoImage(path);
                Console.WriteLine(path);
                Console.WriteLine();
            }
        }

        public static void CreateGeoImage(string path)
        {
            LoadFile(path);

            for (int i = 0; i < points.Length; i++)
            {
                maxX = points[i].X > maxX ? points[i].X : maxX;
                maxY = points[i].Y > maxY ? points[i].Y : maxY;

                points[i].X += 10;
                points[i].Y += 10;
            }

            maxX += 20;
            maxY += 20;

            Image<Rgba32> image = new Image<Rgba32>((int)maxX, (int)maxY, Color.White);
            
            for (int i = 0; i < points.Length; i += 2)
            {
                PointF[] line = points[i..(i + 2)];
                image.Mutate(o => o.DrawLine(Color.Black, 2f, line));
            }


            //var longy = new List<PointF>();
            //foreach (var point in points)
            //{
            //    float length = new Vector2(point.X, point.Y).Length();
            //    if (length > 500)
            //        longy.Add(point);
            //}
            //image.Mutate(o => o.DrawLine(Color.Red, 2f, longy.ToArray()));

            image.SaveAsPng(path + ".png");
        }


        public static void LoadFile(string file)
        {
            using BinaryReader reader = new BinaryReader(File.OpenRead(file));
            using StreamWriter writer = new StreamWriter(File.Create(file + ".txt"));

            int numPoints = (int)(reader.BaseStream.Length - 16) /4 /2;

            if (reader.BaseStream.Length % 16 != 0)
                throw new Exception(file);

            points = new PointF[numPoints];
            int charsLength = points.Length.ToString().Length;

            for (int i = 0; i < numPoints; i++)
            {
                byte[] bytes = reader.ReadBytes(4);
                Array.Reverse(bytes, 0, 4);
                float x = BitConverter.ToSingle(bytes, 0);

                bytes = reader.ReadBytes(4);
                Array.Reverse(bytes, 0, 4);
                float y = BitConverter.ToSingle(bytes, 0);

                points[i].X = x * 10;
                points[i].Y = y * 10;

                writer.WriteLine($"{i.ToString().PadLeft(charsLength)}\t({x},\t{y})");
            }
        }

        public static string[] GetPaths(string basePath)
        {
            string[] paths = Directory.GetFiles(basePath, "*.bin", SearchOption.AllDirectories);
            return paths;
        }

    }
}
