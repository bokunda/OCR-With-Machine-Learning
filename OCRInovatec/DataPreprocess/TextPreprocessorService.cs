using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataPreprocess
{
    static class TextPreprocessorService
    {
        public static int count = 0;
        public static void convertToLower(ref string text)
        {
            text = text.ToLower();

        }

        public static void handleNums(ref string text)
        {

            string pattern = @"[0-9]+";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            // Console.WriteLine("Original string: " + input);

            text = Regex.Replace(text, pattern, "number",
                           RegexOptions.IgnoreCase);
        }

        public static void handleHTML(ref string text)
        {

            string pattern = @"<[^<>]+>";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            // Console.WriteLine("Original string: " + input);

            text = Regex.Replace(text, pattern, " ",
                           RegexOptions.IgnoreCase);
        }

        public static void handleURL(ref string text)
        {

            //string pattern = @"(http|https)://[^\s]*";
            string pattern = @"(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9]\.[^\s]{2,})";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            // Console.WriteLine("Original string: " + input);

            text = Regex.Replace(text, pattern, "httpaddr",
                           RegexOptions.IgnoreCase);
        }


        public static void handleEmail(ref string text)
        {

            string pattern = @"[^\s]+@[^\s]+";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            // Console.WriteLine("Original string: " + input);

            text = Regex.Replace(text, pattern, "emailaddr",
                           RegexOptions.IgnoreCase);
        }

        public static void handleDollarSigns(ref string text)
        {

            string pattern = @"[$]+";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            // Console.WriteLine("Original string: " + input);

            text = Regex.Replace(text, pattern, "dollar",
                           RegexOptions.IgnoreCase);
        }



        public static void handleEuroSigns(ref string text)
        {

            string pattern = @"[€]+";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            // Console.WriteLine("Original string: " + input);

            text = Regex.Replace(text, pattern, "euro",
                           RegexOptions.IgnoreCase);
        }


        //public static void handleTokens(ref string text)
        //{

        //    string pattern = @" @$/#.-:&*+=[]?!(){},''" > _ <;% ";
        //    Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
        //    // Console.WriteLine("Original string: " + input);

        //    text = Regex.Replace(text, pattern, "",
        //                   RegexOptions.IgnoreCase);
        //}

        public static void handleNonAlnum(ref string text)
        {

            string pattern = @"[^a-zA-Z0-9]";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            // Console.WriteLine("Original string: " + input);

            text = Regex.Replace(text, pattern, " ",
                           RegexOptions.IgnoreCase);


        }
        public static string[] trimSpaces(ref string text)
        {
            // words = "this is a list of words, with: a bit of punctuation.";
            string[] textWords = text.Split(new Char[] { ' ', ',', '.', ':' });
            return textWords;

        }


        public static void removeMultiSpace(ref string text)
        {
            text = Regex.Replace(text, @"\s+", " ");
        }


        public static bool checkIsStopWord(string text)
        {
            bool retVal = false;
            // List<string> englishStopWords = new List<string>() { "a", "about", "above", "across", "after", "again", "against", "all", "almost", "alone", "along", "already", "also", "although", "always", "among", "an", "and", "another", "any", "anybody", "anyone", "anything", "anywhere", "are", "area", "areas", "around", "as", "ask", "asked", "asking", "asks", "at", "away", "b", "back", "backed", "backing", "backs", "be", "became", "because", "become", "becomes", "been", "before", "began", "behind", "being", "beings", "best", "better", "between", "big", "both", "but", "by", "c", "came", "can", "cannot", "case", "cases", "certain", "certainly", "clear", "clearly", "come", "could", "d", "did", "differ", "different", "differently", "do", "does", "done", "down", "down", "downed", "downing", "downs", "during", "e", "each", "early", "either", "end", "ended", "ending", "ends", "enough", "even", "evenly", "ever", "every", "everybody", "everyone", "everything", "everywhere", "f", "face", "faces", "fact", "facts", "far", "felt", "few", "find", "finds", "first", "for", "four", "from", "full", "fully", "further", "furthered", "furthering", "furthers", "g", "gave", "general", "generally", "get", "gets", "give", "given", "gives", "go", "going", "good", "goods", "got", "great", "greater", "greatest", "group", "grouped", "grouping", "groups", "h", "had", "has", "have", "having", "he", "her", "here", "herself", "high", "high", "high", "higher", "highest", "him", "himself", "his", "how", "however", "i", "if", "important", "in", "interest", "interested", "interesting", "interests", "into", "is", "it", "its", "itself", "j", "just", "k", "keep", "keeps", "kind", "knew", "know", "known", "knows", "l", "large", "largely", "last", "later", "latest", "least", "less", "let", "lets", "like", "likely", "long", "longer", "longest", "m", "made", "make", "making", "man", "many", "may", "me", "member", "members", "men", "might", "more", "most", "mostly", "mr", "mrs", "much", "must", "my", "myself", "n", "necessary", "need", "needed", "needing", "needs", "never", "new", "new", "newer", "newest", "next", "no", "nobody", "non", "noone", "not", "nothing", "now", "nowhere", "o", "of", "off", "often", "old", "older", "oldest", "on", "once", "one", "only", "open", "opened", "opening", "opens", "or", "order", "ordered", "ordering", "orders", "other", "others", "our", "out", "over", "p", "part", "parted", "parting", "parts", "per", "perhaps", "place", "places", "point", "pointed", "pointing", "points", "possible", "present", "presented", "presenting", "presents", "problem", "problems", "put", "puts", "q", "quite", "r", "rather", "really", "right", "right", "room", "rooms", "s", "said", "same", "saw", "say", "says", "second", "seconds", "see", "seem", "seemed", "seeming", "seems", "sees", "several", "shall", "she", "should", "show", "showed", "showing", "shows", "side", "sides", "since", "small", "smaller", "smallest", "so", "some", "somebody", "someone", "something", "somewhere", "state", "states", "still", "still", "such", "sure", "t", "take", "taken", "than", "that", "the", "their", "them", "then", "there", "therefore", "these", "they", "thing", "things", "think", "thinks", "this", "those", "though", "thought", "thoughts", "three", "through", "thus", "to", "today", "together", "too", "took", "toward", "turn", "turned", "turning", "turns", "two", "u", "under", "until", "up", "upon", "us", "use", "used", "uses", "v", "very", "w", "want", "wanted", "wanting", "wants", "was", "way", "ways", "we", "well", "wells", "went", "were", "what", "when", "where", "whether", "which", "while", "who", "whole", "whose", "why", "will", "with", "within", "without", "work", "worked", "working", "works", "would", "x", "y", "year", "years", "yet", "you", "young", "younger", "youngest", "your", "yours", "z" };
            //List<string> frenchStopWords = new List<string>() { "a", "ai", "aie", "aient", "aies", "ait", "alors", "as", "au", "aucun", "aura", "aurai", "auraient", "aurais", "aurait", "auras", "aurez", "auriez", "aurions", "aurons", "auront", "aussi", "autre", "aux", "avaient", "avais", "avait", "avant", "avec", "avez", "aviez", "avions", "avoir", "avons", "ayant", "ayez", "ayons", "bon", "car", "ce", "ceci", "cela", "ces", "cet", "cette", "ceux", "chaque", "ci", "comme", "comment", "d", "dans", "de", "dedans", "dehors", "depuis", "des", "deux", "devoir", "devrait", "devrez", "devriez", "devrions", "devrons", "devront", "dois", "doit", "donc", "dos", "droite", "du", "d\u00c3\u00a8s", "d\u00c3\u00a9but", "d\u00c3\u00b9", "elle", "elles", "en", "encore", "es", "est", "et", "eu", "eue", "eues", "eurent", "eus", "eusse", "eussent", "eusses", "eussiez", "eussions", "eut", "eux", "e\u00c3\u00bbmes", "e\u00c3\u00bbt", "e\u00c3\u00bbtes", "faire", "fais", "faisez", "fait", "faites", "fois", "font", "force", "furent", "fus", "fusse", "fussent", "fusses", "fussiez", "fussions", "fut", "f\u00c3\u00bbmes", "f\u00c3\u00bbt", "f\u00c3\u00bbtes", "haut", "hors", "ici", "il", "ils", "j", "je", "juste", "l", "la", "le", "les", "leur", "leurs", "lui", "l\u00c3\u00a0", "m", "ma", "maintenant", "mais", "me", "mes", "moi", "moins", "mon", "mot", "m\u00c3\u00aame", "n", "ne", "ni", "nom", "nomm\u00c3\u00a9", "nomm\u00c3\u00a9e", "nomm\u00c3\u00a9s", "nos", "notre", "nous", "nouveau", "nouveaux", "on", "ont", "ou", "o\u00c3\u00b9", "par", "parce", "parole", "pas", "personne", "personnes", "peu", "peut", "plupart", "pour", "pourquoi", "qu", "quand", "que", "quel", "quelle", "quelles", "quels", "qui", "sa", "sans", "se", "sera", "serai", "seraient", "serais", "serait", "seras", "serez", "seriez", "serions", "serons", "seront", "ses", "seulement", "si", "sien", "soi", "soient", "sois", "soit", "sommes", "son", "sont", "sous", "soyez", "soyons", "suis", "sujet", "sur", "t", "ta", "tandis", "te", "tellement", "tels", "tes", "toi", "ton", "tous", "tout", "trop", "tr\u00c3\u00a8s", "tu", "un", "une", "valeur", "voient", "vois", "voit", "vont", "vos", "votre", "vous", "vu", "y", "\u00c3\u00a0", "\u00c3\u00a7a", "\u00c3\u00a9taient", "\u00c3\u00a9tais", "\u00c3\u00a9tait", "\u00c3\u00a9tant", "\u00c3\u00a9tat", "\u00c3\u00a9tiez", "\u00c3\u00a9tions", "\u00c3\u00a9t\u00c3\u00a9", "\u00c3\u00a9t\u00c3\u00a9s", "\u00c3\u00aates", "\u00c3\u00aatre" };
            //Console.WriteLine(englishStopWords.Length);
            List<string> stopWords = new List<string>() { "a", "about", "above", "across", "after", "again", "against", "all", "almost", "alone", "along", "already", "also", "although", "always", "among", "an", "and", "another", "any", "anybody", "anyone", "anything", "anywhere", "are", "area", "areas", "around", "as", "ask", "asked", "asking", "asks", "at", "away", "b", "back", "backed", "backing", "backs", "be", "became", "because", "become", "becomes", "been", "before", "began", "behind", "being", "beings", "best", "better", "between", "big", "both", "but", "by", "c", "came", "can", "cannot", "case", "cases", "certain", "certainly", "clear", "clearly", "come", "could", "d", "did", "differ", "different", "differently", "do", "does", "done", "down", "down", "downed", "downing", "downs", "during", "e", "each", "early", "either", "end", "ended", "ending", "ends", "enough", "even", "evenly", "ever", "every", "everybody", "everyone", "everything", "everywhere", "f", "face", "faces", "fact", "facts", "far", "felt", "few", "find", "finds", "first", "for", "four", "from", "full", "fully", "further", "furthered", "furthering", "furthers", "g", "gave", "general", "generally", "get", "gets", "give", "given", "gives", "go", "going", "good", "goods", "got", "great", "greater", "greatest", "group", "grouped", "grouping", "groups", "h", "had", "has", "have", "having", "he", "her", "here", "herself", "high", "high", "high", "higher", "highest", "him", "himself", "his", "how", "however", "i", "ie", "if", "important", "in", "interest", "interested", "interesting", "interests", "into", "is", "it", "its", "itself", "j", "just", "k", "keep", "keeps", "kind", "knew", "know", "known", "knows", "l", "large", "largely", "last", "later", "latest", "least", "less", "let", "lets", "like", "likely", "long", "longer", "longest", "m", "made", "make", "making", "man", "many", "may", "me", "member", "members", "men", "might", "more", "most", "mostly", "mr", "mrs", "much", "must", "my", "myself", "n", "necessary", "need", "needed", "needing", "needs", "never", "new", "new", "newer", "newest", "next", "no", "nobody", "non", "noone", "not", "nothing", "now", "nowhere", "o", "of", "off", "often", "old", "older", "oldest", "on", "once", "one", "only", "open", "opened", "opening", "opens", "or", "order", "ordered", "ordering", "orders", "other", "others", "our", "out", "over", "p", "part", "parted", "parting", "parts", "per", "perhaps", "place", "places", "point", "pointed", "pointing", "points", "possible", "present", "presented", "presenting", "presents", "problem", "problems", "put", "puts", "q", "quite", "r", "rather", "really", "right", "right", "room", "rooms", "s", "said", "same", "saw", "say", "says", "second", "seconds", "see", "seem", "seemed", "seeming", "seems", "sees", "several", "shall", "she", "should", "show", "showed", "showing", "shows", "side", "sides", "since", "small", "smaller", "smallest", "so", "some", "somebody", "someone", "something", "somewhere", "state", "states", "still", "still", "such", "sure", "t", "take", "taken", "than", "that", "the", "their", "them", "then", "there", "therefore", "these", "they", "thing", "things", "think", "thinks", "this", "those", "though", "thought", "thoughts", "three", "through", "thus", "to", "today", "together", "too", "took", "toward", "turn", "turned", "turning", "turns", "two", "u", "under", "until", "up", "upon", "us", "use", "used", "uses", "v", "very", "w", "want", "wanted", "wanting", "wants", "was", "way", "ways", "we", "well", "wells", "went", "were", "what", "when", "where", "whether", "which", "while", "who", "whole", "whose", "why", "will", "with", "within", "without", "work", "worked", "working", "works", "would", "x", "y", "year", "years", "yet", "you", "young", "younger", "youngest", "your", "yours", "z", "ai", "aie", "aient", "aies", "ait", "alors", "au", "aucun", "aura", "aurai", "auraient", "aurais", "aurait", "auras", "aurez", "auriez", "aurions", "aurons", "auront", "aussi", "autre", "aux", "avaient", "avais", "avait", "avant", "avec", "avez", "aviez", "avions", "avoir", "avons", "ayant", "ayez", "ayons", "bon", "car", "ce", "ceci", "cela", "ces", "cet", "cette", "ceux", "chaque", "ci", "comme", "comment", "dans", "de", "dedans", "dehors", "depuis", "des", "deux", "devoir", "devrait", "devrez", "devriez", "devrions", "devrons", "devront", "dois", "doit", "donc", "dos", "droite", "du", "d\u00c3\u00a8s", "d\u00c3\u00a9but", "d\u00c3\u00b9", "elle", "elles", "en", "encore", "es", "est", "et", "eu", "eue", "eues", "eurent", "eus", "eusse", "eussent", "eusses", "eussiez", "eussions", "eut", "eux", "e\u00c3\u00bbmes", "e\u00c3\u00bbt", "e\u00c3\u00bbtes", "faire", "fais", "faisez", "fait", "faites", "fois", "font", "force", "furent", "fus", "fusse", "fussent", "fusses", "fussiez", "fussions", "fut", "f\u00c3\u00bbmes", "f\u00c3\u00bbt", "f\u00c3\u00bbtes", "haut", "hors", "ici", "il", "ils", "je", "juste", "la", "le", "les", "leur", "leurs", "lui", "l\u00c3\u00a0", "ma", "maintenant", "mais", "mes", "moi", "moins", "mon", "mot", "m\u00c3\u00aame", "ne", "ni", "nom", "nomm\u00c3\u00a9", "nomm\u00c3\u00a9e", "nomm\u00c3\u00a9s", "nos", "notre", "nous", "nouveau", "nouveaux", "ont", "ou", "o\u00c3\u00b9", "par", "parce", "parole", "pas", "personne", "personnes", "peu", "peut", "plupart", "pour", "pourquoi", "qu", "quand", "que", "quel", "quelle", "quelles", "quels", "qui", "sa", "sans", "se", "sera", "serai", "seraient", "serais", "serait", "seras", "serez", "seriez", "serions", "serons", "seront", "ses", "seulement", "si", "sien", "soi", "soient", "sois", "soit", "sommes", "son", "sont", "sous", "soyez", "soyons", "suis", "sujet", "sur", "ta", "tandis", "te", "tellement", "tels", "tes", "toi", "ton", "tous", "tout", "trop", "tr\u00c3\u00a8s", "tu", "un", "une", "valeur", "voient", "vois", "voit", "vont", "vos", "votre", "vous", "vu", "\u00c3\u00a0", "\u00c3\u00a7a", "\u00c3\u00a9taient", "\u00c3\u00a9tais", "\u00c3\u00a9tait", "\u00c3\u00a9tant", "\u00c3\u00a9tat", "\u00c3\u00a9tiez", "\u00c3\u00a9tions", "\u00c3\u00a9t\u00c3\u00a9", "\u00c3\u00a9t\u00c3\u00a9s", "\u00c3\u00aates", "\u00c3\u00aatre", "aa", "bb", "cc", "dd", "ee", "ff", "gg", "hh", "ii", "jj", "kk", "ll", "mm", "nn", "oo", "pp", "qq", "rr", "ss", "tt", "uu", "vv", "ww", "xx", "yy", "zz" };


            if (stopWords.Contains(text))
            {
                retVal = true;

            }
            return retVal;
        }

        public static string parseJSONText(string jsonText)
        {

            //Console.WriteLine("PROCESIRANI JSON: " + TextPreprocessorService.ProcessText(ref jsonText));
            //string parsedText = jsonText.Substring(1, jsonText.Length-2);

            //System.Diagnostics.Debug.WriteLine("Parsirani tekst: " + parsedText);
            //// 
            dynamic json = JsonConvert.DeserializeObject(@jsonText);
            StringBuilder sb = new StringBuilder();
            //string text = JsonConvert.SerializeObject(json.text);
            foreach (JObject item in json)
            {
                System.Diagnostics.Debug.WriteLine(item.GetValue("Text").ToString());
                sb.Append(item.GetValue("Text").ToString());
                //string url = item.GetValue("url").ToString();
                // ...
            }

            //string[] words = parsedText.Split(':');
            //foreach (string word in words)
            //{

            //    try
            //    {
            //        string[] newWords = word.Split(',');
            //        foreach(string newWord in newWords)
            //            FileIO.CSVWrite(word, 1, System.Web.HttpContext.Current.Server.MapPath("~/Data/ex.txt"));
            //    }
            //    catch (Exception ex)
            //    {
            //        System.Diagnostics.Debug.WriteLine(ex.Message);
            //    }
            //    //    //dynamic json = JsonConvert.DeserializeObject(word);
            //    //    //JObject json = JObject.Parse(word);
            //    //    //string[] parsedWords = word.Split("Coords");
            //    //    System.Diagnostics.Debug.WriteLine(word);
            //}
            //return parsedText; 
            return sb.ToString();
        }
        public static string ProcessText(ref string text)
        {

            convertToLower(ref text);
            handleNums(ref text);
            Console.WriteLine("Iz processa: " + text);
            handleHTML(ref text);
            handleEmail(ref text);
            handleURL(ref text);
            handleDollarSigns(ref text);
            handleEuroSigns(ref text);
            handleNonAlnum(ref text);
            removeMultiSpace(ref text);
            string[] words = trimSpaces(ref text);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < words.Length - 1; i++)
            {

                //Console.WriteLine("Ispis pre: " + word);
                if (words[i].Length < 3 && !words[i].Equals("id"))
                    continue;
                if (!TextPreprocessorService.checkIsStopWord(words[i]))
                {
                    sb.Append(words[i] + " ");
                    count++;
                }
            }

            if (!TextPreprocessorService.checkIsStopWord(words[words.Length - 1]))
            {
                sb.Append(words[words.Length - 1]);
                count++;
            }
            System.Diagnostics.Debug.WriteLine("Ovo je isprocesirani tekst: " + sb.ToString());
            return sb.ToString();


        }


        public static void clearCount()
        {
            count = 0;
        }



    }










}


