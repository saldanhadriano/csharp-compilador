using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Compilador
{
    public partial class Form1 : Form
    {
        // VARIAVEIS GLOBAIS
        int count = 0;
        int countLabel = 1;
        int countTemp = 0;
        List<Symbol> listaSymbolos = new List<Symbol>();
        List<string> listaC3E = new List<string>();

        //[0,n] id  [1,n] tipo  [2,n] comprimento  [3,n] endereco   

        //preencher lista lexica
        private void PreencherListaLexica(string entrada, string[,] listaLexica)
        {
            int i = 0;

            //como §, ¨ , e ´  sao utilizados pelo sistema, se faz necessario o teste para que eles nao sejam lidos
            if (TestaCharRestrito(entrada) == true)
            {
                ErroCharRestrito();
            }
            else
            {
                // "\" é reservada pelo Regex, portanto para poder identificar  '\n' '\r' '\t' 
                //foi necessario trocalas por outras variaveis, definidas por nós mesmos,'§' , '´' , '¨' 
                entrada = entrada.Replace("\\n", "§");
                entrada = entrada.Replace("\\t", "´");
                entrada = entrada.Replace("\\r", "¨");

                //identificando e tornando a tecla enter lida no textbox um delimitador
                entrada = entrada.Replace("\n", " enter ");
                entrada = entrada.Replace("\r", "");


                //montando lista lexica com regex-----------
                Regex busca = new Regex(" enter |[a-z]+[0-9]+|[a-z]+|,|==|>=|<=|<>|[0-9]+.[0-9]+|[0-9]+|[ ]|[§]|[´]|[¨]|[[a-z]+[0-9]+]|[[0-9]+]|[A-Za-z]+|[()]|[=]|[<>]|[;]|[^]|[++]|[+]|[-]|[*]|[/]");
                var match = busca.Match(entrada);
                while (match.Success)
                {
                    if (entrada.Substring(match.Index, match.Length) != " " && entrada.Substring(match.Index, match.Length) != " enter ")
                    {
                        //id  tipo  comprimento  endereco 
                        listaLexica[i, 0] = entrada.Substring(match.Index, match.Length);
                        listaLexica[i, 1] = match.Index.ToString();
                        listaLexica[i, 2] = match.Length.ToString();
                        listaLexica[i, 3] = null;
                        i++;
                    }

                    match = match.NextMatch();

                }
                listaLexica[i, 0] = null;
                //--------------------------------------------

                //analisa a lista para identificar caracteristicas de cada tok 
                listaLexica = analisaToken(listaLexica);

            }
        }

        private string[,] analisaToken(string[,] lista)
        {
            int i = 0;
            var match = new Regex("enter|[,]|[§]|[´]|[¨]|[ ]|[;]|[{}]").Match(lista[i, 0]);

            do
            {
                match = new Regex("[(]").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "PAR_ESQ";
                }
                match = new Regex("[)]").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "PAR_DIR";
                }
                match = new Regex("[+]").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "OP_ADD";
                }
                match = new Regex("[-]").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "OP_SUB";
                }
                match = new Regex("[/]").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "OP_DIV";
                }
                match = new Regex("[*]").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "OP_MULT";
                }
                match = new Regex("['^']").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "OP_UNARIO";
                }
                match = new Regex("[0-9]+").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "NUM";
                }
                match = new Regex("[0-9]+.[0-9]+").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "NUM";
                }
                match = new Regex("-[0-9]+").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "NUM";
                }
                match = new Regex("-[0-9]+.[0-9]+").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "NUM";
                }
                match = new Regex("[A-Za-z]+").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "IDENTIFICADOR";
                }
                match = new Regex("[=]").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "ATRIBUIÇÃO";
                }
                match = new Regex("(==)").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "IGUAL";
                }
                match = new Regex("[>]").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "OP_MAIOR";
                }
                match = new Regex("(>=)").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "MAIOR_IGUAL";
                }
                match = new Regex("[<]").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "OP_MENOR";
                }
                match = new Regex("(<=)").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "MENOR_IGUAL";
                }
                match = new Regex("enter|[,]|[§]|[´]|[¨]|[ ]|[;]|[{}]").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "DELIMITADOR";
                }
                match = new Regex("[[0-9]+]").Match(lista[i, 0]);
                if (match.Success)
                {
                    lista[i, 3] = "[VETOR]";
                }

                //// Replace da lista
                if (lista[i, 0].Equals("<>"))
                {
                    lista[i, 3] = "DIFERENTE";
                }
                if (lista[i, 0].Equals("var"))
                {
                    lista[i, 3] = "VAR";
                }
                if (lista[i, 0].Equals("int"))
                {
                    lista[i, 3] = "INTEGER";
                }
                if (lista[i, 0].Equals("real"))
                {
                    lista[i, 3] = "FLOAT";
                }
                if(lista[i, 0].Equals("while"))
                {
                    lista[i, 3] = "WHILE";
                }
                if (lista[i, 0].Equals("if"))
                {
                    lista[i, 3] = "IF";
                }

                //substituindo os caracteres reservados pelo caracter original
                lista[i, 0] = lista[i, 0].Replace("§", "\\n");
                lista[i, 0] = lista[i, 0].Replace("´", "\\t");
                lista[i, 0] = lista[i, 0].Replace("¨", "\\r");

                i++;
            } while (lista[i, 0] != null);
            return lista;
        }

        //analise sintatica-------------------------------------------------------
        private bool AnaliseSintatica(string[,] listaLexica)
        {
            return PROG(listaLexica);
        }

        //gramatica---------------------------------------------------------------
        //PROG
        private bool PROG(string[,] listaLexica)
        {
            int aux = 0;
            int aux2 = 0;
            if (listaLexica[count, 0].Equals("var"))
            {
                count++;
                do
                {
                    if (listaLexica[count, 0] != null && (listaLexica[count, 0].Equals("real") || listaLexica[count, 0].Equals("int")))
                    {
                        aux2 = 1;
                        if (B_ID(listaLexica) == true)
                        {
                            count++;
                            aux = 0;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        aux = 1;

                    }
                } while (aux != 1);
                
                if (aux2 == 1 && SEQ_L(listaLexica) == true)
                {
                    return true;
                }
                else
                {

                    if (SEQ_L(listaLexica) == true)
                    {
                        return true;
                    }
                    else
                    {
                        ErroMessage("Esperando while, if ou atribuição");
                        return true;
                    }
                }
            }
            else
            {
                ErroSintatico("Aceita apenas var.");
                return false;
            }
        }

        //B_ID
        private bool B_ID(string[,] listaLexica)
        {
            String[] B_ID_Tipo = TIPO(listaLexica);

            if (B_ID_Tipo[0].Equals("true"))
            {
                VAR_S(listaLexica, B_ID_Tipo[1]);
                return true;
            }
            else
            {
                return false;
            }
        }

        //VAR_S
        private void VAR_S(string[,] listaLexica, String Tipo)
        {
            do
            {
                Symbol symbol = new Symbol();
                if (ID_S(listaLexica, Tipo, symbol) == true)
                {
                    if (listaSymbolos.Count.Equals(0))
                    {
                        //Adição na lista de simbolos
                        listaSymbolos.Add(symbol);
                    }
                    else
                    {
                        if (TesteVariavelExiste(listaSymbolos, symbol) != true)
                        {
                            //Adição na lista de simbolos
                            listaSymbolos.Add(symbol);
                        }
                        else
                        {

                            ErroMessage("Erro: Variável " + symbol.Local + " já existe.");
                        }
                    }
                }
                count++;
            } while (!listaLexica[count, 0].Equals(";"));
        }

        //ID_S
        private bool ID_S(string[,] listaLexica, string tipo, Symbol s)
        {
            if (listaLexica[count, 3].Equals("IDENTIFICADOR") && !listaLexica[count + 1, 3].Equals("[VETOR]"))
            {
                s.Local = listaLexica[count, 0];
                s.Tipo = tipo;

                return true;
            }
            else
            {
                if (listaLexica[count, 3].Equals("IDENTIFICADOR") && listaLexica[count + 1, 3].Equals("[VETOR]"))
                {
                    String[] idVetor = new String[3];
                    if (IDvetor(listaLexica, idVetor, s) == true)
                    {
                        s.Tipo = tipo;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        //IDvetor
        private bool IDvetor(string[,] listaLexica, string[] idVetor, Symbol s)
        {
            idVetor = VAL_N(listaLexica);
            if (idVetor[0].Equals("1"))
            {
                s.Local = idVetor[2];
                s.Indice = idVetor[3];

                return true;
            }
            else
            {
                return false;
            }
        }

        //TIPO
        private String[] TIPO(string[,] listaLexica)
        {
            String[] retorno = new String[2];
            if (listaLexica[count, 0] == "real" || listaLexica[count, 0] == "int")
            {
                retorno[0] = "true";
                retorno[1] = listaLexica[count, 0];
                count++;
                return retorno;
            }
            else
            {
                ErroSintatico("int ou real");
                retorno[0] = "false";
                return retorno;
            }
        }

        //SEQ_L
        private bool SEQ_L(string[,] listaLexica)
        {
            if(listaLexica[count,0] != null && (listaLexica[count, 3].Equals("WHILE") || 
                                                listaLexica[count, 3].Equals("IF") || 
                                                listaLexica[count, 3].Equals("IDENTIFICADOR")))
            {
                if (SEQ(listaLexica) == true)
                {
                    SEQ_L(listaLexica);
                    return true;
                }
                else
                {
                    SEQ(listaLexica);
                    return true;
                }    
            }
            else
            {
                return false;
            }
        }

        //SEQ
        private bool SEQ(string[,] listaLexica)
        {
            if (listaLexica[count,3].Equals("WHILE"))
            {
                count++;                                
                S_WHILE(listaLexica);
                return true;
            }
            else
            {
                if (listaLexica[count, 3].Equals("IF"))
                {
                    count++;
                    S_IF(listaLexica);
                    return true;
                }
                else
                {
                    if (listaLexica[count,3].Equals("IDENTIFICADOR"))
                    {
                        S_ATR(listaLexica);
                        return true;
                    }
                    else
                    {

                        return false;
                    }
                }
            }
        }

        //S_WHILE
        private bool S_WHILE(string[,] listaLexica)
        {
            string begin = CriaLabel();
            string whileTrue = CriaLabel();

            listaC3E.Add(begin + ":");

            if (listaLexica[count, 0].Equals("("))
            {
                count++;
                string c3e = EXP_I(listaLexica);
                listaC3E.Add(c3e + whileTrue);
                if (c3e != "false")
                {                    
                    if (listaLexica[count, 0].Equals(")"))
                    {
                        count++;
                        if (listaLexica[count, 0].Equals("{"))
                        {
                            count++;
                            if (SEQ_L(listaLexica) == true)
                            {
                                if (listaLexica[count, 0].Equals("}"))
                                {
                                    listaC3E.Add("GOTO " + begin);
                                    listaC3E.Add(whileTrue+":");
                                    count++;                                    
                                    return true;
                                }
                                else
                                {
                                    ErroSintatico("um '}'");
                                    return false;
                                }
                            }
                            else
                            {
                                ErroSintatico("uma SEQ_L");
                                return false;
                            }
                        }
                        else
                        {
                            ErroSintatico("um '{'");
                            return false;
                        }
                    }
                    else
                    {
                        ErroSintatico("um ')'");
                        return false;
                    }
                }
                else
                {
                    ErroSintatico("uma EXP_I");
                    return false;
                }
            }
            else
            {
                ErroSintatico("um '('");
                return false;
            }
        }

        //S_IF
        private bool S_IF(string[,] listaLexica)
        {
            string l0 = CriaLabel();
            string l1 = CriaLabel();
            string end = CriaLabel();

            if (listaLexica[count, 0].Equals("("))
            {
                count++;
                string c3e = EXP_I(listaLexica);
                listaC3E.Add(c3e + l0);

                if(c3e != "false")
                {                    
                    if (listaLexica[count, 0].Equals(")"))
                    {
                        count++;
                        listaC3E.Add("GOTO " + l1);
                        if(listaLexica[count, 0].Equals("{"))
                        {
                            listaC3E.Add(l0 + ":");
                            count++;
                            if(SEQ_L(listaLexica) == true)
                            {                                
                                if(listaLexica[count, 0].Equals("}"))
                                {
                                    listaC3E.Add("GOTO " + end);
                                    listaC3E.Add(l1 + ":");
                                    count++;
                                    if (S_ELSE(listaLexica) == true)
                                    {
                                        listaC3E.Add(end + ":");
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    ErroSintatico("}");
                                    return false;
                                }
                            }
                            else
                            {
                                ErroSintatico("SEQ_L");
                                return false;
                            }
                        }
                        else
                        {
                            ErroSintatico("{");
                            return false;
                        }
                    }
                    else
                    {
                        ErroSintatico(")");
                        return false;
                    }
                }
                else
                {
                    ErroSintatico("EXP_C");
                    return false;
                }
            }
            else
            {
                ErroSintatico("(");
                return false;
            }
        }

        //S_ELSE
        private bool S_ELSE(string[,] listaLexica)
        {
            if (listaLexica[count, 0].Equals("else"))
            {
                count++;
                if (listaLexica[count, 0].Equals("{"))
                {
                    count++;
                    if (SEQ_L(listaLexica) == true)
                    {
                        if (listaLexica[count, 0].Equals("}"))
                        {
                            count++;
                            return true;
                        }
                        else
                        {
                            ErroSintatico("}");
                            return false;
                        }
                    }
                    else
                    {
                        ErroSintatico("SEQ_L");
                        return false;
                    }
                }
                else
                {
                    ErroSintatico("}");
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        //S_ATR
        private bool S_ATR(string[,] listaLexica)
        {
            string vard_lexema = VAR_D(listaLexica);
            if (!vard_lexema.Equals("false"))
            {
                if (listaLexica[count, 0].Equals("="))
                {
                    count++;
                    string exp_local = EXP(listaLexica);

                    listaC3E.Add(vard_lexema + " = " + exp_local);

                    if (listaLexica[count, 0].Equals(";"))
                    {
                        count++;
                        return true;
                    }
                    else
                    {
                        ErroSintatico(";");
                        return false;
                    }
                }
                else
                {
                    ErroSintatico("=");
                    return false;
                }
            }
            else
            {
                ErroMessage("Váriavel não existe.");
                return false;
            }
        }

        private string VAR_D(string[,] listaLexica)
        {
            string[] retorno;
            if (listaLexica[count, 3].Equals("IDENTIFICADOR"))
            {
                string lexema = listaLexica[count, 0];
                count++;

                retorno = VAR_L(listaLexica);

                if (retorno[0].Equals("0"))
                {
                    return lexema;
                }
                else
                {
                    if (retorno[0].Equals("1"))
                    {
                        string var_d_indice = retorno[3];
                        string t0 = CriaTemp();
                        string aux_tipo;

                        if (retorno[2].Equals("int"))
                        {
                            aux_tipo = "4";
                        }
                        else
                        {
                            aux_tipo = "8";
                        }

                        listaC3E.Add(t0 + " = " + var_d_indice + " * " + aux_tipo);                       
                        lexema = lexema + "[" + t0 + "]";
                        count++;
                        return lexema;
                    }
                    else
                    {
                        return "false";
                    }
                }
            }
            else
            {
                ErroSintatico("id");
                return "false";
            }
        }

        //VAR_D        
        private string[] VAR_L(string[,] listaLexica)
        {
            string[] retorno = new string[4];

            if (listaLexica[count, 3].Equals("[VETOR]"))
            {
                foreach (Symbol s in listaSymbolos)
                {
                    if (listaLexica[count - 1, 0].Equals(s.Local))
                    {
                        retorno[0] = "1";
                        retorno[1] = s.Local;
                        retorno[2] = s.Tipo;
                        retorno[3] = BuscaNum(listaLexica[count, 0]);
                    }
                }
                return retorno;
            }
            else
            {
                retorno[0] = "0";
                return retorno;
            }
        }

        //EXP_I
        private string EXP_I(string[,] listaLexica)
        {
            string exp_local = EXP(listaLexica);
            
            if (exp_local != "")
            {
                string expC_local = EXP_C(listaLexica,exp_local);
                if (expC_local != "false")
                {
                    return expC_local;
                }
                else
                {
                    return "false";
                }
            }
            else
            {
                return "false";
            }
        }

        private string EXP_C(string[,] listaLexica, string recebido)
        {
            string comparador = listaLexica[count, 0];

            if (listaLexica[count, 3].Equals("IGUAL"))
            {
                if (listaLexica[count - 3, 3].Equals("WHILE"))
                {
                    comparador = "<>";
                }

                count++;
                string exp_local = EXP(listaLexica);
                string retorno = "IF( " + recebido + comparador + exp_local + " ) GOTO ";
                return retorno;
            }
            else
            {
                if (listaLexica[count, 3].Equals("MAIOR_IGUAL"))
                {
                    if (listaLexica[count - 3, 3].Equals("WHILE"))
                    {
                        comparador = "<";
                    }

                    count++;
                    string exp_local = EXP(listaLexica);
                    string retorno = "IF(" + recebido + comparador + exp_local + ") GOTO ";
                    return retorno;
                }
                else
                {
                    if (listaLexica[count, 3].Equals("MENOR_IGUAL"))
                    {
                        if (listaLexica[count - 3, 3].Equals("WHILE"))
                        {
                            comparador = ">";
                        }

                        count++;
                        string exp_local = EXP(listaLexica);
                        string retorno = "IF(" + recebido + comparador + exp_local + ") GOTO ";
                        return retorno;
                    }
                    else
                    {
                        if (listaLexica[count, 3].Equals("OP_MAIOR"))
                        {
                            if (listaLexica[count - 3, 3].Equals("WHILE"))
                            {
                                comparador = "<=";
                            }

                            count++;
                            string exp_local = EXP(listaLexica);
                            string retorno = "IF(" + recebido + comparador + exp_local + ") GOTO ";
                            return retorno;
                        }
                        else
                        {
                            if (listaLexica[count, 3].Equals("OP_MENOR"))
                            {
                                if (listaLexica[count - 3, 3].Equals("WHILE"))
                                {
                                    comparador = ">=";
                                }

                                count++;
                                string exp_local = EXP(listaLexica);
                                string retorno = "IF(" + recebido + comparador + exp_local + ") GOTO ";
                                return retorno;
                            }
                            else
                            {
                                if (listaLexica[count, 3].Equals("DIFERENTE"))
                                {
                                    if (listaLexica[count - 3, 3].Equals("WHILE"))
                                    {
                                        comparador = "==";
                                    }

                                    count++;
                                    string exp_local = EXP(listaLexica);
                                    string retorno = "IF(" + recebido + comparador + exp_local + ") GOTO ";
                                    return retorno;
                                }
                                else
                                {
                                    ErroSintatico(" [==],[>=],[<=],[>],[<],[<>]");
                                    return "false";
                                }
                            }
                        }
                    }
                }
            }
        }       

        //EXP
        private string EXP(string[,] listaLexica)
        {
            //Add C3E
            string exp_local = TERM(listaLexica);
            string aux = exp_local;
            exp_local = EXPL(listaLexica, aux);
            return exp_local;
        }

        //EXPL
        private string EXPL(string[,] listaLexica, string recebido)
        {
            if (listaLexica[count, 3].Equals("OP_ADD") || listaLexica[count, 3].Equals("OP_SUB"))
            {
                if (listaLexica[count, 3].Equals("OP_ADD"))
                {
                    count++;
                    string termLocal = TERM(listaLexica);
                    string expl1 = CriaTemp();
                    listaC3E.Add(expl1 + " = " + recebido + " + " + termLocal);
                    string retorno = EXPL(listaLexica, expl1);
                    return retorno;
                }
                else
                {
                    if (listaLexica[count, 3].Equals("OP_SUB"))
                    {
                        count++;
                        string termLocal = TERM(listaLexica);
                        string expl1 = CriaTemp();
                        listaC3E.Add(expl1 + " = " + recebido + " + " + termLocal);
                        string retorno = EXPL(listaLexica, expl1);
                        return retorno;
                    }
                    else
                    {
                        ErroSintatico(" + ou -");
                        return recebido;
                    }
                }
            }
            else
            {
                return recebido;
            }
        }

        //TERM
        private string TERM(string[,] listaLexica)
        {
            string term_local = FATOR(listaLexica);
            string aux = term_local;
            term_local = TERML(listaLexica, aux);
            return term_local;
        }

        //TERML
        private string TERML(string[,] listaLexica, string recebido)
        {
            if (listaLexica[count,3].Equals("OP_MULT") || listaLexica[count, 3].Equals("OP_DIV"))
            {
                if (listaLexica[count, 3].Equals("OP_MULT"))
                {
                    count++;
                    string fatorLocal = FATOR(listaLexica);
                    string termlLocal = CriaTemp();
                    listaC3E.Add(termlLocal + " = " + recebido + " * " + fatorLocal);
                    string retorno = TERML(listaLexica, termlLocal);
                    return retorno;
                }
                else
                {
                    if (listaLexica[count, 3].Equals("OP_DIV"))
                    {
                        count++;
                        string termLocal = TERM(listaLexica);
                        string expl1 = CriaTemp();
                        listaC3E.Add(expl1 + " = " + recebido + " * " + termLocal);
                        string retorno = EXPL(listaLexica, expl1);
                        return retorno;
                    }
                    else
                    {
                        ErroSintatico(" * ou /");
                        return recebido;
                    }
                }
            }
            else
            {
                return recebido;
            }
        }

        //FATOR
        private string FATOR(string[,] listaLexica)
        {
            string fator_cod = VAL(listaLexica);
            string aux = fator_cod;
            fator_cod = FATORL(listaLexica, aux);
            return fator_cod;
        }

        //FATORL
        private string FATORL(string[,] listaLexica, string val1)
        {
            if (listaLexica[count,3].Equals("OP_UNARIO"))
            {
                count++;
                string val2 = VAL(listaLexica);
                string t0 = CriaTemp();
                listaC3E.Add(t0 + " = " + val1 + " ^ " + val2);
                return t0;
            }
            else
            {
                return val1;
            }
        }

        //VAL
        private string VAL(string[,] listaLexica)
        {
            String val_cod = null;
            if (listaLexica[count, 0] == "(")
            {
                count++;
                val_cod = EXP(listaLexica);

                if (listaLexica[count, 0] == ")")
                {
                    count++;
                    return val_cod;
                }
                else
                {
                    ErroSintatico(")");
                    return "false";
                }

            }
            else
            {
                if (listaLexica[count, 3] == "IDENTIFICADOR")
                {
                    val_cod = VAR_D(listaLexica);
                    
                    return val_cod;
                }
                else
                {
                    if (listaLexica[count, 3] == "NUM")
                    {
                        val_cod = listaLexica[count, 0];
                        count++;
                        return val_cod;
                    }
                    else
                    {
                        ErroSintatico("id ou número");
                        return "false";
                    }
                }
            }
        }

        //VAL_N
        private String[] VAL_N(string[,] listaLexica)
        {
            string IDL_Num = null;
            String[] retorno = new String[4];


            if ((listaLexica[count, 3] == "IDENTIFICADOR") && (listaLexica[count + 1, 3] != "[VETOR]"))
            {
                IDL_Num = BuscaId(listaLexica[count, 0]);
                count++;
                retorno[0] = "1";
                retorno[1] = "true";
                retorno[2] = IDL_Num;
                return retorno;
            }
            else
            {
                if ((listaLexica[count, 3] == "IDENTIFICADOR") && (listaLexica[count + 1, 3] == "[VETOR]"))
                {
                    string IDL_ID = BuscaId(listaLexica[count, 0]);
                    IDL_Num = BuscaNum(listaLexica[count + 1, 0]);
                    count++;
                    retorno[0] = "1";
                    retorno[1] = "true";
                    retorno[2] = IDL_ID;
                    retorno[3] = IDL_Num;
                    return retorno;
                }
                else
                {
                    retorno[0] = "0";
                    retorno[1] = "true";
                    return retorno;
                }
            }            
        }

        //tela--------------------------------------------------------------
        public Form1()
        {
            InitializeComponent();
        }

        private void BtnLimpar_Click(object sender, EventArgs e)
        {
            Limpar();
        }
        
        private void Limpar()
        {
            listaC3E.Clear();
            dgC3e.Rows.Clear();
            dgC3e.Columns.Clear();
            listaSymbolos.Clear();
            dgSimbolos.Rows.Clear();
            dgSimbolos.Columns.Clear();
            textBoxEntrada.Clear();
            dgresult.Rows.Clear();
            dgresult.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            dgC3e.Rows.Clear();
            dgC3e.Columns.Clear();
            count = 0;
            countLabel = 1;
            countTemp = 0;
        }

        private void LimparAnalise()
        {
            listaC3E.Clear();
            dgC3e.Rows.Clear();
            dgC3e.Columns.Clear();
            listaSymbolos.Clear();
            dgSimbolos.Rows.Clear();
            dgSimbolos.Columns.Clear();
            dgresult.Rows.Clear();
            dgresult.Columns.Clear();
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            dgC3e.Rows.Clear();
            dgC3e.Columns.Clear();
            count = 0;
            countLabel = 0;            
            countTemp = 0;
        }

        //mostrar a lista lexica
        private void MostraListaLexica(string[,] listaLexica)
        {
            int i = 0;
            dataGridView1.Columns.Add("Tok", "Tok");
            dataGridView1.Columns.Add("Pos", "Pos");
            dataGridView1.Columns.Add("Tam", "Tam");
            dataGridView1.AutoResizeColumns();
            dataGridView1.Columns.Add("Lex", "Lex");
            

            i = 0;
            string[] linha = new string[4];
            do
            {
                linha[0] = listaLexica[i, 0];
                linha[1] = listaLexica[i, 1];
                linha[2] = listaLexica[i, 2];
                linha[3] = listaLexica[i, 3];
                i++;
                dataGridView1.Rows.Add(linha);

            } while (listaLexica[i, 0] != null);


        }

        //mostrar a lista de simbolos
        private void MostraTabSimbolos()
        {
            // implementar o formato da tabela de simbolos para mostrar na tela
            dgSimbolos.Columns.Add("Local", "Local");
            dgSimbolos.Columns.Add("Tipo", "Tipo");
            dgSimbolos.Columns.Add("Indice", "Indice");

            foreach(Symbol s in listaSymbolos)
            {
                dgSimbolos.Rows.Add(s.Local,s.Tipo,s.Indice);
                
            }
        }

        //mostrar a lista codigo de 3 endereços
        private void MostraCodTresEnderecos()
        {
            // implementar o formato da tabela de codigo de tres endereços para mostrar na tela
            dgC3e.Columns.Add("C3E", "C3E");
            

            foreach (string c3e in listaC3E)
            {
                dgC3e.Rows.Add(c3e);
            }
        }

        //teste de entradas-------------------------------------------------------------
        public bool TesteEntradaNula()
        {
            String entrada = textBoxEntrada.Text;

            if (entrada == "")
            {
                return false;
            }
            else
                return true;
        }

        private bool TestaCharRestrito(string entrada)
        {
            int flag = 0;
            Regex busca = new Regex("[§]|[´]|[¨]");
            var match = busca.Match(entrada);
            while (match.Success)
            {
                flag = 1;
                match = match.NextMatch();
            }
            if (flag == 1)
            {
                return true;
            }
            else
                return false;
        }

        private bool TesteVariavelExiste(List<Symbol>list, Symbol symbol)
        {
            foreach(Symbol s in list)
            {
                if (s.Local.Equals(symbol.Local))
                {
                    return true;
                }
            }
            return false;
        }

        // função que verifica o numero na regex
        private string BuscaNum(string p)
        {
            Regex busca = new Regex("[0-9]+");
            var match = busca.Match(p);
            return p.Substring(match.Index, match.Length);

        }
        
        // função que busca o id na regex
        private string BuscaId(string p)
        {
            Regex busca = new Regex("[a-zA-Z0-9]+");
            var match = busca.Match(p);
            return p.Substring(match.Index, match.Length);

        }

        //Teste lexico e sintatico------------------------------------------------------
        private void AceitaLexico()
        {
            // implementar a mensagem de aceita lexico
            dgresult.Columns.Add("Resultado", "Resultado");

            foreach (DataGridViewColumn column in dgresult.Columns)
            {
                if (column.DataPropertyName == "Resultado")
                    column.Width = 100; //tamanho fixo da primeira coluna

                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }


            dgresult.Rows.Add("Código aceito pela análise léxica");
        }

        void AceitaSintatico()
        {

            dgresult.Rows.Add("Código aceito pela analise sintática");

        }

        // funcao para criar label
        private string CriaLabel()
        {
            string label = "L" + countLabel.ToString();
            countLabel++;
            return label;
        }

        //funcao para criar temporario
        private string CriaTemp()
        {
            string temp = "T" + countTemp.ToString();
            countTemp++;
            return temp;
        }

        //tratamento de erros--------------------------------------------------------------
        public void ErroEntradaNula()
        {
            dgresult.Columns.Add("Resultado", "Resultado");
            dgresult.Rows.Add("Entrada vazia!");
        }

        public void ErroCharRestrito()
        {
            dgSimbolos.Rows.Add("Erro léxico\n $ , ´ , ¨ sao restritos");
        }

        public void ErroLexico(string erro, string posicao)
        {
            dgSimbolos.Rows.Add("Erro léxico, caracter não permitido:\n" + erro + " na posicao: " + posicao + "\n");
        }

        private void ErroSintatico(string erro)
        {

            if (erro == "")
            {
                dgresult.Rows.Add("Erro sintático: \nAguardando while, if, else");
            }
            else
            {
                dgresult.Rows.Add("Erro sintático: \nAguardando " + erro);
            }

        }
        
        private void ErroMessage(string erro)
        {
            dgresult.Rows.Add(erro);
        }
        
        //formularios da tela---------------------------------------------------------------
        private void BtnAnalisar_Click(object sender, EventArgs e)
        {
            LimparAnalise();
            if (TesteEntradaNula() == true)
            {
                Main();
            }
            else
            {
                ErroEntradaNula();
            }

        }

        private bool AnaliseLexica(string[,] listaLexica)
        {
            //teste para encontrar erros em que caracteres não foram identificados
            int flag = 0;
            int i = 0;

            do
            {
                //se encontrar null, e pq n foi associado nada ao lex, ou seja, nao pertence ao dicionario
                if (listaLexica[i, 3] == null || listaLexica[i, 3] == "")
                {
                    flag = 1;
                    ErroLexico(listaLexica[i, 0], listaLexica[i, 1]);
                    listaLexica[i, 3] = "Não Aceito!!!";
                }
                i++;
            } while (listaLexica[i, 0] != null);

            if (flag == 1)
            {
                return false;
            }
            else
                return true;
        }

        //Main--------------------------------------------------------------------
        public void Main()
        {
            ///matriz de strings que contera a analise lexica
            //[0,n] contem os lex
            //[1,n] contem a posição do lex no texto lido
            //[2,n] contem o tamanho do lex lido
            //[3,n] contem a identificação do lex
            String[,] listaLexica = new String[500, 4];

            // armazena o codigo de entrada
            String entrada = null;
            entrada = textBoxEntrada.Text;

            PreencherListaLexica(entrada, listaLexica);

            if (AnaliseLexica(listaLexica) == false)
            {
                MostraListaLexica(listaLexica);
            }
            else
            {
                AceitaLexico();
                MostraListaLexica(listaLexica);

                if (AnaliseSintatica(listaLexica) == true)
                {
                    MostraTabSimbolos();
                    MostraCodTresEnderecos();
                    AceitaSintatico();
                }
                else
                {
                    ErroSintatico("");
                }
            }
        }
    }
}
