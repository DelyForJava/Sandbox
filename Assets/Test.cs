using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;


class ListNode<T>
{
        public T t;
        public ListNode<T> next;
}


class TreeNode<T>
{
        public T t;
        public TreeNode<T> left;
        public TreeNode<T> right;
}


public class Test : MonoBehaviour
{
        public static string GetAddress(object o)
        {
                GCHandle h = GCHandle.Alloc(o, GCHandleType.WeakTrackResurrection);

                IntPtr addr = GCHandle.ToIntPtr(h);

                return "0x" + addr.ToString("X");
        }

        string txt = "";

        void Start()
        {
                //ListTest();
                TreeTest();

        }

        void BSF(TreeNode<int> root)
        {
                Queue q = new Queue();
                q.Enqueue(root);
                while (q.Count > 0)
                {
                        var e = (TreeNode<int>)q.Dequeue();
                        txt = txt + e.t + ",";
                        var left = e.left;
                        var right = e.right;

                        if (left != null)
                                q.Enqueue(left);
                        if (right != null)
                                q.Enqueue(right);
                }

        }


        void PreOrder(TreeNode<int> root, bool isRecursive = false)
        {
                if (isRecursive)
                {
                        txt = txt + root.t + ",";
                        var left = root.left;
                        if (left != null)
                                PreOrder(left, true);
                        var right = root.right;
                        if (right != null)
                                PreOrder(right, true);
                        return;
                }
                //Stack s = new Stack();
                //s.Push(root);
                //while (s.Count > 0)
                //{
                //        var e = (TreeNode<int>)s.Pop();
                //        txt = txt + e.t + ",";
                //        var r = e.right;
                //        var l = e.left;
                //        if (r != null)
                //                s.Push(r);
                //        if (l != null)
                //                s.Push(l);
                //}

                Stack stack = new Stack();
                TreeNode<int> cursor = root;
                while (!(cursor == null && stack.Count <= 0))
                {
                        if (cursor != null)
                        {
                                stack.Push(cursor);
                                txt = txt + cursor.t + ",";
                                cursor = cursor.left;
                        }
                        else
                        {
                                var top = (TreeNode<int>)stack.Pop();
                                cursor = top.right;
                        }

                }
                //Stack stack = new Stack();
                //TreeNode<int> cursor = root;
                //while (cursor != null || stack.Count > 0)
                //{
                //        while(cursor!=null)
                //        {
                //                stack.Push(cursor);
                //                txt = txt + cursor.t + ",";
                //                cursor = cursor.left;
                //        }

                //        if(stack.Count>0)
                //        {
                //                var top = (TreeNode<int>)stack.Pop();
                //                cursor = top.right;
                //        }

                //}

        }


        void InOrder(TreeNode<int> root, bool isRecursive=false)
        {
                if (isRecursive)
                {
                        var left = root.left;
                        if (left != null)
                                InOrder(left, true);
                        txt = txt + root.t + ",";
                        var right = root.right;
                        if (right != null)
                                InOrder(right, true);
                        return;
                }

                Stack stack = new Stack();
                TreeNode<int> cursor = root;
                while (cursor != null || stack.Count > 0)
                {
                        if (cursor != null)
                        {
                                stack.Push(cursor);
                                cursor = cursor.left;
                        }
                        else
                        {
                                var top = (TreeNode<int>)stack.Pop();
                                txt = txt + top.t + ",";
                                cursor = top.right;
                        }

                }



                //Stack stack = new Stack();
                //TreeNode<int> cursor = root;
                //while (cursor != null || stack.Count > 0)
                //{
                //        while (cursor != null)
                //        {
                //                stack.Push(cursor);
                //                cursor = cursor.left;
                //        }

                //        if (stack.Count > 0)
                //        {
                //                var top = (TreeNode<int>)stack.Pop();
                //                txt = txt + top.t + ",";
                //                cursor = top.right;
                //        }

                //}

        }


        void PostOrder(TreeNode<int> root, bool isRecursive=false)
        {
                if (isRecursive)
                {
                        var left = root.left;
                        if (left != null)
                                PostOrder(left, true);
                        var right = root.right;
                        if (right != null)
                                PostOrder(right, true);
                        txt = txt + root.t + ",";
                        return;
                }

                TreeNode<int> pre=null,curr=null;
                Stack stack = new Stack();
                stack.Push(root);
                while(stack.Count>0)
                {
                        curr = null;
                        curr = (TreeNode<int>)stack.Peek();
                        if( (curr.left==null&&curr.right==null) || (pre!=null&&(pre==curr.left||pre==curr.right) ) )
                        {
                                txt = txt + curr.t + ",";
                                pre = curr;
                                stack.Pop();
                        }
                        else
                        {
                                if (curr.right != null)
                                        stack.Push(curr.right);
                                if (curr.left != null)
                                        stack.Push(curr.left);

                        }

                }

        }


        void TreeTest()
        {
                var root = new TreeNode<int>();
                root.t = 1;
                root.left = new TreeNode<int>();
                root.left.t = 2;
                root.left.left = new TreeNode<int>();
                root.left.right = new TreeNode<int>();
                root.left.left.t = 4;
                root.left.right.t = 5;
                root.right = new TreeNode<int>();
                root.right.t = 3;
                root.right.left = new TreeNode<int>();
                root.right.right = new TreeNode<int>();
                root.right.left.t = 6;
                root.right.right.t = 7;

                txt = "";
                BSF(root);
                Debug.LogError("BSF: " + txt);

                txt = "";
                PreOrder(root);
                Debug.LogError("PreOrder: " + txt);
                txt = "";
                InOrder(root);
                Debug.LogError("InOrder: " + txt);
                txt = "";
                PostOrder(root);
                //PostOrder(root,true);
                Debug.LogError("PostOrder: " + txt);
        }


        void ListTest()
        {
                ListNode<int> next = null;
                for (int i = 99; i >= 0; i--)
                {
                        var n = new ListNode<int>();
                        int v = i + 1;
                        n.t = v;
                        n.next = next;
                        next = n;
                }
                ListNode<int> head = next;
                ListNode<int> iterator = head;
                string txt = "";
                while (true)
                {
                        int vv = iterator.t;
                        txt = txt + vv + ",";
                        iterator = iterator.next;
                        if (iterator == null)
                                break;
                }
                Debug.LogError("List " + txt);

                ListNode<int> pre = null;
                ListNode<int> nxt = null;
                while (head != null)
                {
                        nxt = head.next;
                        head.next = pre;
                        pre = head;
                        head = nxt;
                }
                head = pre;

                string txt2 = "";
                while (true)
                {
                        int vv = head.t;
                        txt2 = txt2 + vv + ",";
                        head = head.next;
                        if (head == null)
                                break;
                }
                Debug.LogError("List2 " + txt2);
        }


}