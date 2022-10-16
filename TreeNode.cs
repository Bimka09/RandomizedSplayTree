using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SplayRandTree
{
    public class TreeNode
    {
        public TreeNode? Parent { get; set; }
        public TreeNode? LeftNode { get; set; }
        public TreeNode? RightNode { get; set; }
        public int Value { get; set; }
        public int Counter { get; set; }
    }
}
