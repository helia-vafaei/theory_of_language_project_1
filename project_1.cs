using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace proj1
{
    class Program1
    {
        public class Transition
        {
            public string startState;
            public char edge; 
            public string endState;

            public Transition(string startState, char edge, string endState)
            {
                this.startState = startState;
                this.edge = edge;
                this.endState = endState;
            }
        }  
        public class DeterminFA
        {
            public List<string> states = new List<string>();
            public List<char> alphabets = new List<char>();
            public List<Transition> delta = new List<Transition>();
            public string q0;
            public List<string> finals = new List<string>();

            public DeterminFA(IEnumerable<string> states,IEnumerable<char> alphabets,IEnumerable<string> finals,IEnumerable<Transition> delta,string q0)
            {
                this.states = states.ToList();
                this.alphabets = alphabets.ToList();
                this.finals=finals.ToList();
                this.q0=q0;
                this.delta=delta.ToList();
            }
        } 
        public static string Solve(string[] states,char[] alphabets,string[] finals,string[][] rules,char[] check,long n)
        {
            List<Transition> delta=new List<Transition>();
            for (int i = 0; i < n; i++)
                delta.Add(new Transition(rules[i][0],rules[i][1].ToCharArray()[0],rules[i][2]));
            DeterminFA fa=new DeterminFA(states,alphabets,finals,delta,states[0]);
            List<string> current=new List<string>();
            current.Add(states[0]);
            foreach (char i in check)
            {
                List<string> next=new List<string>();
                int j=0;
                while(j<current.Count)
                {
                    var statelam=fa.delta.Where(x=>current[j]==x.startState && x.edge=='$').Select(x=> x.endState).ToList();
                    current.AddRange(statelam);
                    j++;
                }
                next=fa.delta.Where(x=>x.edge==i && current.Contains(x.startState)).Select(x=> x.endState).ToList();
                current=next;
            }
            foreach (var i in current)
            {
                if(finals.Contains(i))
                    return "Accepted";
            }
            return "Rejected";
        }
        
        static void Main1(string[] args)
        {
            string[] states=Console.ReadLine().Split(',');
            for (int i = 0; i < states.Length; i++)
                states[i]=states[i].Trim(new char[]{'{','}'});    

            string[] input=Console.ReadLine().Split(',');
            char[] alphabets=new char[input.Length];
            for (int i = 0; i < alphabets.Length; i++)
                alphabets[i]=input[i].Trim(new char[]{'{','}'}).ToCharArray()[0];   

            string[] finals=Console.ReadLine().Split(',');
            for (int i = 0; i < finals.Length; i++)
                finals[i]=finals[i].Trim(new char[]{'{','}'}); 
            
            string[] input1=Console.ReadLine().Split();
            long n=long.Parse(input1[0]);
            string[][] rules=new string[n][];
            for (int i = 0; i < n; i++)
                rules[i]=new string[3];
            for (int i = 0; i < n; i++)
            {
                string[] input2=Console.ReadLine().Split(' ',',');
                rules[i][0]=input2[0];
                rules[i][1]=input2[1];
                rules[i][2]=input2[2];
                
            } 
            string[] input3=Console.ReadLine().Split();
            string input4=input3[0];
            char[] check=input4.ToCharArray();
            System.Console.WriteLine(Solve(states,alphabets,finals,rules,check,n));
        }
    }
}
