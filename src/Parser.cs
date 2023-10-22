using LibTuple;
using LibForm;
using System.IO;
using System.Text.RegularExpressions;
namespace LibParser;

public class Parser { }

public class ParserWaveFrontObj : Parser {
    public List<Point> _fieldVertices;
    public Dictionary<String, CompositeGroup> _fieldGroups;
    private Regex _fieldRgxVertex;
    private Regex _fieldRgxFace;
    private Regex _fieldRgxFaceVertex;
    private Regex _fieldRgxGroup;
    public ParserWaveFrontObj () {
        _fieldVertices = new List<Point>{new Point(0,0,0)};
        _fieldGroups = new Dictionary<String, CompositeGroup>();
        _fieldRgxVertex = new Regex(@"^v\s+(?<PointOne>-?\d*\.?\d*)\s+(?<PointTwo>-?\d*\.?\d*)\s+(?<PointThree>-?\d*\.?\d*)");
        _fieldRgxFace = new Regex(@"^f(?<Vertex>\s+\d)+");
        _fieldRgxFaceVertex = new Regex(@"\d+");
        _fieldRgxGroup = new Regex(@"^g\s.*");
    }
    public int ParseWaveFrontObj(String argData) {
        int varSkipped = 0;
        String varGroupName = "default";
        using (StringReader varRdr = new StringReader(argData)) {
            String varLine;
            while ((varLine = varRdr.ReadLine()) != null) {
                if (_fieldRgxVertex.IsMatch(varLine)) {
                    GroupCollection varGroups = _fieldRgxVertex.Matches(varLine)[0].Groups;
                    _fieldVertices.Add(new Point(double.Parse(varGroups[1].Value), double.Parse(varGroups[2].Value), double.Parse(varGroups[3].Value)));
                } else if (_fieldRgxFace.IsMatch(varLine)) {
                    if (_fieldGroups.Count==0) {
                        SetTriangleMeshNormalization();
                    }
                    Console.WriteLine($"ParseWaveFrontObj()::Face({varLine}");
                    MatchCollection varGroups = _fieldRgxFaceVertex.Matches(varLine);
                    for (int i = 1; i < varGroups.Count-1; ++i) {
                        UnitTriangle varFace = new UnitTriangle(_fieldVertices[int.Parse(varGroups[0].Value)], _fieldVertices[int.Parse(varGroups[i].Value)], _fieldVertices[int.Parse(varGroups[i+1].Value)]);
                        if (!_fieldGroups.ContainsKey(varGroupName)) {
                            _fieldGroups[varGroupName] = new CompositeGroup();
                        }
                        _fieldGroups[varGroupName].SetObject(varFace);
                        varFace._fieldVertexOne.RenderConsole();
                        varFace._fieldVertexTwo.RenderConsole();
                        varFace._fieldVertexThree.RenderConsole();
                    }
                } else if (_fieldRgxGroup.IsMatch(varLine)) {
                    varGroupName = varLine.Split(" ")[1];
                } else {
                    varSkipped += 1;
                }
            }
        }
        return varSkipped;
    }
    public CompositeGroup GetGroup() {
        CompositeGroup varGroup = new CompositeGroup();
        foreach (KeyValuePair<String,CompositeGroup> varKey in _fieldGroups) {
            varGroup.SetObject(varKey.Value);
        }
        return varGroup;
    }
    private void SetTriangleMeshNormalization () {
        AABB varBounds = new AABB();
        for (int i = 0; i < _fieldVertices.Count; i ++) {
            varBounds.SetPoint(_fieldVertices[i]);
        }
        double varSX = varBounds._fieldMax._fieldX - varBounds._fieldMin._fieldX;
        double varSY = varBounds._fieldMax._fieldY - varBounds._fieldMin._fieldY;
        double varSZ = varBounds._fieldMax._fieldZ - varBounds._fieldMin._fieldZ;
        double varScale = Math.Max(Math.Max(varSX, varSY), varSZ);
        for (int i = 0; i < _fieldVertices.Count; i ++) {
            _fieldVertices[i]._fieldX = (_fieldVertices[i]._fieldX - (varBounds._fieldMin._fieldX + varSX/2))/varScale;
            _fieldVertices[i]._fieldY = (_fieldVertices[i]._fieldY - (varBounds._fieldMin._fieldY + varSY/2))/varScale;
            _fieldVertices[i]._fieldZ = (_fieldVertices[i]._fieldZ - (varBounds._fieldMin._fieldZ + varSZ/2))/varScale;
        }
    }
}