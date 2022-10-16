namespace SplayRandTree
{
    public class RandomizedBST
    {
        private TreeNode? _root { get; set; }
        private TreeNode? _splayNode { get; set; }
        private int _size { get; set; }

        public void Insert(int value)
        {
            _size++;
            if (_root == null)
            {
                _root = new TreeNode()
                {
                    Value = value
                };
            }
            else
            {
                _root = Insert(value, _root, _root);
                var randNum = new Random().Next(0, _size);
                if (randNum == _size)
                {
                    if (_splayNode != null)
                        _root = Splay(_splayNode);
                }
            }
        }
        private TreeNode Insert(int value, TreeNode? node, TreeNode parentNode)
        {
            
            if (node == null)
            {
                node = new TreeNode()
                {
                    Parent = parentNode,
                    Value = value
                };
                _splayNode = node;
                return node;
            }
            else if (node.Value < value)
            {
                node.RightNode = Insert(value, node.RightNode, node);
                return node;
            }
            else if (node.Value > value)
            {
                node.LeftNode = Insert(value, node.LeftNode, node);
                return node;
            }
            else
            {
                node.Counter++;
                return node;
            }
        }
        static private TreeNode rightSmallRotate(TreeNode child)
        {
            var parent = child.Parent;
            var grandParent = parent.Parent;
            parent.Parent = child;
            if (grandParent == null)
            {
                child.Parent = null;
            }
            else
            {
                child.Parent = grandParent;
                if (grandParent.Value < child.Value)
                {
                    grandParent.RightNode = child;
                }
                else
                {
                    grandParent.LeftNode = child;
                }
            }
            if (child.RightNode == null)
            {
                parent.LeftNode = null;
            }
            else
            {
                parent.LeftNode = child.RightNode;
                child.RightNode.Parent = parent;
            }
            child.RightNode = parent;
            return child;
        }
        static private TreeNode leftSmallRotate(TreeNode child)
        {
            var parent = child.Parent;
            var grandParent = parent.Parent;
            parent.Parent = child;
            if (grandParent == null)
            {
                child.Parent = null;
            }
            else
            {
                child.Parent = grandParent;
                if(grandParent.Value < child.Value)
                {
                    grandParent.RightNode = child;
                }
                else
                {
                    grandParent.LeftNode = child;
                }
            }
            if (child.LeftNode == null)
            {
                parent.RightNode = null;
            }
            else
            {
                parent.RightNode = child.LeftNode;
                child.LeftNode.Parent = parent;
            }      
            child.LeftNode = parent;
            return child;
        }
        private TreeNode Splay(TreeNode iterRoot)
        {
            if (iterRoot.Parent == null)
            {
                _splayNode = null;
                return iterRoot;
            }
            var grandParent = iterRoot.Parent.Parent;
            var parent = iterRoot.Parent;
            if (grandParent == null)
            {
                //left tree -> right rotate
                if(iterRoot.Value < parent.Value)
                {
                    iterRoot = rightSmallRotate(iterRoot);
                    iterRoot = Splay(iterRoot);
                    return iterRoot;
                }
                //right tree -> left rotate
                else
                {
                    iterRoot = leftSmallRotate(iterRoot);
                    iterRoot = Splay(iterRoot);
                    return iterRoot;
                }
            }
            else
            {
                //Узел является левым потомком родительского элемента,
                //и родитель также является левым потомком прародителя (два разворота вправо)
                if (grandParent.LeftNode == parent & parent.LeftNode == iterRoot)
                {
                    iterRoot = rightSmallRotate(parent);
                    iterRoot = rightSmallRotate(iterRoot.LeftNode);
                    iterRoot = Splay(iterRoot);
                    return iterRoot;

                }
                //Узел является правым потомком своего родительского элемента,
                //и родитель также является правым потомком своего прародитель (два разворота влево)
                else if (grandParent.RightNode == parent & parent.RightNode == iterRoot)
                {
                    iterRoot = leftSmallRotate(parent);
                    iterRoot = leftSmallRotate(iterRoot.RightNode);
                    iterRoot = Splay(iterRoot);
                    return iterRoot;
                }
                // узел является правым потомком своего родительского элемента,
                // а родитель является левым потомком прародителя
                // (разворот влево с последующим разворотом право)
                else if (grandParent.LeftNode == parent & parent.RightNode == iterRoot)
                {
                    iterRoot = leftSmallRotate(iterRoot);
                    iterRoot = rightSmallRotate(iterRoot);
                    iterRoot = Splay(iterRoot);
                    return iterRoot;
                }
                //Узел является левым потомком по отношению к родительскому элементу,
                //а родитель является правым потомком прародителя
                //(разворот вправо с последующим разворотом влево) 
                else
                {
                    iterRoot = rightSmallRotate(iterRoot);
                    iterRoot = leftSmallRotate(iterRoot);
                    iterRoot = Splay(iterRoot);
                    return iterRoot;
                }
                    

            }
        }
        public void Sort()
        {
            PrintAscending(_root);
        }
        private void PrintAscending(TreeNode node)
        {
            if (node.LeftNode != null)
            {
                PrintAscending(node.LeftNode);
            }
            for (int i = 0; i <= node.Counter; i++)
            {
                Console.WriteLine(node.Value);
            }
            if (node.RightNode != null)
            {
                PrintAscending(node.RightNode);
            }
        }
        public bool Search(int value)
        {
            if (_root != null)
            {
                var result = Search(value, _root);
                var randNum = new Random().Next(0, _size);
                if (randNum == _size)
                {
                    if (_splayNode != null)
                        _root = Splay(_splayNode);
                }
                return result;
            }
            else
            {
                return false;
            }
        }
        private bool Search(int value, TreeNode? node)
        {
            if (node != null)
            {
                if (node.Value == value)
                {
                    _splayNode = node;
                    return true;
                }
                else if (node.Value > value)
                {
                    return Search(value, node.LeftNode);
                }
                else
                {
                    return Search(value, node.RightNode);
                }
            }
            else
            {
                return false;
            }
        }
        public void Remove(int value)
        {
            var result = Search(value);
            if(result == false)
            {
                return;
                //throw new Exception("Value not found");
            }
            var leftSubTree = _root.LeftNode;
            var rightSubTree = _root.RightNode;
            if (leftSubTree != null)
            {
                leftSubTree.Parent = null;
                var maxValue = FindMax(leftSubTree);
                maxValue = Splay(maxValue);
                if (rightSubTree != null)
                {
                    maxValue.RightNode = rightSubTree;
                    rightSubTree.Parent = maxValue;
                    _root = maxValue;
                }
                else
                {
                    _root = maxValue;
                }
            }
            else if(rightSubTree != null)
            {
                rightSubTree.Parent = null;
                _root = rightSubTree;
            }
            else
            {
                _root = null;
            }
            _size--;
        }
        private TreeNode FindMax(TreeNode node)
        {
            while (node.RightNode != null)
            {
                node = node.RightNode;
            }
            return node;
        }
    }
}
