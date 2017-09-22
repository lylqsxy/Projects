using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Node
{
    class Program
    {
        static void Main(string[] args)
        {
            var testData = new SingleChildNode("root",
                new TwoChildrenNode("child1",
                    new NoChildrenNode("leaf1"),
                    new SingleChildNode("child2",
                        new NoChildrenNode("leaf2")
                    )
                )
            );
            var testDataTwo = new SingleChildNode("root",
                new TwoChildrenNode("child1",
                    new TwoChildrenNode("leaf1",
                        new SingleChildNode("child2",
                            new NoChildrenNode("leaf2")
                        ),
                        new NoChildrenNode("leaf2")
                    ),
                    new SingleChildNode("child2",
                        new NoChildrenNode("leaf2")
                    )
                )
            );
            var testDataThree = new ManyChildrenNode("root",
                new ManyChildrenNode("child1",
                    new ManyChildrenNode("leaf1"),
                    new ManyChildrenNode("child2",
                        new ManyChildrenNode("leaf2"))));
            var testDataFour = new NoChildrenNode("root");

            INodeDescriber nodeDescriber = new NodeDescriber();
            INodeTransformer nodeTransformer = new NodeTransformer();
            INodeWriter nodeWriter = new NodeWriter(nodeDescriber);

            List<string> result = new List<string>();

            result.Add(nodeDescriber.Describe(testData));            
            result.Add(nodeDescriber.Describe(nodeTransformer.Transform(testDataThree)));
            nodeWriter.WriteToFileAsync(testDataFour, @"C:\Users\Nicky Li\Desktop\output.txt");

            result.ForEach(x => Console.WriteLine(x));

            Console.Read();
        }
    }

    public class NodeDescriber : INodeDescriber
    {
        private StringBuilder stringBuilder = new StringBuilder();
        private const int indentLevel = 4;

        public string Describe(Node node)
        {
            stringBuilder.Clear();
            NodeWriter(node, 0);          
            return stringBuilder.ToString();
        }

        public void NodeWriter(Node currentNode, int level)
        {
            Type currentNodeType = currentNode.GetType();
            if (currentNodeType == typeof(NoChildrenNode))
            {
                stringBuilder.Append(outputBuilder(currentNode.Name, currentNodeType, level));
                stringBuilder.Append(")");
            }
            else if (currentNodeType == typeof(SingleChildNode))
            {
                stringBuilder.Append(outputBuilder(currentNode.Name, currentNodeType, level));
                stringBuilder.Append(",");
                SingleChildNode scn = (SingleChildNode)currentNode;
                level ++;
                NodeWriter(scn.Child, level);
                stringBuilder.Append(")");
            }
            else if (currentNodeType == typeof(TwoChildrenNode))
            {
                stringBuilder.Append(outputBuilder(currentNode.Name, currentNodeType, level));
                stringBuilder.Append(",");
                TwoChildrenNode tcn = (TwoChildrenNode)currentNode;
                level ++;
                NodeWriter(tcn.FirstChild, level);
                stringBuilder.Append(",");
                NodeWriter(tcn.SecondChild, level);
                stringBuilder.Append(")");
            }
            else if (currentNodeType == typeof(ManyChildrenNode))
            {
                stringBuilder.Append(outputBuilder(currentNode.Name, currentNodeType, level));
                stringBuilder.Append(",");
                ManyChildrenNode mcn = (ManyChildrenNode)currentNode;
                level ++;
                foreach (var n in mcn.Children)
                {
                    NodeWriter(n, level);
                    stringBuilder.Append(n == mcn.Children.Last() ? ")" : ",");
                }
            }
        }

        public static string outputBuilder(string nodeName, Type nodeType, int level)
        {
            return "\n\r" + "".PadLeft(level * indentLevel) + "new " + nodeType.ToString().Split('.')[1] + "(\""  + nodeName + "\"";
        }

    }

    public class NodeTransformer : INodeTransformer
    {
        public Node Transform(Node currentNode)
        {
            Type currentNodeType = currentNode.GetType();
            if (currentNodeType == typeof(NoChildrenNode))
            {
                return currentNode;
            }
            else if (currentNodeType == typeof(SingleChildNode))
            {
                SingleChildNode scn = (SingleChildNode)currentNode;
                if(scn.Child == null)
                {
                    return new NoChildrenNode(scn.Name);
                }
                else
                {
                    return new SingleChildNode(scn.Name, Transform(scn.Child));
                }
            }
            else if (currentNodeType == typeof(TwoChildrenNode))
            {
                TwoChildrenNode tcn = (TwoChildrenNode)currentNode;
                if (tcn.FirstChild == null && tcn.SecondChild == null)
                {
                    return new NoChildrenNode(tcn.Name);
                }
                else if (tcn.FirstChild != null && tcn.SecondChild != null)
                {
                    return new TwoChildrenNode(tcn.Name, Transform(tcn.FirstChild), Transform(tcn.SecondChild));
                }
                else
                {
                    return new SingleChildNode(tcn.Name, Transform(tcn.FirstChild == null? tcn.SecondChild : tcn.FirstChild));
                }
            }
            else if (currentNodeType == typeof(ManyChildrenNode))
            {
                ManyChildrenNode mcn = (ManyChildrenNode)currentNode;
                var childrenList = mcn.Children.ToList();
                switch (childrenList.Count())
                {
                    case 0:
                        return new NoChildrenNode(mcn.Name);
                    case 1:
                        return new SingleChildNode(mcn.Name, Transform(childrenList[0]));
                    case 2:
                        return new TwoChildrenNode(mcn.Name, Transform(childrenList[0]), Transform(childrenList[1]));
                    default:
                        return new ManyChildrenNode(mcn.Name, childrenList.Select(x => Transform(x)).ToArray());
                }
            }
            else
            {
                return null;
            }
        }
    }

    public class NodeWriter : INodeWriter
    {
        private INodeDescriber _nodeDescriber;

        public NodeWriter(INodeDescriber nodeDescriber)
        {
            this._nodeDescriber = nodeDescriber;
        }

        public async Task WriteToFileAsync(Node node, string filePath)
        {
            UnicodeEncoding uniencoding = new UnicodeEncoding();
            string filename = filePath;
            byte[] result = uniencoding.GetBytes(_nodeDescriber.Describe(node));
            using (FileStream SourceStream = File.Open(filename, FileMode.Create))
            {
                SourceStream.Seek(0, SeekOrigin.End);
                try
                {
                    await SourceStream.WriteAsync(result, 0, result.Length);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                
            }
        }
    }

    public abstract class Node
    {
        public string Name { get; }
        protected Node(string name)
        {
            Name = name;
        }
    }
    public class NoChildrenNode : Node
    {
        public NoChildrenNode(string name) : base(name)
        { }
    }
    public class SingleChildNode : Node
    {
        public Node Child { get; }
        public SingleChildNode(string name, Node child) : base(name)
        {
            Child = child;
        }
    }
    public class TwoChildrenNode : Node
    {
        public Node FirstChild { get; }
        public Node SecondChild { get; }
        public TwoChildrenNode(string name, Node first, Node second) : base(name)
        {
            FirstChild = first;
            SecondChild = second;
        }
    }
    public class ManyChildrenNode : Node
    {
        public IEnumerable<Node> Children { get; }
        public ManyChildrenNode(string name, params Node[] children) : base(name)
        {
            Children = children;
        }
    }

    public interface INodeDescriber
    {
        string Describe(Node node);
    }

    public interface INodeTransformer
    {
        Node Transform(Node node);
    }
    public interface INodeWriter
    {
        Task WriteToFileAsync(Node node, string filePath);
    }
}
