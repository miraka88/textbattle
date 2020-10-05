using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;


namespace TextBattle
{
    class Program
    {



        static void Main(string[] args)
        {
            /*
            cpuHP, playerHP = 100
            normal attack to start with for simplicity, random damage(5-15 for example)
            when attack is made, damage calculated and deducted from hp
            CPU's turn next, same thing, damage calculated and deducted.

            TO DO LIST

            add furry swipes - DONE
            add a leech seed type attack - DONE

            add an attack which gives the chance for opponents next attack to miss.

            
           
            


             */


            string normAtk = "1";
            string specialAttack = "2";
            string fSwipes = "3";

            int attack = 0;
            bool playAgain = true;
            bool leechSeed = false;
            int leechSeedCount = 0;
            string inputAttack, restart = "";
            Random rnd = new Random();
          

            while (playAgain)
            {
                int cpuHP = 100;
                int PlayerHP = 100;


                while (cpuHP > 0 && PlayerHP > 0)
                {
                    battleMenu(PlayerHP, cpuHP);

                    inputAttack = Console.ReadLine();

                    if (String.IsNullOrEmpty(inputAttack))
                    {
                        Console.WriteLine("Please enter valid input");
                    }
                    else if (inputAttack == normAtk)
                    {   
                        NormalAtk(ref attack, ref cpuHP);
                    }
                    else if (inputAttack == specialAttack)
                    {
                        SpecialAtk(ref attack, ref cpuHP, ref leechSeed);
                    }
                    else if (inputAttack == fSwipes)
                    {
                        FurrySwipes(ref attack, ref cpuHP);
                    }

                
                    if (cpuHP <= 0)
                    {
                        Console.WriteLine("Enemy has been Vanquished!! you win!!\n\nDo you wish to play again?(y/n)");
                        restart = Console.ReadLine();

                        if (restart == "y")
                        {
                            playAgain = true;
                            continue;
                        }
                        else if (restart == "n")
                        {
                            playAgain = false;
                            return;
                        }

                    }

                    EnemyTurn(ref attack, ref PlayerHP);
                    if(leechSeed == true)
                    {
                        LeechSeedDamage(ref leechSeedCount, leechSeed, ref attack, ref cpuHP);
                    }

                    if (PlayerHP <= 0)

                    {
                        Console.WriteLine("You have been defeated!! you lose!!\n\nDo you wish to play again?(y/n)");

                        restart = Console.ReadLine();

                        if (restart == "y")
                        {
                            playAgain = true;
                            continue;
                        }
                        else if (restart == "n")
                        {
                            playAgain = false;
                        }
                    }
                
            }


                //LOCAL FUNCTIONS
                static void battleMenu(int pHP, int Chp)
                {

                    Console.WriteLine("\n\n\nYour HP: {0} Enemy HP: {1}\n", pHP, Chp);
                    Console.WriteLine("Select the Attack you would like to use\n");
                    Console.WriteLine("1 - Normal Attack(Deals normal amount of damage)");
                    Console.WriteLine("2 - Leech seed (Deals damage after enemies turn for 3 turns)");
                    Console.WriteLine("3 - Fury attack (attack the enemy a random number of times)\n");
                }

                static void NormalAtk(ref int attack, ref int cpuHP)

                {
                    Random rnd = new Random();
                    attack = rnd.Next(8, 12);
                    cpuHP -= attack;
                    Console.WriteLine("\nYou strike the Enemy, dealing {0} damage.\n", attack);
                    Thread.Sleep(2000);
                }

                static void SpecialAtk(ref int attack, ref int cpuHP, ref bool leechSeed)

                {
                    Random rnd = new Random();
                    attack = rnd.Next(8,10);
                    leechSeed = true;
                    Console.WriteLine("You use your Leech seed on the enemy, the seed is now attached ");
                    Thread.Sleep(2000);
                    
                }
                static void LeechSeedDamage(ref int leechSeedCount, bool leechSeed, ref int attack, ref int cpuHP)
                {
                    if(leechSeedCount < 3)
                    {
                        Random rnd = new Random();
                        attack = rnd.Next(8, 10);
                        cpuHP -= attack;
                        Console.WriteLine("Leech seed activates, dealing {0} damage to the enemy",attack);
                        leechSeedCount++;
                        Thread.Sleep(2000);
                    }
                    else
                    {
                        Console.WriteLine("Leech seed de-activates and falls off the enemy\n");
                        leechSeed = false;
                        leechSeedCount = 0;
                        Thread.Sleep(2000);
                    }
                }

                static void FurrySwipes(ref int attack, ref int cpuHP)
                {
                    Random rnd = new Random();
                    var furrySwipes = new List<int>();
                    for (int i = 0; i < rnd.Next(1,4); i++)
                    {
                        furrySwipes.Add(rnd.Next(10, 20));
                        cpuHP -= furrySwipes[i];
                        Console.WriteLine("you attack with fury, dealing {0} damage\n",furrySwipes[i]);
                        Thread.Sleep(2000);
                    }
                    Console.WriteLine("You attacked the enemy {0} times\n",furrySwipes.Count);
                }

                static void EnemyTurn(ref int attack, ref int PlayerHP)
                {
                    Random rnd = new Random();
                    attack = rnd.Next(10, 20);
                    Console.WriteLine("\nEnemy turn\n\n");
                    Thread.Sleep(3000);
                    PlayerHP -= attack;
                    Console.WriteLine("\nThe Enemy Strikes you, dealing {0} damage.\n\n", attack);
                    Thread.Sleep(2000);
                }


            }
        
        }   
    }
}
