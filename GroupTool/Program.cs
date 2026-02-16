using System;
using System.Collections.Generic;

namespace GroupTool

/*
 * Forslag til udvidelser/andet?
 *
 * - Kunne f.eks. være noget med tidsstyring ift. individuelle opgaver.
 *
 */

    // Soren skriver lige en comment.
{
    internal class Program
    {

        // Liste til at holde groupMembers.
        public static List<string> groupMembers = new List<string>()
            {
            "Daniel",
            "Lola",
            "Søren",
            "Oskar",
            "Dzemila"
            };

        public static Random random = new Random();

        // Lister til at holde de (mindre) skabte grupper, groupOne og
        // groupTwo, når hele gruppen midlertidigt skal opdeles.
        public static List<string> groupOne = new List<string>();
        public static List<string> groupTwo = new List<string>();

        // Liste til at holde ekspeditionsgrupper vi kan blive sendt ud i.
        public static List<string> expeditionGroups = new List<string>();

        static void Main(string[] args)
        {
            ShowMenu();
        }


        /*
        * I ShowMenu() vælger man gruppe-aktivitet, dvs. enten at man skal
        * opdele sin egen gruppe i to mindre grupper (1) eller om man skal
        * sendes ud enkeltvis i andre grupper (2) for at give feedback/dele gruppens arbejde.
        */
        public static void ShowMenu()
        {

            Console.Write("\nSkriv 1 for gruppeopdeling i mindre grupper eller 2 for individuel gruppeudsendelse til andre grupper: ");

            string taskAssignment = Console.ReadLine();

            switch (taskAssignment)
            {
                case "1":
                    CreateMinorGroups();
                    ShowMinorGroups();
                    break;

                case "2":
                    CreateExpeditionGroups();
                    ShowExpeditionGroups();
                    break;

                default:
                    Console.WriteLine("Ugyldigt valg.");
                    ShowMenu(); // Denne looper tilbage til hovedmenuen, hvis valget var forkert.
                    break;

            }
        }

        /*
        * CreateMinorGroups() kører gennem listen med gruppe-medlemmer
        * ("groupMembers" fra toppen af programmet) og tilføjer dem
        * (på basis af et tilfældigt genereret tal (dvs. 1 eller 2))
        * til en mindre gruppe.
        *
        * Dette gøres helt konkret ved at tilføje dem
        * (f.eks. i gruppe 1's tilfælde: "groupOne.Add(groupMembers[i]")
        * til listen for enten "groupOne" eller "groupTwo".
        *
        */

        public static void CreateMinorGroups()
        {
            groupOne.Clear();
            groupTwo.Clear();

            for (int i = 0; i < groupMembers.Count; i++)
            {
                int randomNumber = random.Next(1, 3);

                // Hvis randomNumber er 1, så ryger personen i gruppe 1
                // (så længe at gruppen ikke er større end 3).
                if (randomNumber == 1 && groupOne.Count < 3)
                {
                    groupOne.Add(groupMembers[i]);
                }

                // Hvis randomNumber er 2, så ryger personen i gruppe 2
                // (så længe at gruppen ikke er større end 3).
                else if (randomNumber == 2 && groupTwo.Count < 3)
                {
                    groupTwo.Add(groupMembers[i]);
                }

                // Og hvis det blev forsøgt at putte en person i en fuld
                // (større end 3) gruppe på basis af randomNumber, så ryger
                // personen i den anden gruppe. Dette er evt. ikke helt så pænt ift.
                // randomisering, men virker tilfældigt nok.
                else
                {
                    if (groupOne.Count < 3)
                    {
                        groupOne.Add(groupMembers[i]);
                    }
                    else
                    {
                        groupTwo.Add(groupMembers[i]);
                    }
                }
            }

        }

        /*
        * ShowMinorGroups() viser de mindre grupper, som blev etableret
        * i CreateMinorGroups(), ved at iterere over hver string (her kaldet member)
        * i listerne "groupOne" og "groupTwo", hvorefter disse udskrives via
        * Console.WriteLine().
        */

        public static void ShowMinorGroups()
        {
            Console.WriteLine("---- Gruppe 1 ----");

            foreach (string member in groupOne)
            {
                Console.WriteLine(member);
            }

            Console.WriteLine("---- Gruppe 2 ----");

            foreach (string member in groupTwo)
            {
                Console.WriteLine(member);
            }

            ShowMenu(); // Vender tilbage til hovedmenuen.
        }

        /*
        * CreateExpeditionGroups() finder de grupper vi enkeltvis skal ud i.
        *
        * Dette ved først at oprette en (tom) liste med allerede brugte gruppemedlemmer,
        * "usedGroupMembers" til at holde op mod listen af "groupMembers" (fra toppen
        * af programmet). Derudover, så sættes "groupNumber" også til 10, fordi det er vores
        * gruppenummer.
        *
        * Dernæst køres en while-løkke (så længe at "usedGroupMembers" < "groupMembers"),
        * hvor et tilfældigt gruppemedlem udvælges via variablerne "randomGroupMember" og
        * "chosenGroupMember".
        *
        * Hvis while-løkken har fået fat i et allerede brugt medlem af gruppen
        * ("if (usedGroupMembers.Contains(chosenGroupMember))"), så genstarter løkken
        * (via "continue;") og prøver igen.
        *
        * Ellers får vedkommende tildelt en gruppe, og bliver også tilføjet til listen af
        * "usedGroupMembers" ("usedGroupMembers.Add(chosenGroupMember);"), hvormed dette
        * gruppemedlem ikke længere kan få tildelt en gruppe at gå ud i.
        *
        * Hvis det er det første medlem ("if (usedGroupMembers.Count == 1)"), så bliver
        * vedkommende fortalt at han/hun skal blive i gruppe 10 og præsentere vores arbejde.
        *
        * Samtidig forøges variablen "groupNumber" med + 1 her, hvilket danner basis for
        * at udvælge den næste gruppe, som der skal sendes et medlem ud til. (10 -> 11 -> 12, osv.)
        */

        public static void CreateExpeditionGroups()
        {

            expeditionGroups.Clear();

            List<string> usedGroupMembers = new List<string>();

            int groupNumber = 10;

            // Mens der stadig er "ubrugte" gruppemedlemmer, så findes der et
            // random tal på et gruppemedlem i listen af groupMembers (0-4, dvs.
            // 5 mulige medlemmer). Hvis dette"chosenGroupMember" allerede er
            // indeholdt i listen af "usedGroupMembers" (og dermed allerede brugt),
            // så køres while-løkken igen via "continue;".

            while (usedGroupMembers.Count < groupMembers.Count)
            {
                int randomGroupMember = random.Next(0, groupMembers.Count);
                string chosenGroupMember = groupMembers[randomGroupMember];

                if (usedGroupMembers.Contains(chosenGroupMember))
                    continue;

                // Hvis medlemmet ikke er "brugt", så tilføjes medlemmet til
                // listen af brugte gruppemedlemmer ("usedGroupMembers") nu.
                // Hvis det er første medlem ("usedGroupMembers.Count == 1")
                // tilføjes en besked til "expeditionGroups" om at medlemmet
                // skal blive i den nuværende gruppe og præsentere vores arbejde.
                // "groupNumber" forøges dernæst, så næste gruppemedlem bliver
                // sendt i den næste gruppe ("groupNumber++;").

                usedGroupMembers.Add(chosenGroupMember);

                if (usedGroupMembers.Count == 1)
                {
                    expeditionGroups.Add(chosenGroupMember + " skal blive i gruppe " + groupNumber + " og præsentere vores arbejde.");
                    groupNumber++;

                }

                // Og hvis det ikke er 1. medlem, så gemmes en besked i listen "expeditionGroups"
                // om at vedkommende skal gå til det nummer som "groupNumber" nu er forøget til og
                // give feedback/dele vores arbejde med denne gruppe.

                else
                {
                    expeditionGroups.Add(chosenGroupMember + " skal gå til gruppe " + groupNumber + " og give feedback/dele vores arbejde med denne gruppe.");
                    groupNumber++;
                }

            }

        }


        /*
        * ShowExpeditionGroups() viser de grupper vi skal ud på ekspedition i ved
        * at iterere over de strings vi gemte i listen "expeditionGroups" ovenfor.
        * Disse vises så her via Console.WriteLine().
        */

        public static void ShowExpeditionGroups()
        {

            Console.WriteLine("---- Ekspeditionsgrupper ----");

            foreach (string line in expeditionGroups)
            {
                Console.WriteLine(line);
            }

            ShowMenu(); // Vender tilbage til hovedmenuen.
        }
    }


}