using LibTuple;
using LibForm;
using System.IO;
using System.Text.RegularExpressions;
namespace LibParser;

public class Parser { }

public class ParserWaveFrontObj : Parser {
    public List<Point> _fieldVertices;
    public List<Vector> _fieldNormals;
    public Dictionary<String, CompositeGroup> _fieldGroups;
    private Regex _fieldRgxVertex;
    private Regex _fieldRgxVertexNormal;
    private Regex _fieldRgxFace;
    private Regex _fieldRgxIndexNormal;
    private Regex _fieldRgxIndexTextureNormal;
    private Regex _fieldRgxIndex;
    private Regex _fieldRgxGroup;
    public ParserWaveFrontObj () {
        _fieldVertices = new List<Point>{new Point(0,0,0)};
        _fieldNormals = new List<Vector>{new Vector(0,0,0)};
        _fieldGroups = new Dictionary<String, CompositeGroup>();
        _fieldRgxVertex = new Regex(@"^v\s+(?<PointOne>-?\d*\.?\d*)\s+(?<PointTwo>-?\d*\.?\d*)\s+(?<PointThree>-?\d*\.?\d*)");
        _fieldRgxVertexNormal = new Regex(@"^vn\s+(?<PointOne>-?\d*\.?\d*)\s+(?<PointTwo>-?\d*\.?\d*)\s+(?<PointThree>-?\d*\.?\d*)");
        _fieldRgxFace = new Regex(@"^f(?<Vertex>\s+\d+\/?\d*\/?\d*)+");
        _fieldRgxIndex = new Regex(@"\d+");
        _fieldRgxIndexNormal = new Regex(@"\d+\/\/\d+");
        _fieldRgxIndexTextureNormal = new Regex(@"\d+\/\d+\/\d+");
        _fieldRgxGroup = new Regex(@"^g\s.*");
    }
    public int ParseWaveFrontObj(String argData, bool argNormalize = true) {
        int varSkipped = 0;
        String varGroupName = "default";
        using (StringReader varRdr = new StringReader(argData)) {
            String varLine;
            while ((varLine = varRdr.ReadLine()) != null) {
                if (_fieldRgxVertexNormal.IsMatch(varLine)) {
                    GroupCollection varGroups = _fieldRgxVertexNormal.Matches(varLine)[0].Groups;
                    _fieldNormals.Add(new Vector(double.Parse(varGroups[1].Value), double.Parse(varGroups[2].Value), double.Parse(varGroups[3].Value)));
                } else if (_fieldRgxVertex.IsMatch(varLine)) {
                    GroupCollection varGroups = _fieldRgxVertex.Matches(varLine)[0].Groups;
                    _fieldVertices.Add(new Point(double.Parse(varGroups[1].Value), double.Parse(varGroups[2].Value), double.Parse(varGroups[3].Value)));
                } else if (_fieldRgxFace.IsMatch(varLine)) {
                    if (_fieldGroups.Count==0 && argNormalize) {
                        SetTriangleMeshNormalization();
                    }
                    if (_fieldRgxIndexNormal.IsMatch(varLine)) {
                        MatchCollection varGroups = _fieldRgxIndexNormal.Matches(varLine);
                        for (int i = 1; i < varGroups.Count-1; ++i) {
                            MatchCollection varVertexNormalOne = _fieldRgxIndex.Matches(varGroups[0].Value);
                            MatchCollection varVertexNormalTwo = _fieldRgxIndex.Matches(varGroups[i].Value);
                            MatchCollection varVertexNormalThree = _fieldRgxIndex.Matches(varGroups[i+1].Value);
                            SmoothTriangle varFace = new SmoothTriangle(_fieldVertices[int.Parse(varVertexNormalOne[0].Value)], _fieldVertices[int.Parse(varVertexNormalTwo[0].Value)], _fieldVertices[int.Parse(varVertexNormalThree[0].Value)], _fieldNormals[int.Parse(varVertexNormalOne[1].Value)], _fieldNormals[int.Parse(varVertexNormalTwo[1].Value)], _fieldNormals[int.Parse(varVertexNormalThree[1].Value)]);
                            if (!_fieldGroups.ContainsKey(varGroupName)) {
                                _fieldGroups[varGroupName] = new CompositeGroup();
                            }
                            _fieldGroups[varGroupName].SetObject(varFace);
                        }
                    } else if (_fieldRgxIndexTextureNormal.IsMatch(varLine)) {
                        MatchCollection varGroups = _fieldRgxIndexTextureNormal.Matches(varLine);
                        for (int i = 1; i < varGroups.Count-1; ++i) {
                            MatchCollection varVertexNormalOne = _fieldRgxIndex.Matches(varGroups[0].Value);
                            MatchCollection varVertexNormalTwo = _fieldRgxIndex.Matches(varGroups[i].Value);
                            MatchCollection varVertexNormalThree = _fieldRgxIndex.Matches(varGroups[i+1].Value);
                            SmoothTriangle varFace = new SmoothTriangle(_fieldVertices[int.Parse(varVertexNormalOne[0].Value)], _fieldVertices[int.Parse(varVertexNormalTwo[0].Value)], _fieldVertices[int.Parse(varVertexNormalThree[0].Value)], _fieldNormals[int.Parse(varVertexNormalOne[2].Value)], _fieldNormals[int.Parse(varVertexNormalTwo[2].Value)], _fieldNormals[int.Parse(varVertexNormalThree[2].Value)]);
                            if (!_fieldGroups.ContainsKey(varGroupName)) {
                                _fieldGroups[varGroupName] = new CompositeGroup();
                            }
                            _fieldGroups[varGroupName].SetObject(varFace);
                        }
                    } else {
                        // Console.WriteLine($"ParseWaveFrontObj()::Face({varLine}");
                        MatchCollection varGroups = _fieldRgxIndex.Matches(varLine);
                        for (int i = 1; i < varGroups.Count-1; ++i) {
                            UnitTriangle varFace = new UnitTriangle(_fieldVertices[int.Parse(varGroups[0].Value)], _fieldVertices[int.Parse(varGroups[i].Value)], _fieldVertices[int.Parse(varGroups[i+1].Value)]);
                            if (!_fieldGroups.ContainsKey(varGroupName)) {
                                _fieldGroups[varGroupName] = new CompositeGroup();
                            }
                            _fieldGroups[varGroupName].SetObject(varFace);
                            // varFace._fieldVertexOne.RenderConsole();
                            // varFace._fieldVertexTwo.RenderConsole();
                            // varFace._fieldVertexThree.RenderConsole();
                        }
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