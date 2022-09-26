public static class FileReaderService
{
    public static string[] ReadMapFile(string filepath)
    {
        var fileInfo = new FileInfo(filepath);
        
        if (!fileInfo.Exists)
            throw new FileNotFoundException();
        else if (fileInfo.Extension is not ".map")
            throw new ArgumentException();
        else
            return File.ReadAllLines(filepath);
    }
}