using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EldosFileLib
{
    public enum NodeType
    {
        File,
        Directory
    }
    public interface ISystemDetails
    {
        string Name { get; set; }
        NodeType Type { get; set; }
    }

    public class FileData : ISystemDetails
    {
        public string Name { get; set; }
        public NodeType Type { get; set; }
    }

    public class Directory : ISystemDetails
    {
        public string Name { get; set; }
        public NodeType Type { get; set; }
        public List<FileData> Files { get; set; }
        public NTree<Directory> Directories { get; set; }
    }

    public delegate void TreeVisitor<T>(T nodeData);

    public class NTree<T>
    {
        private T data;
        private LinkedList<NTree<T>> children;

        public NTree(T data)
        {
            this.data = data;
            children = new LinkedList<NTree<T>>();
        }

        public void AddNode(T data)
        {
            children.AddFirst(new NTree<T>(data));
        }

        public NTree<T> GetNode(int i)
        {
            foreach (NTree<T> n in children)
                if (--i == 0)
                    return n;
            return null;
        }

        public void Traverse(NTree<T> node, TreeVisitor<T> visitor)
        {
            visitor(node.data);
            foreach (NTree<T> kid in node.children)
                Traverse(kid, visitor);
        }
    }
}
