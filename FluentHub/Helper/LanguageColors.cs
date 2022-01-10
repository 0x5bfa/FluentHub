using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;

namespace FluentHub.Helper
{
    public class LanguageColors
    {
        public static SolidColorBrush Get(string language)
        {
            string hexColor = "#00000000";

            switch (language)
            {
                case "1C Enterprise":

                        hexColor = "#FF814CCC";
                        break;


                case "4D":

                        hexColor = "#FF004289";
                        break;

                case "ABAP":

                        hexColor = "#FFE8274B";
                        break;

                case "ABAP CDS":

                        hexColor = "#FF555e25";
                        break;

                case "ActionScript":

                        hexColor = "#FF882B0F";
                        break;

                case "Ada":

                        hexColor = "#FF02f88c";
                        break;

                case "Adobe Font Metrics":

                        hexColor = "#FFfa0f00";
                        break;

                case "Agda":

                        hexColor = "#FF315665";
                        break;

                case "AGS Script":

                        hexColor = "#FFB9D9FF";
                        break;

                case "AIDL":

                        hexColor = "#FF34EB6B";
                        break;

                case "AL":

                        hexColor = "#FF3AA2B5";
                        break;

                case "Alloy":

                        hexColor = "#FF64C800";
                        break;

                case "Alpine Abuild":

                        hexColor = "#FF0D597F";
                        break;

                case "Altium Designer":

                        hexColor = "#FFA89663";
                        break;

                case "AMPL":

                        hexColor = "#FFE6EFBB";
                        break;

                case "AngelScript":

                        hexColor = "#FFC7D7DC";
                        break;

                case "Ant Build System":

                        hexColor = "#FFA9157E";
                        break;

                case "ANTLR":

                        hexColor = "#FF9DC3FF";
                        break;

                case "ApacheConf":

                        hexColor = "#FFd12127";
                        break;

                case "Apex":

                        hexColor = "#FF1797c0";
                        break;

                case "API Blueprint":

                        hexColor = "#FF2ACCA8";
                        break;

                case "APL":

                        hexColor = "#FF5A8164";
                        break;

                case "Apollo Guidance Computer":

                        hexColor = "#FF0B3D91";
                        break;

                case "AppleScript":

                        hexColor = "#FF101F1F";
                        break;

                case "Arc":

                        hexColor = "#FFaa2afe";
                        break;

                case "AsciiDoc":

                        hexColor = "#FF73a0c5";
                        break;

                case "ASP.NET":

                        hexColor = "#FF9400ff";
                        break;

                case "AspectJ":

                        hexColor = "#FFa957b0";
                        break;

                case "Assembly":

                        hexColor = "#FF6E4C13";
                        break;

                case "Astro":

                        hexColor = "#FFff5a03";
                        break;

                case "Asymptote":

                        hexColor = "#FFff0000";
                        break;

                case "ATS":

                        hexColor = "#FF1ac620";
                        break;

                case "Augeas":

                        hexColor = "#FF9CC134";
                        break;

                case "AutoHotkey":

                        hexColor = "#FF6594b9";
                        break;

                case "AutoIt":

                        hexColor = "#FF1C3552";
                        break;

                case "Avro IDL":

                        hexColor = "#FF0040FF";
                        break;

                case "Awk":

                        hexColor = "#FFc30e9b";
                        break;

                case "Ballerina":

                        hexColor = "#FFFF5000";
                        break;

                case "BASIC":

                        hexColor = "#FFff0000";
                        break;

                case "Batchfile":

                        hexColor = "#FFC1F12E";
                        break;

                case "Beef":

                        hexColor = "#FFa52f4e";
                        break;

                case "BibTeX":

                        hexColor = "#FF778899";
                        break;

                case "Bicep":

                        hexColor = "#FF519aba";
                        break;

                case "Bison":

                        hexColor = "#FF6A463F";
                        break;

                case "BitBake":

                        hexColor = "#FF00bce4";
                        break;

                case "Blade":

                        hexColor = "#FFf7523f";
                        break;

                case "BlitzBasic":

                        hexColor = "#FF00FFAE";
                        break;

                case "BlitzMax":

                        hexColor = "#FFcd6400";
                        break;

                case "Bluespec":

                        hexColor = "#FF12223c";
                        break;

                case "Boo":

                        hexColor = "#FFd4bec1";
                        break;

                case "Boogie":

                        hexColor = "#FFc80fa0";
                        break;

                case "Brainfuck":

                        hexColor = "#FF2F2530";
                        break;

                case "Brightscript":

                        hexColor = "#FF662D91";
                        break;

                case "Browserslist":

                        hexColor = "#FFffd539";
                        break;

                case "C":

                        hexColor = "#FF555555";
                        break;

                case "C#":

                        hexColor = "#FF178600";
                        break;

                case "C++":

                        hexColor = "#FFf34b7d";
                        break;

                case "Cabal Config":

                        hexColor = "#FF483465";
                        break;

                case "Cap'n Proto":

                        hexColor = "#FFc42727";
                        break;

                case "Ceylon":

                        hexColor = "#FFdfa535";
                        break;

                case "Chapel":

                        hexColor = "#FF8dc63f";
                        break;

                case "ChucK":

                        hexColor = "#FF3f8000";
                        break;

                case "Cirru":

                        hexColor = "#FFccccff";
                        break;

                case "Clarion":

                        hexColor = "#FFdb901e";
                        break;

                case "Classic ASP":

                        hexColor = "#FF6a40fd";
                        break;

                case "Clean":

                        hexColor = "#FF3F85AF";
                        break;

                case "Click":

                        hexColor = "#FFE4E6F3";
                        break;

                case "CLIPS":

                        hexColor = "#FF00A300";
                        break;

                case "Clojure":

                        hexColor = "#FFdb5855";
                        break;

                case "Closure Templates":

                        hexColor = "#FF0d948f";
                        break;

                case "Cloud Firestore Security Rules":

                        hexColor = "#FFFFA000";
                        break;

                case "CMake":

                        hexColor = "#FFDA3434";
                        break;

                case "CodeQL":

                        hexColor = "#FF140f46";
                        break;

                case "CoffeeScript":

                        hexColor = "#FF244776";
                        break;

                case "ColdFusion":

                        hexColor = "#FFed2cd6";
                        break;

                case "ColdFusion CFC":

                        hexColor = "#FFed2cd6";
                        break;

                case "COLLADA":

                        hexColor = "#FFF1A42B";
                        break;

                case "Common Lisp":

                        hexColor = "#FF3fb68b";
                        break;

                case "Common Workflow Language":

                        hexColor = "#FFB5314C";
                        break;

                case "Component Pascal":

                        hexColor = "#FFB0CE4E";
                        break;

                case "Coq":

                        hexColor = "#FFd0b68c";
                        break;

                case "Crystal":

                        hexColor = "#FF000100";
                        break;

                case "CSON":

                        hexColor = "#FF244776";
                        break;

                case "Csound":

                        hexColor = "#FF1a1a1a";
                        break;

                case "Csound Document":

                        hexColor = "#FF1a1a1a";
                        break;

                case "Csound Score":

                        hexColor = "#FF1a1a1a";
                        break;

                case "CSS":

                        hexColor = "#FF563d7c";
                        break;

                case "CSV":

                        hexColor = "#FF237346";
                        break;

                case "Cuda":

                        hexColor = "#FF3A4E3A";
                        break;

                case "CUE":

                        hexColor = "#FF5886E1";
                        break;

                case "CWeb":

                        hexColor = "#FF00007a";
                        break;

                case "Cython":

                        hexColor = "#FFfedf5b";
                        break;

                case "D":

                        hexColor = "#FFba595e";
                        break;

                case "Dafny":

                        hexColor = "#FFFFEC25";
                        break;

                case "Darcs Patch":

                        hexColor = "#FF8eff23";
                        break;

                case "Dart":

                        hexColor = "#FF00B4AB";
                        break;

                case "DataWeave":

                        hexColor = "#FF003a52";
                        break;

                case "Dhall":

                        hexColor = "#FFdfafff";
                        break;

                case "DirectX 3D File":

                        hexColor = "#FFaace60";
                        break;

                case "DM":

                        hexColor = "#FF447265";
                        break;

                case "Dockerfile":

                        hexColor = "#FF384d54";
                        break;

                case "Dogescript":

                        hexColor = "#FFcca760";
                        break;

                case "Dylan":

                        hexColor = "#FF6c616e";
                        break;

                case "E":

                        hexColor = "#FFccce35";
                        break;

                case "Earthly":

                        hexColor = "#FF2af0ff";
                        break;

                case "Easybuild":

                        hexColor = "#FF069406";
                        break;

                case "eC":

                        hexColor = "#FF913960";
                        break;

                case "Ecere Projects":

                        hexColor = "#FF913960";
                        break;

                case "ECL":

                        hexColor = "#FF8a1267";
                        break;

                case "ECLiPSe":

                        hexColor = "#FF001d9d";
                        break;

                case "EditorConfig":

                        hexColor = "#FFfff1f2";
                        break;

                case "Eiffel":

                        hexColor = "#FF4d6977";
                        break;

                case "EJS":

                        hexColor = "#FFa91e50";
                        break;

                case "Elixir":

                        hexColor = "#FF6e4a7e";
                        break;

                case "Elm":

                        hexColor = "#FF60B5CC";
                        break;

                case "Emacs Lisp":

                        hexColor = "#FFc065db";
                        break;

                case "EmberScript":

                        hexColor = "#FFFFF4F3";
                        break;

                case "EQ":

                        hexColor = "#FFa78649";
                        break;

                case "Erlang":

                        hexColor = "#FFB83998";
                        break;

                case "F#":

                        hexColor = "#FFb845fc";
                        break;

                case "F*":

                        hexColor = "#FF572e30";
                        break;

                case "Factor":

                        hexColor = "#FF636746";
                        break;

                case "Fancy":

                        hexColor = "#FF7b9db4";
                        break;

                case "Fantom":

                        hexColor = "#FF14253c";
                        break;

                case "Faust":

                        hexColor = "#FFc37240";
                        break;

                case "Fennel":

                        hexColor = "#FFfff3d7";
                        break;

                case "FIGlet Font":

                        hexColor = "#FFFFDDBB";
                        break;

                case "Filebench WML":

                        hexColor = "#FFF6B900";
                        break;

                case "fish":

                        hexColor = "#FF4aae47";
                        break;

                case "Fluent":

                        hexColor = "#FFffcc33";
                        break;

                case "FLUX":

                        hexColor = "#FF88ccff";
                        break;

                case "Forth":

                        hexColor = "#FF341708";
                        break;

                case "Fortran":

                        hexColor = "#FF4d41b1";
                        break;

                case "Fortran Free Form":

                        hexColor = "#FF4d41b1";
                        break;

                case "FreeBasic":

                        hexColor = "#FF867db1";
                        break;

                case "FreeMarker":

                        hexColor = "#FF0050b2";
                        break;

                case "Frege":

                        hexColor = "#FF00cafe";
                        break;

                case "Futhark":

                        hexColor = "#FF5f021f";
                        break;

                case "G-code":

                        hexColor = "#FFD08CF2";
                        break;

                case "Game Maker Language":

                        hexColor = "#FF71b417";
                        break;

                case "GAML":

                        hexColor = "#FFFFC766";
                        break;

                case "GAMS":

                        hexColor = "#FFf49a22";
                        break;

                case "GAP":

                        hexColor = "#FF0000cc";
                        break;

                case "GCC Machine Description":

                        hexColor = "#FFFFCFAB";
                        break;

                case "GDScript":

                        hexColor = "#FF355570";
                        break;

                case "GEDCOM":

                        hexColor = "#FF003058";
                        break;

                case "Gemfile.lock":

                        hexColor = "#FF701516";
                        break;

                case "Genie":

                        hexColor = "#FFfb855d";
                        break;

                case "Genshi":

                        hexColor = "#FF951531";
                        break;

                case "Gentoo Ebuild":

                        hexColor = "#FF9400ff";
                        break;

                case "Gentoo Eclass":

                        hexColor = "#FF9400ff";
                        break;

                case "Gerber Image":

                        hexColor = "#FFd20b00";
                        break;

                case "Gherkin":

                        hexColor = "#FF5B2063";
                        break;

                case "Git Attributes":

                        hexColor = "#FFF44D27";
                        break;

                case "Git Config":

                        hexColor = "#FFF44D27";
                        break;

                case "GLSL":

                        hexColor = "#FF5686a5";
                        break;

                case "Glyph":

                        hexColor = "#FFc1ac7f";
                        break;

                case "Gnuplot":

                        hexColor = "#FFf0a9f0";
                        break;

                case "Go":

                        hexColor = "#FF00ADD8";
                        break;

                case "Go Checksums":

                        hexColor = "#FF00ADD8";
                        break;

                case "Go Module":

                        hexColor = "#FF00ADD8";
                        break;

                case "Golo":

                        hexColor = "#FF88562A";
                        break;

                case "Gosu":

                        hexColor = "#FF82937f";
                        break;

                case "Grace":

                        hexColor = "#FF615f8b";
                        break;

                case "Gradle":

                        hexColor = "#FF02303a";
                        break;

                case "Grammatical Framework":

                        hexColor = "#FFff0000";
                        break;

                case "GraphQL":

                        hexColor = "#FFe10098";
                        break;

                case "Graphviz (DOT)":

                        hexColor = "#FF2596be";
                        break;

                case "Groovy":

                        hexColor = "#FF4298b8";
                        break;

                case "Groovy Server Pages":

                        hexColor = "#FF4298b8";
                        break;

                case "Hack":

                        hexColor = "#FF878787";
                        break;

                case "Haml":

                        hexColor = "#FFece2a9";
                        break;

                case "Handlebars":

                        hexColor = "#FFf7931e";
                        break;

                case "HAProxy":

                        hexColor = "#FF106da9";
                        break;

                case "Harbour":

                        hexColor = "#FF0e60e3";
                        break;

                case "Haskell":

                        hexColor = "#FF5e5086";
                        break;

                case "Haxe":

                        hexColor = "#FFdf7900";
                        break;

                case "HiveQL":

                        hexColor = "#FFdce200";
                        break;

                case "HLSL":

                        hexColor = "#FFaace60";
                        break;

                case "HolyC":

                        hexColor = "#FFffefaf";
                        break;

                case "HTML":

                        hexColor = "#FFe34c26";
                        break;

                case "HTML+ECR":

                        hexColor = "#FF2e1052";
                        break;

                case "HTML+EEX":

                        hexColor = "#FF6e4a7e";
                        break;

                case "HTML+ERB":

                        hexColor = "#FF701516";
                        break;

                case "HTML+PHP":

                        hexColor = "#FF4f5d95";
                        break;

                case "HTML+Razor":

                        hexColor = "#FF512be4";
                        break;

                case "HTTP":

                        hexColor = "#FF005C9C";
                        break;

                case "HXML":

                        hexColor = "#FFf68712";
                        break;

                case "Hy":

                        hexColor = "#FF7790B2";
                        break;

                case "IDL":

                        hexColor = "#FFa3522f";
                        break;

                case "Idris":

                        hexColor = "#FFb30000";
                        break;

                case "Ignore List":

                        hexColor = "#FF000000";
                        break;

                case "IGOR Pro":

                        hexColor = "#FF0000cc";
                        break;

                case "ImageJ Macro":

                        hexColor = "#FF99AAFF";
                        break;

                case "INI":

                        hexColor = "#FFd1dbe0";
                        break;

                case "Inno Setup":

                        hexColor = "#FF264b99";
                        break;

                case "Io":

                        hexColor = "#FFa9188d";
                        break;

                case "Ioke":

                        hexColor = "#FF078193";
                        break;

                case "Isabelle":

                        hexColor = "#FFFEFE00";
                        break;

                case "Isabelle ROOT":

                        hexColor = "#FFFEFE00";
                        break;

                case "J":

                        hexColor = "#FF9EEDFF";
                        break;

                case "JAR Manifest":

                        hexColor = "#FFb07219";
                        break;

                case "Jasmin":

                        hexColor = "#FFd03600";
                        break;

                case "Java":

                        hexColor = "#FFb07219";
                        break;

                case "Java Properties":

                        hexColor = "#FF2A6277";
                        break;

                case "Java Server Pages":

                        hexColor = "#FF2A6277";
                        break;

                case "JavaScript":

                        hexColor = "#FFf1e05a";
                        break;

                case "JavaScript+ERB":

                        hexColor = "#FFf1e05a";
                        break;

                case "Jest Snapshot":

                        hexColor = "#FF15c213";
                        break;

                case "JFlex":

                        hexColor = "#FFDBCA00";
                        break;

                case "Jinja":

                        hexColor = "#FFa52a22";
                        break;

                case "Jison":

                        hexColor = "#FF56b3cb";
                        break;

                case "Jison Lex":

                        hexColor = "#FF56b3cb";
                        break;

                case "Jolie":

                        hexColor = "#FF843179";
                        break;

                case "jq":

                        hexColor = "#FFc7254e";
                        break;

                case "JSON":

                        hexColor = "#FF292929";
                        break;

                case "JSON with Comments":

                        hexColor = "#FF292929";
                        break;

                case "JSON5":

                        hexColor = "#FF267CB9";
                        break;

                case "JSONiq":

                        hexColor = "#FF40d47e";
                        break;

                case "JSONLD":

                        hexColor = "#FF0c479c";
                        break;

                case "Jsonnet":

                        hexColor = "#FF0064bd";
                        break;

                case "Julia":

                        hexColor = "#FFa270ba";
                        break;

                case "Jupyter Notebook":

                        hexColor = "#FFDA5B0B";
                        break;

                case "Kaitai Struct":

                        hexColor = "#FF773b37";
                        break;

                case "KakouneScript":

                        hexColor = "#FF6f8042";
                        break;

                case "KiCad Layout":

                        hexColor = "#FF2f4aab";
                        break;

                case "KiCad Legacy Layout":

                        hexColor = "#FF2f4aab";
                        break;

                case "KiCad Schematic":

                        hexColor = "#FF2f4aab";
                        break;

                case "Kotlin":

                        hexColor = "#FFA97BFF";
                        break;

                case "KRL":

                        hexColor = "#FF28430A";
                        break;

                case "LabVIEW":

                        hexColor = "#FFfede06";
                        break;

                case "Lark":

                        hexColor = "#FF2980B9";
                        break;

                case "Lasso":

                        hexColor = "#FF999999";
                        break;

                case "Latte":

                        hexColor = "#FFf2a542";
                        break;

                case "Less":

                        hexColor = "#FF1d365d";
                        break;

                case "Lex":

                        hexColor = "#FFDBCA00";
                        break;

                case "LFE":

                        hexColor = "#FF4C3023";
                        break;

                case "LilyPond":

                        hexColor = "#FF9ccc7c";
                        break;

                case "Liquid":

                        hexColor = "#FF67b8de";
                        break;

                case "Literate Agda":

                        hexColor = "#FF315665";
                        break;

                case "Literate CoffeeScript":

                        hexColor = "#FF244776";
                        break;

                case "Literate Haskell":

                        hexColor = "#FF5e5086";
                        break;

                case "LiveScript":

                        hexColor = "#FF499886";
                        break;

                case "LLVM":

                        hexColor = "#FF185619";
                        break;

                case "Logtalk":

                        hexColor = "#FF295b9a";
                        break;

                case "LOLCODE":

                        hexColor = "#FFcc9900";
                        break;

                case "LookML":

                        hexColor = "#FF652B81";
                        break;

                case "LSL":

                        hexColor = "#FF3d9970";
                        break;

                case "Lua":

                        hexColor = "#FF000080";
                        break;

                case "Macaulay2":

                        hexColor = "#FFd8ffff";
                        break;

                case "Makefile":

                        hexColor = "#FF427819";
                        break;

                case "Mako":

                        hexColor = "#FF7e858d";
                        break;

                case "Markdown":

                        hexColor = "#FF083fa1";
                        break;

                case "Marko":

                        hexColor = "#FF42bff2";
                        break;

                case "Mask":

                        hexColor = "#FFf97732";
                        break;

                case "Mathematica":

                        hexColor = "#FFdd1100";
                        break;

                case "MATLAB":

                        hexColor = "#FFe16737";
                        break;

                case "Max":

                        hexColor = "#FFc4a79c";
                        break;

                case "MAXScript":

                        hexColor = "#FF00a6a6";
                        break;

                case "mcfunction":

                        hexColor = "#FFE22837";
                        break;

                case "Mercury":

                        hexColor = "#FFff2b2b";
                        break;

                case "Meson":

                        hexColor = "#FF007800";
                        break;

                case "Metal":

                        hexColor = "#FF8f14e9";
                        break;

                case "Mirah":

                        hexColor = "#FFc7a938";
                        break;

                case "mIRC Script":

                        hexColor = "#FF3d57c3";
                        break;

                case "MLIR":

                        hexColor = "#FF5EC8DB";
                        break;

                case "Modelica":

                        hexColor = "#FFde1d31";
                        break;

                case "Modula-2":

                        hexColor = "#FF10253f";
                        break;

                case "Modula-3":

                        hexColor = "#FF223388";
                        break;

                case "MoonScript":

                        hexColor = "#FFff4585";
                        break;

                case "Motoko":

                        hexColor = "#FFfbb03b";
                        break;

                case "Motorola 68K Assembly":

                        hexColor = "#FF005daa";
                        break;

                case "MQL4":

                        hexColor = "#FF62A8D6";
                        break;

                case "MQL5":

                        hexColor = "#FF4A76B8";
                        break;

                case "MTML":

                        hexColor = "#FFb7e1f4";
                        break;

                case "mupad":

                        hexColor = "#FF244963";
                        break;

                case "Mustache":

                        hexColor = "#FF724b3b";
                        break;

                case "nanorc":

                        hexColor = "#FF2d004d";
                        break;

                case "NCL":

                        hexColor = "#FF28431f";
                        break;

                case "Nearley":

                        hexColor = "#FF990000";
                        break;

                case "Nemerle":

                        hexColor = "#FF3d3c6e";
                        break;

                case "nesC":

                        hexColor = "#FF94B0C7";
                        break;

                case "NetLinx":

                        hexColor = "#FF0aa0ff";
                        break;

                case "NetLinx+ERB":

                        hexColor = "#FF747faa";
                        break;

                case "NetLogo":

                        hexColor = "#FFff6375";
                        break;

                case "NewLisp":

                        hexColor = "#FF87AED7";
                        break;

                case "Nextflow":

                        hexColor = "#FF3ac486";
                        break;

                case "Nginx":

                        hexColor = "#FF009639";
                        break;

                case "Nim":

                        hexColor = "#FFffc200";
                        break;

                case "Nit":

                        hexColor = "#FF009917";
                        break;

                case "Nix":

                        hexColor = "#FF7e7eff";
                        break;

                case "NPM Config":

                        hexColor = "#FFcb3837";
                        break;

                case "Nu":

                        hexColor = "#FFc9df40";
                        break;

                case "NumPy":

                        hexColor = "#FF9C8AF9";
                        break;

                case "Nunjucks":

                        hexColor = "#FF3d8137";
                        break;

                case "NWScript":

                        hexColor = "#FF111522";
                        break;

                case "Objective-C":

                        hexColor = "#FF438eff";
                        break;

                case "Objective-C++":

                        hexColor = "#FF6866fb";
                        break;

                case "Objective-J":

                        hexColor = "#FFff0c5a";
                        break;

                case "ObjectScript":

                        hexColor = "#FF424893";
                        break;

                case "OCaml":

                        hexColor = "#FF3be133";
                        break;

                case "Odin":

                        hexColor = "#FF60AFFE";
                        break;

                case "Omgrofl":

                        hexColor = "#FFcabbff";
                        break;

                case "ooc":

                        hexColor = "#FFb0b77e";
                        break;

                case "Opal":

                        hexColor = "#FFf7ede0";
                        break;

                case "Open Policy Agent":

                        hexColor = "#FF7d9199";
                        break;

                case "OpenCL":

                        hexColor = "#FFed2e2d";
                        break;

                case "OpenEdge ABL":

                        hexColor = "#FF5ce600";
                        break;

                case "OpenQASM":

                        hexColor = "#FFAA70FF";
                        break;

                case "OpenSCAD":

                        hexColor = "#FFe5cd45";
                        break;

                case "Org":

                        hexColor = "#FF77aa99";
                        break;

                case "Oxygene":

                        hexColor = "#FFcdd0e3";
                        break;

                case "Oz":

                        hexColor = "#FFfab738";
                        break;

                case "P4":

                        hexColor = "#FF7055b5";
                        break;

                case "Pan":

                        hexColor = "#FFcc0000";
                        break;

                case "Papyrus":

                        hexColor = "#FF6600cc";
                        break;

                case "Parrot":

                        hexColor = "#FFf3ca0a";
                        break;

                case "Pascal":

                        hexColor = "#FFE3F171";
                        break;

                case "Pawn":

                        hexColor = "#FFdbb284";
                        break;

                case "PEG.js":

                        hexColor = "#FF234d6b";
                        break;

                case "Pep8":

                        hexColor = "#FFC76F5B";
                        break;

                case "Perl":

                        hexColor = "#FF0298c3";
                        break;

                case "PHP":

                        hexColor = "#FF4F5D95";
                        break;

                case "PicoLisp":

                        hexColor = "#FF6067af";
                        break;

                case "PigLatin":

                        hexColor = "#FFfcd7de";
                        break;

                case "Pike":

                        hexColor = "#FF005390";
                        break;

                case "PLpgSQL":

                        hexColor = "#FF336790";
                        break;

                case "PLSQL":

                        hexColor = "#FFdad8d8";
                        break;

                case "PogoScript":

                        hexColor = "#FFd80074";
                        break;

                case "PostCSS":

                        hexColor = "#FFdc3a0c";
                        break;

                case "PostScript":

                        hexColor = "#FFda291c";
                        break;

                case "POV-Ray SDL":

                        hexColor = "#FF6bac65";
                        break;

                case "PowerBuilder":

                        hexColor = "#FF8f0f8d";
                        break;

                case "PowerShell":

                        hexColor = "#FF012456";
                        break;

                case "Prisma":

                        hexColor = "#FF0c344b";
                        break;

                case "Processing":

                        hexColor = "#FF0096D8";
                        break;

                case "Prolog":

                        hexColor = "#FF74283c";
                        break;

                case "Promela":

                        hexColor = "#FFde0000";
                        break;

                case "Propeller Spin":

                        hexColor = "#FF7fa2a7";
                        break;

                case "Pug":

                        hexColor = "#FFa86454";
                        break;

                case "Puppet":

                        hexColor = "#FF302B6D";
                        break;

                case "PureBasic":

                        hexColor = "#FF5a6986";
                        break;

                case "PureScript":

                        hexColor = "#FF1D222D";
                        break;

                case "Python":

                        hexColor = "#FF3572A5";
                        break;

                case "Python console":

                        hexColor = "#FF3572A5";
                        break;

                case "Python traceback":

                        hexColor = "#FF3572A5";
                        break;

                case "q":

                        hexColor = "#FF0040cd";
                        break;

                case "Q#":

                        hexColor = "#FFfed659";
                        break;

                case "QML":

                        hexColor = "#FF44a51c";
                        break;

                case "Qt Script":

                        hexColor = "#FF00b841";
                        break;

                case "Quake":

                        hexColor = "#FF882233";
                        break;

                case "R":

                        hexColor = "#FF198CE7";
                        break;

                case "Racket":

                        hexColor = "#FF3c5caa";
                        break;

                case "Ragel":

                        hexColor = "#FF9d5200";
                        break;

                case "Raku":

                        hexColor = "#FF0000fb";
                        break;

                case "RAML":

                        hexColor = "#FF77d9fb";
                        break;

                case "Rascal":

                        hexColor = "#FFfffaa0";
                        break;

                case "RDoc":

                        hexColor = "#FF701516";
                        break;

                case "Reason":

                        hexColor = "#FFff5847";
                        break;

                case "Rebol":

                        hexColor = "#FF358a5b";
                        break;

                case "Record Jar":

                        hexColor = "#FF0673ba";
                        break;

                case "Red":

                        hexColor = "#FFf50000";
                        break;

                case "Regular Expression":

                        hexColor = "#FF009a00";
                        break;

                case "Ren'Py":

                        hexColor = "#FFff7f7f";
                        break;

                case "ReScript":

                        hexColor = "#FFed5051";
                        break;

                case "reStructuredText":

                        hexColor = "#FF141414";
                        break;

                case "REXX":

                        hexColor = "#FFd90e09";
                        break;

                case "Ring":

                        hexColor = "#FF2D54CB";
                        break;

                case "Riot":

                        hexColor = "#FFA71E49";
                        break;

                case "RMarkdown":

                        hexColor = "#FF198ce7";
                        break;

                case "RobotFramework":

                        hexColor = "#FF00c0b5";
                        break;

                case "Roff":

                        hexColor = "#FFecdebe";
                        break;

                case "Roff Manpage":

                        hexColor = "#FFecdebe";
                        break;

                case "Rouge":

                        hexColor = "#FFcc0088";
                        break;

                case "Ruby":

                        hexColor = "#FF701516";
                        break;

                case "RUNOFF":

                        hexColor = "#FF665a4e";
                        break;

                case "Rust":

                        hexColor = "#FFdea584";
                        break;

                case "SaltStack":

                        hexColor = "#FF646464";
                        break;

                case "SAS":

                        hexColor = "#FFB34936";
                        break;

                case "Sass":

                        hexColor = "#FFa53b70";
                        break;

                case "Scala":

                        hexColor = "#FFc22d40";
                        break;

                case "Scaml":

                        hexColor = "#FFbd181a";
                        break;

                case "Scheme":

                        hexColor = "#FF1e4aec";
                        break;

                case "Scilab":

                        hexColor = "#FFca0f21";
                        break;

                case "SCSS":

                        hexColor = "#FFc6538c";
                        break;

                case "sed":

                        hexColor = "#FF64b970";
                        break;

                case "Self":

                        hexColor = "#FF0579aa";
                        break;

                case "ShaderLab":

                        hexColor = "#FF222c37";
                        break;

                case "Shell":

                        hexColor = "#FF89e051";
                        break;

                case "ShellCheck Config":

                        hexColor = "#FFcecfcb";
                        break;

                case "Shen":

                        hexColor = "#FF120F14";
                        break;

                case "Singularity":

                        hexColor = "#FF64E6AD";
                        break;

                case "Slash":

                        hexColor = "#FF007eff";
                        break;

                case "Slice":

                        hexColor = "#FF003fa2";
                        break;

                case "Slim":

                        hexColor = "#FF2b2b2b";
                        break;

                case "Smalltalk":

                        hexColor = "#FF596706";
                        break;

                case "Smarty":

                        hexColor = "#FFf0c040";
                        break;

                case "SmPL":

                        hexColor = "#FFc94949";
                        break;

                case "Solidity":

                        hexColor = "#FFAA6746";
                        break;

                case "SourcePawn":

                        hexColor = "#FFf69e1d";
                        break;

                case "SPARQL":

                        hexColor = "#FF0C4597";
                        break;

                case "SQF":

                        hexColor = "#FF3F3F3F";
                        break;

                case "SQL":

                        hexColor = "#FFe38c00";
                        break;

                case "SQLPL":

                        hexColor = "#FFe38c00";
                        break;

                case "Squirrel":

                        hexColor = "#FF800000";
                        break;

                case "SRecode Template":

                        hexColor = "#FF348a34";
                        break;

                case "Stan":

                        hexColor = "#FFb2011d";
                        break;

                case "Standard ML":

                        hexColor = "#FFdc566d";
                        break;

                case "Starlark":

                        hexColor = "#FF76d275";
                        break;

                case "Stata":

                        hexColor = "#FF1a5f91";
                        break;

                case "StringTemplate":

                        hexColor = "#FF3fb34f";
                        break;

                case "Stylus":

                        hexColor = "#FFff6347";
                        break;

                case "SubRip Text":

                        hexColor = "#FF9e0101";
                        break;

                case "SugarSS":

                        hexColor = "#FF2fcc9f";
                        break;

                case "SuperCollider":

                        hexColor = "#FF46390b";
                        break;

                case "Svelte":

                        hexColor = "#FFff3e00";
                        break;

                case "SVG":

                        hexColor = "#FFff9900";
                        break;

                case "Swift":

                        hexColor = "#FFF05138";
                        break;

                case "SystemVerilog":

                        hexColor = "#FFDAE1C2";
                        break;

                case "Tcl":

                        hexColor = "#FFe4cc98";
                        break;

                case "Terra":

                        hexColor = "#FF00004c";
                        break;

                case "TeX":

                        hexColor = "#FF3D6117";
                        break;

                case "Textile":

                        hexColor = "#FFffe7ac";
                        break;

                case "TextMate Properties":

                        hexColor = "#FFdf66e4";
                        break;

                case "Thrift":

                        hexColor = "#FFD12127";
                        break;

                case "TI Program":

                        hexColor = "#FFA0AA87";
                        break;

                case "TLA":

                        hexColor = "#FF4b0079";
                        break;

                case "TOML":

                        hexColor = "#FF9c4221";
                        break;

                case "TSQL":

                        hexColor = "#FFe38c00";
                        break;

                case "TSV":

                        hexColor = "#FF237346";
                        break;

                case "TSX":

                        hexColor = "#FF2b7489";
                        break;

                case "Turing":

                        hexColor = "#FFcf142b";
                        break;

                case "Twig":

                        hexColor = "#FFc1d026";
                        break;

                case "TXL":

                        hexColor = "#FF0178b8";
                        break;

                case "TypeScript":

                        hexColor = "#FF2b7489";
                        break;

                case "Unified Parallel C":

                        hexColor = "#FF4e3617";
                        break;

                case "Unity3D Asset":

                        hexColor = "#FF222c37";
                        break;

                case "Uno":

                        hexColor = "#FF9933cc";
                        break;

                case "UnrealScript":

                        hexColor = "#FFa54c4d";
                        break;

                case "UrWeb":

                        hexColor = "#FFccccee";
                        break;

                case "V":

                        hexColor = "#FF4f87c4";
                        break;

                case "Vala":

                        hexColor = "#FFfbe5cd";
                        break;

                case "Valve Data Format":

                        hexColor = "#FFf26025";
                        break;

                case "VBA":

                        hexColor = "#FF867db1";
                        break;

                case "VBScript":

                        hexColor = "#FF15dcdc";
                        break;

                case "VCL":

                        hexColor = "#FF148AA8";
                        break;

                case "Verilog":

                        hexColor = "#FFb2b7f8";
                        break;

                case "VHDL":

                        hexColor = "#FFadb2cb";
                        break;

                case "Vim Help File":

                        hexColor = "#FF199f4b";
                        break;

                case "Vim Script":

                        hexColor = "#FF199f4b";
                        break;

                case "Vim Snippet":

                        hexColor = "#FF199f4b";
                        break;

                case "Visual Basic .NET":

                        hexColor = "#FF945db7";
                        break;

                case "Volt":

                        hexColor = "#FF1F1F1F";
                        break;

                case "Vue":

                        hexColor = "#FF41b883";
                        break;

                case "wdl":

                        hexColor = "#FF42f1f4";
                        break;

                case "Web Ontology Language":

                        hexColor = "#FF5b70bd";
                        break;

                case "WebAssembly":

                        hexColor = "#FF04133b";
                        break;

                case "Wikitext":

                        hexColor = "#FFfc5757";
                        break;

                case "Windows Registry Entries":

                        hexColor = "#FF52d5ff";
                        break;

                case "wisp":

                        hexColor = "#FF7582D1";
                        break;

                case "Wollok":

                        hexColor = "#FFa23738";
                        break;

                case "World of Warcraft Addon Data":

                        hexColor = "#FFf7e43f";
                        break;

                case "X10":

                        hexColor = "#FF4B6BEF";
                        break;

                case "xBase":

                        hexColor = "#FF403a40";
                        break;

                case "XC":

                        hexColor = "#FF99DA07";
                        break;

                case "XML":

                        hexColor = "#FF0060ac";
                        break;

                case "XML Property List":

                        hexColor = "#FF0060ac";
                        break;

                case "Xojo":

                        hexColor = "#FF81bd41";
                        break;

                case "Xonsh":

                        hexColor = "#FF285EEF";
                        break;

                case "XQuery":

                        hexColor = "#FF5232e7";
                        break;

                case "XSLT":

                        hexColor = "#FFEB8CEB";
                        break;

                case "Xtend":

                        hexColor = "#FF24255d";
                        break;

                case "Yacc":

                        hexColor = "#FF4B6C4B";
                        break;

                case "YAML":

                        hexColor = "#FFcb171e";
                        break;

                case "YARA":

                        hexColor = "#FF220000";
                        break;

                case "YASnippet":

                        hexColor = "#FF32AB90";
                        break;

                case "ZAP":

                        hexColor = "#FF0d665e";
                        break;

                case "ZenScript":

                        hexColor = "#FF00BCD1";
                        break;

                case "Zephir":

                        hexColor = "#FF118f9e";
                        break;

                case "Zig":

                        hexColor = "#FFec915c";
                        break;

                case "ZIL":

                        hexColor = "#FFdc75e5";
                        break;

                case "Zimpl":

                        hexColor = "#FFd67711";
                        break;

            }

            return new SolidColorBrush(Color.FromArgb(
                Convert.ToByte(hexColor.Substring(1, 2), 16),
                Convert.ToByte(hexColor.Substring(3, 2), 16),
                Convert.ToByte(hexColor.Substring(5, 2), 16),
                Convert.ToByte(hexColor.Substring(5, 2), 16)));
        }
    }
}
