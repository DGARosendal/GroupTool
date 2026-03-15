using System;
using System.Collections.Generic;

namespace GroupTool

/*
 * Forslag til udvidelser/andet?
 *
 * - Kunne f.eks. være noget med tidsstyring ift. individuelle opgaver.
 */

/*
 * Globale variabler at justere på (lige nedenunder):
 * 1) Den globale liste med gruppemedlemmerne, "groupMembers"
 * 2) "static int groupNumber = 10;", såfremt gruppenummeret ændres. Jeg har ændret denne til en global variabel for nemmere overblik.
 * 
 */

// Soren skriver lige en comment.
// Daniel skriver lige endnu en comment :)
// Lola skriver lige endnu endnu en comment. :P
// Dzemila skriver en ekstra comment til folket :D
// Hej Hej
// YAY ALT VIRKER!
{
    internal class Program
    {

        // Liste til at holde groupMembers.
        static List<string> groupMembers = new List<string>()
            {
            // 2. gruppe (nyeste): 
            "Daniel",
            "Søren",
            "Benjamin",
            "Selahaddini (Sela)",
            "Blessing"
            };
        // 1. gruppe: 
        //"Daniel",
        //"Lola",
        //"Søren",
        //"Oskar",
        //"Dzemila"


        // "groupNumber" skal evt. ændres, såfremt gruppenummeret ændres. Ændret til en global variabel (static).
        static int groupNumber = 10;

        //Liste til at holde en opdateret gruppeliste ifm case 3, i tilfælde af f.eks. fravær (groupMembers < 5). 
        static List<string> backupGroupMembers = new List<string>();

        static Random random = new Random();

        // Lister til at holde de (mindre) skabte grupper, groupOne og
        // groupTwo, når hele gruppen midlertidigt skal opdeles.
        static List<string> groupOne = new List<string>();
        static List<string> groupTwo = new List<string>();

        // Liste til at holde ekspeditionsgrupper vi kan blive sendt ud i.
        static List<string> expeditionGroups = new List<string>();

        static void Main(string[] args)
        {
            ShowMenu();
        }


        /*
        * I ShowMenu() vælger man gruppe-aktivitet, dvs. enten at man skal
        * opdele sin egen gruppe i to mindre grupper (case 1), om man skal
        * sendes ud enkeltvis i andre grupper (case 2) for at give feedback/dele gruppens 
        * arbejde, eller (case 3) at man skal foretage en midlertid justering i gruppens medlemmer. 
        */
        public static void ShowMenu()
        {

            Console.ForegroundColor = ConsoleColor.Green; // Sætter farven i konsollen til grøn, kan evt. nulstilles via "Console.ResetColor();" senere. 

            while (true) // Bliver ved med at køre indtil den brydes af enten break; eller return; 
            {
            
            Console.Write("Skriv 1 for gruppeopdeling i mindre grupper eller 2 for individuel gruppeudsendelse til andre grupper. \nAlternativt kan du skrive 3, hvis du skal modificere listen af medlemmer: ");

            string taskAssignment = Console.ReadLine();

                switch (taskAssignment)
                {
                    case "1":
                        CreateMinorGroups();
                        ShowMinorGroups();
                        break; // break; bryder løkken

                    case "2":
                        CreateExpeditionGroups();
                        ShowExpeditionGroups();
                        break; // break; bryder løkken

                    case "3":
                        // Metode til (midlertidigt) at opdatere gruppelisten i tilfælde af fravær.
                        UpdateGroupList();
                        break; // break; bryder løkken

                    default:
                        Console.WriteLine("Ugyldigt valg.");
                        Console.WriteLine(""); //Tom linje før menuen vises, for at øge læsbarheden.
                        // ShowMenu(); // Denne looper tilbage til hovedmenuen, hvis valget var forkert. ==> Erstattet af while-løkken ift. menuen.
                        break; // break; bryder løkken
                }
            }
        }

        /*
        * CreateMinorGroups() udvælger først enten "backupGroupMembers" (f.eks. ved fravær)
        * eller bare "groupMembers" som sourceList for de tilstedeværende gruppe-medlemmer
        * På basis af et tilfældigt genereret tal (dvs. 1 eller 2)), så tilføjes disse 
        * til en mindre gruppe (dvs. case 1).
        *
        * Dette gøres helt konkret ved at tilføje dem
        * (f.eks. i gruppe 1's tilfælde: "groupOne.Add(sourceList[i]")
        * til listen for enten "groupOne" eller "groupTwo".
        *
        */

        public static void CreateMinorGroups()
        {
            groupOne.Clear();
            groupTwo.Clear();

            // ternary expression brugt til at opsætte en "sourceList" indeholdende enten "backupGroupMembers"
            // (hvis denne liste ikke er tom), ellers bruges standard-listen af "groupMembers".

            // Ternary expression: Hvis "backupGroupMembers.Count" er større end 0 (dvs. eksisterer), 
            // så vælges "backupGroupMembers" som sourceList, men ellers vælges "groupMembers" som sourceList.
            List<string> sourceList =
                (backupGroupMembers.Count > 0)
                 ? backupGroupMembers
                 : groupMembers;

            // maxGroupSize finder den maksimale gruppestørrelse ift. medlemmer
            // (da gruppens medlemstal kan ændre sig dynamisk i sourceList) på flg. maner:
            // antallet i sourceList (sourcelist.Count) divideres med 2.0 (fordi vi kan
            // få double værdier). Math.Ceiling runder så dette tal op til nærmeste heltal (dvs. int).
            // F.eks. hvis der er 3 gruppemedlemmer, som skal deles i 2 grupper, så har vi 3/2 = 1,5, hvormed
            // Math.Ceiling så runder dette tal op til 2. Dermed kan der ved fordeling af 3
            // gruppemedlemmer i 2 forskellige grupper maks være 2 i gruppen. 
            // Ved en sourceList på 4, så har vi 4 / 2.0 = 2 (igen), dvs. igen maks 2 i grupperne.
            // Og ved en sourceList på 5, så har vi 5 / 2.0 = 2,5, som Math.Ceiling så runder op
            // til nærmeste heltal, 3. Maks gruppestørrelse bliver dermed 3, hvis der er 5 
            // gruppemedlemmer til stede.

            int maxGroupSize = (int)Math.Ceiling(sourceList.Count / 2.0);

            for (int i = 0; i < sourceList.Count; i++)
            {
                int randomNumber = random.Next(1, 3);

                // Hvis randomNumber er 1, så ryger personen i gruppe 1
                // (så længe at gruppen ikke er større end maxGroupSize, som vi fandt ovenfor).
                if (randomNumber == 1 && groupOne.Count < maxGroupSize)
                {
                    groupOne.Add(sourceList[i]);
                }

                // Hvis randomNumber er 2, så ryger personen i gruppe 2
                // (så længe at gruppen ikke er større end maxGroupSize, som vi fandt ovenfor).
                else if (randomNumber == 2 && groupTwo.Count < maxGroupSize)
                {
                    groupTwo.Add(sourceList[i]);
                }

                // Og hvis det blev forsøgt at putte en person i en fuld
                // (større end maxGroupSize) gruppe på basis af randomNumber, så ryger
                // personen i den anden gruppe. Dette er evt. ikke helt så pænt ift.
                // randomisering, men virker tilfældigt nok.
                else
                {
                    if (groupOne.Count < maxGroupSize)
                    {
                        groupOne.Add(sourceList[i]);
                    }
                    else
                    {
                        groupTwo.Add(sourceList[i]);
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

            Console.WriteLine(""); //Tom linje før menuen vises, for at øge læsbarheden.
          //ShowMenu(); // Vender tilbage til hovedmenuen. ==> Når en case er færdig, vender koden automatisk tilbage til starten af while(true) i HovedMenuen og viser menuen igen, derfor redundant nu.
        }

        /*
        * CreateExpeditionGroups() finder de grupper vi enkeltvis skal ud i (case 2).
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

            int localGroupNumber = groupNumber; // Gruppens nummer erklæres som en lokal variabel (fra den globale variabel) for at sikre at der ikke tælles op uhensigtsmæssigt.

            // Mens der stadig er "ubrugte" gruppemedlemmer, så findes der et
            // random tal på et gruppemedlem i listen af groupMembers (0-4, dvs.
            // 5 mulige medlemmer). Hvis dette"chosenGroupMember" allerede er
            // indeholdt i listen af "usedGroupMembers" (og dermed allerede brugt),
            // så køres while-løkken igen via "continue;".

            // Opdateret til at benytte sourceList i stedet for groupMembers som kilde til groupMembers.

            // ternary expression brugt til at opsætte en "sourceList" indeholdende enten "backupGroupMembers"
            // (hvis denne liste ikke er tom), ellers bruges standard-listen af "groupMembers".

            // Ternary expression: Hvis "backupGroupMembers.Count" er større end 0 (dvs. eksisterer), 
            // så vælges "backupGroupMembers" som sourceList, men ellers vælges "groupMembers" som sourceList.
            List<string> sourceList =
                (backupGroupMembers.Count > 0)
                 ? backupGroupMembers
                 : groupMembers;

            while (usedGroupMembers.Count < sourceList.Count)
            {
                int randomGroupMember = random.Next(0, sourceList.Count);
                string chosenGroupMember = sourceList[randomGroupMember];

                if (usedGroupMembers.Contains(chosenGroupMember))
                    continue;

                // Hvis medlemmet ikke er "brugt", så tilføjes medlemmet til
                // listen af brugte gruppemedlemmer ("usedGroupMembers") nu.
                // Hvis det er første medlem ("usedGroupMembers.Count == 1")
                // tilføjes en besked til "expeditionGroups" om at medlemmet
                // skal blive i den nuværende gruppe og præsentere vores arbejde.
                // "localGroupNumber" forøges dernæst, så næste gruppemedlem bliver
                // sendt i den næste gruppe ("localGroupNumber++;").

                usedGroupMembers.Add(chosenGroupMember);

                if (usedGroupMembers.Count == 1)
                {
                    expeditionGroups.Add(chosenGroupMember + " skal blive i gruppe " + localGroupNumber + " og præsentere vores arbejde.");
                    localGroupNumber++;

                }

                // Og hvis det ikke er 1. medlem, så gemmes en besked i listen "expeditionGroups"
                // om at vedkommende skal gå til det nummer som "localGroupNumber" nu er forøget til og
                // give feedback/dele vores arbejde med denne gruppe.

                else
                {
                    expeditionGroups.Add(chosenGroupMember + " skal gå til gruppe " + localGroupNumber + " og give feedback/dele vores arbejde med denne gruppe.");
                    localGroupNumber++;
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

            Console.WriteLine(""); //Tom linje før menuen vises, for at øge læsbarheden.
          //ShowMenu(); // Vender tilbage til hovedmenuen. ==> Når en case er færdig, vender koden automatisk tilbage til starten af while(true) i HovedMenuen og viser menuen igen, derfor redundant nu.
        }


        /*
         * Denne metode (case 3) tager en string som input fra brugeren ift. hvilke gruppemedlemmer
         * der er til stede. Dernæst skiller den disse ad ved at splitte stringen via ".Split(',');"
         * og tilføjer dernæst disse til listen "backupGroupMembers" med 
         * individuelle pladser på listen, der videre kan bearbejdes i de andre metoder. 
         */
        public static void UpdateGroupList()
        {
            backupGroupMembers.Clear();

            string holdBackupGroupMembers;

            Console.WriteLine("Skriv de medlemmer der er til stede (adskil navnene med \",\")"); // Brugen af "\" her er en escape-character, der gør at " betragtes som en del af stringen og ikke afslutningen på den.

            holdBackupGroupMembers = Console.ReadLine();

            // Tjek for om string er null eller bare tomme pladser  (i.e. fejlindtastning). 
            if (string.IsNullOrWhiteSpace(holdBackupGroupMembers))
            {
                Console.WriteLine("Ingen navne indtastet. Vender tilbage til hovedmenuen (og den fulde medlemsliste af gruppen benyttes).\n");
                // ShowMenu(); // Vi bliver sendt tilbage til hovedmenuen ==> Når en case er færdig, vender koden automatisk tilbage til starten af while(true) i HovedMenuen og viser menuen igen.
                return; // Og "return;" stopper metoden nedenfor i at fortsætte videre. 
            }


            // Denne tager stringen og splitter den op på basis af ',', så vi får isoleret navnene.
            string[] namesOfBackupGroupMembers = holdBackupGroupMembers.Split(',');

            // Og for hvert navn fundet, tilføjer vi det til listen af "backupGroupMembers", hvor vi trimmer white spaces fra strengen.
            foreach(string name in namesOfBackupGroupMembers)
            {
                backupGroupMembers.Add(name.Trim());
            }

            Console.WriteLine("\nMedlemmerne, der er til stede og benyttes som gruppe i dag, er således: ");

            foreach(string name in backupGroupMembers)
            {
                Console.WriteLine(name);    
            }

            Console.WriteLine(""); //Tom linje før menuen vises, for at øge læsbarheden.
            //ShowMenu(); // Vender tilbage til hovedmenuen. ==> Når en case er færdig, vender koden automatisk tilbage til starten af while(true) i HovedMenuen og viser menuen igen, derfor redundant nu. 
        }

    }
}