using System.Text;

namespace RiskyAssembler.Assembler
{
    /// <summary>
    /// The formatter formats the file by removing unnecessary whitespace, and structuring the layout of the file such that it simplifies
    /// the algorithm the parser uses to parse the file.
    /// 
    /// The formatter reads the file line by line and constructs a new file from it with a .txt extension.
    /// </summary>
    public class Formatter : IFormatter
    {

        /// <summary>
        /// Formats the .asm file and returns the path to the formatted version of it.
        /// </summary>
        /// <param name="path">The path to the original .asm file.</param>
        /// <returns>The path to the formatted .asm file.</returns>
        public string Format(string path)
        {
            string formattedAsmPath = ToFormattedAsmPath(path);

            if (File.Exists(formattedAsmPath))
                File.Delete(formattedAsmPath);

            using StreamWriter output = new StreamWriter(formattedAsmPath, true);

            foreach (string line in File.ReadLines(path))
            {
                if (line != "")
                    output.WriteLine(FormatLine(line));
            }

            return formattedAsmPath;
        }

        /// <summary>
        /// Convert tabs and remove comments for a line.
        /// </summary>
        /// <param name="line">The line to format.</param>
        /// <returns>Formatted line.</returns>
        public string FormatLine(string line)
        {
            ConvertTabToSpace(ref line);
            RemoveComments(ref line);
            RemoveLeadingAndTrailingWhitespace(ref line);
            ReduceWhitespace(ref line);
            return line;
        }

        /// <summary>
        /// Replaces tabs with space.
        /// </summary>
        /// <param name="line">The line to format.</param>
        public void ConvertTabToSpace(ref string line)
        {
            line = line.Replace("\t", " ");
        }

        /// <summary>
        /// Reduces multiple whitespaces to one single whitespace.
        /// </summary>
        /// <param name="line">The line to format.</param>
        public void ReduceWhitespace(ref string line)
        {
            string[] words = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            StringBuilder sb = new StringBuilder();

            // We don't loop over the last word...
            for (int i = 0; i < words.Length - 1; i++)
            {
                sb.Append(words[i] + " ");
            }

            // ... because we don't want to append a whitespace to it.
            sb.Append(words[words.Length - 1]);

            line = sb.ToString();
        }

        /// <summary>
        /// Removes all characters trailing '#' on the line.
        /// </summary>
        /// <param name="line">The line to format.</param>
        public void RemoveComments(ref string line)
        {
            int index = 0;
            if ((index = line.IndexOf('#')) != -1)
                line = line.Substring(0, index);
        }

        /// <summary>
        /// Removes all leading and trailing whitespace in the file.
        /// </summary>
        /// <param name="line">The line to format.</param>
        public void RemoveLeadingAndTrailingWhitespace(ref string line)
        {
            line = line.Trim();
        }

        /// <summary>
        /// Copies the supplied path and replaces the .asm extension with the .txt extension.
        /// </summary>
        /// <param name="path">The path to modify and copy.</param>
        /// <returns>The path with .txt extension.</returns>
        /// <exception cref="ArgumentException">Thrown if the path doesn't have a .asm extension.</exception>
        public string ToFormattedAsmPath(string path)
        {
            if (!path.EndsWith(".asm"))
                throw new ArgumentException("File must have .asm extension.");

            return path.Replace(".asm", "_formatted.asm");
        }
    }
}
