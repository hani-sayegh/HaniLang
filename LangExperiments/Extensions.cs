﻿using System.Linq;
using System.Text;

namespace LangExperiments
{
    static class Extensions
    {
        public static string Tree(this ISyntaxNode node)
        {
            var sb = new StringBuilder();
            Tree(node, "", "");
            return sb.ToString();

            void Tree(ISyntaxNode n, string indent, string childIndent)
            {
                sb.AppendLine(childIndent + n.ToString());

                var last = n.Children().LastOrDefault();
                foreach (var child in n.Children())
                {
                    Tree(child, indent + "│  ", indent + (last == child ? "└──" : "├──"));
                }
            }
        }

        public static string ToProperties(this object o)
        {
            var sb = new StringBuilder();
            var type = o.GetType();
            foreach (var prop in o.GetType().GetProperties())
            {
                sb.AppendLine($"{prop.Name}: {prop.GetValue(o)}");
            }
            return sb.ToString();
        }
    }
}