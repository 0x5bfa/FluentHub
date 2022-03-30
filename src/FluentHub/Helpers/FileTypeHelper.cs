using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Helpers
{
    public static class FileTypeHelper
    {
        public static string GetFileTypeStringId(string extension)
        {
            string id;

            _ = extension.ToLower() switch
            {
                "ps1" => id = "PowerShell",
                "hs" => id = "Haskell",
                "lhs" => id = "Haskell",
                "" => id = "Koka",
                "fs" => id = "FSharp",
                "ts" => id = "Typescript",
                "cpp" => id = "Cpp",
                "cxx" => id = "Cpp",
                "css" => id = "Css",
                "php" => id = "Php",
                "xml" => id = "Xml",
                "xaml" => id = "Xml",
                "vb" => id = "VbDotNet",
                "sql" => id = "Sql",
                "md" => id = "Markdown",
                "markdown" => id = "Markdown",
                "f90" => id = "Fortran",
                "for" => id = "Fortran",
                "f" => id = "Fortran",
                "java" => id = "Java",
                "htm" => id = "Html",
                "html" => id = "Html",
                "cs" => id = "CSharp",
                "aspx.vb" => id = "AspxVb",
                "aspx.cs" => id = "AspxCs",
                "aspx" => id = "Aspx",
                "asax" => id = "Asax",
                "ashx" => id = "Ashx",
                "js" => id = "JavaScript",
                _ => id = "",
            };

            return id;
        }
    }
}
