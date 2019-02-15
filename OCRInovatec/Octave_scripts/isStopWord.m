## Copyright (C) 2018 inovatec-user
## 
## This program is free software: you can redistribute it and/or modify it
## under the terms of the GNU General Public License as published by
## the Free Software Foundation, either version 3 of the License, or
## (at your option) any later version.
## 
## This program is distributed in the hope that it will be useful, but
## WITHOUT ANY WARRANTY; without even the implied warranty of
## MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
## GNU General Public License for more details.
## 
## You should have received a copy of the GNU General Public License
## along with this program.  If not, see
## <https://www.gnu.org/licenses/>.

## -*- texinfo -*- 
## @deftypefn {} {@var{retval} =} isStopWord (@var{input1}, @var{input2})
##
## @seealso{}
## @end deftypefn

## Author: inovatec-user <inovatec-user@DESKTOP-760TGTJ>
## Created: 2018-08-20

function retVal = isStopWord (text)
  retVal = 0;
  englishStopWords = {"a", "about", "above", "across", "after", "again", "against", "all", "almost", "alone", "along", "already", "also", "although", "always", "among", "an", "and", "another", "any", "anybody", "anyone", "anything", "anywhere", "are", "area", "areas", "around", "as", "ask", "asked", "asking", "asks", "at", "away", "b", "back", "backed", "backing", "backs", "be", "became", "because", "become", "becomes", "been", "before", "began", "behind", "being", "beings", "best", "better", "between", "big", "both", "but", "by", "c", "came", "can", "cannot", "case", "cases", "certain", "certainly", "clear", "clearly", "come", "could", "d", "did", "differ", "different", "differently", "do", "does", "done", "down", "down", "downed", "downing", "downs", "during", "e", "each", "early", "either", "end", "ended", "ending", "ends", "enough", "even", "evenly", "ever", "every", "everybody", "everyone", "everything", "everywhere", "f", "face", "faces", "fact", "facts", "far", "felt", "few", "find", "finds", "first", "for", "four", "from", "full", "fully", "further", "furthered", "furthering", "furthers", "g", "gave", "general", "generally", "get", "gets", "give", "given", "gives", "go", "going", "good", "goods", "got", "great", "greater", "greatest", "group", "grouped", "grouping", "groups", "h", "had", "has", "have", "having", "he", "her", "here", "herself", "high", "high", "high", "higher", "highest", "him", "himself", "his", "how", "however", "i", "ie", "if", "important", "in", "interest", "interested", "interesting", "interests", "into", "is", "it", "its", "itself", "j", "just", "k", "keep", "keeps", "kind", "knew", "know", "known", "knows", "l", "large", "largely", "last", "later", "latest", "least", "less", "let", "lets", "like", "likely", "long", "longer", "longest", "m", "made", "make", "making", "man", "many", "may", "me", "member", "members", "men", "might", "more", "most", "mostly", "mr", "mrs", "much", "must", "my", "myself", "n", "necessary", "need", "needed", "needing", "needs", "never", "new", "new", "newer", "newest", "next", "no", "nobody", "non", "noone", "not", "nothing", "now", "nowhere", "o", "of", "off", "often", "old", "older", "oldest", "on", "once", "one", "only", "open", "opened", "opening", "opens", "or", "order", "ordered", "ordering", "orders", "other", "others", "our", "out", "over", "p", "part", "parted", "parting", "parts", "per", "perhaps", "place", "places", "point", "pointed", "pointing", "points", "possible", "present", "presented", "presenting", "presents", "problem", "problems", "put", "puts", "q", "quite", "r", "rather", "really", "right", "right", "room", "rooms", "s", "said", "same", "saw", "say", "says", "second", "seconds", "see", "seem", "seemed", "seeming", "seems", "sees", "several", "shall", "she", "should", "show", "showed", "showing", "shows", "side", "sides", "since", "small", "smaller", "smallest", "so", "some", "somebody", "someone", "something", "somewhere", "state", "states", "still", "still", "such", "sure", "t", "take", "taken", "than", "that", "the", "their", "them", "then", "there", "therefore", "these", "they", "thing", "things", "think", "thinks", "this", "those", "though", "thought", "thoughts", "three", "through", "thus", "to", "today", "together", "too", "took", "toward", "turn", "turned", "turning", "turns", "two", "u", "under", "until", "up", "upon", "us", "use", "used", "uses", "v", "very", "w", "want", "wanted", "wanting", "wants", "was", "way", "ways", "we", "well", "wells", "went", "were", "what", "when", "where", "whether", "which", "while", "who", "whole", "whose", "why", "will", "with", "within", "without", "work", "worked", "working", "works", "would", "x", "y", "year", "years", "yet", "you", "young", "younger", "youngest", "your", "yours", "z", "ai", "aie", "aient", "aies", "ait", "alors", "au", "aucun", "aura", "aurai", "auraient", "aurais", "aurait", "auras", "aurez", "auriez", "aurions", "aurons", "auront", "aussi", "autre", "aux", "avaient", "avais", "avait", "avant", "avec", "avez", "aviez", "avions", "avoir", "avons", "ayant", "ayez", "ayons", "bon", "car", "ce", "ceci", "cela", "ces", "cet", "cette", "ceux", "chaque", "ci", "comme", "comment", "dans", "de", "dedans", "dehors", "depuis", "des", "deux", "devoir", "devrait", "devrez", "devriez", "devrions", "devrons", "devront", "dois", "doit", "donc", "dos", "droite", "du", "d\u00c3\u00a8s", "d\u00c3\u00a9but", "d\u00c3\u00b9", "elle", "elles", "en", "encore", "es", "est", "et", "eu", "eue", "eues", "eurent", "eus", "eusse", "eussent", "eusses", "eussiez", "eussions", "eut", "eux", "e\u00c3\u00bbmes", "e\u00c3\u00bbt", "e\u00c3\u00bbtes", "faire", "fais", "faisez", "fait", "faites", "fois", "font", "force", "furent", "fus", "fusse", "fussent", "fusses", "fussiez", "fussions", "fut", "f\u00c3\u00bbmes", "f\u00c3\u00bbt", "f\u00c3\u00bbtes", "haut", "hors", "ici", "il", "ils", "je", "juste", "la", "le", "les", "leur", "leurs", "lui", "l\u00c3\u00a0", "ma", "maintenant", "mais", "mes", "moi", "moins", "mon", "mot", "m\u00c3\u00aame", "ne", "ni", "nom", "nomm\u00c3\u00a9", "nomm\u00c3\u00a9e", "nomm\u00c3\u00a9s", "nos", "notre", "nous", "nouveau", "nouveaux", "ont", "ou", "o\u00c3\u00b9", "par", "parce", "parole", "pas", "personne", "personnes", "peu", "peut", "plupart", "pour", "pourquoi", "qu", "quand", "que", "quel", "quelle", "quelles", "quels", "qui", "sa", "sans", "se", "sera", "serai", "seraient", "serais", "serait", "seras", "serez", "seriez", "serions", "serons", "seront", "ses", "seulement", "si", "sien", "soi", "soient", "sois", "soit", "sommes", "son", "sont", "sous", "soyez", "soyons", "suis", "sujet", "sur", "ta", "tandis", "te", "tellement", "tels", "tes", "toi", "ton", "tous", "tout", "trop", "tr\u00c3\u00a8s", "tu", "un", "une", "valeur", "voient", "vois", "voit", "vont", "vos", "votre", "vous", "vu", "\u00c3\u00a0", "\u00c3\u00a7a", "\u00c3\u00a9taient", "\u00c3\u00a9tais", "\u00c3\u00a9tait", "\u00c3\u00a9tant", "\u00c3\u00a9tat", "\u00c3\u00a9tiez", "\u00c3\u00a9tions", "\u00c3\u00a9t\u00c3\u00a9", "\u00c3\u00a9t\u00c3\u00a9s", "\u00c3\u00aates", "\u00c3\u00aatre","aa", "bb", "cc", "dd", "ee", "ff", "gg", "hh", "ii", "jj", "kk", "ll", "mm", "nn", "oo", "pp", "qq", "rr", "ss", "tt", "uu", "vv", "ww", "xx", "yy", "zz"};
  for i=1:length(englishStopWords),
    if(strcmp(englishStopWords{1,i}, text))
      retVal = 1;
      break;
    end
  end
  
 
    
    
endfunction
