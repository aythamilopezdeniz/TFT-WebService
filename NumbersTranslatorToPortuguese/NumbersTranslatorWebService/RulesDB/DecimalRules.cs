using System.Collections.Generic;

namespace NumbersTranslatorWebService.RulesDB
{
    public class DecimalRules
    {
        public SortedList<int, string> SortedListDecimalPosition;

        public DecimalRules()
        {
            SortedListDecimalPosition = new SortedList<int, string>();
        }


        public void Initialize()
        {
            DecimalPositions();
        }

        private void DecimalPositions()
        {
            SortedListDecimalPosition.Add(1, "décimo");
            SortedListDecimalPosition.Add(2, "centésimo");
            SortedListDecimalPosition.Add(3, "milésimo");
            SortedListDecimalPosition.Add(4, "décimo de milésimo");
            SortedListDecimalPosition.Add(5, "centésimo de milésimo");
            SortedListDecimalPosition.Add(6, "milionésimo");
            SortedListDecimalPosition.Add(7, "décimo de milionésimo");
            SortedListDecimalPosition.Add(8, "centésimo de milionésimo");
            SortedListDecimalPosition.Add(9, "milésimo de milionésimo");
            SortedListDecimalPosition.Add(10, "décimo de milésimo de milionésimo");
            SortedListDecimalPosition.Add(11, "centésimo de milésimo de milionésimo");
            SortedListDecimalPosition.Add(12, "bilionésimo");
            SortedListDecimalPosition.Add(13, "décimo de bilionésimo");
            SortedListDecimalPosition.Add(14, "centésimo de bilionésimo");
            SortedListDecimalPosition.Add(15, "milésimo de bilionésimo");
            SortedListDecimalPosition.Add(16, "décimo de milésimo de bilionésimo");
            SortedListDecimalPosition.Add(17, "centésimo de milésimo de bilionésimo");
            SortedListDecimalPosition.Add(18, "trilionésimo");
            SortedListDecimalPosition.Add(19, "décimo de trilionésimo");
            SortedListDecimalPosition.Add(20, "centésimo de trilionésimo");
            SortedListDecimalPosition.Add(21, "milésimo de trilionésimo");
            SortedListDecimalPosition.Add(22, "décimo de milésimo de trilionésimo");
            SortedListDecimalPosition.Add(23, "centésimo de milésimo de trilionésimo");
            SortedListDecimalPosition.Add(24, "quatrilionésimo");
            SortedListDecimalPosition.Add(25, "décimo de quatrilionésimo");
            SortedListDecimalPosition.Add(26, "centésimo de quatrilionésimo");
            SortedListDecimalPosition.Add(27, "milésimo de quatrilionésimo");
            SortedListDecimalPosition.Add(28, "décimo de milésimo de quatrilionésimo");
            SortedListDecimalPosition.Add(29, "centésimo de milésimo de quatrilionésimo");
            SortedListDecimalPosition.Add(30, "quintilionésimo");
            SortedListDecimalPosition.Add(31, "décimo de quintilionésimo");
            SortedListDecimalPosition.Add(32, "centésimo de quintilionésimo");
            SortedListDecimalPosition.Add(33, "milésimo de quintilionésimo");
            SortedListDecimalPosition.Add(34, "décimo de milésimo de quintilionésimo");
            SortedListDecimalPosition.Add(35, "centésimo de milésimo de quintilionésimo");
            SortedListDecimalPosition.Add(36, "sextilionésimo");
            SortedListDecimalPosition.Add(37, "décimo de sextilionésimo");
            SortedListDecimalPosition.Add(38, "centésimo de sextilionésimo");
            SortedListDecimalPosition.Add(39, "milésimo de sextilionésimo");
            SortedListDecimalPosition.Add(40, "décimo de milésimo de sextilionésimo");
            SortedListDecimalPosition.Add(41, "centésimo de milésimo de sextilionésimo");
            SortedListDecimalPosition.Add(42, "septilionésimo");
            SortedListDecimalPosition.Add(43, "décimo de septilionésimo");
            SortedListDecimalPosition.Add(44, "centésimo de septilionésimo");
            SortedListDecimalPosition.Add(45, "milésimo de septilionésimo");
            SortedListDecimalPosition.Add(46, "décimo de milésimo de septilionésimo");
            SortedListDecimalPosition.Add(47, "centésimo de milésimo de septilionésimo");
            SortedListDecimalPosition.Add(48, "optilionésimo");
            SortedListDecimalPosition.Add(49, "décimo de optilionésimo");
            SortedListDecimalPosition.Add(50, "centésimo de optilionésimo");
            SortedListDecimalPosition.Add(51, "milésimo de optilionésimo");
            SortedListDecimalPosition.Add(52, "décimo de milésimo de optilionésimo");
            SortedListDecimalPosition.Add(53, "centésimo de milésimo de optilionésimo");
            SortedListDecimalPosition.Add(54, "nonilionésimo");
            SortedListDecimalPosition.Add(55, "décimo de nonilionésimo");
            SortedListDecimalPosition.Add(56, "centésimo de nonilionésimo");
            SortedListDecimalPosition.Add(57, "milésimo de nonilionésimo");
            SortedListDecimalPosition.Add(58, "décimo de milésimo de nonilionésimo");
            SortedListDecimalPosition.Add(59, "centésimo de milésimo de nonilionésimo");
            SortedListDecimalPosition.Add(60, "decilionésimo");
            SortedListDecimalPosition.Add(61, "décimo de decilionésimo");
            SortedListDecimalPosition.Add(62, "centésimo de decilionésimo");
            SortedListDecimalPosition.Add(63, "milésimo de decilionésimo");
            SortedListDecimalPosition.Add(64, "décimo de milésimo de decilionésimo");
            SortedListDecimalPosition.Add(65, "centésimo de milésimo de decilionésimo");
            SortedListDecimalPosition.Add(66, "undecilionésimo");
            SortedListDecimalPosition.Add(67, "décimo de undecilionésimo");
            SortedListDecimalPosition.Add(68, "centésimo de undecilionésimo");
            SortedListDecimalPosition.Add(69, "milésimo de undecilionésimo");
            SortedListDecimalPosition.Add(70, "décimo de milésimo de undecilionésimo");
            SortedListDecimalPosition.Add(71, "centésimo de milésimo de undecilionésimo");
            SortedListDecimalPosition.Add(72, "duodecilionésimo");
            SortedListDecimalPosition.Add(73, "décimo de duodecilionésimo");
            SortedListDecimalPosition.Add(74, "centésimo de duodecilionésimo");
            SortedListDecimalPosition.Add(75, "milésimo de duodecilionésimo");
            SortedListDecimalPosition.Add(76, "décimo de milésimo de duodecilionésimo");
            SortedListDecimalPosition.Add(77, "centésimo de milésimo de duodecilionésimo");
            SortedListDecimalPosition.Add(78, "tredecilionésimo");
            SortedListDecimalPosition.Add(79, "décimo de tredecilionésimo");
            SortedListDecimalPosition.Add(80, "centésimo de tredecilionésimo");
            SortedListDecimalPosition.Add(81, "milésimo de tredecilionésimo");
            SortedListDecimalPosition.Add(82, "décimo de milésimo de tredecilionésimo");
            SortedListDecimalPosition.Add(83, "centésimo de milésimo de tredecilionésimo");
            SortedListDecimalPosition.Add(84, "quatordecilionésimo");
            SortedListDecimalPosition.Add(85, "décimo de quatordecilionésimo");
            SortedListDecimalPosition.Add(86, "centésimo de quatordecilionésimo");
            SortedListDecimalPosition.Add(87, "milésimo de quatordecilionésimo");
            SortedListDecimalPosition.Add(88, "décimo de milésimo de quatordecilionésimo");
            SortedListDecimalPosition.Add(89, "centésimo de milésimo de quatordecilionésimo");
            SortedListDecimalPosition.Add(90, "quindecilionésimo");
            SortedListDecimalPosition.Add(91, "décimo de quindecilionésimo");
            SortedListDecimalPosition.Add(92, "centésimo de quindecilionésimo");
            SortedListDecimalPosition.Add(93, "milésimo de quindecilionésimo");
            SortedListDecimalPosition.Add(94, "décimo de milésimo de quindecilionésimo");
            SortedListDecimalPosition.Add(95, "centésimo de milésimo de quindecilionésimo");
            SortedListDecimalPosition.Add(96, "sedecilionésimo");
            SortedListDecimalPosition.Add(97, "décimo de sedecilionésimo");
            SortedListDecimalPosition.Add(98, "centésimo de sedecilionésimo");
            SortedListDecimalPosition.Add(99, "milésimo de sedecilionésimo");
            SortedListDecimalPosition.Add(100, "décimo de milésimo de sedecilionésimo");
            SortedListDecimalPosition.Add(101, "centésimo de milésimo de sedecilionésimo");
            SortedListDecimalPosition.Add(102, "septendecilionésimo");
            SortedListDecimalPosition.Add(103, "décimo de septendecilionésimo");
            SortedListDecimalPosition.Add(104, "centésimo de septendecilionésimo");
            SortedListDecimalPosition.Add(105, "milésimo de septendecilionésimo");
            SortedListDecimalPosition.Add(106, "décimo de milésimo de septendecilionésimo");
            SortedListDecimalPosition.Add(107, "centésimo de milésimo de septendecilionésimo");
            SortedListDecimalPosition.Add(108, "optodecilionésimo");
            SortedListDecimalPosition.Add(109, "décimo de optodecilionésimo");
            SortedListDecimalPosition.Add(110, "centésimo de optodecilionésimo");
            SortedListDecimalPosition.Add(111, "milésimo de optodecilionésimo");
            SortedListDecimalPosition.Add(112, "décimo de milésimo de optodecilionésimo");
            SortedListDecimalPosition.Add(113, "centésimo de milésimo de optodecilionésimo");
            SortedListDecimalPosition.Add(114, "nonidecilionésimo");
            SortedListDecimalPosition.Add(115, "décimo de nonidecilionésimo");
            SortedListDecimalPosition.Add(116, "centésimo de nonidecilionésimo");
            SortedListDecimalPosition.Add(117, "milésimo de nonidecilionésimo");
            SortedListDecimalPosition.Add(118, "décimo de milésimo de nonidecilionésimo");
            SortedListDecimalPosition.Add(119, "centésimo de milésimo de nonidecilionésimo");
            SortedListDecimalPosition.Add(120, "vintidecilionésimo");
            SortedListDecimalPosition.Add(121, "décimo de vintidecilionésimo");
            SortedListDecimalPosition.Add(122, "centésimo de vintidecilionésimo");
            SortedListDecimalPosition.Add(123, "milésimo de vintidecilionésimo");
            SortedListDecimalPosition.Add(124, "décimo de milésimo de vintidecilionésimo");
            SortedListDecimalPosition.Add(125, "centésimo de milésimo de vintidecilionésimo");
        }

        public SortedList<int, string> GetSortedListDecimalPosition()
        {
            return SortedListDecimalPosition;
        }
    }
}