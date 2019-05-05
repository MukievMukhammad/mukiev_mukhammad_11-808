using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_3_Tree
{
    interface B_Tree<T>
        where T : IComparable
    {
        void Insert(T node);
        void Remove(T node);
        Node<T> Find(T value);
        void Concatenate(Node<T> node1, Node<T> node2);
        void Disengage(T value);
    }

    public class Node<T> 
        where T : IComparable
    {
        public Node<T> Parent { get; set; }
        public T Value { get; set; }
        public Tuple<T,T> Func
        {
            get
            {
                if (Children[0].Func == null)
                    return Tuple.Create(Children[0].Value, Children[1].Value);
                else return Tuple.Create(Children[0].Func.Item2, Children[1].Func.Item2);
            }
        }
        public int High
        {
            get
            {
                int count = 0;
                var current = this;
                while(current != null)
                {
                    count++;
                    current = current.Children[0];
                }
                return count;
            }
        }
        public List<Node<T>> Children = new List<Node<T>>();
    }

    public class _2_3_Tree<T> : B_Tree<T>
        where T : IComparable
    {
        Node<T> root { get; set; }

        public void Concatenate(Node<T> node1, Node<T> node2)
        {
            if (node1.High == node2.High)
            {
                var otherNode = new Node<T>();
                otherNode.Children.Add(node1);
                otherNode.Children.Add(node2);
                node1.Parent = otherNode;
                node2.Parent = otherNode;
            }
            else
            {
                if (node1.High > node2.High)
                    ConcatenateUnequalTree(node1, node2);
                else ConcatenateUnequalTree(node2, node1);
            }
        }

        private void ConcatenateUnequalTree(Node<T> node1, Node<T> node2)
        {
            var count = node1.High - node2.High;
            var current = node1;
            for (int i = 0; i < count - 1; i++)
                current = current.Children[0];
            current.Children.Insert(0, node2);
            node2.Parent = current;
            Balanse(current);
        }

        public void Disengage(T value)
        {
            var stack = new Stack<Node<T>>();
            stack.Push(root);
            while (stack.Count != 0)
            {
                var current = stack.Pop();
                if (current.Value != null && current.Value.CompareTo(value) == 0) break;
                if (value.CompareTo(current.Func.Item1) <= 0)
                {
                    stack.Push(current.Children[0]);
                    foreach (var e in current.Children)
                        e.Parent = null;
                }
                else if (value.CompareTo(current.Func.Item2) <= 0)
                {
                    stack.Push(current.Children[1]);
                    foreach (var e in current.Children)
                        e.Parent = null;
                }
                else
                {
                    stack.Push(current.Children.Last());
                    foreach (var e in current.Children)
                        e.Parent = null;
                }
            }
        }

        public Node<T> Find(T value)
        {
            var stack = new Stack<Node<T>>();
            stack.Push(root);
            while(stack.Count != 0)
            {
                var current = stack.Pop();
                if (current.Value != null && current.Value.CompareTo(value) == 0) return current;
                if (value.CompareTo(current.Func.Item1) <= 0)
                    stack.Push(current.Children[0]);
                else if (value.CompareTo(current.Func.Item2) <= 0)
                    stack.Push(current.Children[1]);
                else stack.Push(current.Children.Last());
            }
            return new Node<T>();
        }

        public void Insert(T node)
        {
            if (root == null) root = new Node<T>() { Value = node };
            if(root.Children.Count == 0)
            {
                var otherNode = new Node<T>();
                root.Parent = otherNode;
                otherNode.Children.Add(root);
                otherNode.Children.Add(new Node<T> { Value = node, Parent = otherNode });
                otherNode.Children.OrderBy(e => e.Value);
                root = otherNode;
            }

            {
                var stack = new Stack<Node<T>>();
                stack.Push(root);
                var current = new Node<T>();
                while (current.Children[0].Children.Count == 0)
                {
                    current = stack.Pop();
                    if (node.CompareTo(current.Func.Item1) <= 0)
                        stack.Push(current.Children[0]);
                    else if (node.CompareTo(current.Func.Item2) <= 0)
                        stack.Push(current.Children[1]);
                    else stack.Push(current.Children.Last());
                }
                if (node.CompareTo(current.Func.Item1) <= 0)
                    current.Children.Insert(0, new Node<T> { Value = node });
                else if (node.CompareTo(current.Func.Item2) <= 0)
                    current.Children.Insert(1, new Node<T> { Value = node });
                else if (node.CompareTo(current.Children.Last()) <= 0)
                    current.Children.Insert(2, new Node<T> { Value = node });
                else current.Children.Insert(3, new Node<T> { Value = node });
                Balanse(current);
            }
        }

        public void Remove(T node)
        {
            var node1 = Find(node);
            if (node1 == null) return;
            var parent = node1.Parent;
            parent.Children.Remove(node1);
            node1.Parent = null;
            Balanse(parent);
        }

        private void Balanse(Node<T> parent)
        {
            if (parent.Children.Count == 2 || parent.Children.Count == 3) return;
            else if(parent.Children.Count == 4)
            {
                var newNode = new Node<T>();
                newNode.Children.Add(parent.Children.Last());
                parent.Children.Last().Parent = newNode;
                parent.Children.RemoveAt(3);
                newNode.Children.Add(parent.Children.Last());
                parent.Children.Last().Parent = newNode;
                parent.Children.RemoveAt(2);
                newNode.Parent = parent.Parent;
                parent.Parent.Children.Add(newNode);
                parent.Parent.Children.OrderBy(e => e.Value);
                Balanse(parent.Parent);
                return;
            }
            else
            {
                var node1 = parent.Parent.Children.Where(e => e.Children.Count < 3);
                var node2 = parent.Parent.Children.Where(e => e.Children.Count == 3);
                if(node2 != null)
                {
                    if (node2.First().Value.CompareTo(parent.Value) == -1)
                        parent.Children.Add(node2.First().Children.Last());
                    else parent.Children.Add(node2.First().Children.First());
                    parent.Children.OrderBy(e => e.Value);
                    return;
                }
                node1.First().Children.Add(parent.Children[0]);
                parent.Children[0].Parent = node1.First();
                var nextNodeToBalanse = parent.Parent;
                nextNodeToBalanse.Children.Remove(parent);
                if (nextNodeToBalanse == root)
                {
                    root = root.Children[0];
                    root.Parent = null;
                    return;
                }
                Balanse(nextNodeToBalanse);
            }
        }
    }
}
