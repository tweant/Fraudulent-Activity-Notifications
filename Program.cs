using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{

    static int activityNotifications(int[] expenditure, int d)
    {
        var n = expenditure.Length;
        
        //Return if d is too much
        if (d + 1 > n) return 0;
        
        //Boolean if d is even
        var isEven = d % 2 == 0;
        
        //Index of median value
        var medInd = isEven ? d / 2 : (d + 1) / 2;
        
        //Notifications count
        var notCount = 0;

        //Queue for processing expendicture table
        var q = new Queue<int>();

        // We use Count Sort
        int[] count = new int[201]; 

        //Populate Count table for d elements
        for (int i = 0; i < d; i++)
        {
            count[expenditure[i]]++;
            q.Enqueue(expenditure[i]);
        }
        
        //First evaluation
        if (expenditure[d] >= 2 * findMedialVal(count, medInd, isEven)) notCount++;

        //Another evaluations
        for (int i = d + 1; i < n; i++)
        {
            count[q.Dequeue()]--;
            q.Enqueue(expenditure[i-1]);
            count[expenditure[i-1]]++;
            if (expenditure[i] >= 2 * findMedialVal(count, medInd, isEven)) notCount++;
        }
        return notCount;
    }

    //Finds median
    static double findMedialVal(int[] counts, int medInd, bool isEven)
    {
        int i = 0;
        int sum = 0;
        double val = 0;
        for (; i < counts.Length; i++)
        {
            //If we have some number to count
            if (counts[i] > 0)
            {
                sum += counts[i];
                
                //If we already are past half of numbers
                if (sum >= medInd)
                {
                    //If d is not even we don't have problem and return i
                    if (!isEven)
                    {
                        return i; 
                    }
                    else
                    {
                        //If we need to find second number for calculating median we add the first one
                        if (sum == medInd)
                        {
                            val += i;
                        }
                        else if (val == 0) //If we have more then two same values and we don't need to calculate median
                        {
                            return i; 
                        }
                        else
                        {
                            return (val + (double)i) / 2; //We found second value so we add it to calculation
                        }
                    }
                }
            }

        }
        throw new Exception();
    }

    static void Main(String[] args)
    {
        var inputs = new string[] { "input01.txt", "input02.txt","input06.txt", };
        foreach (var name in inputs)
        {
            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(name))
                {
                    // Read the stream to a string, and write the string to the console.
                    int res = Convert.ToInt32(sr.ReadLine());
                    string[] tokens_n = sr.ReadLine().Split(' ');
                    int n = Convert.ToInt32(tokens_n[0]);
                    int d = Convert.ToInt32(tokens_n[1]);
                    string[] expenditure_temp = sr.ReadLine().Split(' ');
                    int[] expenditure = Array.ConvertAll(expenditure_temp, Int32.Parse);
                    int result = activityNotifications(expenditure, d);
                    if (res == result)
                        Console.WriteLine("OK");
                    else
                        Console.WriteLine(result + " vs answer " + res);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
        }


    }
}