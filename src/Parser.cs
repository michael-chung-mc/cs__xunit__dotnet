using LibTuple;
using LibForm;
using System.IO;
using System.Text.RegularExpressions;
namespace LibParser;

public class Parser { }

public class ParserWaveFrontObj : Parser {
    public List<Point> _fieldVertices;
    public CompositeGroup _fieldGroup;
    public ParserWaveFrontObj () {
        _fieldVertices = new List<Point>{new Point(0,0,0)};
        _fieldGroup = new CompositeGroup();
    }
    public int ParseWaveFrontObj(String argData) {
        Regex varRgxVertex = new Regex(@"^v\s(?<PointOne>-?\d*\.?\d*)\s(?<PointTwo>-?\d*\.?\d*)\s(?<PointThree>-?\d*\.?\d*)");
        Regex varRgxFace = new Regex(@"^f\s(?<PointOne>-?\d*\.?\d*)\s(?<PointTwo>-?\d*\.?\d*)\s(?<PointThree>-?\d*\.?\d*)");
        int varSkipped = 0;
        using (StringReader varRdr = new StringReader(argData)) {
            String varLine;
            while ((varLine = varRdr.ReadLine()) != null) {
                if (varRgxVertex.IsMatch(varLine)) {
                    GroupCollection varGroups = varRgxVertex.Matches(varLine)[0].Groups;
                    _fieldVertices.Add(new Point(double.Parse(varGroups[1].Value), double.Parse(varGroups[2].Value), double.Parse(varGroups[3].Value)));
                } else if (varRgxFace.IsMatch(varLine)) {
                    GroupCollection varGroups = varRgxFace.Matches(varLine)[0].Groups;
                    CompositeGroup varFace = new CompositeGroup();
                    varFace.SetObject(new UnitTriangle(_fieldVertices[int.Parse(varGroups[1].Value)], _fieldVertices[int.Parse(varGroups[2].Value)], _fieldVertices[int.Parse(varGroups[3].Value)]));
                    _fieldGroup.SetObject(varFace);
                } else {
                    varSkipped += 1;
                }
            }
        }
        return varSkipped;
    }
}