namespace RiskyAssembler.Assembler
{
    /// <summary>
    /// The formatter formats the file by removing unnecessary whitespace, and structuring the layout of the file such that it simplifies
    /// the algorithm the parser uses to parse the file.
    /// 
    /// The formatter reads the file line by line and constructs a new file from it with a .lst extension.
    /// </summary>
    public static class Formatter
    {
        /// <summary>
        /// Create a [filename].lst file, format each line in [filename].asm and write each line to the .lst file.
        /// </summary>
        /// <param name="file">The file to format.</param>
        public static void Format(string path)
        {
            string txtPath = ToTxtPath(path);

            if(File.Exists(txtPath))
                File.Delete(txtPath);

            using StreamWriter output = new StreamWriter(txtPath, true);

            foreach(string line in File.ReadLines(path))
            {
                if(line != "")
                    output.WriteLine(FormatLine(line));
            }
        }

        /// <summary>
        /// Convert tabs and remove comments for a line.
        /// </summary>
        /// <param name="line">The line to format.</param>
        /// <returns>Formatted line.</returns>
        private static string FormatLine(string line)
        {
            ConvertTabToSpaces(ref line);
            RemoveComments(ref line);
            RemoveLeadingAndTrailingWhitespace(ref line);
            return line;
        }

        /// <summary>
        /// Replaces tabs with spaces.
        /// </summary>
        /// <param name="file">The file to format</param>
        private static void ConvertTabToSpaces(ref string line)
        {
            line = line.Replace("\t", " ");
        }

        /// <summary>
        /// Removes all characters trailing '#' on the line.
        /// </summary>
        /// <param name="line"></param>
        private static void RemoveComments(ref string line)
        {
            int index = 0;
            if ((index = line.IndexOf('#')) != -1)
                line = line.Substring(0, index);
        }

        /// <summary>
        /// Removes all leading and trailing whitespace in the file.
        /// </summary>
        /// <param name="line">The line to format.</param>
        private static void RemoveLeadingAndTrailingWhitespace(ref string line)
        {
            line = line.Trim();
        }

        /// <summary>
        /// Copies the supplied path and replaces the .asm extension with the .txt extension.
        /// </summary>
        /// <param name="path">The path to modify and copy.</param>
        /// <returns>The path with .txt extension.</returns>
        /// <exception cref="ArgumentException">Thrown if the path doesn't have a .asm extension.</exception>
        private static string ToTxtPath(string path)
        {
            if (!path.EndsWith(".asm"))
                throw new ArgumentException("File must have .asm extension.");
            
            return path.Replace(".asm", ".txt");
        }
    }
}
