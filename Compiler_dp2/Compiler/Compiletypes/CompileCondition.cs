﻿using Compiler_dp2.Nodes;
using Compiler_dp2.Tokenizer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Compiler_dp2.Nodes.Visitors;

namespace Compiler_dp2.Compiler
{
    class CompileCondition : Compiler
    {
        Dictionary<TokenType, string> supportedTokens; // add suported tokens like == to get right condition from conditions folder

        public CompileCondition()
        {
            //TODO Add conditions that you want to support
            supportedTokens = new Dictionary<TokenType, string>();
            supportedTokens.Add(TokenType.EqualsEquals, "Equals");
            //? supportedTokens.Add(TokenType.Function, "Write");

            compiledNodes.addLast(new NodeDoNothing());
        }

        public override Token compile(Token currentToken, Token lastToken, NodeLinkedList nodeLinkedList, Node before)
        {
            //string supportedTokenString;
            //int level = currentToken.level;

            var first = currentToken;
            var second = currentToken.nextToken;
            var third = second.nextToken;

            string leftVarName = first.value;
            string rightVarName = third.value;

            Node current = before;

            // numbers omzetten naar identifiers
            if (first.tokenType != TokenType.Identifier)
            {
                current = current.insertPrevious(new NodeDirectFunction("ConstantToReturn", first.value));
                leftVarName = Guid.NewGuid().ToString("N");
                current = current.insertPrevious(new NodeDirectFunction("ReturnToVariable", leftVarName));
            }
            if (third.tokenType != TokenType.Identifier)
            {
                current = current.insertPrevious(new NodeDirectFunction("ConstantToReturn", third.value));
                rightVarName = Guid.NewGuid().ToString("N");
                current = current.insertPrevious(new NodeDirectFunction("ReturnToVariable", rightVarName));
            }

            //Zet een berekening klaar
            //TODO "AreEqual is hardcoded" '!=' elke middelste token zal als 'AreEqual beschouwt worden'
            current = current.insertPrevious(new NodeFunction("AreEqual", leftVarName, rightVarName));

            return third.nextToken;
        }

        public override bool isMatch(Token currentToken)
        {
            var first = currentToken;
            var second = currentToken.nextToken;
            var third = second.nextToken;

            return second.tokenType == TokenType.EqualsEquals &&
                   (first.tokenType == TokenType.Number || first.tokenType == TokenType.Identifier) &&
                   (third.tokenType == TokenType.Number || third.tokenType == TokenType.Identifier);
        }
    }
}
