using LibTuple;
using LibForm;
using System.IO;
using System.Text.RegularExpressions;
namespace LibParser;

public class Parser { }

public class ParserWaveFrontObj : Parser {
    public List<Point> _fieldVertices;
    public CompositeGroup _fieldGroup;
    private Regex _fieldRgxVertex;
    private Regex _fieldRgxFace;
    private Regex _fieldRgxFaceVertex;
    public ParserWaveFrontObj () {
        _fieldVertices = new List<Point>{new Point(0,0,0)};
        _fieldGroup = new CompositeGroup();
        _fieldRgxVertex = new Regex(@"^v\s(?<PointOne>-?\d*\.?\d*)\s(?<PointTwo>-?\d*\.?\d*)\s(?<PointThree>-?\d*\.?\d*)");
        _fieldRgxFace = new Regex(@"^f(?<Vertex>\s\d)+");
        _fieldRgxFaceVertex = new Regex(@"\d");
    }
    public int ParseWaveFrontObj(String argData) {
        int varSkipped = 0;
        using (StringReader varRdr = new StringReader(argData)) {
            String varLine;
            while ((varLine = varRdr.ReadLine()) != null) {
                if (_fieldRgxVertex.IsMatch(varLine)) {
                    GroupCollection varGroups = _fieldRgxVertex.Matches(varLine)[0].Groups;
                    _fieldVertices.Add(new Point(double.Parse(varGroups[1].Value), double.Parse(varGroups[2].Value), double.Parse(varGroups[3].Value)));
                } else if (_fieldRgxFace.IsMatch(varLine)) {
                    MatchCollection varGroups = _fieldRgxFaceVertex.Matches(varLine);
                    for (int i = 1; i < varGroups.Count-1; ++i) {
                        UnitTriangle varFace = new UnitTriangle(_fieldVertices[int.Parse(varGroups[0].Value)], _fieldVertices[int.Parse(varGroups[i].Value)], _fieldVertices[int.Parse(varGroups[i+1].Value)]);
                        _fieldGroup.SetObject(varFace);
                    }
                } else {
                    varSkipped += 1;
                }
            }
        }
        return varSkipped;
    }
}