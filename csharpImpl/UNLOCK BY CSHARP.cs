  class Program
    {
        static void Main(string[] args)
        {

            string src = @".\src";
            string des = @".\src.git";



            CopyDir(src, des);
            Console.Write($"Prosessing finish");
            Console.ReadKey();
        }




        public static void CopyDir(string source, string target)
        {
            var dirs = Directory.GetDirectories(source);
            foreach (var dir in dirs)
            {
                CopyDir(dir, Path.Combine(target, Path.GetFileName(dir)));
            }

            if (!Directory.Exists(target))
            {
                Directory.CreateDirectory(target);
            }


            var files = Directory.GetFiles(source);
            foreach (var f in files)
            {

                Console.WriteLine($"Prosessing {f}");
                if (f.EndsWith(".cs"))
                {
                    using (var fs = new FileStream(f, FileMode.Open))
                    {
                        using (var newfs = new FileStream(Path.Combine(target, Path.GetFileName(f) + "_"), FileMode.CreateNew))
                        {
                            byte[] buffer = new byte[2048];
                            int ret = 0;
                            while ((ret = fs.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                newfs.Write(buffer, 0, ret);
                            }
                        }
                    }
                }
                else
                {
                    File.Copy(f, Path.Combine(target, Path.GetFileName(f)), true);
                }
            }



        }
    }