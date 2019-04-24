﻿using System;
using System.Linq;

using HtmlAgilityPack;

namespace ReverseMarkdown.Converters
{
    public class P : ConverterBase
    {
        public P(Converter converter) : base(converter)
        {
            Converter.Register("p", this);
        }

        public override string Convert(HtmlNode node)
        {
            var indentation = IndentationFor(node);
            return $"{indentation}{TreatChildren(node).Trim()}{Environment.NewLine}";
        }

        private static string IndentationFor(HtmlNode node)
        {
            var length = node.Ancestors("ol").Count() + node.Ancestors("ul").Count();
            bool parentIsList = node.ParentNode.Name.ToLowerInvariant() == "li" || node.ParentNode.Name.ToLowerInvariant() == "ol";
            return parentIsList && node.ParentNode.FirstChild != node
                ? new string(' ', length * 4)
                : Environment.NewLine;
        }
    }
}
