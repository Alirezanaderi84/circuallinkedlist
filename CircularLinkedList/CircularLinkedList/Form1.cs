using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CircularLinkedList
{
    public partial class Form1 : Form
    {
        CircularLinkedList<string> circularList = new CircularLinkedList<string>();
        public Form1()
        {
            InitializeComponent();
        }
        private void addlist_Click(object sender, EventArgs e)
        {

            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("عبارت را وارد کنید");
            }
            else
            {
                circularList.Add(textBox1.Text.ToString());
                circularList.PrintList(listBox1);
            }

        }

        private void removelist_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Trim() == "")
            {
                MessageBox.Show("عبارت را وارد کنید");
            }
            else
            {
                circularList.remove(textBox1.Text.ToString()); 
                circularList.PrintList(listBox1);
            }
        }
        private void addfirst_Click(object sender, EventArgs e)
        {
            circularList.Addtofirst(textBox1.Text.ToString());
            circularList.PrintList(listBox1);
        }
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBox1.Checked) {
                addfirst.Show();
                addlist.Hide();
                checkBox2.Checked = false;
            }
            else
            {
                checkBox2.Checked = true;
            }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox1.Checked = false;
                addfirst.Hide();
                addlist.Show();
            }
            else
            {
                checkBox1.Checked = true;
            }
        }

    }
}
   public class CircularLinkedList<T>
    {
         LinkedList<T> list = new LinkedList<T>();
         Dictionary<LinkedListNode<T>, LinkedListNode<T>> nextNodes= new Dictionary<LinkedListNode<T>, LinkedListNode<T>>();
        public void Add(T data)
        {
            var newNode = new LinkedListNode<T>(data);

            if (list.Count == 0)
            {
                list.AddFirst(newNode);
                nextNodes[newNode] = newNode; 
            }
            else
            {
                var lastNode = list.Last;
                list.AddLast(newNode);
                nextNodes[lastNode] = newNode; 
                nextNodes[newNode] = list.First; 
            }
        }
    public void Addtofirst(T data)
    {
        var newNode = new LinkedListNode<T>(data);
        if (list.Count == 0)
        {
            list.AddFirst(newNode);
            nextNodes[newNode] = newNode;
        }
        else
        {
            var firstNode = list.First;
            list.AddFirst(newNode);
            nextNodes[newNode] = firstNode;
            var lastNode = list.Last;
            nextNodes[lastNode] = newNode;
        }
    }
    
        public void remove(T data)
        {
        
            var removeNode = list.Find(data);
            if (removeNode != null)
            {
                if (list.Count == 1)
                {
                    list.Remove(removeNode);
                    nextNodes.Clear();
                }
                else
                {
                    var prevNode = GetPreviousNode(removeNode);
                    nextNodes[prevNode] = nextNodes[removeNode]; 
                    list.Remove(removeNode);
                    nextNodes.Remove(removeNode);
                }
            }
        else
        {
            MessageBox.Show("در لیست موجود نیست");
        }
        }
        private LinkedListNode<T> GetPreviousNode(LinkedListNode<T> node)
        {
            var current = list.First;
            while (nextNodes[current] != node)
            { current = nextNodes[current]; }
            return current;
        }


    public void PrintList(ListBox listBox)
    {
        listBox.Items.Clear();
        if (list.Count == 0)
        {
            return;
        }

        var current = list.First;
        foreach (T item in list)
        {
            listBox.Items.Add(current.Value);
            current = nextNodes[current];
        
        }
    }  
    
    
   }


    
