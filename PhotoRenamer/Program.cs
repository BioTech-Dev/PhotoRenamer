using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Diagnostics;

namespace PhotoRenamer
{
    class Program
    {
        List<string> fileList;
        string folderPath;
        Bitmap imageFile;

        static void Main(string[] args)
        {
            Program app = new Program();
            Console.WriteLine("フォルダ内の写真の名前を変更します。");
            Console.ReadLine();
            app.Rename();
            Console.WriteLine("正常に終了しました。");
            Console.ReadLine();
        }

        void Rename()
        {
            folderPath = Directory.GetCurrentDirectory();
            TestPath();    //デバッグモードの設定

            fileList = new List<string>(Directory.GetFiles(folderPath));
            foreach (string name in fileList)
            {
                try
                {
                    imageFile = new Bitmap(name);
                    Console.WriteLine(name);
                    int index = Array.IndexOf(imageFile.PropertyIdList, 0x9003);
                    if (index != -1)
                    {
                        PropertyItem item = imageFile.PropertyItems[index];
                        string date = Encoding.ASCII.GetString(item.Value, 2, 17);
                        string newName = date.Replace(":", "").Replace(" ", "-") + ".jpg";

                        imageFile.Dispose();
                        File.Move(name, Path.GetDirectoryName(name) + "\\" + newName);
                    }
                }
                catch
                {
                    //何もしない
                }
            }
        }

        [Conditional("DEBUG")]
        private void TestPath()
        {
            folderPath = @"C:\Users\tsato\Desktop\Test";
            Console.WriteLine("デバッグモードです");
        }

    }
}
