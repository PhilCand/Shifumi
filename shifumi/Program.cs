using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace CSharp_Shell
{

    public static class Program
    {
        public static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("           !SHI FU MI!");
            Console.WriteLine("************************************");
            Console.ResetColor();
            string playerChoice = "";
            bool retry = true;
            string[] shifumi = { "pierre", "feuille", "ciseaux" };                  // tableau pour choix aleatoire de l'IA
            int score = 0;
            int highScore = 0;
            string playerName = "";
            string bestPlayer = "";
            Random rnd = new Random();
   

            if (File.Exists("high_score.txt"))                                    // si le fichier de score existe afficher le meilleur score
            {
                string lireScore = File.ReadAllText("high_score.txt");
                string[] highScores = lireScore.Split(';');
                highScore = int.Parse(highScores[0]);
                bestPlayer = highScores[1];
                Console.WriteLine();
                Console.WriteLine($"Le meilleur score est de {highScore} par {bestPlayer}.");
                Console.WriteLine();
            }

            Console.WriteLine();                                                 // entrer le nom du joueur
            Console.Write("Entrez votre nom : ");
            playerName = Console.ReadLine().ToUpper();
            Console.WriteLine();

            while (retry)
            {
                string IAchoice = shifumi[rnd.Next(0, 3)];                         // genere le choix de l'IA

                while (true)                                                       // demande le choix du joueur
                {
                    Console.Write("Entrez pierre, feuille ou ciseaux : ");
                    playerChoice = Console.ReadLine().ToLower();


                    if ((playerChoice == "pierre") || (playerChoice == "feuille") || (playerChoice == "ciseaux"))
                        break;
                }
                Console.WriteLine();
                Console.WriteLine("Vous avez choisi " + playerChoice);
                Console.WriteLine("votre adversaire a choisi " + IAchoice);

                if (playerChoice == IAchoice)                                          // isolation de l'egalité
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine();
                    Console.WriteLine("Egalité !");
                    Console.WriteLine();
                    Console.ResetColor();
                }

                else if ((playerChoice == "ciseaux") && (IAchoice == "feuille")) win(); //les cas ou le joueur gagne

                else if ((playerChoice == "pierre") && (IAchoice == "ciseaux")) win();

                else if ((playerChoice == "feuille") && (IAchoice == "pierre")) win();


                else                                                                    // sinon il perd
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("Vous avez perdu :(");
                    Console.WriteLine();
                    Console.ResetColor();
                    score--;
                }
                
                Console.WriteLine("score : " + score);

                while (true)                                                            // choix de recommencer
                {
                    Console.WriteLine();
                    Console.Write("Continuer ? O/N : ");
                    string next = Console.ReadLine();
                    Console.WriteLine();
                    if (next.ToLower() == "o") break;
                    if (next.ToLower() == "n")
                    {

                        if (score > highScore)                                          // si le score est battu l'insrire dans le fichier
                        {
                            string lireScore = score + ";" + playerName;
                            File.WriteAllText("high_score.txt", lireScore);
                            Console.WriteLine();
                            Console.WriteLine($"BRAVO ! Votre score est de {score}, vous avez battu le meilleur score !");
                            Console.WriteLine();
                            Console.WriteLine("A bientot.");
                            Console.ReadKey();
                            retry = false;
                            break;
                        }

                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine($"Votre score est de {score}, au revoir.");
                            Console.WriteLine();
                            Console.ReadKey();
                            retry = false;
                            break;
                        }
                    }
                }
            }

            void win()                                                                // methode pour le message "gagner" pour meilleur lisibilité
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();
                Console.WriteLine("Vous avez gagné !");
                Console.WriteLine();
                Console.ResetColor();
                score++;
            }
        }
    }
}