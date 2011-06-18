/*
 * GameWatch - Server Browser for online games
 * Copyright (C) 2003 Rodrigo Reyes <reyes@charabia.net>
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License as
 * published by the Free Software Foundation; either version 2 of the
 * License, or (at your option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place - Suite 330, Boston, MA
 * 02111-1307, USA.
 *
 */
using System;
using System.Collections;

namespace GameWatch.Utils.Net
{

    public class BitVectorTrie
    {
	public class Node
	{
	    public ArrayList Children = null;
	    public BitVector Key = null;
	    public Object Data = null;
	}

	public Node Root = new Node();
	
	public BitVectorTrie()
	{
	}

	public void Add(BitVector key, object data)
	{
	    Add(Root, key, data);
	}

	//
	// This Add method attach an object "data" to a node "n" using
	// the BitVector key as path.
	//
	private void Add(Node n, BitVector key, object data)
	{
	    if (n.Key == null)
		{
		    AddAsChildren(n, key, data);
		    return;
		}

	    //
	    // First, calculate the longest common prefix for the key
	    // and the BitVector stored in this node.
	    //
	    int longest = key.LongestCommonPrefix(n.Key);

	    System.Diagnostics.Debug.Assert(longest != 0);

	    if (longest == n.Key.Length)
		{
		    //
		    // If the current node is a perfect prefix of the
		    // key, then remove the prefix from the key, and
		    // we continue our walk on the children.
		    //
		    key = key.Range(longest, key.Length - longest);
		    AddAsChildren(n, key, data);
		    return;
		}
	    else
		{
		    //
		    // Here, n.Key and key share a common prefix. So we:
		    //
		    // - Create a new node with this common prefix
		    //   held there,
		    //
		    // - make n.Key and a new node with key as
		    // children of this new node.
		    BitVector common = n.Key.Range(0, longest);

		    Node c1 = new Node();
		    c1.Key = n.Key.Range(longest, n.Key.Length - longest);
		    c1.Data = n.Data;
		    c1.Children = n.Children;

		    Node c2 = new Node();
		    c2.Key = key.Range(longest, key.Length - longest);
		    c2.Data = data;
		    
		    n.Key = common;
		    n.Data = null;
		    n.Children = new ArrayList();
		    n.Children.Add(c1);
		    n.Children.Add(c2);

		    return;
		}
	}

	//
	// The AddAsChildren() method create a new node with key
	// "key", attach a data "data" to it, and finally link it to
	// the node "n".
	//
	private void AddAsChildren(Node n, BitVector key, Object data)
	{
	    //
	    // If "n" has no children, just add a new one
	    //
	    if (n.Children == null)
		{
		    n.Children = new ArrayList();
		    Node nu = new Node();
		    nu.Key = key;
		    nu.Data = data;
		    n.Children.Add(nu);
		    return;
		}

	    //
	    // From here, the node n already has at least 1
	    // children.


	    /// Check the one that has a common prefix with our key
	    //(if there is none, the bestindex variable stays at -1).

	    int bestindex = -1;
	    int bestlength = 0;
	    for (int i=0; i<n.Children.Count; i++)
		{
		    int b = ((Node)(n.Children[i])).Key.LongestCommonPrefix(key);

		    if (b > bestlength)
			{
			    bestlength = b;
			    bestindex = i;
			}
		}

	    //
	    // The node n has no children that have a common prefix
	    // with our key, so we create a new children node and
	    // attach our data there.
	    if (bestindex == -1)
		{
		    Node c2 = new Node();
		    c2.Key = key;
		    c2.Data = data;
		    n.Children.Add(c2);
		    return;
		}
	    else
		{
		    // There is a children node that can hold our
		    // data: continue our walk with this node.
		    Add(((Node)n.Children[bestindex]), key, data);
		    return;
		}
	}

	public object Get(BitVector key)
	{
	    Node curnode = Root;
	    while (curnode != null)
		{
		    if (curnode.Children == null)
			return null;

		    // Get the best fitting index
		    int bestindex = -1;
		    int bestlength = 0;
		    for (int i=0; i<curnode.Children.Count; i++)
			{
			    int b = ((Node)(curnode.Children[i])).Key.LongestCommonPrefix(key);
			    if (b > bestlength)
				{
				    bestlength = b;
				    bestindex = i;
				}
			}
		    
		    if (bestindex != -1)
			{
			    key = key.Range(bestlength, key.Length - bestlength);
			    curnode = ((Node)curnode.Children[bestindex]);

			    if (key.Length == 0)
				return curnode.Data;
			}
		    else
			{
			    return null;
			}
		}

	    return null;
	}

	//
	// Returns the object held in the node that best matches our
	// key.
	//
	public object GetBest(BitVector key)
	{
	    Node curnode = Root;
	    while (curnode != null)
		{
		    if (curnode.Children == null)
			return curnode.Data;

		    // Get the best fitting index
		    int bestindex = -1;
		    int bestlength = 0;
		    for (int i=0; i<curnode.Children.Count; i++)
			{
			    int b = ((Node)(curnode.Children[i])).Key.LongestCommonPrefix(key);
			    if (b > bestlength)
				{
				    bestlength = b;
				    bestindex = i;
				}
			}
		    
		    if (bestindex != -1)
			{
			    key = key.Range(bestlength, key.Length - bestlength);
			    curnode = ((Node)curnode.Children[bestindex]);

			    if (key.Length == 0)
				return curnode.Data;
			}
		    else
			{
			    return curnode.Data;
			}
		}

	    return null;
	}

	public void DisplayAsTree(Node n, int offset)
	{
	    for (int i=0; i<offset; i++)
		Console.Write("   ");
	    Console.WriteLine("<{0}> = <{1}>", n.Key, n.Data);
	    if (n.Children != null)
		{
		    foreach(Node c in n.Children)
			{
			    DisplayAsTree(c, offset+1);
			}
		}
	}

	static public void Test()
	{
	    BitVector.Test();
	    BitVectorTrie trie = new BitVectorTrie();
	    BitVector v1 = new BitVector();
	    v1.Set(0, true); // v1.Set(1, false);  v1.Set(2, true);
	    trie.Add(v1, "1");

	    v1 = new BitVector();
	    v1.Set(0, false); // v1.Set(1, false);  v1.Set(2, true);
	    trie.Add(v1, "0");

 	    v1 = new BitVector();
 	    v1.Set(0, false); v1.Set(1, false);  //v1.Set(2, false);
 	    trie.Add(v1, "00");

 	    v1 = new BitVector();
 	    v1.Set(0, true); v1.Set(1, true);  //v1.Set(2, false);
 	    trie.Add(v1, "11");

 	    v1 = new BitVector();
 	    v1.Set(0, false); v1.Set(1, true);  v1.Set(2, true);
 	    trie.Add(v1, "011");

	    v1 = new BitVector(255, 8);
 	    trie.Add(v1, "255");

	    v1 = new BitVector(1025, 11);
 	    trie.Add(v1, "1024");

	    v1 = new BitVector(1153, 11);
 	    trie.Add(v1, "1024+1+128");

	    trie.DisplayAsTree(trie.Root, 0);
	    v1 = new BitVector(1025, 11);
	    object result = trie.Get(v1);
	    Console.WriteLine("data: {0} = {1}", v1, result);

	}

    }


    

}
