﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lexical_Analyzer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] Token = new string[10000];
            string[] Tokentype = new string[10000];//A string used to temporarily store a token.
            int[] Row = new int[10000];
            int[] Column = new int[10000];
            int i = 0;
            int J, j = 0, n, a = 1, g, m = 0, k = 1, G = 0;
            char[,] S1 = new char[10000, 10000]; //Baraye save code file 
            string S2; // save token movagat
            string[] CodeFile = System.IO.File.ReadAllLines("F:\\file2.txt"); //Daraye code haye file
            char[,] S3 = new char[10000, 10000]; //Baraye bade faze 1 token sazi
            char[,] S4 = new char[10000, 10000]; //Baraye bade faze 2 token sazi
            int[] Count = new int[10000]; //Tedad char baade token saszi


            // In summary, this first for loop is responsible for converting each line of code into a 2D array (S1). 
          
            for (i = 0; i < CodeFile.Length; i++)
            {
                string CodeLines = CodeFile[i];
                for (j = 1; j <= CodeLines.Length; j++)
                {
                    S1[i, j] = Convert.ToChar(CodeLines[j - 1]);
                }
                S1[i, j] = ' ';
            }

            //In summary, the second for loop processes each character in a line of code, checks certain conditions, and forms a modified line in the array S3. 
            //The count of characters in each modified line is stored in the array Count. This loop is essentially creating a new representation of the code, where specific characters are separated by spaces for further analysis.

            for (i = 0; i < CodeFile.Length; i++)
            {
                J = 1; //  keep track of the position S3
                for (j = 1; j <= CodeFile[i].Length; j++)
                {
                    if ((S1[i, j + 1] == ';' || S1[i, j + 1] == '(' || S1[i, j + 1] == ')' || S1[i, j + 1] == ',' || S1[i, j + 1] == '=' || S1[i, j + 1] == '[' || S1[i, j + 1] == ']'
                        || S1[i, j + 1] == '{' || S1[i, j + 1] == '}' || S1[i, j + 1] == '*' || S1[i, j + 1] == '/' || S1[i, j + 1] == '<' || S1[i, j + 1] == '+'
                       || S1[i, j + 1] == '>' || S1[i, j + 1] == '-' || S1[i, j + 1] == '|' || S1[i, j + 1] == '&' || S1[i, j + 1] == '!') && (S1[i, j] != ' ') && (S1[i, j + 1] != S1[i, j]))

                    {
                        S3[i, J] = S1[i, j];
                        S3[i, J + 1] = ' ';
                        J = J + 2;
                    }
                    else
                    {
                        S3[i, J] = S1[i, j];
                        J++;
                    }

                }
                Count[i] = J; //count of characters 
            }


            for (i = 0; i < CodeFile.Length; i++)
            {
                J = 1;
                for (j = 1; j <= Count[i]; j++)
                {
                    if ((S3[i, j - 1] == ';' || S3[i, j - 1] == '(' || S3[i, j - 1] == ')' || S3[i, j - 1] == ',' || S3[i, j - 1] == '=' || S3[i, j - 1] == '[' || S3[i, j - 1] == ']'
                     || S3[i, j - 1] == '{' || S3[i, j - 1] == '}' || S3[i, j - 1] == '*' || S3[i, j - 1] == '/' || S3[i, j - 1] == '<' || S3[i, j - 1] == '+'
                     || S3[i, j - 1] == '>' || S3[i, j - 1] == '-' || S3[i, j - 1] == '|' || S3[i, j - 1] == '&' || S3[i, j - 1] == '!') && (S3[i, j] != ' ') && (S3[i, j - 1] != S3[i, j]))
                    {

                        S4[i, J + 1] = S3[i, j];
                        S4[i, J] = ' ';
                        J = J + 2;
                    }
                    else
                    {
                        S4[i, J] = S3[i, j];
                        J++;
                    }
                }
                Count[i] = J;
            }

            //This loop is essentially adding a space at the end of each line in the S4 array. 
            for (i = 0; i < CodeFile.Length; i++)
            {
                S4[i, Count[i]] = ' ';
            }

            for (i = 0; i < CodeFile.Length; i++)
            {
                g = 1;  // keep track of the position  in S4.

                for (j = 1; j <= Count[i]; j++)
                {
                    if (S4[i, j] == ' ')
                    {
                        S2 = "";
                        for (n = g; n < j; n++)
                        {
                            if (S4[i, n] != ' ')
                            {
                                S2 = S2 + Convert.ToString(S4[i, n]);
                            }
                        }
                        G = g;
                        g = j + 1;

                        if (S2 != "")
                        {
                            Token[a] = S2;
                            Tokentype[a] = "";
                            Row[a] = i + 1;

                            switch (G)
                            {
                                case 1:
                                    for (int z = G - 1; z < G + 2; z++)
                                    {
                                        if (Column[a] == 0 && S2[0] == S1[i, z])
                                        {
                                            k = z;
                                            Column[a] = k;
                                        }
                                    }
                                    break;

                                default:
                                    if (G == 2)
                                    {
                                        G = G + 1;
                                    }

                                    for (int z = G - 3; z <= G + 2; z++)
                                    {
                                        if (Column[a] == 0 && S2[0] == S1[i, z])
                                        {
                                            k = z;
                                            Column[a] = k;
                                        }
                                    }
                                    break;
                            }

                            a++;
                        }
                    }
                }
                m = a;
            }



            for (j = 1; j < m; j++)
            {
                switch (Token[j])
                {
                    // Keywords
                    case "alignas":
                    case "alignof":
                    case "and":
                    case "and_eq":
                    case "asm":
                    case "auto":
                    case "bitand":
                    case "bitor":
                    case "bool":
                    case "break":
                    case "case":
                    case "catch":
                    case "char":
                    case "char16_t":
                    case "char32_t":
                    case "class":
                    case "compl":
                    case "const":
                    case "constexpr":
                    case "const_cast":
                    case "continue":
                    case "decltype":
                    case "default":
                    case "delete":
                    case "do":
                    case "double":
                    case "dynamic_cast":
                    case "else":
                    case "enum":
                    case "export":
                    case "extern":
                    case "false":
                    case "float":
                    case "for":
                    case "friend":
                    case "goto":
                    case "if":
                    case "inline":
                    case "int":
                    case "long":
                    case "mutable":
                    case "namespace":
                    case "new":
                    case "noexcept":
                    case "not":
                    case "not_eq":
                    case "nullptr":
                    case "operator":
                    case "or":
                    case "or_eq":
                    case "private":
                    case "public":
                    case "register":
                    case "reinterpret_cast":
                    case "return":
                    case "short":
                    case "signed":
                    case "sizeof":
                    case "static":
                    case "static_assert":
                    case "static_cast":
                    case "struct":
                    case "switch":
                    case "template":
                    case "this":
                    case "thread_local":
                    case "throw":
                    case "true":
                    case "try":
                    case "typedef":
                    case "typeid":
                    case "typename":
                    case "union":
                    case "unsigned":
                    case "using":
                    case "virtual":
                    case "void":
                    case "volatile":
                    case "wchar_t":
                    case "while":
                    case "cout":
                    case "cin":
                    case "main":
                    case "#include":
                    case "iostream.h":
                    case "conio.h":

                        Tokentype[j] = "Keyword";
                        break;

                    // Operators
                    case "+":
                    case "-":
                    case "/":
                    case "*":
                    case "%":
                    case ">":
                    case "<":
                    case "=":
                    case "!":
                    case "|":
                    case "&":
                    case "^":
                    case "~":
                    case "(":
                    case ")":
                    case "[":
                    case "]":
                    case "{":
                    case "}":
                    case "<<":
                    case ">>":
                    case "++":
                    case "--":
                    case "==":
                    case "&&":
                    case "||":
                        Tokentype[j] = "Operator";
                        break;

                    // Delimiters
                    case ",":
                    case ":":
                    case ";":
                        Tokentype[j] = "Delimiter";
                        break;

                    // Literals
                    case var literal when literal.StartsWith("\"") || literal.StartsWith("'"):
                        Tokentype[j] = "Literal";
                        break;

                    // Numbers
                    // 48 to 57 represent 0-9 in ASCII 
                    case var number when 48 <= number[0] && number[0] <= 57:
                        Tokentype[j] = "Number";
                        break;

                    // Identifiers
                    // 65-90 represent ASCII value of A-Z and 97-122 is a-z
                    case var identifier when Tokentype[j] == "":
                        if ((65 <= identifier[0] && identifier[0] <= 90) || (97 <= identifier[0] && identifier[0] <= 122))
                        {
                            Tokentype[j] = "Identifier";
                        }
                        break;
                }
            }



            int Block_level = 0, Starting_block_level = 0, y = 0;
            // int y :  keep track of the number of nested blocks.
            int[] blockno = new int[10000];


            for (j = 1; j < m; j++)
            {
                switch (Token[j])
                {
                    case "{":
                        Block_level++;
                        y++;
                        blockno[j] = Block_level;
                        Starting_block_level = Block_level;
                        break;

                    case "}":
                        blockno[j] = Block_level;
                        Block_level--;
                        y--;
                        if (y == 0)
                        {
                            Block_level = Starting_block_level;
                        }
                        break;

                    default:
                        blockno[j] = Block_level;
                        break;
                }
            }




            for (j = 1; j < m; j++)
            {
                if (Tokentype[j] != "")
                {
                    dataGridView1.Rows.Add(Token[j], Tokentype[j], Row[j], Column[j], blockno[j]);
                }
                dataGridView1.ScrollBars = ScrollBars.Both;
            }
        }
    }
}

