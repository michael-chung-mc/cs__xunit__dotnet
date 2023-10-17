using LibTuple;
using System.IO;
using System.Text.RegularExpressions;
namespace LibParser;

public class Parser {
}

public class ParserWaveFrontObj : Parser {
    public List<Point> _fieldVertices;
    public ParserWaveFrontObj () {
        _fieldVertices = new List<Point>{new Point(0,0,0)};
    }
    public int ParseWaveFrontObj(String argData) {
        Regex varFormat = new Regex(@"^\w\s(?<PointOne>-?\d*\.?\d*)\s(?<PointTwo>-?\d*\.?\d*)\s(?<PointThree>-?\d*\.?\d*)");
        int varSkipped = 0;
        using (StringReader varRdr = new StringReader(argData)) {
            String varLine;
            while ((varLine = varRdr.ReadLine()) != null) {
                if (varFormat.IsMatch(varLine)) {
                    GroupCollection varGroups = varFormat.Matches(varLine)[0].Groups;
                    _fieldVertices.Add(new Point(double.Parse(varGroups[1].Value), double.Parse(varGroups[2].Value), double.Parse(varGroups[3].Value)));
                } else {
                    varSkipped += 1;
                }
            }
        }
        return varSkipped;
    }
}