/*
 * 
 * 
 * 


% Lower case
email_contents = lower(email_contents);

% Strip all HTML
% Looks for any expression that starts with < and ends with > and replace
% and does not have any < or > in the tag it with a space
email_contents = regexprep(email_contents, '<[^<>]+>', ' ');

% Handle Numbers
% Look for one or more characters between 0-9
email_contents = regexprep(email_contents, '[0-9]+', 'number');

% Handle URLS
% Look for strings starting with http:// or https://
email_contents = regexprep(email_contents, ...
                       '(http|https)://[^\s]*', 'httpaddr');

% Handle Email Addresses
% Look for strings with @ in the middle
email_contents = regexprep(email_contents, '[^\s]+@[^\s]+', 'emailaddr');

% Handle $ sign
email_contents = regexprep(email_contents, '[$]+', 'dollar');


% ========================== Tokenize Text ===========================

% Output the email to screen as well
fprintf('\n==== Processed Text ====\n\n');

% Process file
l = 0;

while ~isempty(email_contents)

% Tokenize and also get rid of any punctuation
[str, email_contents] = ...
   strtok(email_contents, ...
          [' @$/#.-:&*+=[]?!(){},''">_<;%' char(10) char(13)]);

% Remove any non alphanumeric characters
str = regexprep(str, '[^a-zA-Z0-9]', '');

% Stem the word 
% (the porterStemmer sometimes has issues, so we use a try catch block)
%try str = porterStemmer(strtrim(str)); 
%catch str = ''; continue;
%end;

% Skip the word if it is too short
if length(str) < 1
   continue;
end
if(strcmp(str_content,""))
  str_content = str;
 else
  str_content = cstrcat(str_content," ",str);
end
 * */

