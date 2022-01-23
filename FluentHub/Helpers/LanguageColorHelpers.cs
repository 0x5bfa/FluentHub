// Copyright (c) 2022 fluenthub-uwp
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;


namespace FluentHub.Helpers
{
    public class LanguageColorHelpers
    {
        public static SolidColorBrush Get(string language)
        {
            string hexColor = "#000000";

            switch (language)
            {
                case "1C Enterprise":
                    hexColor = "#814CCC";
                    break;
                case "4D":
                    hexColor = "#004289";
                    break;
                case "ABAP":
                    hexColor = "#E8274B";
                    break;
                case "ABAP CDS":
                    hexColor = "#555e25";
                    break;
                case "ActionScript":
                    hexColor = "#882B0F";
                    break;
                case "Ada":
                    hexColor = "#02f88c";
                    break;
                case "Adobe Font Metrics":
                    hexColor = "#fa0f00";
                    break;
                case "Agda":
                    hexColor = "#315665";
                    break;
                case "AGS Script":
                    hexColor = "#B9D9FF";
                    break;
                case "AIDL":
                    hexColor = "#34EB6B";
                    break;
                case "AL":
                    hexColor = "#3AA2B5";
                    break;
                case "Alloy":
                    hexColor = "#64C800";
                    break;
                case "Alpine Abuild":
                    hexColor = "#0D597F";
                    break;
                case "Altium Designer":
                    hexColor = "#A89663";
                    break;
                case "AMPL":
                    hexColor = "#E6EFBB";
                    break;
                case "AngelScript":
                    hexColor = "#C7D7DC";
                    break;
                case "Ant Build System":
                    hexColor = "#A9157E";
                    break;
                case "ANTLR":
                    hexColor = "#9DC3FF";
                    break;
                case "ApacheConf":
                    hexColor = "#d12127";
                    break;
                case "Apex":
                    hexColor = "#1797c0";
                    break;
                case "API Blueprint":
                    hexColor = "#2ACCA8";
                    break;
                case "APL":
                    hexColor = "#5A8164";
                    break;
                case "Apollo Guidance Computer":
                    hexColor = "#0B3D91";
                    break;
                case "AppleScript":
                    hexColor = "#101F1F";
                    break;
                case "Arc":
                    hexColor = "#aa2afe";
                    break;
                case "AsciiDoc":
                    hexColor = "#73a0c5";
                    break;
                case "ASP.NET":
                    hexColor = "#9400ff";
                    break;
                case "AspectJ":
                    hexColor = "#a957b0";
                    break;
                case "Assembly":
                    hexColor = "#6E4C13";
                    break;
                case "Astro":
                    hexColor = "#ff5a03";
                    break;
                case "Asymptote":
                    hexColor = "#ff0000";
                    break;
                case "ATS":
                    hexColor = "#1ac620";
                    break;
                case "Augeas":
                    hexColor = "#9CC134";
                    break;
                case "AutoHotkey":
                    hexColor = "#6594b9";
                    break;
                case "AutoIt":
                    hexColor = "#1C3552";
                    break;
                case "Avro IDL":
                    hexColor = "#0040FF";
                    break;
                case "Awk":
                    hexColor = "#c30e9b";
                    break;
                case "Ballerina":
                    hexColor = "#FF5000";
                    break;
                case "BASIC":
                    hexColor = "#ff0000";
                    break;
                case "Batchfile":
                    hexColor = "#C1F12E";
                    break;
                case "Beef":
                    hexColor = "#a52f4e";
                    break;
                case "BibTeX":
                    hexColor = "#778899";
                    break;
                case "Bicep":
                    hexColor = "#519aba";
                    break;
                case "Bison":
                    hexColor = "#6A463F";
                    break;
                case "BitBake":
                    hexColor = "#00bce4";
                    break;
                case "Blade":
                    hexColor = "#f7523f";
                    break;
                case "BlitzBasic":
                    hexColor = "#00FFAE";
                    break;
                case "BlitzMax":
                    hexColor = "#cd6400";
                    break;
                case "Bluespec":
                    hexColor = "#12223c";
                    break;
                case "Boo":
                    hexColor = "#d4bec1";
                    break;
                case "Boogie":
                    hexColor = "#c80fa0";
                    break;
                case "Brainfuck":
                    hexColor = "#2F2530";
                    break;
                case "Brightscript":
                    hexColor = "#662D91";
                    break;
                case "Browserslist":
                    hexColor = "#ffd539";
                    break;
                case "C":
                    hexColor = "#555555";
                    break;
                case "C#":
                    hexColor = "#178600";
                    break;
                case "C++":
                    hexColor = "#f34b7d";
                    break;
                case "Cabal Config":
                    hexColor = "#483465";
                    break;
                case "Cap'n Proto":
                    hexColor = "#c42727";
                    break;
                case "Ceylon":
                    hexColor = "#dfa535";
                    break;
                case "Chapel":
                    hexColor = "#8dc63f";
                    break;
                case "ChucK":
                    hexColor = "#3f8000";
                    break;
                case "Cirru":
                    hexColor = "#ccccff";
                    break;
                case "Clarion":
                    hexColor = "#db901e";
                    break;
                case "Classic ASP":
                    hexColor = "#6a40fd";
                    break;
                case "Clean":
                    hexColor = "#3F85AF";
                    break;
                case "Click":
                    hexColor = "#E4E6F3";
                    break;
                case "CLIPS":
                    hexColor = "#00A300";
                    break;
                case "Clojure":
                    hexColor = "#db5855";
                    break;
                case "Closure Templates":
                    hexColor = "#0d948f";
                    break;
                case "Cloud Firestore Security Rules":
                    hexColor = "#FFA000";
                    break;
                case "CMake":
                    hexColor = "#DA3434";
                    break;
                case "CodeQL":
                    hexColor = "#140f46";
                    break;
                case "CoffeeScript":
                    hexColor = "#244776";
                    break;
                case "ColdFusion":
                    hexColor = "#ed2cd6";
                    break;
                case "ColdFusion CFC":
                    hexColor = "#ed2cd6";
                    break;
                case "COLLADA":
                    hexColor = "#F1A42B";
                    break;
                case "Common Lisp":
                    hexColor = "#3fb68b";
                    break;
                case "Common Workflow Language":
                    hexColor = "#B5314C";
                    break;
                case "Component Pascal":
                    hexColor = "#B0CE4E";
                    break;
                case "Coq":
                    hexColor = "#d0b68c";
                    break;
                case "Crystal":
                    hexColor = "#000100";
                    break;
                case "CSON":
                    hexColor = "#244776";
                    break;
                case "Csound":
                    hexColor = "#1a1a1a";
                    break;
                case "Csound Document":
                    hexColor = "#1a1a1a";
                    break;
                case "Csound Score":
                    hexColor = "#1a1a1a";
                    break;
                case "CSS":
                    hexColor = "#563d7c";
                    break;
                case "CSV":
                    hexColor = "#237346";
                    break;
                case "Cuda":
                    hexColor = "#3A4E3A";
                    break;
                case "CUE":
                    hexColor = "#5886E1";
                    break;
                case "CWeb":
                    hexColor = "#00007a";
                    break;
                case "Cython":
                    hexColor = "#fedf5b";
                    break;
                case "D":
                    hexColor = "#ba595e";
                    break;
                case "Dafny":
                    hexColor = "#FFEC25";
                    break;
                case "Darcs Patch":
                    hexColor = "#8eff23";
                    break;
                case "Dart":
                    hexColor = "#00B4AB";
                    break;
                case "DataWeave":
                    hexColor = "#003a52";
                    break;
                case "Dhall":
                    hexColor = "#dfafff";
                    break;
                case "DirectX 3D File":
                    hexColor = "#aace60";
                    break;
                case "DM":
                    hexColor = "#447265";
                    break;
                case "Dockerfile":
                    hexColor = "#384d54";
                    break;
                case "Dogescript":
                    hexColor = "#cca760";
                    break;
                case "Dylan":
                    hexColor = "#6c616e";
                    break;
                case "E":
                    hexColor = "#ccce35";
                    break;
                case "Earthly":
                    hexColor = "#2af0ff";
                    break;
                case "Easybuild":
                    hexColor = "#069406";
                    break;
                case "eC":
                    hexColor = "#913960";
                    break;
                case "Ecere Projects":
                    hexColor = "#913960";
                    break;
                case "ECL":
                    hexColor = "#8a1267";
                    break;
                case "ECLiPSe":
                    hexColor = "#001d9d";
                    break;
                case "EditorConfig":
                    hexColor = "#fff1f2";
                    break;
                case "Eiffel":
                    hexColor = "#4d6977";
                    break;
                case "EJS":
                    hexColor = "#a91e50";
                    break;
                case "Elixir":
                    hexColor = "#6e4a7e";
                    break;
                case "Elm":
                    hexColor = "#60B5CC";
                    break;
                case "Emacs Lisp":
                    hexColor = "#c065db";
                    break;
                case "EmberScript":
                    hexColor = "#FFF4F3";
                    break;
                case "EQ":
                    hexColor = "#a78649";
                    break;
                case "Erlang":
                    hexColor = "#B83998";
                    break;
                case "F#":
                    hexColor = "#b845fc";
                    break;
                case "F*":
                    hexColor = "#572e30";
                    break;
                case "Factor":
                    hexColor = "#636746";
                    break;
                case "Fancy":
                    hexColor = "#7b9db4";
                    break;
                case "Fantom":
                    hexColor = "#14253c";
                    break;
                case "Faust":
                    hexColor = "#c37240";
                    break;
                case "Fennel":
                    hexColor = "#fff3d7";
                    break;
                case "FIGlet Font":
                    hexColor = "#FFDDBB";
                    break;
                case "Filebench WML":
                    hexColor = "#F6B900";
                    break;
                case "fish":
                    hexColor = "#4aae47";
                    break;
                case "Fluent":
                    hexColor = "#ffcc33";
                    break;
                case "FLUX":
                    hexColor = "#88ccff";
                    break;
                case "Forth":
                    hexColor = "#341708";
                    break;
                case "Fortran":
                    hexColor = "#4d41b1";
                    break;
                case "Fortran Free Form":
                    hexColor = "#4d41b1";
                    break;
                case "FreeBasic":
                    hexColor = "#867db1";
                    break;
                case "FreeMarker":
                    hexColor = "#0050b2";
                    break;
                case "Frege":
                    hexColor = "#00cafe";
                    break;
                case "Futhark":
                    hexColor = "#5f021f";
                    break;
                case "G-code":
                    hexColor = "#D08CF2";
                    break;
                case "Game Maker Language":
                    hexColor = "#71b417";
                    break;
                case "GAML":
                    hexColor = "#FFC766";
                    break;
                case "GAMS":
                    hexColor = "#f49a22";
                    break;
                case "GAP":
                    hexColor = "#0000cc";
                    break;
                case "GCC Machine Description":
                    hexColor = "#FFCFAB";
                    break;
                case "GDScript":
                    hexColor = "#355570";
                    break;
                case "GEDCOM":
                    hexColor = "#003058";
                    break;
                case "Gemfile.lock":
                    hexColor = "#701516";
                    break;
                case "Genie":
                    hexColor = "#fb855d";
                    break;
                case "Genshi":
                    hexColor = "#951531";
                    break;
                case "Gentoo Ebuild":
                    hexColor = "#9400ff";
                    break;
                case "Gentoo Eclass":
                    hexColor = "#9400ff";
                    break;
                case "Gerber Image":
                    hexColor = "#d20b00";
                    break;
                case "Gherkin":
                    hexColor = "#5B2063";
                    break;
                case "Git Attributes":
                    hexColor = "#F44D27";
                    break;
                case "Git Config":
                    hexColor = "#F44D27";
                    break;
                case "GLSL":
                    hexColor = "#5686a5";
                    break;
                case "Glyph":
                    hexColor = "#c1ac7f";
                    break;
                case "Gnuplot":
                    hexColor = "#f0a9f0";
                    break;
                case "Go":
                    hexColor = "#00ADD8";
                    break;
                case "Go Checksums":
                    hexColor = "#00ADD8";
                    break;
                case "Go Module":
                    hexColor = "#00ADD8";
                    break;
                case "Golo":
                    hexColor = "#88562A";
                    break;
                case "Gosu":
                    hexColor = "#82937f";
                    break;
                case "Grace":
                    hexColor = "#615f8b";
                    break;
                case "Gradle":
                    hexColor = "#02303a";
                    break;
                case "Grammatical Framework":
                    hexColor = "#ff0000";
                    break;
                case "GraphQL":
                    hexColor = "#e10098";
                    break;
                case "Graphviz (DOT)":
                    hexColor = "#2596be";
                    break;
                case "Groovy":
                    hexColor = "#4298b8";
                    break;
                case "Groovy Server Pages":
                    hexColor = "#4298b8";
                    break;
                case "Hack":
                    hexColor = "#878787";
                    break;
                case "Haml":
                    hexColor = "#ece2a9";
                    break;
                case "Handlebars":
                    hexColor = "#f7931e";
                    break;
                case "HAProxy":
                    hexColor = "#106da9";
                    break;
                case "Harbour":
                    hexColor = "#0e60e3";
                    break;
                case "Haskell":
                    hexColor = "#5e5086";
                    break;
                case "Haxe":
                    hexColor = "#df7900";
                    break;
                case "HiveQL":
                    hexColor = "#dce200";
                    break;
                case "HLSL":
                    hexColor = "#aace60";
                    break;
                case "HolyC":
                    hexColor = "#ffefaf";
                    break;
                case "HTML":
                    hexColor = "#e34c26";
                    break;
                case "HTML+ECR":
                    hexColor = "#2e1052";
                    break;
                case "HTML+EEX":
                    hexColor = "#6e4a7e";
                    break;
                case "HTML+ERB":
                    hexColor = "#701516";
                    break;
                case "HTML+PHP":
                    hexColor = "#4f5d95";
                    break;
                case "HTML+Razor":
                    hexColor = "#512be4";
                    break;
                case "HTTP":
                    hexColor = "#005C9C";
                    break;
                case "HXML":
                    hexColor = "#f68712";
                    break;
                case "Hy":
                    hexColor = "#7790B2";
                    break;
                case "IDL":
                    hexColor = "#a3522f";
                    break;
                case "Idris":
                    hexColor = "#b30000";
                    break;
                case "Ignore List":
                    hexColor = "#000000";
                    break;
                case "IGOR Pro":
                    hexColor = "#0000cc";
                    break;
                case "ImageJ Macro":
                    hexColor = "#99AAFF";
                    break;
                case "INI":
                    hexColor = "#d1dbe0";
                    break;
                case "Inno Setup":
                    hexColor = "#264b99";
                    break;
                case "Io":
                    hexColor = "#a9188d";
                    break;
                case "Ioke":
                    hexColor = "#078193";
                    break;
                case "Isabelle":
                    hexColor = "#FEFE00";
                    break;
                case "Isabelle ROOT":
                    hexColor = "#FEFE00";
                    break;
                case "J":
                    hexColor = "#9EEDFF";
                    break;
                case "JAR Manifest":
                    hexColor = "#b07219";
                    break;
                case "Jasmin":
                    hexColor = "#d03600";
                    break;
                case "Java":
                    hexColor = "#b07219";
                    break;
                case "Java Properties":
                    hexColor = "#2A6277";
                    break;
                case "Java Server Pages":
                    hexColor = "#2A6277";
                    break;
                case "JavaScript":
                    hexColor = "#f1e05a";
                    break;
                case "JavaScript+ERB":
                    hexColor = "#f1e05a";
                    break;
                case "Jest Snapshot":
                    hexColor = "#15c213";
                    break;
                case "JFlex":
                    hexColor = "#DBCA00";
                    break;
                case "Jinja":
                    hexColor = "#a52a22";
                    break;
                case "Jison":
                    hexColor = "#56b3cb";
                    break;
                case "Jison Lex":
                    hexColor = "#56b3cb";
                    break;
                case "Jolie":
                    hexColor = "#843179";
                    break;
                case "jq":
                    hexColor = "#c7254e";
                    break;
                case "JSON":
                    hexColor = "#292929";
                    break;
                case "JSON with Comments":
                    hexColor = "#292929";
                    break;
                case "JSON5":
                    hexColor = "#267CB9";
                    break;
                case "JSONiq":
                    hexColor = "#40d47e";
                    break;
                case "JSONLD":
                    hexColor = "#0c479c";
                    break;
                case "Jsonnet":
                    hexColor = "#0064bd";
                    break;
                case "Julia":
                    hexColor = "#a270ba";
                    break;
                case "Jupyter Notebook":
                    hexColor = "#DA5B0B";
                    break;
                case "Kaitai Struct":
                    hexColor = "#773b37";
                    break;
                case "KakouneScript":
                    hexColor = "#6f8042";
                    break;
                case "KiCad Layout":
                    hexColor = "#2f4aab";
                    break;
                case "KiCad Legacy Layout":
                    hexColor = "#2f4aab";
                    break;
                case "KiCad Schematic":
                    hexColor = "#2f4aab";
                    break;
                case "Kotlin":
                    hexColor = "#A97BFF";
                    break;
                case "KRL":
                    hexColor = "#28430A";
                    break;
                case "LabVIEW":
                    hexColor = "#fede06";
                    break;
                case "Lark":
                    hexColor = "#2980B9";
                    break;
                case "Lasso":
                    hexColor = "#999999";
                    break;
                case "Latte":
                    hexColor = "#f2a542";
                    break;
                case "Less":
                    hexColor = "#1d365d";
                    break;
                case "Lex":
                    hexColor = "#DBCA00";
                    break;
                case "LFE":
                    hexColor = "#4C3023";
                    break;
                case "LilyPond":
                    hexColor = "#9ccc7c";
                    break;
                case "Liquid":
                    hexColor = "#67b8de";
                    break;
                case "Literate Agda":
                    hexColor = "#315665";
                    break;
                case "Literate CoffeeScript":
                    hexColor = "#244776";
                    break;
                case "Literate Haskell":
                    hexColor = "#5e5086";
                    break;
                case "LiveScript":
                    hexColor = "#499886";
                    break;
                case "LLVM":
                    hexColor = "#185619";
                    break;
                case "Logtalk":
                    hexColor = "#295b9a";
                    break;
                case "LOLCODE":
                    hexColor = "#cc9900";
                    break;
                case "LookML":
                    hexColor = "#652B81";
                    break;
                case "LSL":
                    hexColor = "#3d9970";
                    break;
                case "Lua":
                    hexColor = "#000080";
                    break;
                case "Macaulay2":
                    hexColor = "#d8ffff";
                    break;
                case "Makefile":
                    hexColor = "#427819";
                    break;
                case "Mako":
                    hexColor = "#7e858d";
                    break;
                case "Markdown":
                    hexColor = "#083fa1";
                    break;
                case "Marko":
                    hexColor = "#42bff2";
                    break;
                case "Mask":
                    hexColor = "#f97732";
                    break;
                case "Mathematica":
                    hexColor = "#dd1100";
                    break;
                case "MATLAB":
                    hexColor = "#e16737";
                    break;
                case "Max":
                    hexColor = "#c4a79c";
                    break;
                case "MAXScript":
                    hexColor = "#00a6a6";
                    break;
                case "mcfunction":
                    hexColor = "#E22837";
                    break;
                case "Mercury":
                    hexColor = "#ff2b2b";
                    break;
                case "Meson":
                    hexColor = "#007800";
                    break;
                case "Metal":
                    hexColor = "#8f14e9";
                    break;
                case "Mirah":
                    hexColor = "#c7a938";
                    break;
                case "mIRC Script":
                    hexColor = "#3d57c3";
                    break;
                case "MLIR":
                    hexColor = "#5EC8DB";
                    break;
                case "Modelica":
                    hexColor = "#de1d31";
                    break;
                case "Modula-2":
                    hexColor = "#10253f";
                    break;
                case "Modula-3":
                    hexColor = "#223388";
                    break;
                case "MoonScript":
                    hexColor = "#ff4585";
                    break;
                case "Motoko":
                    hexColor = "#fbb03b";
                    break;
                case "Motorola 68K Assembly":
                    hexColor = "#005daa";
                    break;
                case "MQL4":
                    hexColor = "#62A8D6";
                    break;
                case "MQL5":
                    hexColor = "#4A76B8";
                    break;
                case "MTML":
                    hexColor = "#b7e1f4";
                    break;
                case "mupad":
                    hexColor = "#244963";
                    break;
                case "Mustache":
                    hexColor = "#724b3b";
                    break;
                case "nanorc":
                    hexColor = "#2d004d";
                    break;
                case "NCL":
                    hexColor = "#28431f";
                    break;
                case "Nearley":
                    hexColor = "#990000";
                    break;
                case "Nemerle":
                    hexColor = "#3d3c6e";
                    break;
                case "nesC":
                    hexColor = "#94B0C7";
                    break;
                case "NetLinx":
                    hexColor = "#0aa0ff";
                    break;
                case "NetLinx+ERB":
                    hexColor = "#747faa";
                    break;
                case "NetLogo":
                    hexColor = "#ff6375";
                    break;
                case "NewLisp":
                    hexColor = "#87AED7";
                    break;
                case "Nextflow":
                    hexColor = "#3ac486";
                    break;
                case "Nginx":
                    hexColor = "#009639";
                    break;
                case "Nim":
                    hexColor = "#ffc200";
                    break;
                case "Nit":
                    hexColor = "#009917";
                    break;
                case "Nix":
                    hexColor = "#7e7eff";
                    break;
                case "NPM Config":
                    hexColor = "#cb3837";
                    break;
                case "Nu":
                    hexColor = "#c9df40";
                    break;
                case "NumPy":
                    hexColor = "#9C8AF9";
                    break;
                case "Nunjucks":
                    hexColor = "#3d8137";
                    break;
                case "NWScript":
                    hexColor = "#111522";
                    break;
                case "Objective-C":
                    hexColor = "#438eff";
                    break;
                case "Objective-C++":
                    hexColor = "#6866fb";
                    break;
                case "Objective-J":
                    hexColor = "#ff0c5a";
                    break;
                case "ObjectScript":
                    hexColor = "#424893";
                    break;
                case "OCaml":
                    hexColor = "#3be133";
                    break;
                case "Odin":
                    hexColor = "#60AFFE";
                    break;
                case "Omgrofl":
                    hexColor = "#cabbff";
                    break;
                case "ooc":
                    hexColor = "#b0b77e";
                    break;
                case "Opal":
                    hexColor = "#f7ede0";
                    break;
                case "Open Policy Agent":
                    hexColor = "#7d9199";
                    break;
                case "OpenCL":
                    hexColor = "#ed2e2d";
                    break;
                case "OpenEdge ABL":
                    hexColor = "#5ce600";
                    break;
                case "OpenQASM":
                    hexColor = "#AA70FF";
                    break;
                case "OpenSCAD":
                    hexColor = "#e5cd45";
                    break;
                case "Org":
                    hexColor = "#77aa99";
                    break;
                case "Oxygene":
                    hexColor = "#cdd0e3";
                    break;
                case "Oz":
                    hexColor = "#fab738";
                    break;
                case "P4":
                    hexColor = "#7055b5";
                    break;
                case "Pan":
                    hexColor = "#cc0000";
                    break;
                case "Papyrus":
                    hexColor = "#6600cc";
                    break;
                case "Parrot":
                    hexColor = "#f3ca0a";
                    break;
                case "Pascal":
                    hexColor = "#E3F171";
                    break;
                case "Pawn":
                    hexColor = "#dbb284";
                    break;
                case "PEG.js":
                    hexColor = "#234d6b";
                    break;
                case "Pep8":
                    hexColor = "#C76F5B";
                    break;
                case "Perl":
                    hexColor = "#0298c3";
                    break;
                case "PHP":
                    hexColor = "#4F5D95";
                    break;
                case "PicoLisp":
                    hexColor = "#6067af";
                    break;
                case "PigLatin":
                    hexColor = "#fcd7de";
                    break;
                case "Pike":
                    hexColor = "#005390";
                    break;
                case "PLpgSQL":
                    hexColor = "#336790";
                    break;
                case "PLSQL":
                    hexColor = "#dad8d8";
                    break;
                case "PogoScript":
                    hexColor = "#d80074";
                    break;
                case "PostCSS":
                    hexColor = "#dc3a0c";
                    break;
                case "PostScript":
                    hexColor = "#da291c";
                    break;
                case "POV-Ray SDL":
                    hexColor = "#6bac65";
                    break;
                case "PowerBuilder":
                    hexColor = "#8f0f8d";
                    break;
                case "PowerShell":
                    hexColor = "#012456";
                    break;
                case "Prisma":
                    hexColor = "#0c344b";
                    break;
                case "Processing":
                    hexColor = "#0096D8";
                    break;
                case "Prolog":
                    hexColor = "#74283c";
                    break;
                case "Promela":
                    hexColor = "#de0000";
                    break;
                case "Propeller Spin":
                    hexColor = "#7fa2a7";
                    break;
                case "Pug":
                    hexColor = "#a86454";
                    break;
                case "Puppet":
                    hexColor = "#302B6D";
                    break;
                case "PureBasic":
                    hexColor = "#5a6986";
                    break;
                case "PureScript":
                    hexColor = "#1D222D";
                    break;
                case "Python":
                    hexColor = "#3572A5";
                    break;
                case "Python console":
                    hexColor = "#3572A5";
                    break;
                case "Python traceback":
                    hexColor = "#3572A5";
                    break;
                case "q":
                    hexColor = "#0040cd";
                    break;
                case "Q#":
                    hexColor = "#fed659";
                    break;
                case "QML":
                    hexColor = "#44a51c";
                    break;
                case "Qt Script":
                    hexColor = "#00b841";
                    break;
                case "Quake":
                    hexColor = "#882233";
                    break;
                case "R":
                    hexColor = "#198CE7";
                    break;
                case "Racket":
                    hexColor = "#3c5caa";
                    break;
                case "Ragel":
                    hexColor = "#9d5200";
                    break;
                case "Raku":
                    hexColor = "#0000fb";
                    break;
                case "RAML":
                    hexColor = "#77d9fb";
                    break;
                case "Rascal":
                    hexColor = "#fffaa0";
                    break;
                case "RDoc":
                    hexColor = "#701516";
                    break;
                case "Reason":
                    hexColor = "#ff5847";
                    break;
                case "Rebol":
                    hexColor = "#358a5b";
                    break;
                case "Record Jar":
                    hexColor = "#0673ba";
                    break;
                case "Red":
                    hexColor = "#f50000";
                    break;
                case "Regular Expression":
                    hexColor = "#009a00";
                    break;
                case "Ren'Py":
                    hexColor = "#ff7f7f";
                    break;
                case "ReScript":
                    hexColor = "#ed5051";
                    break;
                case "reStructuredText":
                    hexColor = "#141414";
                    break;
                case "REXX":
                    hexColor = "#d90e09";
                    break;
                case "Ring":
                    hexColor = "#2D54CB";
                    break;
                case "Riot":
                    hexColor = "#A71E49";
                    break;
                case "RMarkdown":
                    hexColor = "#198ce7";
                    break;
                case "RobotFramework":
                    hexColor = "#00c0b5";
                    break;
                case "Roff":
                    hexColor = "#ecdebe";
                    break;
                case "Roff Manpage":
                    hexColor = "#ecdebe";
                    break;
                case "Rouge":
                    hexColor = "#cc0088";
                    break;
                case "Ruby":
                    hexColor = "#701516";
                    break;
                case "RUNOFF":
                    hexColor = "#665a4e";
                    break;
                case "Rust":
                    hexColor = "#dea584";
                    break;
                case "SaltStack":
                    hexColor = "#646464";
                    break;
                case "SAS":
                    hexColor = "#B34936";
                    break;
                case "Sass":
                    hexColor = "#a53b70";
                    break;
                case "Scala":
                    hexColor = "#c22d40";
                    break;
                case "Scaml":
                    hexColor = "#bd181a";
                    break;
                case "Scheme":
                    hexColor = "#1e4aec";
                    break;
                case "Scilab":
                    hexColor = "#ca0f21";
                    break;
                case "SCSS":
                    hexColor = "#c6538c";
                    break;
                case "sed":
                    hexColor = "#64b970";
                    break;
                case "Self":
                    hexColor = "#0579aa";
                    break;
                case "ShaderLab":
                    hexColor = "#222c37";
                    break;
                case "Shell":
                    hexColor = "#89e051";
                    break;
                case "ShellCheck Config":
                    hexColor = "#cecfcb";
                    break;
                case "Shen":
                    hexColor = "#120F14";
                    break;
                case "Singularity":
                    hexColor = "#64E6AD";
                    break;
                case "Slash":
                    hexColor = "#007eff";
                    break;
                case "Slice":
                    hexColor = "#003fa2";
                    break;
                case "Slim":
                    hexColor = "#2b2b2b";
                    break;
                case "Smalltalk":
                    hexColor = "#596706";
                    break;
                case "Smarty":
                    hexColor = "#f0c040";
                    break;
                case "SmPL":
                    hexColor = "#c94949";
                    break;
                case "Solidity":
                    hexColor = "#AA6746";
                    break;
                case "SourcePawn":
                    hexColor = "#f69e1d";
                    break;
                case "SPARQL":
                    hexColor = "#0C4597";
                    break;
                case "SQF":
                    hexColor = "#3F3F3F";
                    break;
                case "SQL":
                    hexColor = "#e38c00";
                    break;
                case "SQLPL":
                    hexColor = "#e38c00";
                    break;
                case "Squirrel":
                    hexColor = "#800000";
                    break;
                case "SRecode Template":
                    hexColor = "#348a34";
                    break;
                case "Stan":
                    hexColor = "#b2011d";
                    break;
                case "Standard ML":
                    hexColor = "#dc566d";
                    break;
                case "Starlark":
                    hexColor = "#76d275";
                    break;
                case "Stata":
                    hexColor = "#1a5f91";
                    break;
                case "StringTemplate":
                    hexColor = "#3fb34f";
                    break;
                case "Stylus":
                    hexColor = "#ff6347";
                    break;
                case "SubRip Text":
                    hexColor = "#9e0101";
                    break;
                case "SugarSS":
                    hexColor = "#2fcc9f";
                    break;
                case "SuperCollider":
                    hexColor = "#46390b";
                    break;
                case "Svelte":
                    hexColor = "#ff3e00";
                    break;
                case "SVG":
                    hexColor = "#ff9900";
                    break;
                case "Swift":
                    hexColor = "#F05138";
                    break;
                case "SystemVerilog":
                    hexColor = "#DAE1C2";
                    break;
                case "Tcl":
                    hexColor = "#e4cc98";
                    break;
                case "Terra":
                    hexColor = "#00004c";
                    break;
                case "TeX":
                    hexColor = "#3D6117";
                    break;
                case "Textile":
                    hexColor = "#ffe7ac";
                    break;
                case "TextMate Properties":
                    hexColor = "#df66e4";
                    break;
                case "Thrift":
                    hexColor = "#D12127";
                    break;
                case "TI Program":
                    hexColor = "#A0AA87";
                    break;
                case "TLA":
                    hexColor = "#4b0079";
                    break;
                case "TOML":
                    hexColor = "#9c4221";
                    break;
                case "TSQL":
                    hexColor = "#e38c00";
                    break;
                case "TSV":
                    hexColor = "#237346";
                    break;
                case "TSX":
                    hexColor = "#2b7489";
                    break;
                case "Turing":
                    hexColor = "#cf142b";
                    break;
                case "Twig":
                    hexColor = "#c1d026";
                    break;
                case "TXL":
                    hexColor = "#0178b8";
                    break;
                case "TypeScript":
                    hexColor = "#2b7489";
                    break;
                case "Unified Parallel C":
                    hexColor = "#4e3617";
                    break;
                case "Unity3D Asset":
                    hexColor = "#222c37";
                    break;
                case "Uno":
                    hexColor = "#9933cc";
                    break;
                case "UnrealScript":
                    hexColor = "#a54c4d";
                    break;
                case "UrWeb":
                    hexColor = "#ccccee";
                    break;
                case "V":
                    hexColor = "#4f87c4";
                    break;
                case "Vala":
                    hexColor = "#fbe5cd";
                    break;
                case "Valve Data Format":
                    hexColor = "#f26025";
                    break;
                case "VBA":
                    hexColor = "#867db1";
                    break;
                case "VBScript":
                    hexColor = "#15dcdc";
                    break;
                case "VCL":
                    hexColor = "#148AA8";
                    break;
                case "Verilog":
                    hexColor = "#b2b7f8";
                    break;
                case "VHDL":
                    hexColor = "#adb2cb";
                    break;
                case "Vim Help File":
                    hexColor = "#199f4b";
                    break;
                case "Vim Script":
                    hexColor = "#199f4b";
                    break;
                case "Vim Snippet":
                    hexColor = "#199f4b";
                    break;
                case "Visual Basic .NET":
                    hexColor = "#945db7";
                    break;
                case "Volt":
                    hexColor = "#1F1F1F";
                    break;
                case "Vue":
                    hexColor = "#41b883";
                    break;
                case "wdl":
                    hexColor = "#42f1f4";
                    break;
                case "Web Ontology Language":
                    hexColor = "#5b70bd";
                    break;
                case "WebAssembly":
                    hexColor = "#04133b";
                    break;
                case "Wikitext":
                    hexColor = "#fc5757";
                    break;
                case "Windows Registry Entries":
                    hexColor = "#52d5ff";
                    break;
                case "wisp":
                    hexColor = "#7582D1";
                    break;
                case "Wollok":
                    hexColor = "#a23738";
                    break;
                case "World of Warcraft Addon Data":
                    hexColor = "#f7e43f";
                    break;
                case "X10":
                    hexColor = "#4B6BEF";
                    break;
                case "xBase":
                    hexColor = "#403a40";
                    break;
                case "XC":
                    hexColor = "#99DA07";
                    break;
                case "XML":
                    hexColor = "#0060ac";
                    break;
                case "XML Property List":
                    hexColor = "#0060ac";
                    break;
                case "Xojo":
                    hexColor = "#81bd41";
                    break;
                case "Xonsh":
                    hexColor = "#285EEF";
                    break;
                case "XQuery":
                    hexColor = "#5232e7";
                    break;
                case "XSLT":
                    hexColor = "#EB8CEB";
                    break;
                case "Xtend":
                    hexColor = "#24255d";
                    break;
                case "Yacc":
                    hexColor = "#4B6C4B";
                    break;
                case "YAML":
                    hexColor = "#cb171e";
                    break;
                case "YARA":
                    hexColor = "#220000";
                    break;
                case "YASnippet":
                    hexColor = "#32AB90";
                    break;
                case "ZAP":
                    hexColor = "#0d665e";
                    break;
                case "ZenScript":
                    hexColor = "#00BCD1";
                    break;
                case "Zephir":
                    hexColor = "#118F9E";
                    break;
                case "Zig":
                    hexColor = "#EC915C";
                    break;
                case "ZIL":
                    hexColor = "#DC75E5";
                    break;
                case "Zimpl":
                    hexColor = "#D67711";
                    break;
                default:
                    return null;
            }

            return new SolidColorBrush(Color.FromArgb(
                0xFF,
                Convert.ToByte(hexColor.Substring(1, 2), 16),
                Convert.ToByte(hexColor.Substring(3, 2), 16),
                Convert.ToByte(hexColor.Substring(5, 2), 16)));
        }
    }
}
