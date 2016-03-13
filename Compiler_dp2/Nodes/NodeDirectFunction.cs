﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler_dp2.Nodes.Visitors;
using Compiler_dp2.Tokenizer;

namespace Compiler_dp2.Nodes
{
    class NodeDirectFunction : NodeAbstractFunction
    {
        private string[] toCompile;

        public NodeDirectFunction(string kind, Token token)
        {
            //toCompile[toCompile.Length + 1] = kind;
            //toCompile[toCompile.Length + 1] = token;
        }

        public NodeDirectFunction(string kind, string value)
        {
            
        }

        public void setStringArray(int arraySize)
        {
            if (arraySize > 0) { toCompile = new string[arraySize]; }
            else { toCompile = null; }
        }

        public void setAt(int index, string text) { toCompile[index] = text; }
        public string getAt(int index) { return toCompile[index]; }

        public string[] getToCompile() { return toCompile; }

        public override void accept(NodeVisistor.NodeVisitor visitor) { visitor.visit(this); }

        public override string nodeKind()
        {
            string returnContent = "Direct Function Node: ";

            foreach(string c in toCompile) { returnContent += c; }

            return returnContent;
        }



    }
}